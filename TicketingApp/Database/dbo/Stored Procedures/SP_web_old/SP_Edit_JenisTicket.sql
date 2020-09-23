
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Edit_JenisTicket
	@IdTicket bigint,
	@namaticket nvarchar(max),
	@HargaWeekDay float,
    @HargaWeekEnd float,
    @HargaHoliday float,
	@Img nvarchar(max),
	@status bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataTicket where IdTicket = @IdTicket)
		begin
			
			update DataTicket
			set
				namaticket=@namaticket,HargaWeekDay=@HargaWeekDay,HargaWeekEnd=@HargaWeekEnd,HargaHoliday=@HargaHoliday,status=@status,Img=@Img
			where IdTicket = @IdTicket
			
			
			declare @Omessage nvarchar(max)
			select  @Omessage='Data Ticket '+namaticket+' berhasil diubah' from DataTicket where IdTicket = @IdTicket

			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataTicket',@IdRow = @IdTicket,@message=@Omessage,@Action='EDIT'

			select 'Succes' title, @Omessage message,'success' status	
			
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_JenisTicket error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END


SET ANSI_NULLS ON

