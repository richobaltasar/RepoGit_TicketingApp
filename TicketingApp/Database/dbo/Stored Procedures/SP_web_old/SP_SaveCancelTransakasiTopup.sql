
CREATE PROCEDURE SP_SaveCancelTransakasiTopup
@IdTrx bigint,
@AccountNumber nvarchar(max),
@SaldoEmoney float,
@AuthorizeBy nvarchar(max), 
@NamaKasir nvarchar(max),
@ComputerName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
BEGIN TRY  
	declare @Succes int
	declare @Message nvarchar(max)

	declare @O_TipeTransaksi nvarchar(MAX)	
	declare @O_PaymentMethod nvarchar(MAX)	
	declare @O_TotalTransaksi float	
	declare @O_AccountNumber nvarchar(MAX)	
	declare @O_NamaKasirYangInputTrx nvarchar(MAX)	
	declare @O_NamaKasirYangCancel nvarchar(MAX)	
	declare @O_Authorize nvarchar(MAX)	
	declare @O_TransactionDate nvarchar(MAX)	
	declare @O_CancelDate nvarchar(50)	
	declare @O_IdTransaksi bigint	
	declare @O_PayTunai float	
	declare @O_PayEmoney float	
	declare @O_DescriptionTransaksi nvarchar(MAX)	
	declare @O_PayEDC float	

	declare @NominalTopup float

	insert into LogCancelTopupDetail
	select*from LogTopupDetail where IdTopup=@IdTrx

	select
	@O_AccountNumber=AccountNumber,
	@O_Authorize=@AuthorizeBy,@O_CancelDate=FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
	@O_DescriptionTransaksi='TOPUP Nominal = Rp '+ REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,NominalTopup),1), '.00',''),',','.'),
	@O_IdTransaksi=IdTopup,@O_NamaKasirYangCancel=@NamaKasir,@O_NamaKasirYangInputTrx=Chasierby,
	@O_PayEDC=ISNULL(TotalDebit,0),@O_PayEmoney=0,@O_PaymentMethod=PaymentMethod,
	@O_PayTunai=PayCash,@O_TipeTransaksi=JenisPayment,@O_TotalTransaksi=TotalBayar,
	@O_TransactionDate=Datetime,@NominalTopup=NominalTopup,
	@O_IdTransaksi = IdTopup
	from LogTopupDetail
	where IdTopup=@IdTrx

	if not exists(select*from DataAccount where AccountNumber=@AccountNumber)
	begin
		set @Succes = 0
		set @Message = 'Sorry, Account Number pada transaksi ini tidak ditemukan'	
	end
	else
	begin
		declare @O_BalanceNow float
		declare @O_UangJaminan float
		declare @O_Ticket float
		

		select @O_BalanceNow=Balanced,@O_UangJaminan=UangJaminan,
		@O_Ticket=Ticket
		from DataAccount where AccountNumber=@O_AccountNumber
		
		set @O_BalanceNow = (@O_BalanceNow - @NominalTopup)

		ulangInsert:
		insert into LogTransaksiCancel
		(TipeTransaksi,	PaymentMethod,	TotalTransaksi,	
		AccountNumber,	NamaKasirYangInputTrx, NamaKasirYangCancel,	
		Authorize,	TransactionDate,	CancelDate,	IdTransaksi,	PayTunai,	
		PayEmoney,	DescriptionTransaksi,	PayEDC)
		values
		(
			@O_TipeTransaksi,@O_PaymentMethod,@O_TotalTransaksi,
			@O_AccountNumber,@O_NamaKasirYangInputTrx,@O_NamaKasirYangCancel,
			@O_Authorize,@O_TransactionDate,@O_CancelDate,@O_IdTransaksi,@O_PayTunai,
			@O_PayEmoney,@O_DescriptionTransaksi,@O_PayEDC
		)

		if exists(select*from LogTransaksiCancel where  id = SCOPE_IDENTITY())
		begin
			delete LogTopupDetail where IdTopup = @IdTrx

			delete from LogDeposit where AccountNumber=@AccountNumber and Datetime=@O_TransactionDate
			and TransactionType='CREDIT'

			update DataAccount 
			set Balanced = @O_BalanceNow
			where AccountNumber = @AccountNumber

			declare @Debit float
			declare @Credit float
			declare @Deposit float
			declare @DepositYesterday float
			declare @TotalUangDiBox float
			declare @TotalUangMasuk float
			declare @TotalUangKeluar float

			select @Debit=sum(isnull(Debit,0)),@Credit=sum(isnull(Credit,0))  from 
			(
				select
				case when TransactionType = 'DEBIT' then Nominal end as Debit,
				case when TransactionType = 'CREDIT' then Nominal end as Credit
				from LogDeposit 
				where 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','') 
			) q

			select @DepositYesterday=Deposit
			from DataDeposit 
			where  
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(dateadd(day,-1, cast(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23) as date)),'-','') 

			update DataDeposit 
			set Credit = @Credit, Debit = @Debit,DepositHariSebelumnya=@DepositYesterday,
			Deposit=((@DepositYesterday-@Debit) + @Credit)
			where 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','') 

			select 
				@TotalUangDiBox=(TotalUangDiBox-@O_PayTunai),
				@TotalUangKeluar=(TotalUangKeluar+@O_PayTunai) 
			from DataChasierBox
			where 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','') 
			and NamaComputer=@ComputerName

			update DataChasierBox
			set TotalUangDiBox=@TotalUangDiBox,TotalUangKeluar=@TotalUangKeluar
			where 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@O_TransactionDate,10),'/','-'), 105), 23),'-','') 
			and NamaComputer=@ComputerName 

			set @Succes = 1
			set @Message = 'Id:'+cast(Scope_Identity() as nvarchar(max))+'~AccountNumber:'+@O_AccountNumber+'~TipeTransaksi:'+@O_TipeTransaksi+'~Ticket:'+CAST(@O_Ticket as nvarchar(max))+'~SaldoEmoney:'+cast(@O_BalanceNow as nvarchar(max))+'~SaldoJaminan:'+cast(@O_UangJaminan as nvarchar(max))+''
		end
		else
		begin
			GOTO ulangInsert 
		end
		
	end
	select @Message  _Message,@Succes _Success		
END TRY 
BEGIN CATCH  
	set @Succes = 0
	set @Message = 'SP error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()
	select @Message  _Message,@Succes _Success				
END CATCH;  
END
