
CREATE PROCEDURE SP_Get_SalesQuotationbyIdQuot
	@IdQuotation bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataQuotationVendor where IdQuotation = @IdQuotation
END
