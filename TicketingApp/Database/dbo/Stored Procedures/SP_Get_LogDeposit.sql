CREATE PROCEDURE SP_Get_LogDeposit
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
select
    q.Category,
    q.NamaItem NamaItem,
    q.Harga Harga,
    sum(q.Qtx) Qty,
    sum(q.Total) Total
    from
    (
        select Category,'TRANSAKSI NEW MEMBER' NamaItem,'-' Harga,Qtx,Total,left(Datetime,10) Tanggal from LogTransaksiListDetailPOS a
        left join LogTransaksiPOS b on b.idTrx = a.IdTrx
        where a.Category = 'CARD'
        and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
        between 
        replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
        and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
		and b.Status = 1
    ) q
    group BY q.Category,q.Harga,q.NamaItem

UNION ALL
select
    q.Category,
    q.NamaItem NamaItem,
    q.Harga Harga,
    sum(q.Qtx) Qty,
    sum(q.Total) Total
    from
    (
        select Category,'TRANSAKSI TOPUP' NamaItem,'-' Harga,Qtx,Total,left(Datetime,10) Tanggal from LogTransaksiListDetailPOS a
        left join LogTransaksiPOS b on b.idTrx = a.IdTrx
        where a.Category = 'TOPUP'
        and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
        between 
        replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
        and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
		and b.Status = 1
    ) q
    group BY q.Category,q.Harga,q.NamaItem
UNION ALL
select
 'EMONEY' Category, 'TRANSAKSI MENGGUNAKAN EMONEY ' NamaItem,
    '-' Harga,
    COUNT(idTrx) Qty,
    sum(isnull(Emoney,0)) Total
    from LogTransaksiPOS
    where Emoney !=0
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
    between 
    replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
	and Status = 1
UNION ALL
select  
    'REFUND' Category, 'TRANSAKSI REFUND' NamaItem,
    '-' Harga,
    COUNT(AccountNumber) Qty,
    -(sum(isnull(SaldoEmoney,0)+ISNULL(SaldoJaminan,0))) Total
from LogRefundDetail
where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
between 
replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
UNION ALL
select
    'SALDO' Category,
    'DEPOSIT SALDO EMONEY & JAMINAN GELANG' NamaItem,
    '-' Harga,
    count(AccountNumber) Qty,
    sum(isnull(Balanced,0)) + sum(isnull(UangJaminan,0)) Total
    from DataAccount where [Status] = 1


END