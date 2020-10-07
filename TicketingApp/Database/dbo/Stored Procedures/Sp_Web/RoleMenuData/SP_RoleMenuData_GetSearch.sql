CREATE PROCEDURE [dbo].[SP_RoleMenuData_GetSearch]
	--@IdRole bigint,
	@IdModule bigint,
	--@Posisi bigint,
	--@IdParent bigint,
	--@Urutan bigint,
	@IdMenu bigint
AS
	if(@IdModule=0 and @IdMenu=0)
	begin
		
		select
		a.*,b.NamaModule,c.NamaMenu,d.NamaPosisi NamaPosisi,
		(
			select NamaMenu from DataMenu where idMenu = a.IdParent
		) NamaParent,
		c.Platform
		from Role_MenuTree a
		left join DataModule b on b.IdModul = a.IdModule
		left join DataMenu c on c.idMenu = a.IdMenu
		left join (select Text NamaPosisi,value Id from Master_ListItem where ListName='ListPosisiMenu') d on d.Id = a.Posisi
	end
	else if(@IdModule>0 and @IdMenu=0)
	begin
		select
		a.*,b.NamaModule,c.NamaMenu,d.NamaPosisi NamaPosisi,
		(
			select NamaMenu from DataMenu where idMenu = a.IdParent
		) NamaParent,
		c.Platform
		from Role_MenuTree a
		left join DataModule b on b.IdModul = a.IdModule
		left join DataMenu c on c.idMenu = a.IdMenu
		left join (select Text NamaPosisi,value Id from Master_ListItem where ListName='ListPosisiMenu') d on d.Id = a.Posisi
		where IdModule = @IdModule
	end
	else
	begin
		select
		a.*,b.NamaModule,c.NamaMenu,d.NamaPosisi NamaPosisi,
		(
			select NamaMenu from DataMenu where idMenu = a.IdParent
		) NamaParent,
		c.Platform
		from Role_MenuTree a
		left join DataModule b on b.IdModul = a.IdModule
		left join DataMenu c on c.idMenu = a.IdMenu
		left join (select Text NamaPosisi,value Id from Master_ListItem where ListName='ListPosisiMenu') d on d.Id = a.Posisi
		where 
		(a.IdModule = @IdModule and a.IdMenu = @IdMenu)
	end

