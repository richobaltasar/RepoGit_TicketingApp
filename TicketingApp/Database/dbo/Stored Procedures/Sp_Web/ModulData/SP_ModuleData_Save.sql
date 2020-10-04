create PROCEDURE [dbo].[SP_ModuleData_Save]
	@IdModul bigint,
	@NamaModule nvarchar(max),
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@Img nvarchar(max),
	@Status int
AS

declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

declare @Id bigint
set @Id = 0
if(@IdModul = 0)
begin
	if not exists(select*from [dbo].[DataModule] where NamaModule = @NamaModule)
	begin
		insert into DataModule
		(NamaModule,Action,Controller,Img,Status)
		values (@NamaModule,@Action,@Controller,@Img,@Status)
		set @Id = (select IdModul from DataModule where IdModul = SCOPE_IDENTITY())
		set @Title = 'Success'
		set @Message = 'Penambahan Modul '+ @NamaModule + ' berhasil dibuat'
		set @MStatus = 'success'
	end
	else
	begin
		set @Title = 'Sorry'
		set @Message = 'Modul '+ @NamaModule + ' already exists'
		set @MStatus = 'error'
	end
end
else
begin
	declare @namamodule_sebelum nvarchar(max)
	set @namamodule_sebelum = (select NamaModule from DataModule where IdModul=@IdModul)
	if(@namamodule_sebelum = @NamaModule)
	begin
		update DataModule
		set 
			Action=@Action,
			Controller = @Controller,
			Img = @Img,
			Status= @Status
		where 
		IdModul=@IdModul

		set @Id = @IdModul
		set @Title = 'Success'
		set @Message = 'Modul '+ @NamaModule + ' berhasil diupdate'
		set @MStatus = 'success'

	end
	else
	begin
		declare @idmodule_sama bigint
		if((select count(*) from DataModule where NamaModule= @NamaModule) > 0)
		begin
			set @Id = @IdModul
			set @Title = 'Sorry'
			set @Message = 'Modul '+ @NamaModule + ' already exists, silahkan cari nama lain'
			set @MStatus = 'error'
		end
		else
		begin
			update DataModule
			set 
				NamaModule=@NamaModule,
				Action=@Action,
				Controller = @Controller,
				Img = @Img,
				Status= @Status
			where 
			IdModul=@IdModul

			set @Id = @IdModul
			set @Title = 'Success'
			set @Message = 'Modul '+ @NamaModule + ' berhasil diupdate'
			set @MStatus = 'success'
		end
	end

end


select @Title Title, @Message Message, @MStatus Status, @Id Id