-- exec SP_SaveLogTransaksiV2 @TotalTransaksi =6000,@TotalBayar =6000,@PaymentMethod ='CASH',@Emoney =0,@AccountNumber ='37794393-20200914211824',@Tunai =100000,@Kembalian =-94000,@BankName ='',@CardNumber ='',@Noreff ='',@MerchantName ='DESKTOP-8VM0K0G',@ChasierName ='Riko Ade Rinanda'
CREATE PROCEDURE SP_SaveLogTransaksiV2
	@TotalTransaksi float,
    @TotalBayar float,
    @PaymentMethod nvarchar(max),
	@Emoney float,
	@AccountNumber nvarchar(max),
	@Tunai float,
    @Kembalian float,	
	@BankName nvarchar(MAX),
    @CardNumber nvarchar(max),
    @Noreff nvarchar(max),
	@MerchantName nvarchar(max),
	@ChasierName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @title nvarchar(max)
	declare @message nvarchar(max)
	declare @status nvarchar(max)

	BEGIN TRY  

		insert into [dbo].[LogTransaksiPOS]
		(			
			Datetime,
			MerchantName,
			ChasierName,
			TotalTransaksi,
			PaymentMethod,
			TotalBayar,
			Emoney,
			AccountNumber,
			Tunai,
			Kembalian,
			BankName,
			CardNumber,
			Noreff
		)
		values(
			FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
			@MerchantName,
			@ChasierName,
			@TotalTransaksi,
			@PaymentMethod,
			@TotalBayar,
			@Emoney,
			@AccountNumber,
			@Tunai,
			@Kembalian,
			@BankName,
			@CardNumber,
			@Noreff
		)
		
		declare @idTrx bigint
		declare @DatetimeO nvarchar(max)

		select @message = convert(nvarchar(max), idTrx), @idTrx=idTrx, @DatetimeO = Datetime
		from LogTransaksiPOS where idTrx = SCOPE_IDENTITY()
		
		set @title='SP_SaveLogTransaksiV2 berhasil'
		set @status='SUCCESS'
		
		select @status status, @message message, @title title
	END TRY 
	BEGIN CATCH  
		select 'ERROR' status, 'Msg :'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'Error Exception' title
	END CATCH;  
END
