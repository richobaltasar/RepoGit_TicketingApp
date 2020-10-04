CREATE PROCEDURE [dbo].[SP_MenuData_GetSearch]
	@idMenu bigint,
	@NamaMenu nvarchar(max),
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@Platform nvarchar(max),
	@Img nvarchar(max),
	@Status bigint
AS

select*from DataMenu
where NamaMenu like '%'+@NamaMenu+'%' and Platform like '%'+@Platform+'%'
and Controller like '%'+@Controller+'%'