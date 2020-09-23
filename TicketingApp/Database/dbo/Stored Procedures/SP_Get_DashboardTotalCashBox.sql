
CREATE PROCEDURE SP_Get_DashboardTotalCashBox
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select
	sum(DanaModalSetelah) DanaModal,
	sum(TotalUangMasuk) TotalUangMasuk,
	sum(TotalUangKeluar) TotalUangKeluar,
	sum(TotalUangDiBox) TotalUangDiBox
	from DataChasierBox
	where 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
	between 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
	and 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
END
