CREATE PROCEDURE SP_getDataTransaksiTopupCancel
	@IdTrx bigint
AS
BEGIN
	SET NOCOUNT ON;
	select IdTopup Id,
	JenisPayment TipeTransaksi, TotalBayar TotalTransaksi,
	PaymentMethod,AccountNumber
	from LogTopupDetail
	where IdTopup = @IdTrx --10463

	
END
