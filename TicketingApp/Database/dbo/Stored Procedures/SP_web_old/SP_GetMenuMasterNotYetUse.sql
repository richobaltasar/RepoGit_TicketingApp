CREATE PROCEDURE SP_GetMenuMasterNotYetUse
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	
	select distinct a.*,c.NamaModule from DataMenu a
	inner join Role_MenuTree b on b.IdMenu = a.idMenu
	left join DataModule c on c.IdModul = b.IdModule
	where a.idMenu not in (select distinct IdMenu from DataHakAkses where UserId = @Id)
END