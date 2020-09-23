
CREATE PROCEDURE SP_Get_DashboardCashierStatus
AS
BEGIN
	SET NOCOUNT ON;

	select
	a.DanaModalSetelah DanaModal,a.TotalUangDiBox TotalCash,a.OpenBy NamaKasir,b.Photo ImgLink,
	case when a.Status = 1 then 'OPEN' else 'CLOSED' end As Status
	from DataChasierBox a
	left join UserData b on b.NamaLengkap = a.OpenBy
	where 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') 
	--= '20200914'
	= replace(CONVERT(VARCHAR(10), CONVERT(date, GETDATE(), 105), 23),'-','')

END
