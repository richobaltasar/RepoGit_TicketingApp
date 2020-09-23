---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_Get_RABList
	@Id bigint,
	@IdRab bigint
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from DataRAB_Item where IdRAB = @IdRab
	end
	else
	begin
		select*from DataRAB_Item where IdRAB = @IdRab and Id = @Id
	end
END

SET ANSI_NULLS ON

SET ANSI_NULLS ON
