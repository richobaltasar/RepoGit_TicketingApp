CREATE PROCEDURE SP_GetDataModule
	@IDUser bigint
AS
BEGIN
	SET NOCOUNT ON;

	select*from DataModule where Status=1
	and IdModul in (select IdModule from [dbo].[Role_MenuTree] where IdMenu in (
	select IdMenu from DataHakAkses where UserId = @IDUser and Category = 'WEB'))
	order by NamaModule asc

END