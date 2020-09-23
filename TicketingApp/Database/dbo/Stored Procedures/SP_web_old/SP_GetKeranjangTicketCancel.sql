
CREATE PROCEDURE SP_GetKeranjangTicketCancel
	@IdTrx bigint
AS
BEGIN
	SET NOCOUNT ON;
	declare @IdKeranjang bigint

	select @IdKeranjang=IdTicketTrx from LogCancelRegistrasiDetail where idTrx = @IdTrx

	select*from LogCancelTicketDetail
	where IdTicket = @IdKeranjang
END
