CREATE PROCEDURE SP_Save_SettingPEMDA
	@FNB float,
	@Parkir float,
	@Ticket float
AS
BEGIN
	SET NOCOUNT ON;

	--select*from DataSettingReportforExternal

	update DataSettingReportforExternal
	set Persentase= @Ticket
	where NamaLaporan = 'Setting Laporan PEMDA' and Category='Ticket'

	update DataSettingReportforExternal
	set Persentase= @Parkir
	where NamaLaporan = 'Setting Laporan PEMDA' and Category='Parkir'

	update DataSettingReportforExternal
	set Persentase= @FNB
	where NamaLaporan = 'Setting Laporan PEMDA' and Category='F&B'

	select 'success' title,'save data success' message,'success' status
END
