
CREATE PROCEDURE SP_Get_PaymentMethod_TotalEDC
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;
	select
        isnull(sum(isnull(TotalBayar,0)),0) Total
    from LogTransaksiPOS
    where 
     Tunai = 0 and CardNumber != ''
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
    between 
    replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
    and [Status] = 1
END