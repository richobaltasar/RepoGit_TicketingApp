CREATE PROCEDURE SP_Get_BulanLog_Tahunan
	@Tahun nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	--Ticketing
	select distinct right(left(b.Datetime,10),7) Bulan
	from LogTransaksiListDetailPOS a
	left join LogTransaksiPOS b on b.idTrx = a.IdTrx
	where 
	left(replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-',''),4) in ('2020')--@Tahun)
	and b.Status=1

END