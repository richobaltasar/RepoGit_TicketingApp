
CREATE PROCEDURE SP_Save_SettingPajak
	@FNB float,
	@Parkir float,
	@Ticket float,
	@PPN float,
	@PPH21 float
AS
BEGIN
	SET NOCOUNT ON;
	update DataSettingReportforExternal
	set Persentase= @Ticket
	where NamaLaporan = 'Setting Laporan Pajak' and Category='Ticket'

	update DataSettingReportforExternal
	set Persentase= @Parkir
	where NamaLaporan = 'Setting Laporan Pajak' and Category='Parkir'

	update DataSettingReportforExternal
	set Persentase= @FNB
	where NamaLaporan = 'Setting Laporan Pajak' and Category='F&B'

	update DataSettingReportforExternal
	set Persentase= @PPN
	where NamaLaporan = 'Setting Laporan Pajak' and Category='PPN'

	update DataSettingReportforExternal
	set Persentase= @PPH21
	where NamaLaporan = 'Setting Laporan Pajak' and Category='PPH 21'

	select 'success' title,'save data success' message,'success' status
END
