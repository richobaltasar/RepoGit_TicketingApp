--SP_Get_TrxDataBulanan '09/2020','TICKETING'

CREATE PROCEDURE SP_Get_TrxDataBulanan
	@Bulan nvarchar(max),
	@Category nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	set @Bulan = '01/'+@Bulan

	select distinct left(Datetime,10) Tgl into #TempDate from LogTransaksiPOS
	where status = 1
	and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),6) =  left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Bulan,'/','-'), 105), 23),'-',''),6)

	select
	sum(q.Total) Value,Q.Tanggal Time into #TempData
	from 
	(
	select a.*, left(b.Datetime,10) Tanggal 
	from LogTransaksiListDetailPOS a
	left join LogTransaksiPOS b on b.idTrx = a.IdTrx
	where b.Status = 1
	and Category = @Category
	and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-',''),6) =  left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Bulan,'/','-'), 105), 23),'-',''),6)
	) Q
	group by q.Tanggal

	create table #TempChart
	(
		Value Float,
		Time Nvarchar(max)
	)

	while exists(select*from #TempDate)
	begin
		declare @date nvarchar(max)
		select top 1 @date=Tgl from #TempDate order by Tgl asc
		if exists(select*from #TempData where Time = @date)
		begin
			declare @val float
			select @val = sum(isnull(Value,0)) from #TempData where Time = @date
		end
		else
		begin	
			set @val = 0;
		end

		insert into #TempChart
		(Time,Value)
		values(@date,@val)
		delete from #TempDate where  Tgl = @date
	end

	select*from #TempChart

	drop table #TempChart
	drop table #TempData
	drop table #TempDate
END