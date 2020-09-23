---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Get_JenisTicketParkir
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from DataTicketParkir where JenisTicket like '%'+@search+'%'
	end
	else
	begin
		select*from DataTicketParkir
		where Id = @Id
	end
END

SET ANSI_NULLS ON
