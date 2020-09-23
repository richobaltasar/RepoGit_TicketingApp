--sp_helptext SP_Get_PaymentMethod_TotalEmoney
CREATE PROCEDURE SP_Get_PaymentMethod_TotalTransaksi
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;

    select
        isnull(sum(isnull(ABS(TotalTransaksi),0)),0) Total
    from LogTransaksiPOS
    where TotalTransaksi != 0
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
    between 
    replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
    and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
    and [Status] = 1
END