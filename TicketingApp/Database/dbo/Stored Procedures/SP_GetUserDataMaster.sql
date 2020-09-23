CREATE PROCEDURE SP_GetUserDataMaster
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if(@Id = 0)
	begin
		select*from UserData where NamaLengkap like '%'+@search+'%'
	end
	else if(@Id != 0 and @search = '')
	begin
		select*from UserData where id = @Id
	end

END