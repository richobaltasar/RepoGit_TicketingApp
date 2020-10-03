CREATE PROCEDURE [dbo].[SP_GetProfileUser]
	@IdUser bigint
AS
	select
	*
	from UserData where id =@IdUser
