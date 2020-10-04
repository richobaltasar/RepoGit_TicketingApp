CREATE PROCEDURE [dbo].[SP_FormData_Save]
	@idLog bigint,
	@NamaForm nvarchar(max),
	@Type nvarchar(max),
	@Id nvarchar(max),
	@TextLabel nvarchar(max),
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@ValueInput nvarchar(max),
	@ListModel nvarchar(max),
	@Urutan bigint,
	@ShowHide nvarchar(max),
	@ReadOnly nvarchar(max),
	@Enable nvarchar(max),
	@Mandatory nvarchar(max),
	@IsNumber bigint,
	@FilterBy bigint
AS

declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

declare @IdS bigint
set @IdS = 0
if(@idLog = 0)
begin
	if not exists(select*from Master_Form where NamaForm = @NamaForm and Id = @Id)
	begin
		insert into Master_Form
		(
			NamaForm,
			Type,
			Id,
			TextLabel,
			Action,
			Controller,
			ValueInput,
			ListModel,
			Urutan,
			ShowHide,
			ReadOnly,
			Enable,
			Mandatory,
			IsNumber,
			FilterBy
		)
		values 
		(
			@NamaForm,
			@Type,
			@Id,
			@TextLabel,
			@Action,
			@Controller,
			@ValueInput,
			@ListModel,
			@Urutan,
			@ShowHide,
			@ReadOnly,
			@Enable,
			@Mandatory,
			@IsNumber,
			@FilterBy
		)

		set @Id = (select idLog from Master_Form where idLog = SCOPE_IDENTITY())
		set @Title = 'Success'
		set @Message = 'Penambahan Element Form '+ @NamaForm + ' dengan object :'+ @Id +' berhasil dibuat'
		set @MStatus = 'success'
	end
	else
	begin
		set @Title = 'Sorry'
		set @Message = 'Data Element Form '+ @NamaForm + ' dengan object :'+ @Id +' already exists'
		set @MStatus = 'error'
	end
end
else
begin
	declare @namaObject_sebelum nvarchar(max)
	set @namaObject_sebelum = (select Id from Master_Form where idLog=@idLog)
	if(@namaObject_sebelum = @Id)
	begin
		update Master_Form
		set 
			NamaForm=@NamaForm,
			Type=@Type,
			Id=@Id,
			TextLabel=@TextLabel,
			Action=@Action,
			Controller=@Controller,
			ValueInput=@ValueInput,
			ListModel=@ListModel,
			Urutan=@Urutan,
			ShowHide=@ShowHide,
			ReadOnly=@ReadOnly,
			Enable=@Enable,
			Mandatory=@Mandatory,
			IsNumber=@IsNumber,
			FilterBy=@FilterBy
		where 
		idLog=@idLog

		set @Id = @idLog
		set @Title = 'Success'
		set @Message = 'Data Element Form '+ @NamaForm + ' dengan object :'+ @Id +' berhasil diupdate'
		set @MStatus = 'success'

	end
	else
	begin
		declare @idmodule_sama bigint
		if((select count(*) from Master_Form where NamaForm=@NamaForm and Id=@Id) > 0)
		begin
			set @Id = @idLog
			set @Title = 'Sorry'
			set @Message = 'Data Element Form '+ @NamaForm + ' dengan object :'+ @Id +' already exists, silahkan cari nama lain'
			set @MStatus = 'error'
		end
		else
		begin
			update Master_Form
			set 
				NamaForm=@NamaForm,
				Type=@Type,
				Id=@Id,
				TextLabel=@TextLabel,
				Action=@Action,
				Controller=@Controller,
				ValueInput=@ValueInput,
				ListModel=@ListModel,
				Urutan=@Urutan,
				ShowHide=@ShowHide,
				ReadOnly=@ReadOnly,
				Enable=@Enable,
				Mandatory=@Mandatory,
				IsNumber=@IsNumber,
				FilterBy=@FilterBy
			where 
			idLog=@idLog

			set @Id = @idLog
			set @Title = 'Success'
			set @Message = 'Data Element Form '+ @NamaForm + ' dengan object :'+ @Id +' berhasil diupdate'
			set @MStatus = 'success'
		end
	end

end


select @Title Title, @Message Message, @MStatus Status, @Id Id
