-- exec GetDataAccount @AccountNumber='2735000457-20200614011130'
CREATE PROCEDURE GetDataAccount
	@AccountNumber nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataAccount 
	where AccountNumber = @AccountNumber
	--and Status = 1
END
