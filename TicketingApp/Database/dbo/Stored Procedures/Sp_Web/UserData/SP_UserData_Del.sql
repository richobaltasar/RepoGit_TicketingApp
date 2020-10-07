CREATE PROCEDURE [dbo].[SP_UserData_Del]
	@Id bigint
AS

declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

if exists(select*from UserData where id = @Id)
begin
	declare @Username nvarchar(max)
	set @Username =(select username from UserData where id=@Id)
	
	delete from UserData where id = @Id

	set @Title = 'Success'
	set @Message = 'User '+ @Username + ' berhasil dihapus'
	set @MStatus = 'success'
end
else
begin
	set @Title = 'Sorry'
	set @Message = 'User '+ @Username + ' not exists'
	set @MStatus = 'error'
end

select @Title Title, @Message Message, @MStatus Status, @Id Id