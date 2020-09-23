CREATE PROCEDURE [dbo].[SP_GetNamaUser]
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	select NamaLengkap from UserData where id=@IdUser and Status = 1
END

--exec SP_GetNamaUser @IdUser=6

