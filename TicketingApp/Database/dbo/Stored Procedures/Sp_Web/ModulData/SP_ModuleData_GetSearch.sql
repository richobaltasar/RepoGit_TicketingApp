CREATE PROCEDURE [dbo].[SP_ModuleData_GetSearch]
	@IdModul bigint,
	@NamaModule nvarchar(max),
	@Action nvarchar(max),
	@Controller nvarchar(max),
	@Img nvarchar(max),
	@Status bigint
AS
	select*from DataModule where NamaModule like '%'+@NamaModule+'%'
