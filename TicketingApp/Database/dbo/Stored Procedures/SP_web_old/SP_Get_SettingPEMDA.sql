CREATE PROCEDURE SP_Get_SettingPEMDA
AS
BEGIN
	SET NOCOUNT ON;
	select
	sum(isnull(q.Ticket,0)) Ticket,
	sum(isnull(q.FNB,0)) FNB,
	sum(isnull(q.Parkir,0)) Parkir
	from
	(
		select
		case when Category = 'Ticket' then isnull(Persentase,0) end as Ticket,
		case when Category = 'Parkir' then isnull(Persentase,0) end as Parkir,
		case when Category = 'F&B' then isnull(Persentase,0) end as FNB
		from DataSettingReportforExternal
		where Status = 1
		and NamaLaporan = 'Setting Laporan PEMDA'
	) q
END