
CREATE PROCEDURE SP_Get_POByIdPO
	@IdPO bigint
AS
BEGIN
	SET NOCOUNT ON;
	
	select*from DataPO where IdPO = @IdPO

END
