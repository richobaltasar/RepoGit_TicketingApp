CREATE PROCEDURE SP_Get_TrxDataPieTahunan
	@Tahun nvarchar(max),
	@Category nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	if(@Category != 'FOODCOURT')
	begin
	select
	Q.Label,sum(Q.Value) Value into #TempData
	from
	(
		select sum(Total) Value,
		case 
			when NamaItem like '%Diskon%' then 'DISKON'
			when NamaItem like '%Nama Ticket%' then 'TIKET'
			when NamaItem like '%Asuransi%' then 'ASURANSI'
			when NamaItem like '%Motor%' then 'MOTOR'
			when NamaItem like '%Mobil%' then 'MOBIL'
			end as Label
		from LogTransaksiListDetailPOS a
		left join LogTransaksiPOS b on b.idTrx = a.IdTrx
		where b.Status =1 
		and Category = @Category
		and 
		left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),4) = @Tahun
		group by a.NamaItem
	) Q
	group by Q.Label

	create table #TempPie
	(
		Value Float,
		Label Nvarchar(max),
		Color Nvarchar(max),
	)

	while exists(select*from #TempData)
	begin
		declare @Label nvarchar(max)
		declare @Value nvarchar(max)
		declare @Color nvarchar(max)
		
		select top 1 @Label=Label,@Value=Value from #TempData order by Label asc
		
		select distinct @Color=BackgroundColor from DataColor where NamaFunction = 'Pie Chart Ticketing' and Label = @Label

		insert into #TempPie
		(Label,Value,Color)
		values(@Label,@Value,@Color)
		delete from #TempData where  Label = @Label
	end

	select*from #TempPie

	drop table #TempPie
	drop table #TempData
	end
	else
	begin
		select q.NamaTenant Label,sum(Total) Value, '' Color  from
		(
			select
				a.*, b.Datetime,c.NamaTenant
			from LogTransaksiListDetailPOS a
			left join LogTransaksiPOS b on b.idTrx = a.IdTrx
			left join DataTenant c on c.idTenant = (SELECT top 1 value  FROM STRING_SPLIT(a.Id, '-'))
			where Category = 'FOODCOURT'
			and b.Status=1
			and 
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),4)= @Tahun
			
		)q
		group by q.NamaTenant
	end
END
