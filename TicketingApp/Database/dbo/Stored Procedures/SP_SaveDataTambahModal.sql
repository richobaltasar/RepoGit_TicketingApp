CREATE PROCEDURE [dbo].[SP_SaveDataTambahModal]
	@ComputerName Nvarchar(max),
	@NamaUser nvarchar(max),
	@Nominal float
AS
BEGIN
	BEGIN TRY  
		SET NOCOUNT ON;
		declare @msg nvarchar(max)
		declare @IdMaster bigint

		if exists(select*from [dbo].[DataChasierBox] 
				where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and status = 1)
		begin
			declare @DanaSebelumnya float
			declare @TotalUangDiBox float
			declare @TotalUangMasuk float

			set @DanaSebelumnya = (select DanaModalSetelah from DataChasierBox where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and Status = 1)
			set @TotalUangDiBox = (select TotalUangDiBox from DataChasierBox where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and Status = 1)
			set @TotalUangMasuk = (select TotalUangMasuk from DataChasierBox where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and Status = 1)

			update DataChasierBox 
			set DanaModalSebelum =  @DanaSebelumnya,
			DanaModalSetelah = (@DanaSebelumnya + @Nominal),
			TotalUangDiBox = (@TotalUangDiBox + @Nominal),
			TotalUangMasuk = (@TotalUangMasuk + @Nominal ),
			UpdateBy = @NamaUser
			where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy')
			and status = 1
			
			
			select @IdMaster=IdModal from  DataChasierBox
			where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy')
			and status = 1

			set @msg = 'Update dana Cashbox berhasil'
			-- save log tambah modal
			insert into [dbo].[LogCashierTambahModal]
			([Datetime],[NamaComputer],[NamaUser],[NominalTambahModal],[Status],IdMaster)
			values(
			FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
			@ComputerName,@NamaUser,@Nominal,1,@IdMaster)
		end
		else
		begin
			insert into DataChasierBox
			([Datetime],[NamaComputer],[DanaModalSebelum],[DanaModalSetelah],
			[TotalUangDiBox],[TotalUangMasuk],[Status],
			[OpenBy])
			values(FORMAT(GETDATE() , 'dd/MM/yyyy'), @ComputerName, 0, @Nominal,
			@Nominal,@Nominal,1,@NamaUser)

			select distinct @IdMaster = IdModal  from DataChasierBox where  IdModal= SCOPE_IDENTITY()

			-- save log tambah modal
			insert into [dbo].[LogCashierTambahModal]
			([Datetime],[NamaComputer],[NamaUser],[NominalTambahModal],[Status],IdMaster)
			values(FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@ComputerName,@NamaUser,@Nominal,1,@IdMaster)
			set @msg = 'Insert dana ke Cashbox berhasil'
		end

		select @msg as _Message, 'TRUE' as Success
	END TRY 
	BEGIN CATCH  
		select 'Error SP_SaveDataTambahModal : ex. '+'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as _Message, 'FALSE' as Success
	END CATCH;  
	
END
