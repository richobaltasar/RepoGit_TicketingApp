
CREATE PROCEDURE SP_Add_SalesQuotationbyStatus_ListItem
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
		if not exists(select*from DataQuotationVendor_Item where IdQuotation = @IdQuotation and IdRab = @IdRab and IdItemRAB = @IdItemRAB and NamaItem = @NamaItem)
		begin
			set @SubTotal = @Harga*@Unit

			insert into DataQuotationVendor_Item 
			(IdQuotation,IdRab,IdItemRAB,Category,NamaItem,Satuan,Unit,Harga,SubTotal,Attachment1,Status)
			values(
			@IdQuotation,@IdRab,@IdItemRAB,@Category,@NamaItem,@Satuan,@Unit,@Harga,@SubTotal,@Attachment1,@Status
			)

			declare @totalPenawaran float
			select @totalPenawaran= SUM(isnull(SubTotal,0)) from DataQuotationVendor_Item where IdQuotation = @IdQuotation
			
			update DataQuotationVendor 
			set 
				TotalPenawaran = @totalPenawaran
			where IdQuotation = @IdQuotation

			
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' berhasil ditambah',
			@id = id from DataQuotationVendor_Item 
			where id = SCOPE_IDENTITY()
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataQuotationVendor_Item',@IdRow = @Id,@message=@Omessage,@Action='ADD'
			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select @Omessage='Data IdQuotation = '+CAST(IdQuotation as nvarchar(max))+' Id RAB = '+CAST(IdRab as nvarchar(max))+' Id Item RAB = '+cast(IdItemRAB as nvarchar(max))+' '+
			NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' already exists' 
			from DataQuotationVendor_Item 
			where IdQuotation = @IdQuotation and IdRab = @IdRab and IdItemRAB = @IdItemRAB and NamaItem = @NamaItem
			select 'Sorry' title, @Omessage message,'error' status
		
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_SalesQuotationbyStatus_ListItem error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
