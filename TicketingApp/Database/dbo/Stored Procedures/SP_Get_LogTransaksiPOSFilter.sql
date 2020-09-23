
--exec SP_Get_LogTransaksiPOSFilter @Awal_Datetime='30/08/2020',@Akhir_Datetime='30/08/2020',@PaymentMethod='',@AccountNumber=''

CREATE PROCEDURE SP_Get_LogTransaksiPOSFilter
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime nvarchar(max),
	@PaymentMethod nvarchar(max),
	@AccountNumber nvarchar(max)
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
	and AccountNumber like '%'+@AccountNumber+'%'
	and PaymentMethod like '%'+@PaymentMethod+'%'
	and Status = 1
END
