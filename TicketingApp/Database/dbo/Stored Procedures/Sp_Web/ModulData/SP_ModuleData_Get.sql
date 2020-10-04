CREATE PROCEDURE [dbo].[SP_ModuleData_Get]
AS
	select top 100 *from DataModule
RETURN 
