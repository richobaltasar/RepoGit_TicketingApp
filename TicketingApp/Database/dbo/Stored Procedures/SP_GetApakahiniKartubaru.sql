CREATE PROCEDURE SP_GetApakahiniKartubaru
	@IdTrx bigint,
	@AccountNumber nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		declare @Succes int
		declare @Message nvarchar(max)

		if exists(select*from LogRegistrasiDetail where idTrx = @IdTrx)
		begin
			declare @JaminanBefore float
			declare @TotalPengembalian float
			
			select @TotalPengembalian = TotalBayar from LogRegistrasiDetail where idTrx = @IdTrx
			select @JaminanBefore=SaldoJaminan from LogRegistrasiDetail where idTrx = @IdTrx
			if(@JaminanBefore = 0)
			begin
				set @Succes = 1	
				set @Message = 'Apakah Anda yakin untuk menghapus transaksi ini dan mengembalikan Uang Tunai dengan total transaksi sebesar Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@TotalPengembalian),1), '.00',''),',','.')
			end
			else
			begin
				declare @PayEmoney float
				declare @PayCash float

				select @PayEmoney = ISNULL(PayEmoney,0),@PayCash=ISNULL(PayCash,0) from LogRegistrasiDetail where idTrx=@IdTrx
				if(@PayEmoney = 0 and @PayCash > 0)
				begin
					set @Succes = 2
					set @Message = 'Apakah Anda yakin untuk menghapus transaksi ini dan mengembalikan Uang Tunai dengan total transaksi sebesar Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@TotalPengembalian),1), '.00',''),',','.')
				end
				else if(@PayEmoney>0 and @PayCash=0)
				begin
					set @Succes = 3
					set @Message = 'Apakah Anda yakin untuk menghapus transaksi ini dan mengembalikan Saldo eMoney ke kartu dengan total transaksi sebesar Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@TotalPengembalian),1), '.00',''),',','.')
				end
				else if(@PayEmoney>0 and @PayCash>0)
				begin
					set @Succes = 4
					set @Message = 'Apakah Anda yakin untuk menghapus transaksi ini dan mengembalikan Saldo eMoney = Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@PayEmoney),1), '.00',''),',','.')+' & Uang Tunai = Rp '+REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,@PayCash),1), '.00',''),',','.')
				end
			end			
		end
		else
		begin
			set @Succes = 0
			set @Message = 'Transaksi Id :'+cast(@IdTrx as nvarchar(max))+' tidak ditemukan'
		end
		select @Message  _Message,@Succes _Success	
	END TRY 
	BEGIN CATCH  
		set @Succes = 0
		set @Message = 'SP error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()
		select @Message  _Message,@Succes _Success				
	END CATCH;  
END
