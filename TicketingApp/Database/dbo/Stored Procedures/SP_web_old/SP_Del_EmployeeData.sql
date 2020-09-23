
CREATE PROCEDURE SP_Del_EmployeeData
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;

	if exists(select*from UserData where id = @Id)
	begin
		delete from UserData
		where id = @Id
		
		delete from DataHakAkses where UserId = @Id

		select 'Success' title, 'delete data is success' message,'success' status
	end
    else
	begin
		select 'Sorry' title, 'Data not Exists' message,'error' status
	end
END
