CREATE PROCEDURE [dbo].[SP_ListItemData_Save]
	@id int,
	@ListName nvarchar(max),
	@Urutan nvarchar(max),
	@Text nvarchar(max),
	@Value nvarchar(max)
AS

declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

declare @IdS bigint
set @IdS = 0
if(@id = 0)
begin
	if not exists(select*from Master_ListItem where ListName = @ListName and Text = @Text)
	begin
		insert into Master_ListItem
		(
			ListName,
			Urutan,
			Text,
			Value
		)
		values 
		(
			@ListName,
			@Urutan,
			@Text,
			@Value
		)

		set @Id = (select id from Master_ListItem where id = SCOPE_IDENTITY())
		set @Title = 'Success'
		set @Message = 'Data ListItem '+ @ListName + ' dengan Item :'+ @Text +' berhasil dibuat'
		set @MStatus = 'success'
	end
	else
	begin
		set @Title = 'Sorry'
		set @Message = 'Data ListItem '+ @ListName + ' dengan Item :'+ @Text +' already exists'
		set @MStatus = 'error'
	end
end
else
begin
	declare @item_sebelum nvarchar(max)
	set @item_sebelum = (select Text from Master_ListItem where id=@id)
	if(@item_sebelum = @Text)
	begin
		update Master_ListItem
		set 
			ListName=@ListName,
			Urutan=@Urutan,
			Text=@Text,
			Value=@Value
		where 
		id=@id

		set @Title = 'Success'
		set @Message = 'Data ListItem '+ @ListName + ' dengan Item :'+ @Text +' berhasil diupdate'
		set @MStatus = 'success'

	end
	else
	begin
		declare @id_sama bigint
		if((select count(*) from Master_ListItem where ListName = @ListName and Text = @Text) > 0)
		begin
			set @Title = 'Sorry'
			set @Message = 'Data ListItem '+ @ListName + ' dengan Item :'+ @Text +' already exists, silahkan cari nama lain'
			set @MStatus = 'error'
		end
		else
		begin
			update Master_ListItem
			set 
				ListName=@ListName,
				Urutan=@Urutan,
				Text=@Text,
				Value=@Value
			where 
			id=@id

			set @Title = 'Success'
			set @Message = 'Data ListItem '+ @ListName + ' dengan Item :'+ @Text +' berhasil diupdate'
			set @MStatus = 'success'
		end
	end

end


select @Title Title, @Message Message, @MStatus Status, @Id Id
