CREATE PROCEDURE [dbo].[SP_RoleMenuData_Save]
	@IdRole bigint,
	@IdModule bigint,
	@Posisi bigint,
	@IdParent bigint,
	@Urutan bigint,
	@IdMenu bigint
AS


declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

declare @IdS bigint
set @IdS = 0

declare @NamaModule nvarchar(max) set @NamaModule = (select NamaModule from DataModule where IdModul = @IdModule)
declare @NamaMenu nvarchar(max) set @NamaMenu = (select NamaMenu from DataMenu where idMenu = @IdMenu)

if(@IdRole = 0)
begin
	if not exists(select*from Role_MenuTree where IdModule = @IdModule and IdMenu = @IdMenu)
	begin
		insert into Role_MenuTree
		(
			IdModule,
			Posisi,
			IdParent,
			Urutan,
			IdMenu
		)
		values 
		(
			@IdModule,
			@Posisi,
			@IdParent,
			@Urutan,
			@IdMenu
		)

		set @IdRole = (select IdRole from Role_MenuTree where IdRole = SCOPE_IDENTITY())
		set @Title = 'Success'
		set @Message = 'Data '+ @NamaModule + ' - '+ @NamaMenu +' berhasil dibuat'
		set @MStatus = 'success'
	end
	else
	begin
		set @Title = 'Sorry'
		set @Message = 'Data '+ @NamaModule + ' - '+ @NamaMenu +' already exists'
		set @MStatus = 'error'
	end
end
else
begin
	declare @idModule_sebelum bigint
	declare @idMenu_sebelum bigint

	set @idModule_sebelum = (select IdModule from Role_MenuTree where IdRole=@IdRole)
	set @idMenu_sebelum = (select IdMenu from Role_MenuTree where IdRole=@IdRole)
	
	if(@IdModule = @idModule_sebelum and @IdMenu = @idMenu_sebelum)
	begin
		update Role_MenuTree
		set 
			IdModule=@IdModule,
			Posisi=@Posisi,
			IdParent=@IdParent,
			Urutan=@Urutan,
			IdMenu=@IdMenu
		where 
		IdRole=@IdRole

		set @Title = 'Success'
		set @Message = 'Data '+ @NamaModule + ' - '+ @NamaMenu +' berhasil diupdate'
		set @MStatus = 'success'
	end
	else
	begin
		if((select count(*) from Role_MenuTree where IdModule = @IdModule and IdMenu = @IdMenu) > 0)
		begin
			set @Title = 'Sorry'
			set @Message = 'Data '+ @NamaModule + ' - '+ @NamaMenu  +' already exists, silahkan cari nama lain'
			set @MStatus = 'error'
		end
		else
		begin
			update Role_MenuTree
			set 
				IdModule=@IdModule,
				Posisi=@Posisi,
				IdParent=@IdParent,
				Urutan=@Urutan,
				IdMenu=@IdMenu
			where 
			IdRole=@IdRole

			set @Title = 'Success'
			set @Message = 'Data '+ @NamaModule + ' - '+ @NamaMenu +' berhasil diupdate'
			set @MStatus = 'success'
		end
	end

end


select @Title Title, @Message Message, @MStatus Status, @IdRole Id