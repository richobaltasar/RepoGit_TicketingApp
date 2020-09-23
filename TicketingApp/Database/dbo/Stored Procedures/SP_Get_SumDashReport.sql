--exec SP_Get_SumDashReport '14/09/2020','14/09/2020'

CREATE PROCEDURE SP_Get_SumDashReport
	@awal nvarchar(max),
	@akhir nvarchar(max)
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
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-','')
		between 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@awal,'/','-'), 105), 23),'-','') 
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@akhir,'/','-'), 105), 23),'-','')
		group by a.Category,left(b.Datetime,10)
	) W
	group by w.Category
END

--select*from LogTransaksiPOS where 
--replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
--between 
--replace(CONVERT(VARCHAR(10), CONVERT(date, replace('14/09/2020','/','-'), 105), 23),'-','') 
--and replace(CONVERT(VARCHAR(10), CONVERT(date, replace('14/09/2020','/','-'), 105), 23),'-','')
--and status = 1

--select*from LogTransaksiListDetailPOS 
--where IdTrx in (select idTrx from LogTransaksiPOS where 
--replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
--between 
--replace(CONVERT(VARCHAR(10), CONVERT(date, replace('14/09/2020','/','-'), 105), 23),'-','') 
--and replace(CONVERT(VARCHAR(10), CONVERT(date, replace('14/09/2020','/','-'), 105), 23),'-','')
--and status = 1)

