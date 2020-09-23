
CREATE PROCEDURE SP_DelModuleMaster
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	delete from DataModule where IdModul = @Id
	select 'success' title,'Delete data berhasil' message,'success' status
END
