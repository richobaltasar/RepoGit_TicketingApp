
CREATE PROCEDURE SP_Get_SalesQuotationbyIdRAB
	@IdRAB bigint
AS
BEGIN
	SET NOCOUNT ON;
	if exists(select*from DataQuotationVendor where IdRab = @IdRAB)
	begin
		select*from [dbo].[DataQuotationVendor] where IdRab =  @IdRAB
	end
END
