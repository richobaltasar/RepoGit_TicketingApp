CREATE PROCEDURE WebReportSP_GetLogCancelTransaksi
	@FilterBy nvarchar(max),
	@Search nvarchar(max),
	@SetAwal nvarchar(max),
	@SetAkhir nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	if(@FilterBy = 'All')
	begin
		select q.* from
		(
			select
			TipeTransaksi+'~'+PaymentMethod+'~'+cast(TotalTransaksi as nvarchar(max))
			+'~'+AccountNumber+'~'+NamaKasirYangInputTrx+'~'+NamaKasirYangCancel
			+'~'+Authorize+'~'+cast(IdTransaksi as nvarchar(max))+'~'+DescriptionTransaksi as Search
			,*
			from LogTransaksiCancel
		) q
		where  
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(TransactionDate,10),'/','-'), 105), 23),'-','') between
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAwal,10),'/','-'), 105), 23),'-','')
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAkhir,10),'/','-'), 105), 23),'-','')
		and Search like '%'+@Search+'%'
	end
	else if(@FilterBy = 'Account Number')
	begin
		select*from LogTransaksiCancel	
		where  
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(TransactionDate,10),'/','-'), 105), 23),'-','') between
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAwal,10),'/','-'), 105), 23),'-','')
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAkhir,10),'/','-'), 105), 23),'-','')
		and AccountNumber like '%'+@Search+'%'
	end
	else if(@FilterBy = 'ID Transaksi')
	begin
		select*from LogTransaksiCancel	
		where  
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(TransactionDate,10),'/','-'), 105), 23),'-','') between
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAwal,10),'/','-'), 105), 23),'-','')
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAkhir,10),'/','-'), 105), 23),'-','')
		and IdTransaksi like '%'+@Search+'%'
	end
	else if(@FilterBy = 'Nama Kasir')
	begin
		select*from LogTransaksiCancel	
		where  
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(TransactionDate,10),'/','-'), 105), 23),'-','') between
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAwal,10),'/','-'), 105), 23),'-','')
		and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@SetAkhir,10),'/','-'), 105), 23),'-','')
		and NamaKasirYangCancel like '%'+@Search+'%'
	end
END

