CREATE PROCEDURE SP_Get_KolomLog_bulanan
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

	--Ticketing
	select
	*
	from
	(
		select
			case when q.NamaItem like '%ASURANSI%' then 'ASURANSI'
				 when q.NamaItem like '%DISKON%'then 'DISKON'
				 when q.NamaItem like '%NAMA TICKET%'then 'TICKET'
				 when q.NamaItem like '%MOTOR%' then 'MOTOR'
				 when q.NamaItem like '%MOBIL%' then 'MOBIL'
				 else q.NamaItem
			end as NamaItem,
			q.Category
			from
			(
				select UPPER(NamaItem) NamaItem,Category
				from LogTransaksiListDetailPOS a
				left join LogTransaksiPOS b on b.idTrx = a.IdTrx
				where 
				a.Category not in ('TOPUP','FOODCOURT')
				and
				left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
				and b.Status=1
			) q
			group BY q.NamaItem,q.Category

			union all

			select
			p.NamaItem,
			Category
			from
			(
				select
				case when StatusKepemilikan = 'Management' then 'MANAGEMENT' else 'NON MANAGEMENT' end as NamaItem,
				Category
				from
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
			group by p.NamaItem,Category
	) w

END