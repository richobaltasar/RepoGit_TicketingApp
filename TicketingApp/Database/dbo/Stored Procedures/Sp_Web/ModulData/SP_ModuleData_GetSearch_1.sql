-- exec SP_ModuleData_GetSearch @NamaModule='',@Status=0
CREATE PROCEDURE [dbo].[SP_ModuleData_GetSearch]
	@NamaModule nvarchar(max),
	@Status  bigint
AS
	select*from DataModule
	where NamaModule like '%'+@NamaModule+'%' and Status = @Status
