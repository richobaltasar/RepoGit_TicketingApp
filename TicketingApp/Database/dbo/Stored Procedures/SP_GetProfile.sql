CREATE PROCEDURE SP_GetProfile
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from UserData where id = @IdUser
END
