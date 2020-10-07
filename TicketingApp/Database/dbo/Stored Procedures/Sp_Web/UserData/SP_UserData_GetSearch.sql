CREATE PROCEDURE [dbo].[SP_UserData_GetSearch]
	--@id bigint,
	@username nvarchar(max),
	--@password nvarchar(max),
	--@hakakses nvarchar(max),
	--@NamaLengkap nvarchar(max),
	--@Email nvarchar(max),
	--@Gender nvarchar(max),
	--@NoHp nvarchar(max),
	--@Photo nvarchar(max),
	--@Alamat nvarchar(max),
	--@Status bigint,
	--@TanggalLahir nvarchar(max),
	--@TempatLahir nvarchar(max),
	--@Agama nvarchar(max),
	--@ScanKTP nvarchar(max),
	--@JenisIdentitas nvarchar(max),
	--@NoIdentitas nvarchar(max),
	@NamaDivisi nvarchar(max)
	--@NamaPosisi nvarchar(max),
	--@TglAwalKerja nvarchar(max),
	--@CreateDate nvarchar(max),
	--@ModifyDate nvarchar(max),
	--@CreateBy nvarchar(max),
	--@ImgLink nvarchar(max)
AS
	select*from UserData
	where REPLACE(RTRIM(LTRIM(username)),' ','') like '%'+REPLACE(RTRIM(LTRIM(@username)),' ','')+'%'
	and REPLACE(RTRIM(LTRIM(NamaDivisi)),' ','') like '%'+REPLACE(RTRIM(LTRIM(@NamaDivisi)),' ','')+'%'
