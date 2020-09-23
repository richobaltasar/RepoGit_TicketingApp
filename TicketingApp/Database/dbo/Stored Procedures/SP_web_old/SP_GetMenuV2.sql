CREATE PROCEDURE SP_GetMenuV2
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataTenant where NamaTenant like '%'+@search+'%'
	and Status =1
END
