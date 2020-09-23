
CREATE PROCEDURE SP_ReadParkirCheckin
	@BarcodeId nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from LogParkir where BarcodeReciptCode = @BarcodeId
	and status =1
END
