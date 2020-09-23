CREATE PROCEDURE [dbo].[SP_GetDataBank]
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from [dbo].[DataBank]
	where [NamaBank] like '%'+@search+'%' and status = 1
END
