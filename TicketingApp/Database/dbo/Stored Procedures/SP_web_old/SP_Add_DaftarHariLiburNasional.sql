
CREATE PROCEDURE [dbo].[SP_Add_DaftarHariLiburNasional]
	@Id bigint,
	@NamaHariLibur nvarchar(max),
	@DariTanggal nvarchar(max),
	@SampaiTanggal nvarchar(max),
	@Status bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if not exists(select*from DataHariLiburNasional where NamaHariLibur = @NamaHariLibur)
		begin
			insert into DataHariLiburNasional
			(
				NamaHariLibur,
				DariTanggal,
				SampaiTanggal,
				Status
			)
			values(
				@NamaHariLibur,
				@DariTanggal,
				@SampaiTanggal,
				@Status
			)
			
			declare @message nvarchar(max)
			declare @IdRow bigint
			
			select  @message=@NamaHariLibur,@IdRow = Id from DataHariLiburNasional where Id = SCOPE_IDENTITY()
	
			-- insert Log Activity
			declare @Username nvarchar(max)
			select @Username=username from UserData where id = @IdUser

			insert into LogActivityUser (NamaTable,Message,IdRow,Action,UserBy,Datetime)
			values ('DataHariLiburNasional','Data '+@message+ ' tanggal : '+@DariTanggal+' - '+@SampaiTanggal+' berhasil ditambah',cast(@IdRow as nvarchar(max)),'ADD',@Username,FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'))

			select 'Succes' title, 'Data '+@message+ ' berhasil ditambah' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry [SP_Add_DaftarHariLiburNasional] error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END


