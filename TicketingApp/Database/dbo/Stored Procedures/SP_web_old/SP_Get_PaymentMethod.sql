CREATE PROCEDURE SP_Get_PaymentMethod
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;
    select
    distinct 
    'PAYMENT METHOD' Category,
    PaymentMethod NamaItem,
    '-' Harga,
    count(idTrx) Qty,
    case 
        when PaymentMethod='CASH' then sum((isnull(Tunai,0) - isnull(ABS(Kembalian),0))) 
        when PaymentMethod='CASH+EMONEY' then sum(isnull(ABS(Emoney),0))+ sum(isnull(Tunai,0))
        when PaymentMethod='EDC' then sum(isnull(TotalBayar,0)) 
        when PaymentMethod='EDC+EMONEY' then sum(isnull(TotalBayar,0)) + sum(isnull(ABS(Emoney),0))
        when PaymentMethod='EMONEY' then sum(isnull(ABS(Emoney),0))
    end as Total
    from LogTransaksiPOS
    where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
    between 
    replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
	and Status=1
    group by PaymentMethod

END
