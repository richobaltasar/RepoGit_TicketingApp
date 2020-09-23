CREATE PROCEDURE GetDashBoardKasir
	@MerchantName nvarchar(max),
	@UserName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	select
	sum(ISNULL(w.DanaModal,0)) DanaModal,
	sum(ISNULL(w.Tunai,0)) Tunai,
	sum(ISNULL(w.Emoney,0)) Emoney,
	sum(ISNULL(w.EDC,0)) EDC,
	sum(ISNULL(w.QtyCard,0)) QtyCard,
	sum(ISNULL(w.TotalAmountCard,0)) TotalAmountCard,
	sum(ISNULL(w.QtyMotorParkir,0)) QtyMotorParkir,
	sum(ISNULL(w.QtyMobilParkir,0)) QtyMobilParkir,
	sum(ISNULL(w.TotalAmountParkir,0)) TotalAmountParkir,
	sum(ISNULL(w.QtyTicket,0)) QtyTicket,
	sum(ISNULL(w.TotalAmountTicket,0)) TotalAmountTicket,
	sum(ISNULL(w.QtyAsuransi,0)) QtyAsuransi,
	sum(ISNULL(w.TotalAmountAsuransi,0)) TotalAmountAsuransi,
	sum(ISNULL(w.QtyTopup,0)) QtyTopup,
	sum(ISNULL(w.TotalAmountTopup,0)) TotalAmountTopup,
	sum(ISNULL(w.QtyFoodCourt,0)) QtyFoodCourt,
	sum(ISNULL(w.TotalAmountFoodCourt,0)) TotalAmountFoodCourt

from
(
select isnull(DanaModalSetelah,0) DanaModal,
0 as Tunai,
0 as Emoney,
0 as EDC,
0 as QtyCard,
0 as TotalAmountCard,
0 as QtyMotorParkir,
0 as QtyMobilParkir,
0 as TotalAmountParkir,
0 as QtyTicket,
0 as TotalAmountTicket,
0 as QtyAsuransi,
0 as TotalAmountAsuransi,
0 as QtyTopup,
0 as TotalAmountTopup,
0 as QtyFoodCourt,
0 as TotalAmountFoodCourt
from DataChasierBox
where LEFT(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') 
	and NamaComputer = @MerchantName and OpenBy = @UserName
	and Status =1 
union all
select
	0 as DanaModal,
	sum(ISNULL(a.Tunai,0)) Tunai,
	sum(abs(ISNULL(a.Emoney,0))) Emoney,
	sum(ISNULL(a.EDC,0)) EDC,
	0 as QtyCard,
	0 as TotalAmountCard,
	0 as QtyMotorParkir,
	0 as QtyMobilParkir,
	0 as TotalAmountParkir,
	0 as QtyTicket,
	0 as TotalAmountTicket,
	0 as QtyAsuransi,
	0 as TotalAmountAsuransi,
	0 as QtyTopup,
	0 as TotalAmountTopup,
	0 as QtyFoodCourt,
	0 as TotalAmountFoodCourt
from
(
	select
		case when PaymentMethod ='CASH' then sum(isnull(Tunai,0)) end as Tunai,
		case when PaymentMethod ='EMONEY' then sum(isnull(Emoney,0)) end as Emoney,
		case when PaymentMethod ='EDC' then sum(isnull(TotalBayar,0)) end as EDC
	from LogTransaksiPOS 
	where LEFT(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') 
	and MerchantName = @MerchantName and ChasierName = @UserName
	and Status = 1
	group by PaymentMethod
) a
union all
select 
	0 as DanaModal,
	0 as Tunai,
	0 as Emoney,
	0 as EDC,
	sum(ISNULL(q.QtyCard,0)) QtyCard,
	sum(ISNULL(q.TotalAmountCard,0)) TotalAmountCard,
	sum(ISNULL(q.QtyMotorParkir,0)) QtyMotorParkir,
	sum(ISNULL(q.QtyMobilParkir,0)) QtyMobilParkir,
	sum(ISNULL(q.TotalAmountParkir,0)) TotalAmountParkir,
	sum(ISNULL(q.QtyTicket,0)) QtyTicket,
	sum(ISNULL(q.TotalAmountTicket,0)) TotalAmountTicket,
	sum(ISNULL(q.QtyAsuransi,0)) QtyAsuransi,
	sum(ISNULL(q.TotalAmountAsuransi,0)) TotalAmountAsuransi,
	sum(ISNULL(q.QtyTopup,0)) QtyTopup,
	sum(ISNULL(q.TotalAmountTopup,0)) TotalAmountTopup,
	sum(ISNULL(q.QtyFoodCourt,0)) QtyFoodCourt,
	sum(ISNULL(q.TotalAmountFoodCourt,0)) TotalAmountFoodCourt
from
(
	select
		case when Category = 'CARD' then sum(isnull(Qtx,0)) end as QtyCard,
		case when Category = 'CARD' then sum(isnull(Total,0)) end as TotalAmountCard,

		case when Category = 'PARKIR' and NamaItem like '%Motor%' then sum(isnull(Qtx,0)) end as QtyMotorParkir,
		case when Category = 'PARKIR' and NamaItem like '%Mobil%' then sum(isnull(Qtx,0)) end as QtyMobilParkir,
		case when Category = 'PARKIR' then sum(isnull(Total,0)) end as TotalAmountParkir,

		case when Category = 'TICKETING' and NamaItem like '%Nama Ticket%' then sum(isnull(Qtx,0)) end as QtyTicket,
		case when Category = 'TICKETING' and NamaItem like '%Nama Ticket%' then sum(isnull(Total,0)) end as TotalAmountTicket,

		case when Category = 'TICKETING' and NamaItem like '%Asuransi%' then sum(isnull(Qtx,0)) end as QtyAsuransi,
		case when Category = 'TICKETING' and NamaItem like '%Asuransi%' then sum(isnull(Total,0)) end as TotalAmountAsuransi,

		case when Category = 'TOPUP' then sum(isnull(Qtx,0)) end as QtyTopup,
		case when Category = 'TOPUP' then sum(isnull(Total,0)) end as TotalAmountTopup,

		case when Category = 'FOODCOURT' then sum(isnull(Qtx,0)) end as QtyFoodCourt,
		case when Category = 'FOODCOURT' then sum(isnull(Total,0)) end as TotalAmountFoodCourt

	from LogTransaksiListDetailPOS
	where IdTrx in (
	select idTrx from LogTransaksiPOS 
	where LEFT(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') 
	and MerchantName = @MerchantName and ChasierName = @UserName
	and Status =1)
	group by Category, NamaItem
) q
)w

END
