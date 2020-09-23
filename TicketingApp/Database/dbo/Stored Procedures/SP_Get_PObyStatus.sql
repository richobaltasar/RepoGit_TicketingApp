
CREATE PROCEDURE SP_Get_PObyStatus
	@StatusPO bigint,
	@IdUser bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	select*from DataPO where StatusPO = @StatusPO

END
