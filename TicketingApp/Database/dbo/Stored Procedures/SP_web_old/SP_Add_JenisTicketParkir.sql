---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_Add_JenisTicketParkir
	@Id bigint,
	@JenisTicket nvarchar(max),
	@Category nvarchar(max),
	@Code nvarchar(max),
	--@Harga float,
	@HargaWeekDay float,
	@HargaWeekEnd float,
	@HargaHoliday float,
	--@Periode nvarchar(max),
	@Img nvarchar(max),
	@Status bigint,
	@CreateBy nvarchar(max),
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if not exists(select*from DataTicketParkir where JenisTicket = @JenisTicket)
		begin
			insert into DataTicketParkir
			(
				JenisTicket,
				Category,
				Code,
				HargaWeekDay,HargaWeekEnd,HargaHoliday,
				Img,
				Status,
				CreateDate,
				CreateBy
			)
			values(
				@JenisTicket,
				@Category,
				@Code,
				@HargaWeekDay,@HargaWeekEnd,@HargaHoliday,
				@Img,
				@Status,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				@CreateBy
			)
			
			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Ticket Parkir  '+JenisTicket+ ' berhasil ditambah',@Id = Id from DataTicketParkir where Id = SCOPE_IDENTITY()

			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataTicketParkir',@IdRow = @Id,@message=@Omessage,@Action='ADD'

			if exists(select  * from DataTicketParkir where id = @Id)
			begin
				select 'Succes' title, @Omessage message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal insert' message,'error' status
			end

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_JenisTicketParkir error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON
