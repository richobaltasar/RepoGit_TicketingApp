CREATE PROCEDURE [dbo].[SP_ListItemData_GetSearch]
	@id int,
	@ListName nvarchar(max),
	@Urutan nvarchar(max),
	@Text nvarchar(max),
	@Value nvarchar(max)
AS
	select*from Master_ListItem
	where 
	ListName like '%'+@ListName+'%'
