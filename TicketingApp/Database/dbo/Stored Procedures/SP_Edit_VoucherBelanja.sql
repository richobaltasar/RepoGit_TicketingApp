---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Edit_VoucherBelanja
	@id bigint,
	@NamaVoucher nvarchar(max),
	@JenisVoucher nvarchar(max),
	@NominalVoucher float,
	@CodeVoucher nvarchar(max),
	@Img nvarchar(max),
	@Description nvarchar(max),
	@Status bigint,
	@BerlakuDari nvarchar(max),
	@BerlakuSampai nvarchar(max),
	@CreateBy nvarchar(max),
	@IdUser bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if exists(select*from DataVoucher where id = @id)
		begin
			update DataVoucher 
			set 
				NamaVoucher=@NamaVoucher,
				JenisVoucher=@JenisVoucher,
				NominalVoucher=@NominalVoucher,
				CodeVoucher=@CodeVoucher,
				Img=@Img,
				Description=@Description,
				Status=@Status,
				BerlakuDari=@BerlakuDari,
				BerlakuSampai=@BerlakuSampai,
				ModifyDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				CreateBy=@CreateBy
			where id =@id

			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaVoucher+' berhasil dirubah',@id = id from DataVoucher where id = @id
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataVoucher',@IdRow = @Id,@message=@Omessage,@Action='EDIT'
			
			select 'Succes' title, @Omessage message,'success' status
		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_VoucherBelanja error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON

SET ANSI_NULLS ON
