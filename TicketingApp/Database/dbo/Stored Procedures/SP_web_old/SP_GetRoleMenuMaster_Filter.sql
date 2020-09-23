

--sp_helptext SP_GetRoleMenuMaster
CREATE PROCEDURE SP_GetRoleMenuMaster_Filter
	@NamaMenu nvarchar(max),
    @Platform nvarchar(max),
    @NamaModule nvarchar(max),
	@MainMenu nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
		select*from 
		(
			select 
				a.*,isnull(b.NamaModule,'') NamaModule,isnull(c.NamaMenu ,'') NamaMenu,
				c.Action,c.Controller,c.Status,c.Img,c.Platform,
				case when a.IdParent != 0 then 
					(select NamaMenu from DataMenu where idMenu in (select idMenu from Role_MenuTree where IdRole = a.IdParent))
					else 
						''
					end as MainMenu
				from Role_MenuTree a
				left join DataModule b on b.IdModul = a.IdModule
				left join DataMenu c on c.idMenu = a.IdMenu
			where UPPER(isnull(c.NamaMenu,'')) like '%'+UPPER(@NamaMenu)+'%'
			and UPPER(isnull(c.Platform,'')) like '%'+UPPER(@Platform)+'%'
			and UPPER(isnull(b.NamaModule,'')) like '%'+UPPER(@NamaModule)+'%'
		) q	
		where UPPER(isnull(q.MainMenu,'')) like '%'+UPPER(@MainMenu)+'%'
END