CREATE PROCEDURE [dbo].[SP_LoginProc_GetID]
	@Username nvarchar(max),
	@Password nvarchar(max),
	@Category nvarchar(max)
AS
	
	select id
	from UserData where username = @Username and password=@Password
