CREATE PROCEDURE SP_Add_Recruitment
	@Id bigint,
	@NamaCandidat nvarchar(max),
	@Divisi nvarchar(max),
	@Photo nvarchar(max),
	@Cuvi nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if(@NamaCandidat!='' and @Divisi!='')
		begin
		if not exists(select*from DataRecruitment where NamaCandidat = @NamaCandidat and Divisi = @Divisi)
		begin

			insert into DataRecruitment
			(NamaCandidat,Divisi,Photo,Cuvi)
			values(
				@NamaCandidat,@Divisi,@Photo,@Cuvi
			)
			
			declare @message nvarchar(max)
			select  @message=NamaCandidat+' - '+Divisi from DataRecruitment where id = SCOPE_IDENTITY()
			select 'Succes' title, 'Data '+@message+ ' berhasil ditambah' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
		end
		else
		begin
			if not exists(select*from DataRecruitment where Cuvi = @Cuvi)
			begin
				insert into DataRecruitment
				(NamaCandidat,Divisi,Photo,Cuvi)
				values(
					@NamaCandidat,@Divisi,@Photo,@Cuvi
				)
				select  @message=NamaCandidat+' - '+Divisi from DataRecruitment where id = SCOPE_IDENTITY()
				select 'Succes' title, 'Data '+@message+ ' berhasil ditambah' message,'success' status
			end
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_Recruitment error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
