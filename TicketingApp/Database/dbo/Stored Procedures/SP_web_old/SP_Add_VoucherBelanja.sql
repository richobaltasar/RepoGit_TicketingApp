---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE SP_Add_VoucherBelanja
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
		if not exists(select*from DataVoucher where NamaVoucher = @NamaVoucher)
		begin
			insert into DataVoucher
			(
				NamaVoucher,	JenisVoucher,	NominalVoucher,	CodeVoucher,	Img,	
				Description,	Status,	BerlakuDari,	BerlakuSampai,	CreateDate,	
				CreateBy
			)
			values(
				@NamaVoucher,
				@JenisVoucher,
				@NominalVoucher,
				@CodeVoucher,
				@Img,
				@Description,
				@Status,
				@BerlakuDari,
				@BerlakuSampai,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				@CreateBy
			)
			
			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaVoucher+' berhasil ditambah',@id = id from DataVoucher where id = SCOPE_IDENTITY()
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUser,@NamaTable='DataVoucher',@IdRow = @Id,@message=@Omessage,@Action='ADD'

			if exists(select  * from DataVoucher where id = @id)
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
		select 'Sorry SP_Add_VoucherBelanja error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

SET ANSI_NULLS ON

SET ANSI_NULLS ON
