CREATE PROCEDURE SP_Del_PO_Item
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataPO_Item where Id = @Id)
		begin
			declare @Omessage nvarchar(max)
			
			select  @Omessage='Data  '+Category+' - '+NamaItem+' berhasil dihapus pada list PO Item'
			from DataPO_Item where Id = @Id
			
			delete from DataPO_Item where Id = @Id
			
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataPO_Item',@IdRow = @Id,@message=@Omessage,@Action='DELETE'

			if not exists(select  * from DataPO_Item where Id = @Id)
			begin
				select 'Succes' title, @Omessage message,'success' status
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
		select 'Sorry SP_Del_PO_Item error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
