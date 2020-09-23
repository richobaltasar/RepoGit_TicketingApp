
create PROCEDURE SP_Del_Recruitment
	@Id bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataRecruitment where id = @Id)
		begin
			declare @Omessage nvarchar(max)
			select @Omessage='Data  '+NamaCandidat+' - '+Divisi+' berhasil dihapus' 
			from DataRecruitment where id = @Id
			
			delete from DataRecruitment where id = @Id
			
			if not exists(select  * from DataRecruitment where id = @Id)
			begin
				select 'Succes' title, @Omessage message,'success' status
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
		select 'Sorry SP_Del_Recruitment error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
