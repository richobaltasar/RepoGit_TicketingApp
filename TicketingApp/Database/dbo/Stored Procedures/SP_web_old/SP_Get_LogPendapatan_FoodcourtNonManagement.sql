CREATE PROCEDURE SP_Get_LogPendapatan_FoodcourtNonManagement
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	   select NamaTenant Category,NamaItem NamaItem,Harga Harga, sum(isnull(Qtx,0)) Qty,sum(isnull(Total,0)) Total  from
		(
			select
				a.*,b.NamaTenant
			from LogTransaksiListDetailPOS a
			left join DataTenant b on b.idTenant = (SELECT top 1 value FROM STRING_SPLIT(a.Id, '-'))
			left join LogTransaksiPOS c on c.idTrx = a.IdTrx
			where a.Category = 'FOODCOURT'
			and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(c.Datetime,10),'/','-'), 105), 23),'-','')
			between 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
			and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
			and c.Status = 1
			and b.StatusKepemilikan = 'NonManagement'
		) q
		group by NamaTenant,NamaItem,Harga
		order by Category
END