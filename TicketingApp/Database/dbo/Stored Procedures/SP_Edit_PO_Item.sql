
CREATE PROCEDURE SP_Edit_PO_Item
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
		if exists(select*from DataPO_Item where Id=@Id)
		begin
			set @SubTotal = @Harga*@Unit
			
			update DataPO_Item
			set 
				IdPO=@IdPO,
				IdQuotation=@IdQuotation,
				IdItemQuotation=@IdItemQuotation,
				Category=@Category,
				NamaItem=@NamaItem,
				Satuan=@Satuan,
				Unit=@Unit,
				Harga=@Harga,
				SubTotal=@SubTotal,
				Attachment1=@Attachment1,
				Status=@Status
			where 
			Id = @Id

			declare @totalPO float
			select @totalPO= SUM(isnull(SubTotal,0)) from DataPO_Item where IdPO = @IdPO
			
			--select*from DataPO
			update DataPO
			set 
				TotalPO = @totalPO
			where IdPO = @IdPO

			
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' berhasil dirubah'
			from DataPO_Item
			where id = @Id
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataPO_Item',@IdRow = @Id,@message=@Omessage,@Action='EDIT'
			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			
			select @Omessage='Data Id PO = '+CAST(IdPO as nvarchar(max))+' Id Quot = '+CAST(IdQuotation as nvarchar(max))+' Id Item Quot = '+cast(IdItemQuotation as nvarchar(max))+' '+
			NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' not exists' 
			from DataPO_Item 
			where @Id = @Id
			
			select 'Sorry' title, @Omessage message,'error' status
		
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_PO_Item error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
