﻿---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Get_Promo
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from DataPromo where NamaPromo like '%'+@search+'%'
	end
	else
	begin
		select*from DataPromo where idPromo = @Id
	end
END

SET ANSI_NULLS ON
