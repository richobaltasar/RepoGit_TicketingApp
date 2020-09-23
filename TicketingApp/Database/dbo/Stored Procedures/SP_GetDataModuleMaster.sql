
CREATE PROCEDURE SP_GetDataModuleMaster
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from DataModule where NamaModule like '%'+@search+'%'
	end
	else if(@Id != 0 and @search = '')
	begin
		select*from DataModule where IdModul = @Id
	end

END
