CREATE PROCEDURE [dbo].[SP_SaveTicketKeranjang]
	@AccountNumber nvarchar(max),
	@IdTicket bigint,
	@NamaTicket nvarchar(max),
	@Harga float,
	@Qty float,
	@Total float,
	@IdDiskon bigint,
	@NamaDiskon nvarchar(max),
	@Diskon float,
	@TotalDiskon float,
	@TotalAfterDiskon float,
	@ChasierBy nvarchar(max),
	@ComputerName nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	declare @asuransi float

	select @asuransi=convert(float,val2) from DataParam where NamaParam = 'Asuransi' and val1 = 'Asuransi Kecelakaan'

	set @asuransi = @asuransi*@Qty

	insert into LogTicketDetail 
	(Datetime,IdTicket,NamaTicket,Harga,Qty,Total,IdDiskon,NamaDiskon,Diskon,TotalDiskon,TotalAfterDiskon,status,
	[ChasierBy],[ComputerName],AccountNumber,Asuransi)
	values(
		FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@IdTicket,@NamaTicket,@Harga,@Qty,@Total,
		@IdDiskon,@NamaDiskon,@Diskon,@TotalDiskon,(@TotalAfterDiskon+@asuransi),1,
		@ChasierBy,@ComputerName,@AccountNumber,@asuransi
	)

	select 'Insert LogticketDetail success' as _Message, 'TRUE' as Success
END