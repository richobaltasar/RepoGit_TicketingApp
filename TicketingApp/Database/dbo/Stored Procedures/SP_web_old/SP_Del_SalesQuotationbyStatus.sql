CREATE PROCEDURE SP_Del_SalesQuotationbyStatus
	@IdQuotation bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataQuotationVendor where IdQuotation=@IdQuotation)
		begin
			declare @Omessage nvarchar(max)
			select  @Omessage='Sales  '+Perihal+' - '+CompanyName+' No. Quot :'+NumberQuotationByVendor+' berhasil ditambah',@IdQuotation = IdQuotation from DataQuotationVendor where IdQuotation = @IdQuotation
			
			delete from DataQuotationVendor where IdQuotation = @IdQuotation
			
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataQuotationVendor',@IdRow = @IdQuotation,@message=@Omessage,@Action='DELETE'

			if not exists(select  * from DataQuotationVendor where IdQuotation = @IdQuotation)
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
		select 'Sorry SP_Del_RAB error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
