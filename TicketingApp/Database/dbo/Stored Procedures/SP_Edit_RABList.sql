
CREATE PROCEDURE SP_Edit_RABList
	@Id bigint,
	@IdRAB bigint,
	@Category nvarchar(max),
	@NamaItem nvarchar(max),
	@Satuan nvarchar(max),
	@Unit float,
	@Harga float,
	@SubTotal float,
	@Status bigint,
	@IdUserBy bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataRAB_Item where id = @id)
		begin
			update DataRAB_Item 
			set 
				IdRAB=@IdRAB,
				Category=@Category,
				NamaItem=@NamaItem,
				Satuan=@Satuan,
				Unit=@Unit,
				Harga=@Harga,
				SubTotal=@SubTotal,
				Status = @Status
			where id =@id

			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' berhasil dirubah',
			@id = id from DataRAB_Item where id = @id
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataRAB_Item',@IdRow = @Id,@message=@Omessage,@Action='EDIT'
			
			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_RABList error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON
