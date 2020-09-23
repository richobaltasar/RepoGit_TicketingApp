CREATE PROCEDURE [dbo].[WebSP_LoginProc]
	@Username nvarchar(max),
	@Password nvarchar(max),
	@Category nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @title nvarchar(max)
	declare @message nvarchar(max)
	declare @icon nvarchar(max)
	declare @Akses nvarchar(max)

	if exists (
		select distinct a.* from UserData a 
		--left join DataHakAkses b on b.UserId = a.id 
		where 
		--b.Category = @Category and 
		a.username = @Username and password = @Password
	)
	begin
		set @title='Login Success'
		set @message='Akses anda diterima'
		set @icon='success'
		set @Akses = (select id from UserData where username = @Username and password = @Password)
	end
	else
	begin
		set @title='Login Failed'
		set @message='Username atau password anda tidak teregistrasi'
		set @icon='error'
	end
	select @title title, @message message,@icon icon,@Username Username,@Akses Akses
END