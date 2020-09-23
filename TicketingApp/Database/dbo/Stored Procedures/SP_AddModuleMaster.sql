
CREATE PROCEDURE SP_AddModuleMaster
	@NamaModule nvarchar(max),
    @Action nvarchar(max),
    @Controller nvarchar(max),
    @Img nvarchar(max),
    @Status bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if not exists(select*from DataModule where NamaModule = @NamaModule)
		begin
			insert into DataModule
			(NamaModule,Action,Controller,Img,Status)
			values(@NamaModule,@Action,@Controller,@Img,@Status)

			select 'Succes' title, 'Adding Modul '+@NamaModule+' is success' message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'Modul '+@NamaModule+' already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_AddModuleMaster error' title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'error' status
	END CATCH;  
END
