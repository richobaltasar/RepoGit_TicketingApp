CREATE PROCEDURE SP_Edit_RAB
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
		if exists(select*from DataRAB where id = @id)
		begin
			declare @Omessage nvarchar(max)

			if(@StatusRAB > 1)
			begin
				if exists(select*from DataRAB_Item where IdRAB = @Id)
				begin
					update DataRAB 
				set 
					NamaRAB=@NamaRAB,
					JenisRAB=@JenisRAB,
					DescriptionRAB=@DescriptionRAB,
					TargetImplementDate=@TargetImplementDate,
					PIC1=@PIC1,
					PIC2=@PIC2,
					DisetujuiOleh=@DisetujuiOleh,
					Attachment1=@Attachment1,
					Attachment2=@Attachment2,
					Attachment3=@Attachment3,
					TotalBudget=@TotalBudget,
					EstimasiPurchase=@EstimasiPurchase,
					StatusRAB=@StatusRAB,
					IdUserBy=@IdUserBy
				where id =@id

				-- insert Log Activity
			
				select  @Omessage='Data  '+NamaRAB+' - '+JenisRAB+
				' dengan total budget :Rp '+REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,TotalBudget),1), '.00','')+' berhasil dirubah',
				@id = id from DataRAB where id = @id
				exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataRAB',@IdRow = @Id,@message=@Omessage,@Action='EDIT'
			
				select 'Succes' title, @Omessage message,'success' status
				end
				else
				begin
					select 'Sorry' title, 'Data Item RAB belom terisi,silahkan isi terlebih dahulu' message,'error' status
				end
			end
			else
			begin
				update DataRAB 
				set 
					NamaRAB=@NamaRAB,
					JenisRAB=@JenisRAB,
					DescriptionRAB=@DescriptionRAB,
					TargetImplementDate=@TargetImplementDate,
					PIC1=@PIC1,
					PIC2=@PIC2,
					DisetujuiOleh=@DisetujuiOleh,
					Attachment1=@Attachment1,
					Attachment2=@Attachment2,
					Attachment3=@Attachment3,
					TotalBudget=@TotalBudget,
					EstimasiPurchase=@EstimasiPurchase,
					StatusRAB=@StatusRAB,
					IdUserBy=@IdUserBy
				where id =@id

				-- insert Log Activity
			
				select  @Omessage='Data  '+NamaRAB+' - '+JenisRAB+
				' dengan total budget :Rp '+REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,TotalBudget),1), '.00','')+' berhasil dirubah',
				@id = id from DataRAB where id = @id
			
				exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataRAB',@IdRow = @Id,@message=@Omessage,@Action='EDIT'
			
				select 'Succes' title, @Omessage message,'success' status
			end

			
		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_RAB error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END