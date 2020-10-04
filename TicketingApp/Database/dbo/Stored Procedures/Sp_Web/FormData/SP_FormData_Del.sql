CREATE PROCEDURE [dbo].[SP_FormData_Del]
	@Id bigint
AS
declare @Title nvarchar(max)
declare @Message nvarchar(max)
declare @MStatus nvarchar(max)

if exists(select*from Master_Form where idLog = @Id)
begin
	declare @NamaForm nvarchar(max)
	declare @IdObj nvarchar(max)

	select @IdObj=Id, @NamaForm=NamaForm  from Master_Form where idLog=@Id
	
	delete from Master_Form where idLog=@Id

	set @Title = 'Success'
	set @Message = 'Data '+ @NamaForm + ' dengan ID Object : '+@IdObj+' berhasil dihapus'
	set @MStatus = 'success'
end
else
begin
	set @Title = 'Sorry'
	set @Message = 'data not exists'
	set @MStatus = 'error'
end

select @Title Title, @Message Message, @MStatus Status, @Id Id