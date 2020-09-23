CREATE PROCEDURE [dbo].[SP_SaveTransaksiRegistrasi]
	@AccountNumber nvarchar(max),
	@SaldoEmoney float,
	@SaldoEmoneyAfter float,
	@TicketWeekDay float,
	@TicketWeekDayAfter float,
	@TicketWeekEnd float,
	@TicketWeekEndAfter float,
	@SaldoJaminan float,
	@SaldoJaminanAfter float,
	@IdTicketTrx bigint,
	@Cashback float,
	@Topup float,
	@Asuransi float,
	@QtyTotalTiket float,
	@TotalBeliTiket float,
	@TotalAll float,
	@JenisTransaksi nvarchar(max),
	@TotalBayar float,
	@PayEmoney float,
	@PayCash float,
	@TerimaUang float,
	@Kembalian float,
	@ComputerName nvarchar(max),
	@Chasier nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
BEGIN TRY
	declare @Succes nvarchar(max)
	declare @Message nvarchar(max)

	declare @DateTrx nvarchar(max)
	declare @IdTicket nvarchar(max)

	insert into [dbo].[LogRegistrasiDetail]
	(Datetime,AccountNumber,SaldoEmoney,SaldoEmoneyAfter,TicketWeekDay,TicketWeekDayAfter,TicketWeekEnd,
	TicketWeekEndAfter,SaldoJaminan,SaldoJaminanAfter,Cashback,Topup,Asuransi,QtyTotalTiket,
	TotalBeliTiket,TotalAll,JenisTransaksi,TotalBayar,PayEmoney,PayCash,TerimaUang,Kembalian,
	CashierBy,ComputerName,status)
	values(
		FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
		@AccountNumber,@SaldoEmoney,@SaldoEmoneyAfter,@TicketWeekDay,@TicketWeekDayAfter,@TicketWeekEnd,
		@TicketWeekEndAfter,@SaldoJaminan,@SaldoJaminanAfter,@Cashback,@Topup,@Asuransi,@QtyTotalTiket,
		@TotalBeliTiket,@TotalAll,@JenisTransaksi,@TotalBayar,@PayEmoney,@PayCash,@TerimaUang,@Kembalian,
		@Chasier,@ComputerName,1
	)

	select @DateTrx=Datetime,@IdTicket = idTrx from LogRegistrasiDetail where  idTrx = SCOPE_IDENTITY()

	update LogRegistrasiDetail 
	set IdTicketTrx = @IdTicket
	where  idTrx = SCOPE_IDENTITY()

	-- Save log FundTransaction
	if(@PayEmoney = 0)
	begin
		declare @TotalSaldo float
		
		if exists(select*from DataAccount where AccountNumber=@AccountNumber and Status = 1)
		begin
			if(@Topup > 0)
			begin
				Insert into [dbo].[LogDeposit]
				([Datetime],[AccountNumber],[TransactionType],[Nominal],[Status])
				values
				(
					FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@AccountNumber,'CREDIT',@Topup,1
				)
			end
		end
		else
		begin
			set @TotalSaldo = (@SaldoEmoneyAfter+@SaldoJaminanAfter)
			Insert into [dbo].[LogDeposit]
			([Datetime],[AccountNumber],[TransactionType],[Nominal],[Status])
			values
			(
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@AccountNumber,'CREDIT',@TotalSaldo,1
			)
		end
	end
	else
	begin
		Insert into [dbo].[LogDeposit]
		([Datetime],[AccountNumber],[TransactionType],[Nominal],[Status])
		values
		(
			FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@AccountNumber,'DEBIT',@PayEmoney,1
		)
	end

	-- Update ChasierBox
	EXECUTE SP_UpdateChasierBox @ComputerName,@TerimaUang,@Kembalian

	-- Update Deposit
	EXECUTE SP_UpdateDepositData @DateTrx  ,@Succes OUTPUT,@Message OUTPUT      

	set @Succes = 'TRUE'
	set @Message = 'Insert LogRegistrasiDetail Success ~'+FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')+'~'+ cast(@IdTicket as nvarchar(max))
	
	select @Message  _Message,@Succes _Success				
END TRY
BEGIN CATCH  
	set @Succes = 'FALSE'
	set @Message = 'SP error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()
	select @Message  _Message,@Succes _Success				
END CATCH;  
END