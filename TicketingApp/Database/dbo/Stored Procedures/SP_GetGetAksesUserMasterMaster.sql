CREATE PROCEDURE SP_GetGetAksesUserMasterMaster
	@IdUser bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	select b.idMenu,b.NamaMenu,c.NamaModule,b.Img,
		case when b.Status =1 then 'Aktif' else 'Non Aktif' end as Status
		into #Temp
	from Role_MenuTree a
	left join DataMenu b on b.idMenu = a.IdMenu
	left join DataModule c on c.IdModul = a.IdModule

	select distinct b.idMenu,b.NamaMenu,b.Img,Status,a.Category,b.NamaModule
	from DataHakAkses a 
	left join #Temp b on b.idMenu = a.IdMenu
	where a.UserId = @IdUser
	and NamaMenu like '%'+@search+'%'

END
