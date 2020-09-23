CREATE PROCEDURE SP_UpdateLogTransaksiPosStatus
	@idtrx bigint,
	@status bigint
AS
BEGIN
	SET NOCOUNT ON;

	update LogTransaksiPOS
	set Status = @status
	where idTrx = @idtrx
	
	declare @Tunai float
	declare @Kembalian float
	declare @MerchantName nvarchar(max)
	declare @DatetimeO nvarchar(max)
	declare @ChasierName nvarchar(max)

	select 
	@Tunai=Tunai,
	@Kembalian = Kembalian,
	@MerchantName = MerchantName,
	@DatetimeO = Datetime,
	@ChasierName = ChasierName

	from LogTransaksiPOS a where idTrx = @idtrx and Status=1

	if(@Tunai > 0)
	begin
		EXECUTE SP_Update_DataChasierBox @MerchantName,@DatetimeO,@ChasierName
	end

	select
		*
	from LogTransaksiPOS a
	where Status = @status and idTrx = @idtrx



END
