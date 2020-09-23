CREATE PROCEDURE [dbo].[SP_Del_DaftarHariLiburNasional]
	@Id bigint,
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataHariLiburNasional where Id = @Id)
		begin
			declare @message nvarchar(max)
			select @message=NamaHariLibur+' Tanggal :'+ DariTanggal+' - '+SampaiTanggal from DataHariLiburNasional where Id = @Id
			
			delete from DataHariLiburNasional where id = @Id
			
			declare @Username nvarchar(max)
			select @Username=username from UserData where id = @IdUser

			-- insert Log Activity
			insert into LogActivityUser (NamaTable,Message,IdRow,Action,UserBy,Datetime)
			values ('DataHariLiburNasional','Data '+@message+ ' berhasil dihapus',@Id,'DELETE',
			@Username,FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'))

			if not exists(select  * from DataHariLiburNasional where Id = @Id)
			begin
				
				select 'Succes' title, 'data '+@message+ ' berhasil dihapus' message,'success' status
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
		select 'Sorry SP_Del_DaftarHariLiburNasional error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
