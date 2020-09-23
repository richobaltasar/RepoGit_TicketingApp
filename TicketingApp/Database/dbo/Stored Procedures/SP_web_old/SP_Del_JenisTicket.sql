
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Del_JenisTicket
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		
		if exists(select*from DataTicket where IdTicket = @Id)
		begin
			declare @Omessage nvarchar(max)
			select  @Omessage='Data Ticket '+namaticket+' berhasil dihapus' from DataTicket where IdTicket = @Id

			delete from DataTicket where IdTicket = @Id

			-- insert Log Activity
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataTicket',@IdRow = @Id,@message=@Omessage,@Action='DELETE'


			select 'Succes' title,@Omessage message,'success' status	
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Del_JenisTicket error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

