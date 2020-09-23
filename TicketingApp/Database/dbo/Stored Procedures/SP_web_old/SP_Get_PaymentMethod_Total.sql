CREATE PROCEDURE SP_Get_PaymentMethod_Total
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;
    
    select
    sum(EMONEY) EMONEY,
    sum(EDC) EDC,
    sum(CASH) CASH
    FROM
    (
        select
        case when Emoney!= 0 then isnull(sum(isnull(Emoney,0)),0) else 0 end as EMONEY,
        case when Tunai!= 0 then isnull(sum(isnull(Tunai,0)),0) else 0 end as CASH,
        case when BankName!= '' or BankName!= NULL then isnull(sum(isnull(TotalBayar,0)),0) else 0 end as EDC
        from LogTransaksiPOS
        where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
        between 
        replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
        and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
        and [Status] = 1
        group by Emoney,Tunai,BankName    
    ) q
    
END