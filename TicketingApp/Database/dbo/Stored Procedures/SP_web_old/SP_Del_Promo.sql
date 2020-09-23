---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Del_Promo
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataPromo where idPromo = @Id)
		begin
			declare @Omessage nvarchar(max)
			select @Omessage='Data  '+NamaPromo +' dengan diskon :'+CAST(Diskon as nvarchar(max))+'% berhasil dihapus' from DataPromo where idPromo = @Id
			delete from DataPromo where idPromo = @Id
			
			if not exists(select  * from DataPromo where idPromo = @Id)
			begin
				-- insert Log Activity
				exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataPromo',@IdRow = @Id,@message=@Omessage,@Action='DELETE'

				select 'Succes' title,@Omessage message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal delete' message,'error' status
			end
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Del_Promo error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
