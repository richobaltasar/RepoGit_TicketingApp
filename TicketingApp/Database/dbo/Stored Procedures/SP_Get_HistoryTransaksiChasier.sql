CREATE PROCEDURE SP_Get_HistoryTransaksiChasier
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime nvarchar(max),
	@ChasierName  nvarchar(max),
	@MerchantName  nvarchar(max),
	@PaymentMethod  nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	select
	*
	from LogTransaksiPOS
	where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
	between 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
	and 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
	and ChasierName like '%'+@ChasierName+'%'
	and MerchantName like '%'+@MerchantName+'%'
	and PaymentMethod like '%'+@PaymentMethod+'%'
	and Status = 1
END
