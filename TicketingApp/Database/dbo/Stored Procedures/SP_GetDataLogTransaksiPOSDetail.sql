-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetDataLogTransaksiPOSDetail
	@IdTrx bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from LogTransaksiListDetailPOS
	where IdTrx = @IdTrx
END
