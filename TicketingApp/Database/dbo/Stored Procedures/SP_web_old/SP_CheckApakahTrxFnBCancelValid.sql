-- exec SP_CheckApakahTrxFnBCancelValid @IdTrx=15595,@AccountNumber='32250697-20200305141212'

CREATE PROCEDURE SP_CheckApakahTrxFnBCancelValid
	@IdTrx bigint,
	@AccountNumber nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRY  
	declare @Succes int
	declare @Message nvarchar(max)
	if exists(select*from LogCancelFoodcourtTransaksi where IdTrx = @IdTrx)
	begin
		
		
		declare @Datetime nvarchar(max)
		declare @Kasir nvarchar(max)

		select @Datetime=Datetime,@Kasir=CashierBy from LogCancelFoodcourtTransaksi where IdTrx = @IdTrx
		set @Succes = 0
		set @Message = 'Sorry, Transaksi ini telah dilakukan cancel pada waktu :'+@Datetime+' oleh '+@Kasir
	end
	else
	begin
		if not exists(select*from LogFoodcourtTransaksi where IdTrx = @IdTrx and left(Datetime,10) =FORMAT(GETDATE() , 'dd/MM/yyyy'))
		begin
			
			set @Succes = 0
			set @Message = 'Sorry, Transaksi ini tidak ditemukan melakukan transaksi pada hari ini'
		end
		else
		begin
			
			if not exists(select*from LogFoodcourtTransaksi where IdTrx=@IdTrx and AccountNumber=@AccountNumber)
			begin
				set @Succes = 0
				set @Message = 'Sorry, Account Number pada transaksi dengan Account Number pada kartu tidak sesuai'	
			end
			else
			begin
				
				declare @JenisTrx nvarchar(max)
				set @JenisTrx = (select UPPER(JenisTransaksi) from LogFoodcourtTransaksi where IdTrx=@IdTrx and AccountNumber=@AccountNumber)
				if(@JenisTrx !='EMONEY')
				begin
					set @Succes = 0
					set @Message = 'Sorry, Transaksi ini gagal untuk dibatalkan karena menggunakan metode pembayaran CASH'		
				end
				else
				begin
					print 1
					declare @PayEmoney float
					set @PayEmoney = (select PayEmoney from LogFoodcourtTransaksi where IdTrx = @IdTrx)
					set @Succes = 1
					set @Message = 'Apakah anda yakin untuk menghapus transaksi ini dan mengembalikan Saldo sebesar Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@PayEmoney),1), '.00',''),',','.')	
				end
			end
		end
	end
	select @Message  _Message,@Succes _Success
END TRY 
BEGIN CATCH  
	set @Succes = 0
	set @Message = 'SP_CheckApakahTrxFnBCancelValid, error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()+''
	select @Message  _Message,@Succes _Success				
END CATCH;  

END