
CREATE PROCEDURE SP_GetLogTransaksiListDetailPOS
	@TrxId bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from LogTransaksiListDetailPOS where IdTrx = @TrxId
END
