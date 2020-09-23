CREATE PROCEDURE SP_CheckApakahIniTopupYangTerakhir
	@IdTrx bigint,
	@AccountNumber nvarchar(max) 
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRY  
		declare @Succes int
		declare @Message nvarchar(max)
		
		if exists(select*from LogCancelTopupDetail where IdTopup = @IdTrx)
		begin
			declare @Datetime nvarchar(max)
			declare @Kasir nvarchar(max)

			select @Datetime=Datetime,@Kasir=Chasierby from LogCancelTopupDetail where IdTopup = @IdTrx
			set @Succes = 0
			set @Message = 'Sorry, Transaksi ini telah dilakukan cancel pada waktu :'+@Datetime+' oleh '+@Kasir
		end
		else
		begin
			if not exists(select*from LogTopupDetail where IdTopup=@IdTrx and left(Datetime,10) =FORMAT(GETDATE() , 'dd/MM/yyyy'))
			begin
				set @Succes = 0
				set @Message = 'Sorry, Transaksi ini tidak ditemukan'
			end
			else
			begin
				if not exists(select*from LogTopupDetail where IdTopup=@IdTrx and AccountNumber=@AccountNumber)
				begin
					set @Succes = 0
					set @Message = 'Sorry, Account Number pada transaksi dengan Account Number pada kartu tidak sesuai'	
				end
				else
				begin
					declare @datetimeV nvarchar(max) 
					select @datetimeV = 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
					+ REPLACE(RIGHT(Datetime,8),':','')
					from LogTopupDetail 
					where IdTopup=@IdTrx and AccountNumber=@AccountNumber

					if exists(
					select qw.* from
					(
						select IdTrx ,replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')+ REPLACE(RIGHT(Datetime,8),':','') Tangga
						from LogFoodcourtTransaksi where AccountNumber = @AccountNumber
						union all
						select IdTrx ,replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')+ REPLACE(RIGHT(Datetime,8),':','') Tangga	
						from LogRegistrasiDetail
						where AccountNumber = @AccountNumber
					) qw
					where Tangga >= @datetimeV)
					begin
						set @Succes = 0
						set @Message = 'Sorry, Akun ini telah bertransaksi setelah melakukan topup'	
					end
					else
					begin

					declare @IdTopup bigint
					set @IdTopup =(select top 1 IdTopup from LogTopupDetail 
					where left(Datetime,10) =FORMAT(GETDATE() , 'dd/MM/yyyy') and AccountNumber = @AccountNumber
					order by IdTopup desc)

					if(@IdTopup!=@IdTrx)
					begin
						set @Succes = 0
						set @Message = 'Sorry,tidak bisa dilakukan refund karena Transaksi ini bukan transaksi terakhir'	
					end
					else
					begin
						declare @PayCash float
						select @PayCash=PayCash from LogTopupDetail where IdTopup=@IdTrx and AccountNumber=@AccountNumber
						set @Succes = 1
						set @Message = 'Apakah anda yakin untuk menghapus transaksi Topup ini dan mengembalikan uang ke pengunjung sebesar Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@PayCash),1), '.00',''),',','.')	
					end

					end
				end
			end
		end
	
		select @Message  _Message,@Succes _Success	
	END TRY 
	BEGIN CATCH  
		set @Succes = 0
		set @Message = 'SP_CheckApakahIniTopupYangTerakhir, error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()+''
		select @Message  _Message,@Succes _Success				
	END CATCH;  
END

