--exec SP_GetIdModule @NamaModule='FNB'

CREATE PROCEDURE SP_GetIdModule
	@NamaModule nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select IdModul from DataModule where UPPER(Controller) = UPPER(@NamaModule)

	--select*from DataModule
END