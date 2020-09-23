
CREATE PROCEDURE SP_GetDetailRegCancel
	@IdTrx bigint
AS
BEGIN
	SET NOCOUNT ON;
	select*from LogCancelRegistrasiDetail
	where idTrx = @IdTrx
END
