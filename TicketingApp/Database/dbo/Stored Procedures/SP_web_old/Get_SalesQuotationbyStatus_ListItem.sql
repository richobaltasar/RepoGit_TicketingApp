
CREATE PROCEDURE Get_SalesQuotationbyStatus_ListItem
	@Id bigint,
	@IdQuotation bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		if(@IdQuotation != 0)
		begin 
			select*from DataQuotationVendor_Item where IdQuotation = @IdQuotation
		end
	end 
	else
	begin
		select*from DataQuotationVendor_Item where IdQuotation = @IdQuotation and Id = @Id
	end
END