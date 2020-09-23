
CREATE PROCEDURE SP_Get_ListPObyIdQuot
	@IdQuotation bigint
AS
BEGIN
	SET NOCOUNT ON;
	
	select*from DataPO where IdQuotation = @IdQuotation

END
