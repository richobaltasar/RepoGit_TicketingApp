CREATE PROCEDURE SP_GetDataParkir
	@AccountNumber nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from LogParkir where AccountNumber = @AccountNumber
	and Status = 1
END
