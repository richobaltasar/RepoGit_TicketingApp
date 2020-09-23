
CREATE PROCEDURE [dbo].[SP_Edit_SalesQuotationbyStatus]
	@IdQuotation bigint,
	@IdRab bigint,
	@NumberQuotationByVendor nvarchar(max),
	@CategoryPerusahaan nvarchar(max),
	@CompanyName nvarchar(max),
	@AlamatPerusahaan nvarchar(max),
	@Contact nvarchar(max),
	@Sales nvarchar(max),
	@TotalPenawaran float,
	@Perihal nvarchar(max),
	@Description nvarchar(max),
	@CreateDate nvarchar(max),
	@StatusQuotation  bigint,
	@Attachment1 nvarchar(max),
	@IdUserBy bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		declare @Omessage nvarchar(max)
		if exists(select*from DataQuotationVendor where IdQuotation = @IdQuotation)
		begin	
			if(@StatusQuotation > 1)
			begin
				if(@TotalPenawaran > 0)
				begin
					declare @totalbugetRAB float
					select @totalbugetRAB = TotalBudget from DataRAB where Id = @IdRab 
					if(@totalbugetRAB < @TotalPenawaran)
					begin
						set @Omessage = 'Total Penawaran tidak sesuai budget RAB' 
						goto Error	
					end
					else
					begin
						goto Success
					end
				end
				else
				begin
					set @Omessage = 'Total Penawaran masih 0, silahkan masukan item quotation' 
					goto Error
				end
			end
			else
			begin
				goto Success
			end
			

			Success:
			update DataQuotationVendor
			set 
				IdRab=@IdRab,
				NumberQuotationByVendor=@NumberQuotationByVendor,
				CategoryPerusahaan=@CategoryPerusahaan,
				CompanyName=@CompanyName,
				AlamatPerusahaan=@AlamatPerusahaan,
				Contact=@Contact,
				Sales=@Sales,
				TotalPenawaran=@TotalPenawaran,
				Perihal=@Perihal,
				Description=@Description,
				CreateDate=@CreateDate,
				StatusQuotation=@StatusQuotation,
				Attachment1=@Attachment1
			where  IdQuotation = @IdQuotation

			if(@IdRab != 0)
			begin 
				if not exists(select*from DataQuotationVendor_Item where IdQuotation=@IdQuotation and Idrab = @IdRab) 
				begin
					select* into #temp from DataRAB_Item where IdRAB = @IdRab
					while((select count(*) from #temp)> 0)
					begin
						declare @id bigint
						declare @IdRABL	bigint
						declare @Category	nvarchar(max)
						declare @NamaItem	nvarchar(max)
						declare @Satuan	nvarchar(max)
						declare @Unit	float
						declare @Harga	float
						declare @SubTotal float
						declare @Status bigint

						select top 1 @id = Id, 
						@IdRABL = idRAB,
						@Category=Category,
						@NamaItem=NamaItem,
						@Satuan=Satuan,
						@Unit=Unit,
						@Harga=Harga,
						@SubTotal=SubTotal,
						@Status=Status
						from #temp

						if not exists(select*from DataQuotationVendor_Item where NamaItem = @NamaItem and Category = @Category and IdQuotation = @IdQuotation)
						begin
							insert into DataQuotationVendor_Item
							(IdQuotation,IdRab,IdItemRAB,Category,NamaItem,Satuan,Unit,Status)
							values(
							@IdQuotation,@IdRABL,@id,@Category,@NamaItem,@Satuan,@Unit,@Status
							)
						end

						delete #temp where Id = @id
					end
				end
			end

			-- insert Log Activity
			
			select  @Omessage='Sales  '+Perihal+' - '+CompanyName+' No. Quot :'+NumberQuotationByVendor+' berhasil ditambah',@IdQuotation = IdQuotation from DataQuotationVendor where IdQuotation = @IdQuotation
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataQuotationVendor',@IdRow = @IdQuotation,@message=@Omessage,@Action='EDIT'

			select 'Succes' title, @Omessage message,'success' status

			Error:
				
				select 'Sorry' title, @Omessage message,'error' status
		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Edit_SalesQuotationbyStatus error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH;  
END

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
