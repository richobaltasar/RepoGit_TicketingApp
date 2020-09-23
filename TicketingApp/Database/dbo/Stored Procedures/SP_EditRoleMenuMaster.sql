CREATE PROCEDURE SP_EditRoleMenuMaster
	@IdMenu bigint,
	@IdModule bigint,
	@IdParent bigint,
	@IdRole bigint,
	@Posisi bigint,
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@Img nvarchar(max),
	@NamaMenu nvarchar(max),
	@NamaModule nvarchar(max),
	@Platform nvarchar(max),
	@Status bigint,
	@Urutan bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from Role_MenuTree where IdRole = @IdRole)
		begin
			-- select*from Role_MenuTree
			update Role_MenuTree
			set
				IdModule = @IdModule,
				IdMenu = @IdMenu,
				Posisi = @Posisi,
				IdParent = @IdParent,
				Urutan = @Urutan
			where IdRole =@IdRole

			--select*from DataMenu
			update DataMenu
			set NamaMenu = @NamaMenu,
			Action = @Action,
			Controller = @Controller,
			Platform= @Platform,
			Img=@Img,
			Status = @Status
			where idMenu = @IdMenu

			select 'Succes' title, 'Update Role Id '+cast(@IdRole as nvarchar(max))+' is success' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'Role Id '+cast(@IdRole as nvarchar(max))+' doesn''t exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_EditRoleMenuMaster error' title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'error' status
	END CATCH;  
END