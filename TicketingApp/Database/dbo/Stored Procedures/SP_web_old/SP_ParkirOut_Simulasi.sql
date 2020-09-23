
CREATE PROCEDURE SP_ParkirOut_Simulasi
	@AccountNumber nvarchar(max),
	@GateID bigint
AS
BEGIN
	SET NOCOUNT ON;
	declare @title nvarchar(max)
	declare @message nvarchar(max)
	declare @status nvarchar(max)
	BEGIN TRY  
    
	if exists(select*from LogParkir where AccountNumber = @AccountNumber and Status = 2)
	begin
		update LogParkir 
		set 
		DatetimeOut = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
		GateOut = @GateID,
		In_Out_Status = 'OUT',
		Img1Out = 'ParkirB3364BTT.jpeg',
		Img2Out = 'ParkirB3364BTT.jpeg',
		Img3Out = 'ParkirB3364BTT.jpeg',
		Img4Out = 'ParkirB3364BTT.jpeg',
		Status = 3
		where 
		AccountNumber = @AccountNumber and Status = 2
		
		select @message = 'Update Success'
		set @title='SP_ParkirOut_Simulasi berhasil'
		set @status='SUCCESS'
	end
	else
	begin
		select @message = 'AccountNumber belom checkout'
		set @title='SP_ParkirOut_Simulasi Gagal'
		set @status='ERROR'
	end
	select @status status, @message message, @title title
	END TRY 
	BEGIN CATCH  
		select 'ERROR' status, 'Msg :'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'Error Exception' title
	END CATCH;  
END
