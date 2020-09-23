
CREATE PROCEDURE SP_Get_SettingPajak
AS
BEGIN
	SET NOCOUNT ON;
	select
	sum(isnull(q.Ticket,0)) Ticket,
	sum(isnull(q.FNB,0)) FNB,
	sum(isnull(q.Parkir,0)) Parkir,
	sum(isnull(q.PPN,0)) PPN,
	sum(isnull(q.PPH21,0)) PPH21
	from
	(
		select
		case when Category = 'Ticket' then isnull(Persentase,0) end as Ticket,
		case when Category = 'Parkir' then isnull(Persentase,0) end as Parkir,
		case when Category = 'F&B' then isnull(Persentase,0) end as FNB,
		case when Category = 'PPN' then isnull(Persentase,0) end as PPN,
		case when Category = 'PPH 21' then isnull(Persentase,0) end as PPH21
		from DataSettingReportforExternal
		where Status = 1
		and NamaLaporan = 'Setting Laporan Pajak'
	) q
END