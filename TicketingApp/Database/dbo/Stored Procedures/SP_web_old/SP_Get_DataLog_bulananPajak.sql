CREATE PROCEDURE SP_Get_DataLog_bulananPajak
	@Tanggal nvarchar(max),
	@NamaItem nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	declare @persenTiket float
	declare @persenParkir float
	declare @persenFNB float

	set @persenTiket = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='Ticket')
	set @persenParkir = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='Parkir')
	set @persenFNB = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='F&B')
	
	select
	ee.Qty,
	case when NamaItem ='DISKON' then -ee.Total
	else ee.Total end as Total
	from 
	(
		select
		sum(Qty) Qty, sum(Total) Total,
		NamaItem
		from
		(
				select
				q.Tanggal,
				q.Category,
				case when q.NamaItem like '%ASURANSI%' then 'ASURANSI'
						when q.NamaItem like '%DISKON%'then 'DISKON'
						when q.NamaItem like '%NAMA TICKET%'then 'TICKET'
						when q.NamaItem like '%MOTOR%' then 'MOTOR'
						when q.NamaItem like '%MOBIL%' then 'MOBIL'
						else q.NamaItem

				end as NamaItem,
				sum(q.Qtx) Qty,
				sum(q.Total) Total
				from
				(
				-- select*from LogTransaksiListDetailPOS
					select Category,UPPER(NamaItem) NamaItem,
					Harga,
					case when Category ='TICKETING' then Round((Qtx*@persenTiket/100),0) 
						when Category ='PARKIR' then Round((Qtx*@persenParkir/100),0) 
						else Qtx end as Qtx,
					case when Category ='TICKETING' then (Round((Qtx*@persenTiket/100),0) * Harga)
						when Category ='PARKIR' then Round((Total*(@persenParkir/100)),0)
						else Total end as Total, 
					left(b.Datetime,10) Tanggal
					from LogTransaksiListDetailPOS a
					left join LogTransaksiPOS b on b.idTrx = a.IdTrx
					where 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-','') = 
					--'20200831'
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
					and b.Status=1
					and a.Category not in ('FOODCOURT','TOPUP')
				) q
				group BY q.Category,q.Harga,q.NamaItem,q.Tanggal

				union all

				select
				p.Tanggal,
				p.Category,
				p.NamaItem,
				sum(p.Qty) Qty,
				sum(p.Total) Total
				from
				(
					select Tanggal,Category,
					case when StatusKepemilikan = 'Management' then 'MANAGEMENT' else 'NON MANAGEMENT' end as NamaItem, 
					ROUND((sum(isnull(Qtx,0))*@persenFNB/100),0) Qty,
					ROUND((sum(isnull(Total,0))*@persenFNB/100),0) Total  
					from
					(
						select
							a.*,b.StatusKepemilikan,
							left(c.Datetime,10) Tanggal
						from LogTransaksiListDetailPOS a
						left join DataTenant b on b.idTenant = (SELECT top 1 value FROM STRING_SPLIT(a.Id, '-'))
						left join LogTransaksiPOS c on c.idTrx = a.IdTrx
						where 
						replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(c.Datetime,10),'/','-'), 105), 23),'-','') = 
						--'20200831'
						replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
						and c.Status = 1
						and a.Category = 'FOODCOURT'
					) q
					group by Category,StatusKepemilikan,Tanggal
				) p
				group by p.Tanggal,p.Category,p.NamaItem
		) w
		where w.NamaItem = @NamaItem
		group by NamaItem
	) ee
END
