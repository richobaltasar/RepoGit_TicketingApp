CREATE PROCEDURE SP_Edit_Recruitment
	@Id bigint,
	@NamaCandidat nvarchar(max),
	@Divisi nvarchar(max),
	@Photo nvarchar(max),
	@Cuvi nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataRecruitment where Id = @Id)
		begin
			declare @nama nvarchar(max)
			declare @message nvarchar(max)
			select @nama = NamaCandidat from DataRecruitment where Id = @Id
			if(@nama != @NamaCandidat)
			begin
				if not exists(select*from DataRecruitment where Id not in (@Id) and NamaCandidat = @NamaCandidat )
				begin
					update DataRecruitment
					set 
					NamaCandidat=@NamaCandidat,Divisi=@Divisi,Photo=@Photo,Cuvi=@Cuvi
					where Id = @Id
					select  @message=NamaCandidat+' - '+Divisi from DataRecruitment where id = @Id
					select 'Succes' title, 'Data '+@message+ ' berhasil ditambah' message,'success' status
				end
				else
				begin
					select 'Sorry' title, 'Nama kandidat : '+@NamaCandidat+' sudah terdaftar' message,'error' status
				end
			end
			else
			begin
				update DataRecruitment
				set 
				NamaCandidat=@NamaCandidat,Divisi=@Divisi,Photo=@Photo,Cuvi=@Cuvi
				where Id = @Id
				select  @message=NamaCandidat+' - '+Divisi from DataRecruitment where id = @Id
				select 'Succes' title, 'Data '+@message+ ' berhasil ditambah' message,'success' status
			end
		end
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
		
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_Recruitment error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END
