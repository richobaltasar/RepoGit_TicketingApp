CREATE PROCEDURE SP_Get_LogPendapatan_TicketParkir
	@Awal nvarchar(max),
	@Akhir nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
    select
        q.Category,
        q.NamaItem NamaItem,
        q.Harga Harga,
        sum(q.Qtx) Qty,
        sum(q.Total) Total
        from
        (
            select Category,c.TypeKendaraan NamaItem,Harga Harga,Qtx,Total,left(b.Datetime,10) Tanggal from LogTransaksiListDetailPOS a
            left join LogTransaksiPOS b on b.idTrx = a.IdTrx
			left join LogParkir c on c.BarcodeReciptCode = a.Id
            where a.Category = 'PARKIR'
            and 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-','')
            between 
            replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Awal,'/','-'), 105), 23),'-','') 
            and replace(CONVERT(VARCHAR(10), CONVERT(date, replace(@Akhir,'/','-'), 105), 23),'-','')
			and b.Status=1
        ) q
        group BY q.Category,q.Harga,q.NamaItem
END