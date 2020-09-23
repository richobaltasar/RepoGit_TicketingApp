CREATE PROCEDURE SP_Add_RAB
	@Id nvarchar(max),
	@NamaRAB nvarchar(max),
	@JenisRAB nvarchar(max),
	@DescriptionRAB nvarchar(max),
	@TargetImplementDate nvarchar(max),
	@PIC1 bigint,
	@PIC2 bigint,
	@DisetujuiOleh bigint,
	@Attachment1 nvarchar(max),
	@Attachment2 nvarchar(max),
	@Attachment3 nvarchar(max),
	@TotalBudget float,
	@EstimasiPurchase float,
	@StatusRAB bigint,
	@IdUserBy bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		if not exists(select*from DataRAB where NamaRAB = @NamaRAB and JenisRAB = @JenisRAB and TargetImplementDate = @TargetImplementDate 
			and TotalBudget = @TotalBudget and EstimasiPurchase = @EstimasiPurchase)
		begin
			insert into DataRAB
			(
				NamaRAB,
				JenisRAB,
				DescriptionRAB,
				CreateDate,
				TargetImplementDate,
				PIC1,
				PIC2,
				DisetujuiOleh,
				Attachment1,
				Attachment2,
				Attachment3,
				TotalBudget,
				EstimasiPurchase,
				StatusRAB,
				IdUserBy
			)
			values(
				@NamaRAB,
				@JenisRAB,
				@DescriptionRAB,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				@TargetImplementDate,
				@PIC1,
				@PIC2,
				@DisetujuiOleh,
				@Attachment1,
				@Attachment2,
				@Attachment3,
				@TotalBudget,
				@EstimasiPurchase,
				1,
				@IdUserBy
			)
			
			-- insert Log Activity
			declare @Omessage nvarchar(max)
			select  @Omessage='Data  '+NamaRAB+' - '+JenisRAB+' total budget :Rp '+CAST(TotalBudget as nvarchar(max))+' berhasil ditambah',@id = id from DataRAB where id = SCOPE_IDENTITY()
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataRAB',@IdRow = @Id,@message=@Omessage,@Action='ADD'

			if exists(select  * from DataRAB where id = @id)
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
		select 'Sorry SP_Add_RAB error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END