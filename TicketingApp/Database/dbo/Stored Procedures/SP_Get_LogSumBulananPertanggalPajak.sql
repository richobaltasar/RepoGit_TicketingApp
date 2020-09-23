CREATE PROCEDURE SP_Get_LogSumBulananPertanggalPajak
	@Month nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    value  into #temp
	FROM 
		STRING_SPLIT(@Month, '/')
	order by value desc
	
	declare @SMonth nvarchar(max)
	set @SMonth = ''
	while exists(select *from #temp)
	begin
		declare @S nvarchar(max)
		select top 1 @S=value from #temp order by value desc
		set @SMonth = @SMonth + @S
		
		delete #temp where value = @S
	end
	drop table #temp
	print @SMonth
	declare @persenTiket float
	declare @persenParkir float
	declare @persenFNB float

	set @persenTiket = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='Ticket')
	set @persenParkir = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='Parkir')
	set @persenFNB = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='F&B')

	--Ticketing
	select
	w.Tanggal,
	w.Category,
	w.NamaItem,
	w.Qty,
	w.Total
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
				select Category,UPPER(NamaItem) NamaItem,Harga,Qtx,Total, 
				left(b.Datetime,10) Tanggal
				from LogTransaksiListDetailPOS a
				left join LogTransaksiPOS b on b.idTrx = a.IdTrx
				where 
				a.Category not in ('TOPUP','FOODCOURT')
				and
				left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
				and b.Status=1
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
				case when StatusKepemilikan = 'Management' then 'MANAGEMENT' else 'NON MANAGEMENT' end as NamaItem, sum(isnull(Qtx,0)) Qty,sum(isnull(Total,0)) Total  from
				(
					select
						a.*,b.StatusKepemilikan,
						left(c.Datetime,10) Tanggal
					from LogTransaksiListDetailPOS a
					left join DataTenant b on b.idTenant = (SELECT top 1 value FROM STRING_SPLIT(a.Id, '-'))
					left join LogTransaksiPOS c on c.idTrx = a.IdTrx
					where a.Category = 'FOODCOURT'
					and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(c.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
					and c.Status = 1
				) q
				group by Category,StatusKepemilikan,Tanggal
			) p
			group by p.Tanggal,p.Category,p.NamaItem
	) w
END
