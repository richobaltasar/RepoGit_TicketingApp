CREATE PROCEDURE [dbo].[SP_ListItemData_GetSearch]
	
	@ListName nvarchar(max)
AS
	select*from Master_ListItem
	where 
	REPLACE(RTRIM(LTRIM(ListName)),' ','') like '%'+ REPLACE(RTRIM(LTRIM(@ListName)),' ','')+'%'