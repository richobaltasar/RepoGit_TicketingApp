CREATE PROCEDURE SP_Del_PO
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataPO where IdPO = @Id)
		begin
			declare @Omessage nvarchar(max)
			
			select  @Omessage='Sales  '+Perihal+' - '+CompanyName+' No. Quot :'+NumberQuotationByVendor+' berhasil dihapus pada list PO'
			from DataQuotationVendor where IdQuotation in (select IdQuotation from DataPO where IdPO = @Id)
			
			delete from DataPO where IdPO = @Id
			
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataPO',@IdRow = @Id,@message=@Omessage,@Action='DELETE'

			if not exists(select  * from DataPO where IdPO = @Id)
			begin
				delete DataPO_Item where IdPO = @Id
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
		select 'Sorry SP_Del_PO error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
