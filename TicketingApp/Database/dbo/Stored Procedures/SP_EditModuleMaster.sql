CREATE PROCEDURE SP_EditModuleMaster
	@NamaModule nvarchar(max),
    @Action nvarchar(max),
    @Controller nvarchar(max),
    @Img nvarchar(max),
    @Status bigint,
	@IdModul bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataModule where IdModul = @IdModul)
		begin
		if(@Img = '')
		begin
			update DataModule
			set
				NamaModule = @NamaModule,
				Action = @Action,
				Controller = @Controller,
				Status = @Status
			where IdModul =@IdModul
		end
		else
		begin
			update DataModule
			set
				NamaModule = @NamaModule,
				Action = @Action,
				Controller = @Controller,
				Img = @Img,
				Status = @Status
			where IdModul =@IdModul
		end
			select 'Succes' title, 'Update Modul '+@NamaModule+' is success' message,'success' status

		end
		else
		begin
			select 'Sorry' title, 'Modul '+@NamaModule+' doesn''t exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_EditModuleMaster error' title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() message,'error' status
	END CATCH;  
END
