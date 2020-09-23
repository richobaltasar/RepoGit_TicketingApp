
CREATE PROCEDURE SP_GetDataLogTransaksiPOS
	@IdTrx bigint
AS
BEGIN
	SET NOCOUNT ON;
	select
	idTrx,
	Datetime,
	MerchantName,
	ChasierName,
	PaymentMethod,
	TotalTransaksi,
	Emoney,
	AccountNumber,
	TotalBayar,
	Tunai,
	Kembalian,
	BankName,
	CardNumber,
	Noreff,
	case when Status = 1 then 'Success' else 'Fail' end as Status
	from LogTransaksiPOS 
	where idTrx = @IdTrx
END
