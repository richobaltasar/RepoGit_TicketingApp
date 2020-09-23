
CREATE PROCEDURE SP_Get_LogPenggunaanVoucher
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select
	a.*,b.Datetime,b.MerchantName,b.ChasierName,b.PaymentMethod
	from LogTransaksiListDetailPOS a
	left join LogTransaksiPOS b on b.idTrx = a.IdTrx
	where a.IdTrx in (select distinct idTrx from LogTransaksiPOS where Status = 1)
	and Category in ('VOUCHER')
END
