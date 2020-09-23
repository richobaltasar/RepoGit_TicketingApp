CREATE PROCEDURE SP_GetDataAccountV2
	@AccountNumber nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataAccount where AccountNumber = @AccountNumber
END


