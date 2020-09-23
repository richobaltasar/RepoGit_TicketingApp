CREATE PROCEDURE SP_Get_DataLog_tahunan
	@Bulan nvarchar(max),
	@NamaItem nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	set @Bulan = '01/'+@Bulan
	--Ticketing
	select
	sum(Qty) Qty, sum(Total) Total
	from
	(
			select
			q.Bulan,
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
				right(left(b.Datetime,10),7) Bulan
				from LogTransaksiListDetailPOS a
				left join LogTransaksiPOS b on b.idTrx = a.IdTrx
				where 
				left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) = --'202008'
				left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Bulan,10),'/','-'), 105), 23),'-',''),6)
				and b.Status=1
				and a.Category not in ('FOODCOURT','TOPUP')
			) q
			group BY q.Category,q.Harga,q.NamaItem,q.Bulan

			union all

			select
			p.Bulan,
			p.Category,
			p.NamaItem,
			sum(p.Qty) Qty,
			sum(p.Total) Total
			from
			(
				select Bulan,Category,
				case when StatusKepemilikan = 'Management' then 'MANAGEMENT' else 'NON MANAGEMENT' end as NamaItem, sum(isnull(Qtx,0)) Qty,sum(isnull(Total,0)) Total  from
				(
					select
						a.*,b.StatusKepemilikan,
						right(left(c.Datetime,10),7) Bulan
					from LogTransaksiListDetailPOS a
					left join DataTenant b on b.idTenant = (SELECT top 1 value FROM STRING_SPLIT(a.Id, '-'))
					left join LogTransaksiPOS c on c.idTrx = a.IdTrx
					where 
					left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(c.Datetime,10),'/','-'), 105), 23),'-',''),6) = --'202008'
				    left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Bulan,10),'/','-'), 105), 23),'-',''),6)
					and c.Status = 1
					and a.Category = 'FOODCOURT'
				) q
				group by Category,StatusKepemilikan,Bulan
			) p
			group by p.Bulan,p.Category,p.NamaItem
	) w
	where w.NamaItem = @NamaItem

END