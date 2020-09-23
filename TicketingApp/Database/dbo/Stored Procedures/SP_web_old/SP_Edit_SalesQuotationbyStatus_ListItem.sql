CREATE PROCEDURE SP_Edit_SalesQuotationbyStatus_ListItem
	@Id bigint,
	@IdQuotation bigint,
	@IdRab bigint,
	@IdItemRAB bigint,
	@Category nvarchar(max),
	@NamaItem nvarchar(max),
	@Satuan nvarchar(max),
	@Unit float,
	@Harga float,
	@SubTotal float,
	@Attachment1 nvarchar(max),
	@Status bigint,
	@IdUserBy bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataQuotationVendor_Item where Id = @Id)
		begin
			set @SubTotal = @Harga*@Unit

			update DataQuotationVendor_Item 
				set 
					IdQuotation=@IdQuotation,
					IdRab=@IdRab,
					IdItemRAB=@IdItemRAB,
					Category=@Category,
					NamaItem=@NamaItem,
					Satuan=@Satuan,
					Unit=@Unit,
					Harga=@Harga,
					SubTotal=@SubTotal,
					Attachment1=@Attachment1,
					Status=@Status
			where id =@id

			declare @totalPenawaran float
			select @totalPenawaran= SUM(isnull(SubTotal,0)) from DataQuotationVendor_Item where IdQuotation = @IdQuotation
			
			update DataQuotationVendor 
			set 
			TotalPenawaran = @totalPenawaran
			where IdQuotation = @IdQuotation

			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' berhasil dirubah',
			@id = id from DataQuotationVendor_Item where id = @id
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataQuotationVendor_Item',@IdRow = @Id,@message=@Omessage,@Action='EDIT'
			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_SalesQuotationbyStatus_ListItem error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
