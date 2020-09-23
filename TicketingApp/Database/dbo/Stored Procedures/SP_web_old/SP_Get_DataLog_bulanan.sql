-- exec SP_Get_DataLog_bulanan '31/08/2020','NON MANAGEMENT'

CREATE PROCEDURE SP_Get_DataLog_bulanan
	@Tanggal nvarchar(max),
	@NamaItem nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	IF(@NamaItem NOT IN ('CASH','EMONEY','EDC','TRANSAKSI','NEW MEMBER','TOPUP'))
	BEGIN
	--Ticketing
	select
	sum(Qty) Qty, sum(Total) Total
	from
	(
			select
			q.Tanggal,
			q.Category,
			case when q.NamaItem like '%ASURANSI%' then 'ASURANSI'
					when q.NamaItem like '%DISKON%'then 'DISKON'
					when q.NamaItem like '%NAMA TICKET%'then 'TICKET'
					when q.NamaItem like '%MOTOR%' then 'MOTOR'
					when q.NamaItem like '%MOBIL%' then 'MOBIL'
					else q.NamaItem

			end as NamaItem,
			sum(q.Qtx) Qty,
			sum(q.Total) Total
			from
			(
				select Category,UPPER(NamaItem) NamaItem,Harga,Qtx,Total, 
				left(b.Datetime,10) Tanggal
				from LogTransaksiListDetailPOS a
				left join LogTransaksiPOS b on b.idTrx = a.IdTrx
				where 
				replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-','') = --'20200831'
				replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
				and b.Status=1
				and a.Category not in ('FOODCOURT','TOPUP')
			) q
			group BY q.Category,q.Harga,q.NamaItem,q.Tanggal

			union all

			select
			p.Tanggal,
			p.Category,
			p.NamaItem,
			sum(p.Qty) Qty,
			sum(p.Total) Total
			from
			(
				select Tanggal,Category,
				case when StatusKepemilikan = 'Management' then 'MANAGEMENT' else 'NON MANAGEMENT' end as NamaItem, sum(isnull(Qtx,0)) Qty,sum(isnull(Total,0)) Total  from
				(
					select
						a.*,b.StatusKepemilikan,
						left(c.Datetime,10) Tanggal
					from LogTransaksiListDetailPOS a
					left join DataTenant b on b.idTenant = (SELECT top 1 value FROM STRING_SPLIT(a.Id, '-'))
					left join LogTransaksiPOS c on c.idTrx = a.IdTrx
					where 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(c.Datetime,10),'/','-'), 105), 23),'-','') = 
					--'20200831'
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
					and c.Status = 1
					and a.Category = 'FOODCOURT'
				) q
				group by Category,StatusKepemilikan,Tanggal
			) p
			group by p.Tanggal,p.Category,p.NamaItem
	) w
	where w.NamaItem = @NamaItem
	END
	ELSE
	BEGIN
		IF(@NamaItem not in ('TRANSAKSI','NEW MEMBER','TOPUP'))
		BEGIN
		SELECT
		W.QTY Qty, W.TOTAL Total
		FROM 
		(
			SELECT
			COUNT(*) QTY,
			SUM(TOTAL) TOTAL,
			JENIS
			FROM
			(
				SELECT 
				left(Datetime,10) Tanggal,
				idTrx,
				CASE WHEN Tunai != 0 THEN Tunai+Kembalian 
					WHEN Emoney != 0 THEN ABS(Emoney)
					WHEN Tunai = 0 AND BankName !='' THEN TotalBayar
					ELSE 0 END AS TOTAL,
				CASE WHEN Tunai != 0 THEN 'CASH'
					WHEN Emoney != 0 THEN 'EMONEY'
					WHEN Tunai = 0 AND BankName !='' THEN 'EDC'
					ELSE 'UNKNOWN' END AS JENIS
				FROM LogTransaksiPOS
				WHERE 
				replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = --'20200831'
				replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
				and Status=1
			) Q
			group by q.JENIS
		) W
		WHERE W.JENIS = @NamaItem
		END
		ELSE if(@NamaItem IN ('NEW MEMBER','TOPUP'))
		BEGIN
			select
			sum(Qty) Qty, sum(Total) Total
			from
			(
				select
				q.Tanggal,
				q.Category,
				case when q.NamaItem like '%NEW MEMBER%' then 'NEW MEMBER'
						when q.NamaItem like '%TOPUP%'then 'TOPUP'
						else q.NamaItem
				end as NamaItem,
				sum(q.Qtx) Qty,
				sum(q.Total) Total
				from
				(
					select Category,UPPER(NamaItem) NamaItem,Harga,Qtx,Total, 
					left(b.Datetime,10) Tanggal
					from LogTransaksiListDetailPOS a
					left join LogTransaksiPOS b on b.idTrx = a.IdTrx
					where 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(b.Datetime,10),'/','-'), 105), 23),'-','') = --'20200831'
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
					and b.Status=1
					and a.Category in ('CARD','TOPUP')
				) q
				group BY q.Category,q.Harga,q.NamaItem,q.Tanggal
			) W
			where w.NamaItem = @NamaItem
		END
		ELSE
		BEGIN
			SELECT 
				COUNT(idTrx) Qty,
				SUM(TotalTransaksi) Total
			FROM LogTransaksiPOS
			WHERE 
				replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = --'20200831'
				replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@Tanggal,10),'/','-'), 105), 23),'-','')
			and Status=1
		END
	END
END
