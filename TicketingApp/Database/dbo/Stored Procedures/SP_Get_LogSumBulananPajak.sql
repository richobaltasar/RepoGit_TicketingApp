CREATE PROCEDURE SP_Get_LogSumBulananPajak
	@Month nvarchar(Max)
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
	declare @persenTiket float
	declare @persenParkir float
	declare @persenFNB float

	set @persenTiket = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='Ticket')
	set @persenParkir = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='Parkir')
	set @persenFNB = (select Persentase from DataSettingReportforExternal where NamaLaporan='Setting Laporan Pajak' and Category='F&B')

	select
	Category,
	NamaItem,Harga,Round(Qty*@persenTiket/100,0) Qty, 
	case when NamaItem like '%DISKON%' then -(Harga*Round(Qty*@persenTiket/100,0)) else (Harga*Round(Qty*@persenTiket/100,0)) end as Total
	from
	(
		select	
		q.Category,
		q.NamaItem NamaItem,
		q.Harga Harga,
		sum(q.Qtx) Qty,
		sum(q.Total) Total
		from
		(
			select Category,UPPER(NamaItem) NamaItem,Harga,Qtx,Total, 
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Tanggal
			from LogTransaksiListDetailPOS a
			left join LogTransaksiPOS b on b.idTrx = a.IdTrx
			where 
			a.Category = 'TICKETING'
			and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
			and b.Status=1
		) q
		group BY q.Category,q.Harga,q.NamaItem
	) w

	union all   
	
    select
	Category,NamaItem,Harga,Qty, (Round(Total*@persenParkir/100,0)) Total
	from
	(
	select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,'TRANSAKSI PARKIR' NamaItem,0 Harga,Qtx,Total,
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Tanggal
			from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'PARKIR'
            and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
	) w
    union all   
	select
	Category,NamaItem,Harga,Qty, (Round(Total*@persenFNB/100,0)) Total
	from
	(
    select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,'TRANSAKSI F&B' NamaItem,0 Harga,Qtx,Total,
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Tanggal
			from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'FOODCOURT'
            and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
	) w
    union all   
    select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,'TRANSAKSI VOUCHER' NamaItem,0 Harga,Qtx,Total,
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Tanggal
			from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'VOUCHER'
            and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) in (@SMonth)
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
END