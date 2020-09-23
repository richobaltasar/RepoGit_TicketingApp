
CREATE PROCEDURE WebSP_GetDataListView
	@NamaList nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	select*from DataListView where NameList = @NamaList

END
