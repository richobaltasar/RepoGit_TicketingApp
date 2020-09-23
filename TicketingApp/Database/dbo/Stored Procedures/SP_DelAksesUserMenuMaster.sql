
CREATE PROCEDURE SP_DelAksesUserMenuMaster
	@Id bigint,
	@userid bigint
AS
BEGIN
	SET NOCOUNT ON;
	delete from DataHakAkses where UserId = @userid and IdMenu = @Id

	select 'success' title,'Delete data berhasil' message,'success' status
END
