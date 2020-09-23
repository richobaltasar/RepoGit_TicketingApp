CREATE PROCEDURE SP_EditMenuMaster
	@NamaMenu nvarchar(max),
    @Action nvarchar(max),
    @Controller nvarchar(max),
    @Img nvarchar(max),
    @Platform nvarchar(max),
	@Status bigint,
	@idMenu bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataMenu where idMenu = @idMenu)
		begin
		if(@Img = '')
		begin
			update DataMenu
			set
				NamaMenu = @NamaMenu,
				Action = @Action,
				Controller = @Controller,
				Platform = @Platform,
				Status = @Status
			where idMenu =@idMenu
		end
		else
		begin
			update DataMenu
			set
				NamaMenu = @NamaMenu,
				Action = @Action,
				Controller = @Controller,
				Platform = @Platform,
				Status = @Status,
				Img = @Img
			where idMenu =@idMenu
		end
			select 'Succes' title, 'Update Menu '+@NamaMenu+' is success' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'Menu '+@NamaMenu+' doesn''t exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_EditMenuMaster error' title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'error' status
	END CATCH;  
END
