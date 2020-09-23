
CREATE PROCEDURE SP_GetFilterData
	@FormName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select Id as Msg from ERP.dbo.Master_Form
	where NamaForm = @FormName
	and FilterBy = 1
END
