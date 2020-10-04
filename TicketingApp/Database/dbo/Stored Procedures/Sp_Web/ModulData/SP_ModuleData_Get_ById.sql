CREATE PROCEDURE [dbo].[SP_ModuleData_Get_ById]
	@Id bigint
AS
	select*from DataModule
	where IdModul = @Id
RETURN 
