CREATE PROCEDURE SP_GetLogTransaksiPos
	@search nvarchar(max),
	@MerchantName nvarchar(max),
	@user_name nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select
		a.idTrx, a.Datetime,a.AccountNumber,a.TotalTransaksi,a.PaymentMethod,a.ChasierName,
		case when status = 1 then 'Success' else 'Fail' end Status
	from LogTransaksiPOS a
	where MerchantName = @MerchantName and ChasierName = @user_name and AccountNumber like '%'+@search+'%'
	and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = replace(CONVERT(VARCHAR(10), CONVERT(date, replace(FORMAT(GETDATE() , 'dd/MM/yyyy'),'/','-'), 105), 23),'-','')
END