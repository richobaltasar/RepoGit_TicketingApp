
CREATE PROCEDURE SP_Get_LogHistoryTransaksiUangBox
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	declare @Datetime nvarchar(max)
	declare @NamaComputer nvarchar(max)
	declare @KasirName nvarchar(max)
	--select*from DataChasierBox
	
	select distinct
	@Datetime=Datetime,
	@NamaComputer=NamaComputer,
	@KasirName=(case when UpdateBy is null then OpenBy else UpdateBy end)
	from DataChasierBox where IdModal = @Id

	select
	idLog,
	Datetime,
	NamaComputer,
	NamaUser,
	NominalTambahModal Nominal,
	Status,
 	'DANA MODAL' as Category
	from LogCashierTambahModal where IdMaster = @Id
	union all
	select
	idTrx idLog,Datetime,MerchantName NamaComputer, ChasierName,(Tunai-(ABS(Kembalian))) Nominal,Status,
	'TRANSAKSI PENJUALAN' as Category
	from LogTransaksiPOS
	where Status is not null
	and 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Datetime,10),'/','-'), 105), 23),'-','')
	and Tunai>0
	and MerchantName = @NamaComputer
	and ChasierName = @KasirName
END
