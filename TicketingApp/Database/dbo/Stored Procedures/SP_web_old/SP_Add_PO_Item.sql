
CREATE PROCEDURE SP_Add_PO_Item
	@Id bigint,
	@IdPO bigint,
	@IdQuotation bigint,
	@IdItemQuotation bigint,
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
		if not exists(select*from DataPO_Item where IdPO = @IdPO and IdQuotation = @IdQuotation and IdItemQuotation = @IdItemQuotation and NamaItem = @NamaItem)
		begin
			set @SubTotal = @Harga*@Unit

			insert into DataPO_Item
			(
				IdPO,
				IdQuotation,
				IdItemQuotation,
				Category,
				NamaItem,
				Satuan,
				Unit,
				Harga,
				SubTotal,
				Attachment1,
				Status
			)
			values(
				@IdPO,
				@IdQuotation,
				@IdItemQuotation,
				@Category,
				@NamaItem,
				@Satuan,
				@Unit,
				@Harga,
				@SubTotal,
				@Attachment1,
				@Status
			)

			declare @totalPO float
			select @totalPO= SUM(isnull(SubTotal,0)) from DataPO_Item where IdPO = @IdPO
			
			--select*from DataPO
			update DataPO
			set 
				TotalPO = @totalPO
			where IdPO = @IdPO

			
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' berhasil ditambah',
			@id = id from DataPO_Item
			where id = SCOPE_IDENTITY()
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataPO_Item',@IdRow = @Id,@message=@Omessage,@Action='ADD'
			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select @Omessage='Data Id PO = '+CAST(IdPO as nvarchar(max))+' Id Quot = '+CAST(IdQuotation as nvarchar(max))+' Id Item Quot = '+cast(IdItemQuotation as nvarchar(max))+' '+
			NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' already exists' 
			from DataPO_Item 
			where IdPO = @IdPO and IdQuotation = @IdQuotation and IdItemQuotation = @IdItemQuotation and NamaItem = @NamaItem
			select 'Sorry' title, @Omessage message,'error' status
		
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_PO_Item error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
