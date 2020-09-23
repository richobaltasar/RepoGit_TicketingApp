
CREATE PROCEDURE SP_Get_LogTransaksiTiketWahana_Filter
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime nvarchar(max),
	@ChasierName nvarchar(max),
	@PaymentMethod nvarchar(max)
	
AS
BEGIN
	SET NOCOUNT ON;
	
	select
	a.*,b.Datetime,b.MerchantName,b.ChasierName,b.PaymentMethod
	from LogTransaksiListDetailPOS a
	left join LogTransaksiPOS b on b.idTrx = a.IdTrx
	where 
	a.IdTrx in (select distinct idTrx from LogTransaksiPOS where Status = 1)
	and Category in ('TICKETING','VOUCHER')
	and
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
	between 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
	and 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
	and Status = 1
	and b.PaymentMethod like '%'+ @PaymentMethod + '%'
	and b.ChasierName like '%'+ @ChasierName + '%'
END