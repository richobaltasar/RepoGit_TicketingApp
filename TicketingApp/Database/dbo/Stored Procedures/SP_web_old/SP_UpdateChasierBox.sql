
CREATE PROCEDURE SP_UpdateChasierBox
	@ComputerName	nvarchar(max),
	@TerimaUang	float,
	@Kembalian	float
AS
BEGIN
	SET NOCOUNT ON;

	declare @TotalChasin float
	declare @TotalChasout float
	declare @TotalUangDiBox float
	set @TotalChasin = ISNULL((select TotalUangMasuk from DataChasierBox where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and status =1),0)
	set @TotalChasout = ISNULL((select TotalUangKeluar from DataChasierBox where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and status =1),0)
	set @TotalUangDiBox = ISNULL((select TotalUangDiBox from DataChasierBox where NamaComputer = @ComputerName and left(Datetime,10) = FORMAT(GETDATE() , 'dd/MM/yyyy') and status =1),0)

	update DataChasierBox 
	set TotalUangMasuk =(@TotalChasin+@TerimaUang),
	TotalUangKeluar = (@TotalChasout+@Kembalian),
	TotalUangDiBox = ((@TotalChasin+@TerimaUang) - (@TotalChasout+@Kembalian))
	where NamaComputer = @ComputerName and left(Datetime,10) =   FORMAT(GETDATE() , 'dd/MM/yyyy')
	and status = 1

END
