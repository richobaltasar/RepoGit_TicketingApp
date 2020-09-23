
CREATE PROCEDURE SP_AuthorizeCancelTrx
	@Username nvarchar(max),
	@Password nvarchar(max) 
AS
BEGIN
	SET NOCOUNT ON;

    select a.NamaLengkap,a.NamaDivisi,a.NamaPosisi,c.NamaMenu 
	from UserData a
	left join DataHakAkses b on b.UserId = a.id
	left join DataMenu c on c.idMenu = b.IdMenu
	where username = @Username and password=@Password
	and c.NamaMenu= 'AuthorizeCancelTrx'

END
