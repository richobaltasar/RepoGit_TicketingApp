
CREATE PROCEDURE SP_GetRekapanTenantHarian
	@setTanggal nvarchar(max),
	@NamaTenant nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	--declare @setTanggal nvarchar(max)
	--set @setTanggal = '18/01/2020'
	select NamaItem Uraian, Harga HargaSatuan,ISNULL(Qtx,0) Qty,ISNULL(Total,0) Jumlah from
	(
		select KodeBarang,NamaItem,Harga,sum(Qtx) Qtx, sum(isnull(Total,0)) Total from [LogItemsF&BTrx] a
		where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(a.Datetime,10),'/','-'), 105), 23),'-','') 
		= replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@setTanggal,'/','-'), 105), 23),'-','') 
		and NamaTenant = @NamaTenant
		group by KodeBarang,NamaItem,Harga
	) q

END
