
CREATE PROCEDURE SP_Get_DaftarHariLiburNasional
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from DataHariLiburNasional where NamaHariLibur like '%'+@search+'%'
	end
	else
	begin
		select*from DataHariLiburNasional
		where Id = @Id
	end
END


