
CREATE PROCEDURE SP_GetNamaMenu
	@Action nvarchar(max),
	@Controller nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	select NamaMenu as Result from DataMenu where Action = @Action and Controller = @Controller and Status = 1
END
