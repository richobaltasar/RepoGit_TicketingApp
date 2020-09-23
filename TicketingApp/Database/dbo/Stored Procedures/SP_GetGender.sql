CREATE PROCEDURE SP_GetGender
AS
BEGIN
	SET NOCOUNT ON;
	select*from ERP.dbo.Master_ListItem where ListName = 'ListGender'
END
