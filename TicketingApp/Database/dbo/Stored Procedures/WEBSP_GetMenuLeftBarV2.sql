--exec WEBSP_GetMenuLeftBarV2 @IdModul = 24,@IdPosisi=1,@IdParent=0,@IdUser=6
--exec WEBSP_GetMenuLeftBarV2 @IdModul = 24,@IdPosisi=2,@IdParent=40132,@IdUser=6
-- exec WEBSP_GetMenuLeftBarV2 @IdModul = 24,@IdPosisi=3,@IdParent=60133,@IdUser=6

CREATE PROCEDURE WEBSP_GetMenuLeftBarV2
	@IdModul bigint,
	@IdPosisi bigint,
	@IdParent bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	if(@IdPosisi = 1 or @IdPosisi =2)
	begin
		select a.IdRole,b.NamaMenu,b.Action,b.Controller,b.Img, b.idMenu from Role_MenuTree a
		left join DataMenu b on b.idMenu = a.IdMenu
		left join DataModule c on c.IdModul = a.IdModule
		where 
		IdModule = @IdModul 
		and a.Posisi=@IdPosisi 
		and IdParent = @IdParent
		and b.idMenu in (select IdMenu from DataHakAkses where UserId = @IdUser and Category = 'WEB')
		and b.Status = 1
		order by a.Urutan asc
	end
	else if(@IdPosisi=3)
	begin
		select a.IdRole,b.NamaMenu,b.Action,b.Controller,b.Img, b.idMenu from Role_MenuTree a
		left join DataMenu b on b.idMenu = a.IdMenu
		left join DataModule c on c.IdModul = a.IdModule
		where IdModule = @IdModul and Posisi=@IdPosisi and IdParent=@IdParent
		and b.idMenu in (select IdMenu from DataHakAkses where UserId = @IdUser and Category = 'WEB')
		
		--select*from DataMenu where idMenu=60154
		--select*from Role_MenuTree where Posisi =3

	end
END


