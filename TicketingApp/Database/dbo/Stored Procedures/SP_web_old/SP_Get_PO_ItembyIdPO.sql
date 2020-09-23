
CREATE PROCEDURE SP_Get_PO_ItembyIdPO
	@IdItem bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataPO_Item where Id = @IdItem
   
END
