-- exec SP_Get_SumDashReportBulanan @bulan='09/2020'
CREATE PROCEDURE SP_Get_SumDashReportBulanan
	@bulan nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	set @bulan = '01/'+@bulan

	select
	Category,sum(Total) Total
	from 
	(
		select
			a.Category,sum(Total) Total, left(b.Datetime,10) Tanggal
		from LogTransaksiListDetailPOS a
		left join LogTransaksiPOS b on b.idTrx = a.IdTrx
		where 
		b.Status=1
		and Category in ('TICKETING','PARKIR','FOODCOURT')
		and 
		left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),6)
		= left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@bulan,10),'/','-'), 105), 23),'-',''),6)
		group by a.Category,left(b.Datetime,10)
	) W
	group by w.Category
END