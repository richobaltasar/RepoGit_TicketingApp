CREATE PROCEDURE [dbo].[SP_RoleMenuData_GetById]
	@Id bigint
AS
	select
	a.*,b.NamaModule,c.NamaMenu,'' NamaPosisi,'' NamaParent,
	c.Platform
	from Role_MenuTree a
	left join DataModule b on b.IdModul = a.IdModule
	left join DataMenu c on c.idMenu = a.IdMenu
	where IdRole = @Id
