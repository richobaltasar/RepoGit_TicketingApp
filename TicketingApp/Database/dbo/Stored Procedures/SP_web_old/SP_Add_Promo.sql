---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Add_Promo
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
		if not exists(select*from DataPromo where NamaPromo =@NamaPromo)
		begin
			if(@JenisTicket = '' or @JenisTicket is null)
			begin
				select distinct @JenisTicket=namaticket from DataTicket where IdTicket = @IdJenisTicket
			end
			insert into DataPromo
			(NamaPromo,CategoryPromo,Diskon,Status,BerlakuDari,BerlakuSampai,Img,IdJenisTicket,JenisTicket,CreateDate)
			values(
				@NamaPromo,@CategoryPromo,@Diskon,@Status,@BerlakuDari,@BerlakuSampai,@Img,@IdJenisTicket,@JenisTicket,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')
			)
			
			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaPromo+' berhasil ditambah',@idPromo = idPromo from DataPromo where idPromo = SCOPE_IDENTITY()

			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataPromo',@IdRow = @idPromo,@message=@Omessage,@Action='ADD'

			if exists(select  * from DataPromo where idPromo = @idPromo)
			begin
				
				select 'Succes' title, @Omessage message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal insert' message,'error' status
			end

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_Promo error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON

