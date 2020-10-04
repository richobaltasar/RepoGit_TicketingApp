CREATE PROCEDURE [dbo].[SP_MenuData_Del]
	@Id bigint
AS
declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

if exists(select*from DataMenu where idMenu = @Id)
begin
	declare @NamaMenu nvarchar(max)
	set @NamaMenu =(select NamaMenu from DataMenu where idMenu=@Id)
	
	delete from DataMenu where idMenu = @Id

	set @Title = 'Success'
	set @Message = 'Menu '+ @NamaMenu + ' berhasil dihapus'
	set @MStatus = 'success'
end
else
begin
	set @Title = 'Sorry'
	set @Message = 'Modul '+ @NamaMenu + ' not exists'
	set @MStatus = 'error'
end

select @Title Title, @Message Message, @MStatus Status, @Id Id