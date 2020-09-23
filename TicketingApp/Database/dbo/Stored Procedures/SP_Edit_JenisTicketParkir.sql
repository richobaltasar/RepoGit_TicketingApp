---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_Edit_JenisTicketParkir
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
		if exists(select*from DataTicketParkir where Id = @Id)
		begin
			update DataTicketParkir
			set	
				JenisTicket=@JenisTicket,
				Category=@Category,
				Code=@Code,
				HargaWeekDay=@HargaWeekDay,HargaWeekEnd=@HargaWeekEnd,HargaHoliday=@HargaHoliday,
				--Harga=@Harga,
				--Periode=@Periode,
				Img=@Img,
				Status=@Status,
				ModifyDate=FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				CreateBy=@CreateBy
			where Id = @Id

			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Ticket Parkir  '+JenisTicket+ ' berhasil diubah',@Id = Id from DataTicketParkir where Id = @Id
			
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataTicketParkir',@IdRow = @Id,@message=@Omessage,@Action='EDIT'


			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_JenisTicketParkir error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON
