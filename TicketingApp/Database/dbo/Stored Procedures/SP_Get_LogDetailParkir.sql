CREATE PROCEDURE SP_Get_LogDetailParkir
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;
/*
    Category
	NamaItem
	Harga
	Qty
	Total
*/
	select Category,NamaItem NamaItem,Harga Harga, sum(isnull(Qtx,0)) Qty,sum(isnull(Total,0)) Total  from
	(
		select
			a.Category,a.Harga,a.Qtx,a.Total, b.TypeKendaraan NamaItem,c.Status
		from LogTransaksiListDetailPOS a
		left join LogParkir b on b.BarcodeReciptCode = a.Id
		left join LogTransaksiPOS c on c.idTrx = a.IdTrx
		where 
		Category='PARKIR'
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(c.Datetime,10),'/','-'), 105), 23),'-','')
		between 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
		and c.Status = 1
	) q
	group by Category,NamaItem,Harga
END



