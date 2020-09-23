
CREATE PROCEDURE [dbo].[SP_Edit_DaftarHariLiburNasional]
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
		if exists(select*from DataHariLiburNasional where Id = @Id)
		begin
			update DataHariLiburNasional
			set	
				NamaHariLibur=@NamaHariLibur,
				DariTanggal=@DariTanggal,
				SampaiTanggal=@SampaiTanggal,
				Status=@Status
			where Id = @Id
			
			-- insert Log Activity
			declare @message nvarchar(max)
			set @message='Data '+@NamaHariLibur

			declare @Username nvarchar(max)
			select @Username=username from UserData where id = @IdUser

			insert into LogActivityUser (NamaTable,Message,IdRow,Action,UserBy,Datetime)
			values ('DataHariLiburNasional',@message+ ' tanggal : '+@DariTanggal+' - '+@SampaiTanggal+' berhasil dirubah',cast(@Id as nvarchar(max)),'EDIT',@Username,FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'))

			select 'Success' title,@message+ ' berhasil ditambah' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_DaftarHariLiburNasional error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

