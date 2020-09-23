
CREATE PROCEDURE Get_RAB_ById
	@IdRAB bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from DataRAB where id = @IdRAB
END
