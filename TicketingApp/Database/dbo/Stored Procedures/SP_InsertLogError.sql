
CREATE PROCEDURE SP_InsertLogError 
	@MessageError nvarchar(max),
	@FuncionName nvarchar(max),
	@Parameter nvarchar(max),
	@Username nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		declare @Succes int
		declare @Message nvarchar(max)

		Insert into logErrorMessage
		(
			MessageError,FunctionName,Parameter,Datetime,Username
		)
		values
		(
			@MessageError,@FuncionName,@Parameter,FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@Username
		)
	END TRY 
    BEGIN CATCH  
		set @Succes = 0
		set @Message = 'SP error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()
		select @Message  _Message,@Succes _Success
	END CATCH;  
END
