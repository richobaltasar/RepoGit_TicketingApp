CREATE PROCEDURE SP_ParkirInWithTombolTicket_Simulasi
	@categoryKendaraan nvarchar(max),
	@Image1 nvarchar(max),
	@Image2 nvarchar(max),
	@Image3 nvarchar(max),
	@Image4 nvarchar(max),
	@GateID bigint
AS
BEGIN
	SET NOCOUNT ON;
	declare @title nvarchar(max)
	declare @message nvarchar(max)
	declare @status nvarchar(max)
	
	BEGIN TRY  
		declare @BarcodeId nvarchar(max)
		declare @ParkirCharges float

		select @BarcodeId =replace(cast(FORMAT(GETDATE() , 'yyyyMMdd HHmmss') as nvarchar(max)),' ','')+ cast(@GateID as nvarchar(10))

		if not exists(select*from LogParkir where BarcodeReciptCode = @BarcodeId)
		begin
			select @ParkirCharges = cast(val2 as float) from DataParam where NamaParam = 'ParkirCharges' and val1 = @categoryKendaraan

			insert into LogParkir
			(
				BarcodeReciptCode,
				Datetime,
				TypeKendaraan,
				GateID,
				In_Out_Status,
				Img1,
				Img2,
				Img3,
				Img4,
				Charges,
				Status
			)
			values
			(
				@BarcodeId,FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@categoryKendaraan,@GateID,
				'IN',@Image1,@Image2,@Image3,@Image4,@ParkirCharges,1
			)

			select @message = BarcodeReciptCode from LogParkir where Id = SCOPE_IDENTITY()
			set @title='SP_SaveLogTransaksiV2 berhasil'
			set @status='SUCCESS'
		end
		else
		begin
			set @message ='Barcode ID Already exists'
			set @title='Gagal'
			set @status='ERROR'
		end

		select @status status, @message message, @title title
	END TRY 
	BEGIN CATCH  
		select 'ERROR' status, 'Msg :'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'Error Exception' title
	END CATCH;  
END

