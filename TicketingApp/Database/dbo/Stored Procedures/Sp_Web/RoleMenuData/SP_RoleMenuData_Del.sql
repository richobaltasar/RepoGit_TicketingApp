CREATE PROCEDURE [dbo].[SP_RoleMenuData_Del]
	@Id bigint
AS

declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)


if exists(select*from Role_MenuTree where IdRole = @Id)
begin
	
	declare @NamaModule nvarchar(max) 
	set @NamaModule = (select NamaModule from DataModule where IdModul in (select IdModule from Role_MenuTree where IdRole=@Id))
	
	declare @NamaMenu nvarchar(max) 
	set @NamaMenu = (select NamaMenu from DataMenu where idMenu in (select idMenu from Role_MenuTree where IdRole=@Id))
	
	delete from Role_MenuTree where IdRole=@Id

	set @Title = 'Success'
	set @Message = 'Data '+ @NamaModule + ' - '+ @NamaMenu +' berhasil dihapus'
	set @MStatus = 'success'
end
else
begin
	set @Title = 'Sorry'
	set @Message = 'data not exists'
	set @MStatus = 'error'
end

select @Title Title, @Message Message, @MStatus Status, @Id Id