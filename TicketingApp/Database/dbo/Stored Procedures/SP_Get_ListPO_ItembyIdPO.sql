
CREATE PROCEDURE SP_Get_ListPO_ItembyIdPO
	@IdPO BIGINT
AS
BEGIN
	SET NOCOUNT ON;

    select*from DataPO_Item where IdPO = @IdPO
END
