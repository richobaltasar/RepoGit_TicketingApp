CREATE PROCEDURE SP_Edit_EmployeeData
	@id bigint,
	@username nvarchar(max),
	@password nvarchar(max),
	@hakakses nvarchar(max),
	@NamaLengkap nvarchar(max),
	@Gender nvarchar(max),
	@Email nvarchar(max),
	@NoHp nvarchar(max),
	@Photo nvarchar(max),
	@Alamat nvarchar(max),
	@TanggalLahir nvarchar(max),
	@TempatLahir nvarchar(max),
	@Agama nvarchar(max),
	@ScanKTP nvarchar(max),
	@JenisIdentitas nvarchar(max),
	@NoIdentitas nvarchar(max),
	@NamaDivisi nvarchar(max),
	@NamaPosisi nvarchar(max),
	@TglAwalKerja nvarchar(max),
	@Status bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from UserData where id = @id)
		begin
			update UserData
			set
				username=@username,
				password=@password,
				hakakses=@hakakses,
				NamaLengkap=@NamaLengkap,
				Gender=@Gender,
				Email=@Email,
				NoHp=@NoHp,
				Photo=@Photo,
				Alamat=@Alamat,
				TanggalLahir=@TanggalLahir,
				TempatLahir=@TempatLahir,
				Agama=@Agama,
				ScanKTP=@ScanKTP,
				JenisIdentitas=@JenisIdentitas,
				NoIdentitas=@NoIdentitas,
				NamaDivisi=@NamaDivisi,
				NamaPosisi=@NamaPosisi,
				TglAwalKerja=@TglAwalKerja,
				Status=@Status
			where id = @id
			select 'Succes' title, 'Name : '+@NamaLengkap+' (NIK- '+cast(@id as nvarchar(max))+') suskes dirubah' message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_EmployeeData error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END