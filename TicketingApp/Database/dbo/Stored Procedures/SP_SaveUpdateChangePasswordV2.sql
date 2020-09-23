
CREATE PROCEDURE SP_SaveUpdateChangePasswordV2
@UserId bigint,
@CurrentPassword nvarchar(max),
@NewPassword nvarchar(max),
@ConfPassword nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	if exists(select*from UserData where id = @UserId and password = @CurrentPassword)
	begin
		if(@NewPassword = @ConfPassword )
		begin
			update UserData
			set 
				password = @NewPassword,
				ModifyDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')
			where id = @UserId
			select 'Info' title, 'Password successfully changed' message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'Password confirmation not match' message,'error' status
		end
	end
	else
	begin
		select 'Sorry' title, 'current Password tidak valid' message,'error' status
	end
END
