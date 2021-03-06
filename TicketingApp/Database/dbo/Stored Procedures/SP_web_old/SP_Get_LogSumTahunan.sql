﻿-- exec SP_Get_LogSumTahunan @Tahun='2020'

CREATE PROCEDURE SP_Get_LogSumTahunan
	@Tahun nvarchar(Max)
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
			select Category,UPPER(NamaItem) NamaItem,Harga,Qtx,Total, 
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Bulan
			from LogTransaksiListDetailPOS a
			left join LogTransaksiPOS b on b.idTrx = a.IdTrx
			where 
			a.Category = 'TICKETING'
			and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),4) in (@Tahun)
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
            select Category,'TRANSAKSI PARKIR' NamaItem,0 Harga,Qtx,Total,
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Bulan
			from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'PARKIR'
            and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),4) in (@Tahun)
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
            select Category,'TRANSAKSI F&B' NamaItem,0 Harga,Qtx,Total,
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Bulan
			from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'FOODCOURT'
            and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),4) in (@Tahun)
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
            select Category,'TRANSAKSI VOUCHER' NamaItem,0 Harga,Qtx,Total,
			left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),6) Bulan
			from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
            where a.Category = 'VOUCHER'
            and left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),4) in (@Tahun)
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
END