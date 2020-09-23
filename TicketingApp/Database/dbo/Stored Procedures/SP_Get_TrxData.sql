--SP_Get_TrxDataTicket '',''

CREATE PROCEDURE SP_Get_TrxData
	@awal nvarchar(max),
	@akhir nvarchar(max),
	@Category nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	select
	distinct
	right(left(Datetime,13),2)+':00' Date
	into #tempDatet
	from LogTransaksiPOS
	where status=1 
	and 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
	between 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@awal,'/','-'), 105), 23),'-','') 
	and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@akhir,'/','-'), 105), 23),'-','')

	select
	w.Category,
	sum(total) Total,
	right(Tanggal,2)+':00' Tanggal
	into #TempaData
	from 
	(
		select
			a.Category,sum(Total) Total, left(b.Datetime,13) Tanggal
		from LogTransaksiListDetailPOS a
		left join LogTransaksiPOS b on b.idTrx = a.IdTrx
		where 
		b.Status=1
		and Category in ('TICKETING','PARKIR','FOODCOURT')
		and 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
		between 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@awal,'/','-'), 105), 23),'-','') 
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@akhir,'/','-'), 105), 23),'-','')
		group by a.Category,left(b.Datetime,13)
	) W
	where W.Category = @Category
	group by w.Category,right(Tanggal,2)+':00'

	--select*from #tempDatet
	--select*from #TempaData

	create table #TempChart
	(
		Value Float,
		Time Nvarchar(max)
	)

	while exists(select*from #tempDatet)
	begin
		declare @Time nvarchar(max)
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
		insert into #TempChart
		(Time,Value)
		values(@Time,@val)
		delete from #tempDatet where  Date = @Time
	end

	select*from #TempChart

	drop table #TempChart
	drop table #tempDatet
	drop table #TempaData

END
