--exec SP_GetMenu @Platform='SettingDesktop', @IdUser =6

CREATE PROCEDURE SP_GetMenu
	@Platform nvarchar(max),
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	select a.*,b.Urutan from dataMenu a
	left join Role_MenuTree b on b.IdMenu = a.idMenu
	where a.Platform = @Platform
	and a.idMenu in (select IdMenu from DataHakAkses where UserId = @IdUser)
	order by b.Urutan asc


	

END
