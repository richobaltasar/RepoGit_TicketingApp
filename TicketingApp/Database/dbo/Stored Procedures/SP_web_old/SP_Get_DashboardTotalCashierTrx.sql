
--select*from LogTransaksiPOS
--exec SP_Get_DashboardTotalCashierTrx '01/09/2020','15/09/2020','Riko Ade Rinanda'

CREATE PROCEDURE SP_Get_DashboardTotalCashierTrx
	@Awal_Datetime nvarchar(max),
	@Akhir_Datetime nvarchar(max),
	@Kasir nvarchar(max)	
AS
BEGIN
	SET NOCOUNT ON;

	if(@Kasir = '')
	begin
		select
		sum(TotalTrx) TotalTrx,
		sum(Q.TrxEmoney) TrxEmoney,
		sum(Q.TrxTunai) TrxTunai,
		sum(Q.TrxEDC) TrxEDC
		from 
		(select
		TotalTransaksi TotalTrx,ABS(Emoney) TrxEmoney,
		case when Tunai>0 and BankName ='' then (Tunai-(ABS(Kembalian))) else 0 end as TrxTunai,
		case when Tunai=0 and BankName !='' then TotalBayar else 0 end as TrxEDC
		from LogTransaksiPOS
		where 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
		between 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
		and 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
		and Status = 1) Q
	end
	else
	begin
		select
		sum(TotalTrx) TotalTrx,
		sum(Q.TrxEmoney) TrxEmoney,
		sum(Q.TrxTunai) TrxTunai,
		sum(Q.TrxEDC) TrxEDC
		from 
		(select
		TotalTransaksi TotalTrx,Emoney TrxEmoney,
		case when Tunai>0 and BankName ='' then (Tunai-(ABS(Kembalian))) else 0 end as TrxTunai,
		case when Tunai=0 and BankName !='' then TotalBayar else 0 end as TrxEDC
		from LogTransaksiPOS
		where 
		replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','')
		between 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Awal_Datetime,10),'/','-'), 105), 23),'-','')
		and 
			replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Akhir_Datetime,10),'/','-'), 105), 23),'-','')	
		and Status = 1
		and ChasierName = @Kasir
		) Q
	end

END
