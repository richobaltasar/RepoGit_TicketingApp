CREATE PROCEDURE SP_Add_RABList
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
		if not exists(select*from DataRAB_Item where IdRAB = @IdRAB and NamaItem = @NamaItem and Category = @Category)
		begin
			insert into DataRAB_Item
			(
				IdRAB,
				Category,
				NamaItem,
				Satuan,
				Unit,
				Harga,
				SubTotal,
				Status
			)
			values(
				@IdRAB,
				@Category,
				@NamaItem,
				@Satuan,
				@Unit,
				@Harga,
				@SubTotal,
				@Status
			)
			
			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaItem+' - '+Category+' unit: '+cast(Unit as nvarchar)+' '+Satuan+' Harga :Rp '+CAST(Harga as nvarchar(max))+' berhasil ditambah',
			@id = id from DataRAB_Item where id = SCOPE_IDENTITY()
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataRAB_Item',@IdRow = @Id,@message=@Omessage,@Action='ADD'

			if exists(select  * from DataRAB_Item where id = @id)
			begin
				select 'Succes' title, @Omessage message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal insert' message,'error' status
			end

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_RABList error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON

SET ANSI_NULLS ON
