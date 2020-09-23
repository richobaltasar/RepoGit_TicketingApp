---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Get_JenisTicket
	@Id bigint, 
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from DataTicket where namaticket like '%'+namaticket+'%'
	end
	else
	begin
		select*from DataTicket where IdTicket = @Id
	end
END


SET ANSI_NULLS ON

