 
--exec SP_Get_LogHistoryDanaModal 110352

CREATE PROCEDURE SP_Get_LogHistoryDanaModal
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;

    select*from LogCashierTambahModal where IdMaster = @Id
END
