CREATE PROCEDURE [dbo].[SP_UserData_Save]
	@id bigint,
	@username nvarchar(max),
	@password nvarchar(max),
	@hakakses nvarchar(max),
	@NamaLengkap nvarchar(max),
	@Email nvarchar(max),
	@Gender nvarchar(max),
	@NoHp nvarchar(max),
	@Photo nvarchar(max),
	@Alamat nvarchar(max),
	@Status bigint,
	@TanggalLahir nvarchar(max),
	@TempatLahir nvarchar(max),
	@Agama nvarchar(max),
	@ScanKTP nvarchar(max),
	@JenisIdentitas nvarchar(max),
	@NoIdentitas nvarchar(max),
	@NamaDivisi nvarchar(max),
	@NamaPosisi nvarchar(max),
	@TglAwalKerja nvarchar(max),
	@CreateDate nvarchar(max),
	@ModifyDate nvarchar(max),
	@CreateBy nvarchar(max),
	@ImgLink nvarchar(max)
AS
	declare @Title nvarchar(max)
	declare @Message nvarchar(max)
	declare @MStatus nvarchar(max)
	
if(@Id=0)
begin
	-- Create
	if not exists(select*from UserData where (username = @username or Email = @Email))
	begin
		insert into UserData
		(
			username,
			password,
			hakakses,
			NamaLengkap,
			Email,
			Gender,
			NoHp,
			Photo,
			Alamat,
			Status,
			TanggalLahir,
			TempatLahir,
			Agama,
			ScanKTP,
			JenisIdentitas,
			NoIdentitas,
			NamaDivisi,
			NamaPosisi,
			TglAwalKerja,
			CreateDate,
			ModifyDate,
			CreateBy,
			ImgLink
		)
		values 
		(
			@username,
			@password,
			@hakakses,
			@NamaLengkap,
			@Email,
			@Gender,
			@NoHp,
			@Photo,
			@Alamat,
			@Status,
			@TanggalLahir,
			@TempatLahir,
			@Agama,
			@ScanKTP,
			@JenisIdentitas,
			@NoIdentitas,
			@NamaDivisi,
			@NamaPosisi,
			@TglAwalKerja,
			@CreateDate,
			@ModifyDate,
			@CreateBy,
			@ImgLink
		)

		set @Id = (select id from UserData where id = SCOPE_IDENTITY())
		set @Title = 'Success'
		set @Message = 'Penambahan user '+ @username + ' berhasil dibuat'
		set @MStatus = 'success'
	end
	else
	begin
		set @Title = 'Sorry'
		set @Message = 'user '+ @username + ' atau email '+@Email+'already exists'
		set @MStatus = 'error'
	end
end
else
begin
	-- Modif
	declare @username_sebelum nvarchar(max)
	declare @Email_sebelum nvarchar(max)
	select @username_sebelum =  username, @Email_sebelum = Email from UserData where id = @id

	if(@username_sebelum = @username and @Email_sebelum = @Email)
	begin
		update UserData
		set 
			username=@username,
			password=@password,
			hakakses=@hakakses,
			NamaLengkap=@NamaLengkap,
			Email=@Email,
			Gender=@Gender,
			NoHp=@NoHp,
			Photo=@Photo,
			Alamat=@Alamat,
			Status=@Status,
			TanggalLahir=@TanggalLahir,
			TempatLahir=@TempatLahir,
			Agama=@Agama,
			ScanKTP=@ScanKTP,
			JenisIdentitas=@JenisIdentitas,
			NoIdentitas=@NoIdentitas,
			NamaDivisi=@NamaDivisi,
			NamaPosisi=@NamaPosisi,
			TglAwalKerja=@TglAwalKerja,
			CreateDate=@CreateDate,
			ModifyDate=@ModifyDate,
			CreateBy=@CreateBy,
			ImgLink=@ImgLink
		where id=@id

		set @Title = 'Success'
		set @Message = 'User '+ @username + ' berhasil diupdate'
		set @MStatus = 'success'
	end
	else
	begin
		--kalo ada perubahan username
		if not exists(select*from UserData where (username = @username or Email=@Email) and id not in (@id))
		begin
			update UserData
			set 
				username=@username,
				password=@password,
				hakakses=@hakakses,
				NamaLengkap=@NamaLengkap,
				Email=@Email,
				Gender=@Gender,
				NoHp=@NoHp,
				Photo=@Photo,
				Alamat=@Alamat,
				Status=@Status,
				TanggalLahir=@TanggalLahir,
				TempatLahir=@TempatLahir,
				Agama=@Agama,
				ScanKTP=@ScanKTP,
				JenisIdentitas=@JenisIdentitas,
				NoIdentitas=@NoIdentitas,
				NamaDivisi=@NamaDivisi,
				NamaPosisi=@NamaPosisi,
				TglAwalKerja=@TglAwalKerja,
				CreateDate=@CreateDate,
				ModifyDate=@ModifyDate,
				CreateBy=@CreateBy,
				ImgLink=@ImgLink
			where id=@id

			set @Title = 'Success'
			set @Message = 'User '+ @username + ' berhasil diupdate'
			set @MStatus = 'success'
		end
		else
		begin
			set @Title = 'Sorry'
			set @Message = 'User / Email already exists, silahkan cari nama lain'
			set @MStatus = 'error'
		end
	end
	
	
end

select @Title Title, @Message Message, @MStatus Status, @Id Id