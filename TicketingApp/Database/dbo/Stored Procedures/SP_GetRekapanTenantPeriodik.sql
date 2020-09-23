
CREATE PROCEDURE SP_GetRekapanTenantPeriodik
	@SetAwal nvarchar(max),
	@SetAkhir nvarchar(max),
	@NamaTenant nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	--declare @setTanggal nvarchar(max)
	--set @setTanggal = '18/01/2020'
	select NamaItem Uraian, Harga HargaSatuan,ISNULL(Qtx,0) Qty,ISNULL(Total,0) Jumlah from
	(
		select KodeBarang,NamaItem,a.Harga,sum(Qtx) Qtx, sum(isnull(Total,0)) Total from [LogItemsF&BTrx] a
		left join DataBarang b on b.NamaMenu = a.NamaItem
		where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(a.Datetime,10),'/','-'), 105), 23),'-','') 
		between replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@SetAwal,'/','-'), 105), 23),'-','')  and 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@SetAkhir,'/','-'), 105), 23),'-','')
		and NamaTenant = @NamaTenant
		group by KodeBarang,NamaItem,a.Harga
	) q
	order by q.Qtx desc
END
