
CREATE PROCEDURE SP_Get_EmployeeData
	@Id bigint,
	@search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	
	if(@Id = 0)
	begin
		select*from UserData where NamaLengkap like '%'+@search+'%'
	end
	else
	begin
		select*from UserData where id = @Id
	end

END
