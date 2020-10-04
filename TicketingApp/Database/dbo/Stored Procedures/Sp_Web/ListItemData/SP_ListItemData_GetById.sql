CREATE PROCEDURE [dbo].[SP_ListItemData_GetById]
	@Id bigint
AS
	select*from Master_ListItem
	where id = @Id

