
CREATE PROCEDURE SP_Get_HistoryTrxChasier
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime  nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	select
	MerchantName TerminalName,
	ChasierName NamaKasir,
	Datetime,
	idTrx IdTransaksi,
	TotalTransaksi,
	PaymentMethod,
	AccountNumber,
	Emoney,
	case when Tunai > 0 then (Tunai + Kembalian) else 0 end as Cash,
	case when Tunai = 0 and BankName != '' then TotalBayar else 0 end as EDC
	from LogTransaksiPOS
	where Status=1

END
