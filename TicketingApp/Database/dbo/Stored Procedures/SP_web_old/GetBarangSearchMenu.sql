CREATE PROCEDURE GetBarangSearchMenu
	@IdTenant  nvarchar(max),
	@Search nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	declare @IdTenantq bigint
	declare @NamaTenant nvarchar(max)
	
	if exists(select*from DataTenant where NamaTenant = @IdTenant and StatusJual = 'Karyawan' 
	and StatusKepemilikan = 'Management' and FollowTenant != '')
	begin
		
		set @NamaTenant = (select FollowTenant from DataTenant where NamaTenant= @NamaTenant and StatusJual = 'Karyawan' and StatusKepemilikan = 'Management' and FollowTenant != '')
		set @IdTenantq = (select idTenant from DataTenant where NamaTenant = @NamaTenant)
		
		select idMenu,NamaMenu,HargaKaryawan as HargaJual,
		'UploadImages/'+ImgLink ImgLink,Stok from DataBarang 
		where IdTenant = @IdTenantq
		and Status = 'Aktif'
		and NamaMenu like '%'+@Search+'%'
	end
	else
	begin
		print 1
		
		select idMenu,NamaMenu,HargaJual,
		'UploadImages/'+ImgLink ImgLink,Stok from DataBarang 
		where IdTenant in (select idTenant from DataTenant where NamaTenant =@IdTenant)
		and Status = 'Aktif'
		and NamaMenu like '%'+@Search+'%'

	end

	--select idMenu,NamaMenu,HargaJual,ImgLink,Stok from DataBarang 
	--where IdTenant in (select idTenant from DataTenant where NamaTenant ='Ayam Crispy')
	----and Status = 'Aktif'
	--and NamaMenu like '%N%'
END