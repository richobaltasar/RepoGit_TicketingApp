
CREATE PROCEDURE SP_Get_LogTransaksiPOS
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from LogTransaksiPOS
	where Status =1
END
