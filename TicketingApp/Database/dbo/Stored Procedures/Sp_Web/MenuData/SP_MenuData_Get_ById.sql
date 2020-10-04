CREATE PROCEDURE [dbo].[SP_MenuData_Get_ById]
	@Id bigint
AS
	select*from DataMenu
	where idMenu = @Id
