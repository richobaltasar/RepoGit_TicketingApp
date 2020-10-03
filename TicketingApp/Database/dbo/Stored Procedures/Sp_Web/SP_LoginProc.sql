CREATE PROCEDURE [dbo].[SP_LoginProc]
	@Username nvarchar(max),
	@Password nvarchar(max),
	@Category nvarchar(max)
AS
	
	if exists(select*from UserData where username = @Username and password=@Password)
	begin
		select 
		'Success' title,
		'Login Success' message,
		'success' status
	end
	else
	begin
		select 
		'Sorry' title,
		'Username dan password tidak teregistrasi, mohon dicheck kembali ' message,
		'error' status
	end
