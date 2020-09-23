CREATE PROCEDURE SP_EditListDataMaster
	@id bigint,
    @ListName nvarchar(max),
    @Text nvarchar(max),
    @Urutan nvarchar(max),
    @Value nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from erp.dbo.Master_ListItem where id = @id)
		begin
			update ERP.dbo.Master_ListItem
			set
				ListName = @ListName,
				Text = @Text,
				Urutan = @Urutan,
				Value = @Value
			where id = @id
			select 'Succes' title, 'List name '+@ListName+' dengan item '+@Text+' suskes dirubah' message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'List name '+@ListName+' dengan item '+@Text+' not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_AddListDataMaster error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
