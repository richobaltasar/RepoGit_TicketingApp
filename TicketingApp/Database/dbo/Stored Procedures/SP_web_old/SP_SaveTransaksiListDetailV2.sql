
CREATE PROCEDURE SP_SaveTransaksiListDetailV2
	@Id nvarchar(max),
    @IdTrx float,
    @Category nvarchar(max),
	@NamaItem nvarchar(max),
	@Harga float,
    @Qtx float,	
	@Total float
AS
BEGIN
	SET NOCOUNT ON;
	declare @title nvarchar(max)
	declare @message nvarchar(max)
	declare @status nvarchar(max)

	BEGIN TRY  
		delete from LogTransaksiListDetailPOS 
		where IdTrx = @IdTrx and Id = @Id and Category = @Category
		and NamaItem = @NamaItem
		and Harga = @Harga
		and Qtx = @Qtx
		and Total = @Total

		insert into [dbo].[LogTransaksiListDetailPOS]
		(			
			IdTrx,
			Id,
			Category,
			NamaItem,
			Harga,
			Qtx,
			Total
		)
		values(
			@IdTrx,
			@Id,
			@Category,
			@NamaItem,
			@Harga,
			@Qtx,
			@Total
		)

		select @message = convert(nvarchar(max), count(idTrx))
		from LogTransaksiListDetailPOS where idTrx = @IdTrx
		set @title='SP_SaveTransaksiListDetailV2 berhasil'
		set @status='SUCCESS'
		
		select @status status, @message message, @title title
	END TRY 
	BEGIN CATCH  
		select 'ERROR' status, 'Msg :'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'Error Exception' title
	END CATCH;  
END
