-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Function_SetLogActivity
	@IdUser bigint,
	@NamaTable nvarchar(max),
	@IdRow bigint,
	@message nvarchar(max),
	@Action nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	declare @Username nvarchar(max)
	select @Username=username from UserData where id = @IdUser

	insert into LogActivityUser (NamaTable,Message,IdRow,Action,UserBy,Datetime)
	values (@NamaTable,@message,@IdRow,@Action,@Username,FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'))
	
END
