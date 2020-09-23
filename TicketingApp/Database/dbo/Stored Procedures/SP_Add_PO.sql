CREATE PROCEDURE SP_Add_PO
	@IdPO bigint,
	@IdQuotation bigint,
	@LastPrint nvarchar(max),
	@TotalPO float,
	@CreateDatePO nvarchar(max),
	@SendPODate nvarchar(max),
	@NoWhatsAppSales nvarchar(max),
	@PhoneCode nvarchar(max),
	@StatusPO bigint,
	@IdUserBy bigint
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY  
		declare @Omessage nvarchar(max)

		if not exists(select*from DataPO where IdQuotation = @IdQuotation)
		begin
			if(@StatusPO > 1)
			begin
				if(@TotalPO > 0)
				begin
					declare @totalbugetRAB float
					select @totalbugetRAB = TotalBudget from DataRAB where Id in (select IdRab from DataQuotationVendor where IdQuotation = @IdQuotation)
					
					if(@totalbugetRAB < @TotalPO)
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
					set @Omessage = 'Total Penawaran masih 0, silahkan masukan item Purchase Order' 
					goto Error
				end
			end
			else
			begin
				goto Success
			end
			
			Success:
			
			insert into DataPO
			(
				IdQuotation,
				LastPrint,
				TotalPO,
				CreateDatePO,
				SendPODate,
				NoWhatsAppSales,
				StatusPO,
				PhoneCode
			)
			values(
				@IdQuotation,
				@LastPrint,
				@TotalPO,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				@SendPODate,
				@NoWhatsAppSales,
				@StatusPO,
				@PhoneCode
			)

			set @IdPO = (select IdPO from DataPO where IdPO = SCOPE_IDENTITY())

			if(@IdPO != 0)
			begin 
				if not exists(select*from DataPO_Item where IdQuotation =@IdQuotation) 
				begin
					select* into #temp from DataQuotationVendor_Item where IdQuotation = @IdQuotation
					while exists(select * from #temp)
					begin
						declare @IdQuot bigint
						declare @idItem bigint
						declare @Category nvarchar(max)
						declare @NamaItem nvarchar(max)
						declare @Satuan nvarchar(max)
						declare @Unit float
						declare @Harga float
						declare @SubTotal float
						declare @Attachment1 nvarchar(max)
						declare @Status nvarchar(max)

						select top 1 @idItem = Id, 
						@IdQuot = IdQuotation,
						@Category=Category,
						@NamaItem=NamaItem,
						@Satuan=Satuan,
						@Unit=Unit,
						@Harga=Harga,
						@SubTotal=SubTotal,
						@Status=Status
						from #temp

						if not exists(select*from DataPO_Item where NamaItem = @NamaItem and Category = @Category)
						begin
							insert into DataPO_Item
							(	
								IdPO,
								IdQuotation,
								IdItemQuotation,
								Category,
								NamaItem,
								Satuan,
								Unit,
								Harga,
								SubTotal,
								Attachment1,
								Status
							)
							values(
								@IdPO,
								@IdQuot,
								@idItem,
								@Category,
								@NamaItem,
								@Satuan,
								@Unit,
								@Harga,
								@SubTotal,
								@Attachment1,
								@Status
							)
						end

						delete #temp where Id = @idItem
					end
				end
			end

			-- insert Log Activity

			select  @Omessage='Sales  '+Perihal+' - '+CompanyName+' No. Quot :'+NumberQuotationByVendor+' berhasil ditambahkan pada list PO'
			from DataQuotationVendor where IdQuotation in (select IdQuotation from DataPO where IdPO = @IdPO)
			
			exec dbo.SP_Function_SetLogActivity @IdUser=@IdUserBy,@NamaTable='DataPO',@IdRow = @IdPO,@message=@Omessage,@Action='ADD'

			if exists(select  * from DataPO where IdPO = @IdPO)
			begin
				select 'Succes' title, @Omessage message,'success' status
			end
			else 
			begin
				select 'Sorry' title, 'data gagal insert' message,'error' status
			end
			
			RETURN

			Error:	
				select 'Sorry' title, @Omessage message,'error' status
				RETURN

		end
		else
		begin
			select 'Sorry' title, 'data is already exists' message,'error' status
		end
	END TRY  
	BEGIN CATCH  
		select 'Sorry SP_Add_PO error' as title, 'Code:'+STR(ERROR_NUMBER())+' -' +ERROR_MESSAGE() as message,'error' as status
	END CATCH
END

	

SET ANSI_NULLS ON
