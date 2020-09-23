---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Edit_Promo
	@idPromo bigint,
	@NamaPromo nvarchar(max),
	@CategoryPromo nvarchar(max),
	@Diskon float,
	@Status bigint,
	@BerlakuDari nvarchar(max),
	@BerlakuSampai nvarchar(max),
	@Img nvarchar(max),
	@IdJenisTicket bigint,
	@JenisTicket nvarchar(max),
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataPromo where @idPromo = @idPromo)
		begin
			if(@JenisTicket = '' or @JenisTicket is null)
			begin
				select distinct @JenisTicket=namaticket from DataTicket where IdTicket = @IdJenisTicket
			end

			update DataPromo
			set	
				NamaPromo = @NamaPromo,
				CategoryPromo = @CategoryPromo,
				Diskon = @Diskon,
				Status = @Status,
				BerlakuDari=@BerlakuDari,
				BerlakuSampai = @BerlakuSampai,
				Img = @Img,
				IdJenisTicket=@IdJenisTicket,
				JenisTicket = @JenisTicket,
				ModifyDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')
			where idPromo = @idPromo

			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaPromo +' dengan diskon :'+CAST(Diskon as nvarchar(max))+'% berhasil diubah' from DataPromo where idPromo = @idPromo

			-- insert Log Activity
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataPromo',@IdRow = @idPromo,@message=@Omessage,@Action='EDIT'


			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data is not exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_Promo error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON

