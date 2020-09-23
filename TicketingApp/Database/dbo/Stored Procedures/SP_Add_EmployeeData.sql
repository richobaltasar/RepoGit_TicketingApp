CREATE PROCEDURE SP_Add_EmployeeData
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
		if not exists(select*from UserData where username=@username)
		begin

			insert into UserData
			(username,password,hakakses,NamaLengkap,Email,Gender,NoHp,Photo,Alamat,Status,TanggalLahir,
			TempatLahir,Agama,ScanKTP,JenisIdentitas,NoIdentitas,NamaDivisi,NamaPosisi,TglAwalKerja,CreateDate)
			values(
				@username,@password,@hakakses,@NamaLengkap,@Email,@Gender,@NoHp,@Photo,@Alamat,@Status,@TanggalLahir,
				@TempatLahir,@Agama,@ScanKTP,@JenisIdentitas,@NoIdentitas,@NamaDivisi,@NamaPosisi,@TglAwalKerja,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')
			)
			
			declare @message nvarchar(max)
			select  @message=NamaLengkap+' - '+NamaDivisi from UserData where id = SCOPE_IDENTITY()
			select 'Succes' title, 'Data '+@message+ ' berhasil ditambah' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_EmployeeData error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END