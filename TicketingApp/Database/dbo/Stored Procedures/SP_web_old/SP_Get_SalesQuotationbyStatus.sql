CREATE PROCEDURE SP_Get_SalesQuotationbyStatus
	@IdQuotation bigint,
	@IdUser bigint,
	@StatusQuotationVendor bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select Text,Value into #temp from erp.dbo.Master_ListItem where ListName like 'ListStatusQuotationVendor%' order by Urutan asc

	if(@IdQuotation = 0)
	begin
		select
		IdQuotation,
		IdRab,
		NumberQuotationByVendor,
		CategoryPerusahaan,
		CompanyName,
		AlamatPerusahaan,
		Contact,
		Sales,
		TotalPenawaran,
		Perihal,
		Description,
		CreateDate,
		a.StatusQuotation StatusQuotation,
		Attachment1
		from [dbo].[DataQuotationVendor] a
		where StatusQuotation = @StatusQuotationVendor
	end
    else
	begin
		select*from [dbo].[DataQuotationVendor] where IdQuotation = @IdQuotation
	end
END
