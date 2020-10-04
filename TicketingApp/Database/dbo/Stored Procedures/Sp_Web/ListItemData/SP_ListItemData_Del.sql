CREATE PROCEDURE [dbo].[SP_ListItemData_Del]
	@Id bigint
AS
declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

if exists(select*from Master_ListItem where id = @Id)
begin
	declare @NamaList nvarchar(max)
	declare @Item nvarchar(max)

	select @Item=Text, @NamaList=ListName  from Master_ListItem where id=@Id
	
	delete from Master_ListItem where id=@Id

	set @Title = 'Success'
	set @Message = 'Data '+ @NamaList + ' dengan Item : '+@Item+' berhasil dihapus'
	set @MStatus = 'success'
end
else
begin
	set @Title = 'Sorry'
	set @Message = 'data not exists'
	set @MStatus = 'error'
end

select @Title Title, @Message Message, @MStatus Status, @Id Id