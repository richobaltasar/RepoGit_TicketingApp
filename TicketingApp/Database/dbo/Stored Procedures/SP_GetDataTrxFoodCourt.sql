CREATE PROCEDURE [dbo].[SP_GetDataTrxFoodCourt]
	@IdTrx bigint
AS
BEGIN
	SET NOCOUNT ON;
	select
	*
	from LogFoodcourtTransaksi
	where IdTrx = @IdTrx
END
