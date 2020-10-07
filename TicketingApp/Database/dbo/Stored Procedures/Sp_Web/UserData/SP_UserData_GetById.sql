CREATE PROCEDURE [dbo].[SP_UserData_GetById]
	@Id bigint
AS
	select*from UserData
	where id = @Id
