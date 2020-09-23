CREATE PROCEDURE SP_AddRoleMenuMaster
	@IdMenu bigint,
	@IdModule bigint,
	@IdParent bigint,
	@IdRole bigint,
	@Posisi bigint,
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@Img nvarchar(max),
	@NamaMenu nvarchar(max),
	@Platform nvarchar(max),
	@Status bigint,
	@Urutan bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		--select*from DataMenu where NamaMenu = 'Voucher'
		
		if not exists(select*from DataMenu where NamaMenu = @NamaMenu and Controller=@Controller and status > 0)
		begin
			insert into DataMenu
			(NamaMenu,Action,Controller,Platform,Img,Status)
			values(@NamaMenu,@Action,@Controller,@Platform,@Img,@Status)

			select @IdMenu=idMenu from DataMenu where idMenu = SCOPE_IDENTITY()

			if not exists(select*from Role_MenuTree where IdMenu = @IdMenu)
			begin
				insert into Role_MenuTree
				(IdModule,Posisi,IdParent,Urutan,IdMenu)
				values(@IdModule,@Posisi,@IdParent,@Urutan,@IdMenu)
				select 'Succes' title, 'Adding Role for Menu '+@NamaMenu+' is success' message,'success' status
			end
			else
			begin
				update Role_MenuTree
				set 
					IdModule = @IdModule,
					Posisi = @Posisi,
					IdParent = @IdParent,
					Urutan = @Urutan
				where IdMenu = @IdMenu
				select 'Succes' title, 'Update Role for Menu '+@NamaMenu+' is success' message,'success' status
			end
		end
		else
		begin
			select 'Sorry' title, 'Role Menu '+@NamaMenu+' already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_AddRoleMenuMaster error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END