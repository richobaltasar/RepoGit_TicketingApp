CREATE PROCEDURE [dbo].[SP_MenuData_GetMenuByIdModule]
	@Id bigint
AS
	
	select*from DataMenu where idMenu in (select IdMenu from Role_MenuTree where IdModule = @Id) 
