CREATE PROCEDURE SP_GetRoleMenuMaster
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select 
		a.*,isnull(b.NamaModule,'') NamaModule,isnull(c.NamaMenu ,'') NamaMenu,
		c.Action,c.Controller,c.Status,c.Img,c.Platform
		from Role_MenuTree a
		left join DataModule b on b.IdModul = a.IdModule
		left join DataMenu c on c.idMenu = a.IdMenu
		where UPPER(isnull(b.NamaModule,'') +' '+isnull(c.NamaMenu ,'')) like '%'+UPPER(@search)+'%'
	end
	else if(@Id != 0 and @search = '')
	begin
		select 
		a.*,isnull(b.NamaModule,'') NamaModule,isnull(c.NamaMenu ,'') NamaMenu,
		c.Action,c.Controller,c.Status,c.Img,c.Platform
		from Role_MenuTree a
		left join DataModule b on b.IdModul = a.IdModule
		left join DataMenu c on c.idMenu = a.IdMenu
		where IdRole = @Id
	end

END