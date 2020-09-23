
CREATE PROCEDURE [dbo].[SP_GetDataLogTransaksiCancel]
	@IdLog bigint
AS BEGIN 
	SET NOCOUNT ON;
	select
	*
	from LogTransaksiCancel
	where Id = @IdLog
END

