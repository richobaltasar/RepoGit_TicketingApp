CREATE PROCEDURE SP_AddListDataMaster
	@id bigint,
    @ListName nvarchar(max),
    @Text nvarchar(max),
    @Urutan nvarchar(max),
    @Value nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if not exists(select*from erp.dbo.Master_ListItem where ListName = @ListName and Text = @Text)
		begin
			insert into ERP.dbo.Master_ListItem
			(ListName,Urutan,Text,Value)
			values(@ListName,@Urutan,@Text,@Value)

			if exists(select * from ERP.dbo.Master_ListItem where id = SCOPE_IDENTITY())
			begin
				select 'Succes' title, 'List name '+@ListName+' dengan item '+@Text+' suskes ditambahkan' message,'success' status
			end
			else
			begin
				select 'Sorry' title, 'List name '+@ListName+' dengan item '+@Text+' gagal dalam proses insert' message,'error' status
			end
		end
		else
		begin
			select 'Sorry' title, 'List name '+@ListName+' dengan item '+@Text+' already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_AddListDataMaster error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
