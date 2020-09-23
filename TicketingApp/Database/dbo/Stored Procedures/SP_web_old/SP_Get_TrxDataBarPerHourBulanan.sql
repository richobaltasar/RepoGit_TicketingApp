CREATE PROCEDURE SP_Get_TrxDataBarPerHourBulanan
	@Bulan nvarchar(max),
	@Category nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	set @Bulan = '01/'+@Bulan

	select
	distinct
	left(Datetime,10) Date
	into #tempDatet
	from LogTransaksiPOS
	where status=1 
	and 
	left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),6)
	= left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Bulan,'/','-'), 105), 23),'-',''),6)

	select
	w.Category,
	sum(total) Total,
	Tanggal Tanggal
	into #TempaData
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
		= left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Bulan,'/','-'), 105), 23),'-',''),6)
		group by a.Category,left(b.Datetime,10)
	) W
	where W.Category = @Category
	group by w.Category,Tanggal

	create table #TempChart
	(
		Value Float,
		Time Nvarchar(max),
		BackgroundColor Nvarchar(max),
		BorderColor Nvarchar(max)
	)

	while exists(select*from #tempDatet)
	begin
		declare @Time nvarchar(max)
		declare @BgColor nvarchar(max)
		declare @BoColor nvarchar(max)

		select top 1 @Time=Date from #tempDatet order by Date asc
		if exists(select*from #TempaData where Tanggal = @Time)
		begin
			declare @val float
			select @val = sum(isnull(Total,0)) from #TempaData where Tanggal = @Time
		end
		else
		begin
			set @val = 0;
		end

		select distinct @BgColor=BackgroundColor,@BoColor=BorderColor from DataColor where NamaFunction ='Bar Chart Per Hours' and Label=@Time

		insert into #TempChart
		(Time,Value,BackgroundColor,BorderColor)
		values(@Time,@val,@BgColor,@BoColor)
		delete from #tempDatet where  Date = @Time
	end

	select
	Value,Left(Time,2) Time,BackgroundColor,BorderColor
	from #TempChart

	drop table #TempChart
	drop table #tempDatet
	drop table #TempaData

END
