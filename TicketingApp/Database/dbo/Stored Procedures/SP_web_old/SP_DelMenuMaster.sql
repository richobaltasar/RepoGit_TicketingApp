CREATE PROCEDURE SP_DelMenuMaster
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	delete from DataMenu where idMenu = @Id
	select 'success' title,'Delete data berhasil' message,'success' status
END