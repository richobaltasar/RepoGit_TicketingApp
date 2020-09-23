
CREATE PROCEDURE SP_F_AddMenuHakAksesUser
	@Id bigint,
	@userid bigint
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY  
		declare @category nvarchar(max)
	
		set @category  = (select case when Platform = 'WEB' then 'WEB' else 'POS' end as Category from DataMenu where idMenu = @Id)

		if not exists(select*from DataHakAkses where UserId = @userid and IdMenu = @Id and Category = @category)
		begin
			insert into DataHakAkses
			(UserId,Category,IdMenu)
			values(@userid,@category,@Id)
			select
			'success' title,
			'Save success' message,
			'succes' status
		end
		else
		begin
			select
			'success' title,
			'Save success' message,
			'succes' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Error' title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'error' icon
	END CATCH;  
END
