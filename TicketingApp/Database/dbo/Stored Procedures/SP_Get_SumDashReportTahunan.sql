CREATE PROCEDURE SP_Get_SumDashReportTahunan
	@Tahun nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
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
		left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),4)= @Tahun
		group by a.Category,left(b.Datetime,10)
	) W
	group by w.Category
END
