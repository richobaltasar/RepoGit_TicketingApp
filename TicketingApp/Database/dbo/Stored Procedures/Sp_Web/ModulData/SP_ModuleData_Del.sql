CREATE PROCEDURE [dbo].[SP_ModuleData_Del]
	@Id bigint
AS
declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

if exists(select*from DataModule where IdModul = @Id)
begin
	declare @NamaModule nvarchar(max)
	set @NamaModule =(select NamaModule from DataModule where IdModul=@Id)
	
	delete from DataModule where IdModul = @Id

	set @Title = 'Success'
	set @Message = 'Modul '+ @NamaModule + ' berhasil dihapus'
	set @MStatus = 'success'
end
else
begin
	set @Title = 'Sorry'
	set @Message = 'Modul '+ @NamaModule + ' not exists'
	set @MStatus = 'error'
end

select @Title Title, @Message Message, @MStatus Status, @Id Id