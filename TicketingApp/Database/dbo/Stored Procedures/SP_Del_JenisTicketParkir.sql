---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_Del_JenisTicketParkir
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataTicketParkir where Id = @Id)
		begin
			declare @Omessage nvarchar(max)
			select @Omessage='Ticket Parkir '+JenisTicket+' berhasil dihapus' from DataTicketParkir where Id = @Id
			delete from DataTicketParkir where id = @Id
			
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataTicketParkir',@IdRow = @Id,@message=@Omessage,@Action='DELETE'

			if not exists(select  * from DataTicketParkir where Id = @Id)
			begin
				select 'Succes' title,@Omessage+ ' berhasil dihapus' message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal delete' message,'error' status
			end
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Del_JenisTicketParkir error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

