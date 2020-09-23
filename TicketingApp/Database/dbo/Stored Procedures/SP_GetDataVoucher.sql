
CREATE PROCEDURE SP_GetDataVoucher
	@Barcode nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select TOP 1* from DataVoucher where CodeVoucher = @Barcode
END
