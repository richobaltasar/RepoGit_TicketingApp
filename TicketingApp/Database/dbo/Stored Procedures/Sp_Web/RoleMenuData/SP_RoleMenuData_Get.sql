CREATE PROCEDURE [dbo].[SP_RoleMenuData_Get]
AS
	select
	a.*,b.NamaModule,c.NamaMenu,d.NamaPosisi NamaPosisi,(
			select NamaMenu from Role_MenuTree x
			left join DataMenu xx on x.idMenu = xx.IdMenu
			where IdRole=a.IdParent
		) NamaParent,
	c.Platform
	from Role_MenuTree a
	left join DataModule b on b.IdModul = a.IdModule
	left join DataMenu c on c.idMenu = a.IdMenu
	left join (select Text NamaPosisi,value Id from Master_ListItem where ListName='ListPosisiMenu') d on d.Id = a.Posisi


	