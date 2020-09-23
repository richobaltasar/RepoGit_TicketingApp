
CREATE PROCEDURE SP_GetDataUserV2
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from UserData where id = @IdUser
END
