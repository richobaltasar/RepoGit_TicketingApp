
CREATE PROCEDURE SP_GetDataAccount
	@AccountNumber nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataAccount where AccountNumber = @AccountNumber
END











