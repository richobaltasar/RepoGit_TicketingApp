
--exec SP_DELETE_ALL '10/03/2020'

CREATE PROCEDURE [dbo].[SP_DELETE_ALL]
	@Tgl nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	delete from [dbo].[DataAccount] where left(CreateDate,10) = @Tgl
	delete from [dbo].[DataChasierBox] where left(Datetime,10)= @Tgl
	delete from [dbo].[LogClosing] where LEFT(datetime,10)= @Tgl
	delete from [dbo].[LogEmoneyTrxAccount] where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogFoodcourtTransaksi] where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogItemsF&BTrx]  where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogRefundDetail]  where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogRegistrasiDetail]  where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogTicketDetail]  where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogTopupDetail]  where LEFT(Datetime,10)= @Tgl
	delete from LogCashierTambahModal  where LEFT(Datetime,10)= @Tgl
	delete from [LogDeposit]  where LEFT(Datetime,10)= @Tgl
	delete from LogEDCTransaksi  where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogSetoranDepositExpired]  where LEFT(Datetime,10)= @Tgl
	delete from [dbo].[LogStokOpname]  where LEFT(Datetime,10)= @Tgl
	delete from DataDeposit  where LEFT(Datetime,10)= @Tgl
	delete from LogClosingV2 where LEFT(Datetime,10)= @Tgl
	delete from LogTransaksiCancel where left(CancelDate,10)=@Tgl

	delete from LogCancelRegistrasiDetail where LEFT(Datetime,10)= @Tgl
	delete from LogCancelFoodcourtTransaksi where LEFT(Datetime,10)= @Tgl
	delete from LogCancelTopupDetail where LEFT(Datetime,10)= @Tgl
	delete from [LogCancelItemsF&BTrx] where LEFT(Datetime,10)= @Tgl
END












