CREATE PROCEDURE SP_DelRoleMenuMaster
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	delete DataMenu
	where idMenu in
	(select IdMenu from Role_MenuTree where IdRole = @Id)
	
	delete from Role_MenuTree where IdRole = @Id
	
	select 'success' title,'Delete data berhasil' message,'success' status
END