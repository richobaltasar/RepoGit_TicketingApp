CREATE PROCEDURE SP_Update_DataChasierBox
	@NamaComputer nvarchar(max),
	@Datetime  nvarchar(max),
	@UserKasir nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @IdModal bigint
	declare @TotalModal float
	declare @TotalUangDiBox float
	declare @TotalUangMasuk float
	declare @TotalUangKeluar float

	-- select*from DataChasierBox
    select @IdModal = IdModal,@TotalModal=DanaModalSetelah
	from DataChasierBox
	where Status = 1
	and 
	replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Datetime,10),'/','-'), 105), 23),'-','')
	and NamaComputer = @NamaComputer
	and (OpenBy = @UserKasir or UpdateBy = @UserKasir)

	select @TotalUangMasuk=Q.Tunai,@TotalUangKeluar=Q.Kembalian from
	(
		select sum(isnull(Tunai,0)) Tunai,sum(isnull(Kembalian,0)) Kembalian from LogTransaksiPOS
		where replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Datetime,10),'/','-'), 105), 23),'-','')
		and Tunai > 0
		and MerchantName = @NamaComputer
		and ChasierName = @UserKasir
		and status = 1
	) Q

	set @TotalUangMasuk = @TotalUangMasuk+@TotalModal
	set @TotalUangDiBox = @TotalUangMasuk - (ABS(@TotalUangKeluar))

	update DataChasierBox
	set TotalUangDiBox = @TotalUangDiBox,
	TotalUangMasuk= @TotalUangMasuk,
	TotalUangKeluar = @TotalUangKeluar
	where IdModal = @IdModal

	print 'OKE'
END