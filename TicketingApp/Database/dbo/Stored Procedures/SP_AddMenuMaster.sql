CREATE PROCEDURE SP_AddMenuMaster
	@NamaMenu nvarchar(max),
    @Action nvarchar(max),
    @Controller nvarchar(max),
    @Img nvarchar(max),
    @Platform nvarchar(max),
	@Status bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if not exists(select*from DataMenu where NamaMenu = @NamaMenu)
		begin
			--select*from DataMenu
			insert into DataMenu
			(NamaMenu,Action,Controller,Platform,Img,Status)
			values(@NamaMenu,@Action,@Controller,@Platform,@Img,@Status)

			select 'Succes' title, 'Adding Menu '+@NamaMenu+' is success' message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'Menu '+@NamaMenu+' already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_AddMenuMaster error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END