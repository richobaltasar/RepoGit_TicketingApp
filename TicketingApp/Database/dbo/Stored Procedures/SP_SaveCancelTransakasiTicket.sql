CREATE PROCEDURE [dbo].[SP_SaveCancelTransakasiTicket]
	@IdTrx bigint,
	@AccountNumber nvarchar(max),
	@SaldoEmoney float,
	@AuthorizeBy nvarchar(max),
	@NamaKasir nvarchar(max), 
	@ComputerName nvarchar(max)
AS BEGIN 
	BEGIN TRY  
		declare @TipeTransaksi nvarchar(MAX)
		declare @PaymentMethod nvarchar(MAX)
		declare @TotalTransaksi float
		declare @RAccountNumber nvarchar(MAX)
		declare @NamaKasirYgCancel nvarchar(MAX)
		declare @NamaKasirYangInputTrx nvarchar(MAX)
		declare @Authorize nvarchar(MAX)
		declare @TransactionDate nvarchar(MAX)
		declare @CancelDate nvarchar(50)
		declare @IdTransaksi bigint
		declare @PayTunai float
		declare @PayEmoney float
		declare @DescriptionTransaksi nvarchar(MAX)
		declare @PayEDC float

		declare @Succes int
		declare @Message nvarchar(max)

		if exists(select*from LogRegistrasiDetail where idTrx = @IdTrx)
		begin
			if exists(select*from LogTransaksiCancel where IdTransaksi=@IdTrx)
			begin
				set @Succes = 0
				set @Message = 'Transaksi idTrx sudah melakukan Cancel'
			end
			else
			begin
				declare @JaminanBefore float
				declare @JenisTransaksi nvarchar(max)
				declare @IdTicket bigint
				declare @Topup float
				declare @Cashback float
				declare @Asuransi float
				declare @QtyTicket float
				declare @balanced float
				declare @ticket float
				declare @Debit float
				declare @Credit float
				declare @Deposit float
				declare @DepositYesterday float
				declare @TotalUangDiBox float
				declare @TotalUangMasuk float
				declare @TotalUangKeluar float

				declare @OutTicket float
				declare @OutSaldoEmoney float
				declare @OutSaldoJaminan float

				select @IdTicket=IdTicketTrx,@TotalTransaksi=TotalBayar,@AccountNumber=AccountNumber,@NamaKasirYangInputTrx=CashierBy,
				@NamaKasirYgCancel=@NamaKasir,@Authorize=@AuthorizeBy,@TransactionDate=Datetime,
				@CancelDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),@IdTransaksi = idTrx,@PayTunai=PayCash,
				@PayEmoney=PayEmoney,@PayEDC=TotalDebit,@Topup=Topup,@Cashback=Cashback,@Asuransi=Asuransi,@QtyTicket=QtyTotalTiket,
				@JaminanBefore =SaldoJaminan,@JenisTransaksi =JenisTransaksi
				--select*
				from LogRegistrasiDetail 
				where idTrx=@IdTrx	

				select NamaTicket+' | '+cast(Harga as nvarchar(50))+' | '+cast(Qty as nvarchar(50)) +' | '+cast(Total as nvarchar(50))+
									 ' | '+NamaDiskon+' | '+cast(TotalDiskon as nvarchar(50)) +' | '+cast(TotalAfterDiskon as nvarchar(50)) Deskripsi
				into #TempA
				from LogTicketDetail where IdTicket = @IdTicket

				insert into LogCancelTicketDetail
				select*from LogTicketDetail where IdTicket = @IdTicket

				set @DescriptionTransaksi = ''
								
				while EXISTS (select*from #TempA)
				begin
					declare @desc nvarchar(max)
					select TOP 1 @desc = Deskripsi from #TempA
					set @DescriptionTransaksi = @DescriptionTransaksi + @desc +'~'
					delete from #TempA where Deskripsi = @desc
				end

				if(@Asuransi > 0 )
				begin
					declare @descAsuransi nvarchar(max)
					set @descAsuransi = 'Asuransi | - |'+cast(@QtyTicket as nvarchar(50))+ '|-|-|-| '+ cast(@Asuransi as nvarchar(50))
					set @DescriptionTransaksi = @DescriptionTransaksi + @descAsuransi +'~'
				end

				if(@Topup > 0)
				begin
					declare @descTopup nvarchar(max)
					set @descTopup = 'Topup | | | | | |'+cast(@Topup as nvarchar(50))
					set @DescriptionTransaksi = @DescriptionTransaksi + @descTopup +'~'
				end

				if(@Cashback > 0)
				begin
					declare @desccashback nvarchar(max)
					set @desccashback = 'Cashback | | | | | | -'+ cast(@Cashback as nvarchar(max))
					set @DescriptionTransaksi = @DescriptionTransaksi + @desccashback +'~'
				end

				if(@JaminanBefore = 0)
				begin
					if not exists((select*from LogTopupDetail where AccountNumber = @AccountNumber))
					begin
						if not exists((select*from LogFoodcourtTransaksi where AccountNumber = @AccountNumber))
						begin
							declare @totalTrxReg bigint
							set @totalTrxReg = (select count(*) total from LogRegistrasiDetail 
							where AccountNumber in (select AccountNumber from LogRegistrasiDetail where idTrx=@IdTrx))
							if(@totalTrxReg<=1)
							begin
								if(@JenisTransaksi = 'Cash')
								begin
									set @PaymentMethod = @JenisTransaksi
									set @TipeTransaksi = 'TICKET+KARTU'
									
									declare @descJaminan nvarchar(max)
									declare @SaldoJaminan float

									select @SaldoJaminan=SaldoJaminanAfter from LogRegistrasiDetail where idTrx = @IdTrx
									set @descJaminan = 'JAMINAN KARTU | | | | | |'+ cast(@SaldoJaminan as nvarchar(max))
									set @DescriptionTransaksi = @DescriptionTransaksi + @descJaminan +'~'

									set @OutSaldoJaminan = 0
									set @OutSaldoEmoney = 0
									set @OutTicket = 0

									drop table #TempA
									insert into LogCancelRegistrasiDetail
									select*from LogRegistrasiDetail where idTrx = @IdTrx
									
									delete from LogRegistrasiDetail where idTrx = @IdTrx
									delete from LogTicketDetail where IdTicket = @IdTicket
									delete from DataAccount where AccountNumber = @AccountNumber
									delete from LogDeposit where AccountNumber = @AccountNumber
								
									set @Succes = 1
								end	
								else
								begin
									set @Succes = 0
									set @Message = 'Maaf, karena akun ini adalah kartu baru tidak mungkin memiliki emoney'
								end
							end
							else
							begin
								set @Succes = 0
								set @Message = 'Account ini telah melakukan lebih dari 1x transaksi pada Registrasi, transaksi cancel untuk ID : REG'+cast(@IdTrx as nvarchar(max))+' tidak bisa dilakukan.'	
							end
						end
						else
						begin
							set @Succes = 0
							set @Message = 'Account ini telah melakukan transaksi pada Tenant, transaksi cancel tidak bisa dilakukan.'
						end
					end
					else
					begin
						set @Succes = 0
						set @Message = 'Account ini telah melakukan transaksi topup, transaksi cancel tidak bisa dilakukan.'
					end
				end
				else
				begin
					if(@JenisTransaksi = 'Cash')
					begin
						set @PaymentMethod = @JenisTransaksi
						set @TipeTransaksi = 'TICKET'
						
						drop table #TempA
						insert into LogCancelRegistrasiDetail
						select*from LogRegistrasiDetail where idTrx = @IdTrx

						delete from LogRegistrasiDetail where idTrx = @IdTrx
						delete from LogTicketDetail where IdTicket = @IdTicket
						
						select @balanced=Balanced,@ticket=Ticket from DataAccount where AccountNumber = @AccountNumber

						if(@balanced >= @Topup)
						begin
							set @balanced = @balanced - @Topup
						end

						if(@ticket >= @QtyTicket )
						begin
							set @ticket = @ticket - @QtyTicket
						end

						select @OutSaldoJaminan=isnull(UangJaminan,0) from DataAccount where AccountNumber = @AccountNumber
						set @OutSaldoEmoney = @balanced
						set @OutTicket = @ticket

						update DataAccount 
						set Balanced = @balanced, Ticket=@ticket
						where AccountNumber = @AccountNumber
						
						set @Succes = 1
					end	
					else if(@JenisTransaksi='eMoney')
					begin
						set @PaymentMethod = @JenisTransaksi
						set @TipeTransaksi = 'TICKET'
						
						drop table #TempA
						insert into LogCancelRegistrasiDetail
						select*from LogRegistrasiDetail where idTrx = @IdTrx

						delete from LogRegistrasiDetail where idTrx = @IdTrx
						delete from LogTicketDetail where IdTicket = @IdTicket
						
						select @balanced=Balanced,@ticket=Ticket from DataAccount where AccountNumber = @AccountNumber

						--select*from DataAccount where AccountNumber='32250697-20200304092347'

						if(@PayEmoney > 0)
						begin
							set @balanced = (@balanced+@PayEmoney)
						end

						if(@Topup > 0)
						begin
							set @balanced = (@balanced - @Topup)
						end

						if(@ticket >= @QtyTicket )
						begin
							set @ticket = @ticket - @QtyTicket
						end

						select @OutSaldoJaminan=UangJaminan from DataAccount where AccountNumber = @AccountNumber
						set @OutSaldoEmoney = @balanced
						set @OutTicket = @ticket

						update DataAccount 
						set Balanced = @balanced, Ticket=@ticket
						where AccountNumber = @AccountNumber

						delete from LogDeposit where AccountNumber=@AccountNumber
						and Datetime = @TransactionDate and TransactionType = 'DEBIT'

						if(@Topup > 0)
						begin
							delete from LogDeposit where AccountNumber=@AccountNumber
							and Datetime = @TransactionDate and TransactionType = 'CREDIT' and Nominal = @Topup	
						end

						set @Succes = 1
					end
					else if(@JenisTransaksi = 'eMoney & Cash')
					begin
						set @PaymentMethod = @JenisTransaksi
						set @TipeTransaksi = 'TICKET'
						
						drop table #TempA
						insert into LogCancelRegistrasiDetail
						select*from LogRegistrasiDetail where idTrx = @IdTrx

						delete from LogRegistrasiDetail where idTrx = @IdTrx
						delete from LogTicketDetail where IdTicket = @IdTicket
						
						select @balanced=Balanced,@ticket=Ticket from DataAccount where AccountNumber = @AccountNumber

						if(@PayEmoney > 0)
						begin
							set @balanced = (@balanced+@PayEmoney)
						end

						if(@Topup > 0)
						begin
							set @balanced = (@balanced - @Topup)
						end

						if(@ticket >= @QtyTicket )
						begin
							set @ticket = @ticket - @QtyTicket
						end

						select @OutSaldoJaminan=UangJaminan from DataAccount where AccountNumber = @AccountNumber
						set @OutSaldoEmoney = @balanced
						set @OutTicket = @ticket

						update DataAccount 
						set Balanced = @balanced, Ticket=@ticket
						where AccountNumber = @AccountNumber

						delete from LogDeposit where AccountNumber=@AccountNumber
						and Datetime = @TransactionDate and TransactionType = 'DEBIT'

						if(@Topup > 0)
						begin
							delete from LogDeposit where AccountNumber=@AccountNumber
							and Datetime = @TransactionDate and TransactionType = 'CREDIT' and Nominal = @Topup	
						end

						set @Succes = 1
					end
					else
					begin
						set @Succes = 0
						set @Message = 'Maaf, Untuk transaksi ini '+@JenisTransaksi +' belum tersedia'
					end
				end

				if(@Succes = 1)
				begin
					select @Debit=sum(isnull(Debit,0)),@Credit=sum(isnull(Credit,0))  from 
					(
						select
						case when TransactionType = 'DEBIT' then Nominal end as Debit,
						case when TransactionType = 'CREDIT' then Nominal end as Credit
						from LogDeposit 
						where 
							replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
							replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@TransactionDate,10),'/','-'), 105), 23),'-','') 
					) q

					print cast(@Debit as nvarchar(max))
					print cast(@Credit as nvarchar(max))

					select @DepositYesterday=Deposit
					from DataDeposit 
					where  
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
					replace(dateadd(day,-1, cast(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@TransactionDate,10),'/','-'), 105), 23) as date)),'-','') 

					update DataDeposit 
					set Credit = @Credit, Debit = @Debit,DepositHariSebelumnya=@DepositYesterday,
					Deposit=((@DepositYesterday-@Debit) + @Credit)
					where 
							replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
							replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@TransactionDate,10),'/','-'), 105), 23),'-','') 

					select 
						@TotalUangDiBox=(TotalUangDiBox-@PayTunai),
						@TotalUangKeluar=(TotalUangKeluar+@PayTunai) 
					from DataChasierBox
					where 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@TransactionDate,10),'/','-'), 105), 23),'-','') 
					and NamaComputer=@ComputerName

					update DataChasierBox
					set TotalUangDiBox=@TotalUangDiBox,TotalUangKeluar=@TotalUangKeluar
					where 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(Datetime,10),'/','-'), 105), 23),'-','') = 
					replace(CONVERT(VARCHAR(10), CONVERT(date, replace(left(@TransactionDate,10),'/','-'), 105), 23),'-','') 
					and NamaComputer=@ComputerName 

					ulangInsert:
					insert into LogTransaksiCancel
					(TipeTransaksi,	PaymentMethod,	TotalTransaksi,	AccountNumber,	NamaKasirYangInputTrx, NamaKasirYangCancel,	
					Authorize,	TransactionDate,	CancelDate,	IdTransaksi,	PayTunai,	
					PayEmoney,	DescriptionTransaksi,	PayEDC)
					values(@TipeTransaksi,@PaymentMethod,@TotalTransaksi,@AccountNumber,@NamaKasirYangInputTrx,@NamaKasirYgCancel,
					@Authorize,@TransactionDate,@CancelDate,@IdTransaksi,@PayTunai,
					@PayEmoney,@DescriptionTransaksi,@PayEDC)

					if exists(select*from LogTransaksiCancel where  id = SCOPE_IDENTITY())
					begin
						set @Succes = 1
						set @Message = 'Id:'+cast(Scope_Identity() as nvarchar(max))+'~AccountNumber:'+@AccountNumber+'~TipeTransaksi:'+@TipeTransaksi+'~Ticket:'+cast(@OutTicket as nvarchar(max))+'~SaldoEmoney:'+cast(@OutSaldoEmoney as nvarchar(max))+'~SaldoJaminan:'+cast(@OutSaldoJaminan as nvarchar(max))+''
					end
					else
					begin
						GOTO ulangInsert 
					end
				end
			end
		end
		else
		begin
			set @Succes = 0
			set @Message = 'Transaksi Id :'+cast(@IdTrx as nvarchar(max))+' tidak ditemukan'
		end
		select @Message  _Message,@Succes _Success				
	END TRY 
	BEGIN CATCH  
		set @Succes = 0
		set @Message = 'SP error :'+cast(ERROR_NUMBER() as nvarchar(max))+' -' +ERROR_MESSAGE()
		select @Message  _Message,@Succes _Success				
	END CATCH;  
END