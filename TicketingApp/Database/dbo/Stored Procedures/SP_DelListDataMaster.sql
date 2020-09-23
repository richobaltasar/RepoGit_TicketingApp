create PROCEDURE SP_DelListDataMaster
	@id bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from erp.dbo.Master_ListItem where id = @id)
		begin
			declare @ListName nvarchar(max)
			declare @Text nvarchar(max)
			select @ListName=ListName, @Text=Text from erp.dbo.Master_ListItem where id=@id
			
			delete from erp.dbo.Master_ListItem where id=@id
			select 'Succes' title, 'List name '+@ListName+' dengan item '+@Text+' suskes dirubah' message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_DelListDataMaster error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
