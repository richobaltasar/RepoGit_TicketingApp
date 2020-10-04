CREATE PROCEDURE [dbo].[SP_MenuData_Save]
	@idMenu bigint,
	@NamaMenu nvarchar(max),
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@Platform nvarchar(max),
	@Img nvarchar(max),
	@Status bigint
AS

declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

declare @Id bigint
set @Id = 0
if(@idMenu = 0)
begin
	if not exists(select*from [dbo].[DataMenu] where NamaMenu = @NamaMenu)
	begin
		insert into DataMenu
		(
			NamaMenu,
			Action,
			Controller,
			Platform,
			Img,
			Status
		)
		values 
		(
			@NamaMenu,
			@Action,
			@Controller,
			@Platform,
			@Img,
			@Status
		)
		set @Id = (select idMenu from DataMenu where idMenu = SCOPE_IDENTITY())
		set @Title = 'Success'
		set @Message = 'Penambahan Menu '+ @NamaMenu + ' berhasil dibuat'
		set @MStatus = 'success'
	end
	else
	begin
		set @Title = 'Sorry'
		set @Message = 'Modul '+ @NamaMenu + ' already exists'
		set @MStatus = 'error'
	end
end
else
begin
	declare @namamenu_sebelum nvarchar(max)
	set @namamenu_sebelum = (select NamaMenu from DataMenu where idMenu=@idMenu)
	if(@namamenu_sebelum = @NamaMenu)
	begin
		update DataMenu
		set 
			NamaMenu=@NamaMenu,
			Action=@Action,
			Controller=@Controller,
			Platform=@Platform,
			Img=@Img,
			Status=@Status
		where 
		idmenu=@idMenu

		set @Id = @idMenu
		set @Title = 'Success'
		set @Message = 'Menu '+ @NamaMenu + ' berhasil diupdate'
		set @MStatus = 'success'

	end
	else
	begin
		declare @idmodule_sama bigint
		if((select count(*) from DataMenu where NamaMenu= @NamaMenu) > 0)
		begin
			set @Id = @idMenu
			set @Title = 'Sorry'
			set @Message = 'Modul '+ @NamaMenu + ' already exists, silahkan cari nama lain'
			set @MStatus = 'error'
		end
		else
		begin
			update DataMenu
			set 
				NamaMenu=@NamaMenu,
				Action=@Action,
				Controller=@Controller,
				Platform=@Platform,
				Img=@Img,
				Status=@Status
			where 
			idMenu=@idMenu

			set @Id = @idMenu
			set @Title = 'Success'
			set @Message = 'Menu '+ @NamaMenu + ' berhasil diupdate'
			set @MStatus = 'success'
		end
	end

end


select @Title Title, @Message Message, @MStatus Status, @Id Id
