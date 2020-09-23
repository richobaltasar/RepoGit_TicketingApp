CREATE PROCEDURE SP_Get_LogPendapatan
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
    --Ticketing
	select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,UPPER(NamaItem) NamaItem,Harga,Qtx,Total,left(Datetime,10) Tanggal from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'TICKETING'
            and 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
            between 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
            and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
    union all   
    select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,'TRANSAKSI PARKIR' NamaItem,0 Harga,Qtx,Total,left(Datetime,10) Tanggal from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'PARKIR'
            and 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
            between 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
            and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
    union all   
    select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,'TRANSAKSI F&B' NamaItem,0 Harga,Qtx,Total,left(Datetime,10) Tanggal from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'FOODCOURT'
            and 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
            between 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
            and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem



    union all   
    select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,'TRANSAKSI VOUCHER' NamaItem,0 Harga,Qtx,Total,left(Datetime,10) Tanggal from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'VOUCHER'
            and 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
            between 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
            and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
END