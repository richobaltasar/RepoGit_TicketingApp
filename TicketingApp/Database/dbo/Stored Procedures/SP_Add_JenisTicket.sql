---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Add_JenisTicket
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
		if not exists(select*from DataTicket where namaticket=@namaticket)
		begin
			insert into DataTicket
			(namaticket,HargaWeekDay,HargaWeekEnd,HargaHoliday,status,Img)
			values(
				@namaticket,@HargaWeekDay,@HargaWeekEnd,@HargaHoliday,@status,@Img
			)
			
			declare @Omessage nvarchar(max)
			select  @Omessage='Data Ticket '+namaticket+' berhasil ditambah',@IdTicket = IdTicket from DataTicket where IdTicket = SCOPE_IDENTITY()
	
			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataTicket',@IdRow = @IdTicket,@message=@Omessage,@Action='ADD'

			select 'Succes' title, @Omessage message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_JenisTicket error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON

