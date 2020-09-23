CREATE PROCEDURE SP_Get_ListStatusRAB
AS
BEGIN
	SET NOCOUNT ON;
	select Text,Value from ERP.dbo.Master_ListItem where ListName='ListStatusRAB'
END