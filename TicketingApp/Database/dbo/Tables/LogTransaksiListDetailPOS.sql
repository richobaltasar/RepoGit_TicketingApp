CREATE TABLE [dbo].[LogTransaksiListDetailPOS] (
    [IdTrx]    BIGINT         NOT NULL,
    [Id]       NVARCHAR (MAX) NOT NULL,
    [Category] NVARCHAR (MAX) NOT NULL,
    [NamaItem] NVARCHAR (MAX) NOT NULL,
    [Harga]    FLOAT (53)     NOT NULL,
    [Qtx]      FLOAT (53)     NOT NULL,
    [Total]    FLOAT (53)     NOT NULL
);


GO
CREATE TRIGGER [dbo].[TR_DataAccountTopup]  
ON [dbo].[LogTransaksiListDetailPOS]  
AFTER INSERT
AS  
   declare @Topup float
   declare @Saldo float
   declare @AccountNumber nvarchar(max)
   declare @IdTrx bigint

   declare @Card float
   select @Card=sum(Harga),@IdTrx = IdTrx from Inserted where Category ='CARD'
   group by IdTrx

   if(@Card > 0)
   begin
   if exists(select*from LogTransaksiPOS where idTrx = @IdTrx)
   begin
		select @AccountNumber = AccountNumber from LogTransaksiPOS where IdTrx = @IdTrx
		if not exists(select*from DataAccount where AccountNumber = @AccountNumber and Status =1)
		begin
			insert into DataAccount
			(
				AccountNumber,
				UangJaminan,
				CreateDate,
				ExpiredDate,
				Status
			)
			values
			(
				@AccountNumber,@Card,
				FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
				FORMAT(dateadd(day, 30, getdate()),'dd/MM/yyyy HH:mm:ss'),
				1
			)	
		end
   end
   end

   select @Topup=sum(Harga),@IdTrx = IdTrx from Inserted where Category ='TOPUP'
   group by IdTrx
   if(@Topup>0)	
   begin
   if exists(select*from LogTransaksiPOS where IdTrx = @IdTrx)
   begin
		select @AccountNumber = AccountNumber from LogTransaksiPOS where IdTrx = @IdTrx
		if exists(select*from DataAccount where AccountNumber = @AccountNumber)
		begin
			select @Saldo = Balanced from DataAccount where AccountNumber = @AccountNumber
			update DataAccount
			set Balanced = (isnull(@Saldo,0) + isnull(@Topup,0)),
			UpdateDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss'),
			ExpiredDate = FORMAT(dateadd(day, 30, getdate()),'dd/MM/yyyy HH:mm:ss')
			where AccountNumber = @AccountNumber
		end
   end
   end

   declare @Qty float
   declare @Ticket float

   select @Qty=sum(Qtx),@IdTrx = IdTrx from Inserted where Category ='TICKETING' and NamaItem like '%Nama Ticket%'
   group by IdTrx
   if(@Qty>0)
   begin
   if exists(select*from LogTransaksiPOS where IdTrx = @IdTrx)
   begin
		select @AccountNumber = AccountNumber from LogTransaksiPOS where IdTrx = @IdTrx
		if exists(select*from DataAccount where AccountNumber = @AccountNumber)
		begin
			select @Ticket = Ticket from DataAccount where AccountNumber = @AccountNumber
			update DataAccount
			set Ticket = (isnull(@Ticket,0) + isnull(@Qty,0)),
			UpdateDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')
			where AccountNumber = @AccountNumber
		end
   end
   end


   declare @Id bigint
   declare @NamaItem nvarchar(max)
   declare @Harga float
   select @IdTrx = IdTrx,@Id=Id,@NamaItem=NamaItem,@Harga=Total from Inserted where Category ='PARKIR'
   	
	if(@Id > 0)
	begin
		if exists(select*from LogParkir where BarcodeReciptCode = @Id and status = 1)
		begin
			if exists(select*from LogTransaksiPOS where IdTrx = @IdTrx)
			begin
				select @AccountNumber = AccountNumber from LogTransaksiPOS where IdTrx = @IdTrx
				
				update LogParkir
				set AccountNumber = @AccountNumber,
				PolisNum = LTRIM(RTRIM(PARSENAME(REPLACE(@NamaItem, '-', '.'), 1))),
				Status = 2
				where BarcodeReciptCode = @Id and Status = 1 
			end
		end
	end

   
GO
CREATE TRIGGER [dbo].[TR_UpdateParkiran]  
ON [dbo].[LogTransaksiListDetailPOS]  
AFTER INSERT
AS  
   declare @IdTrx bigint
   declare @Id bigint
   declare @NamaItem nvarchar(max)
   declare @Harga float
   declare @AccountNumber nvarchar(max)

   
   select @IdTrx = IdTrx,@Id=Id,@NamaItem=NamaItem,@Harga=Total from Inserted where Category ='PARKIR'
   	
	if(@Id > 0)
	begin
		if exists(select*from LogParkir where BarcodeReciptCode = @Id and status = 1)
		begin
			if exists(select*from LogTransaksiPOS where IdTrx = @IdTrx)
			begin
				select @AccountNumber = AccountNumber from LogTransaksiPOS where IdTrx = @IdTrx
				
				update LogParkir
				set AccountNumber = @AccountNumber,
				PolisNum = LTRIM(RTRIM(PARSENAME(REPLACE(@NamaItem, '-', '.'), 1))),
				Status = 2
				where BarcodeReciptCode = @Id and Status = 1  
			end
		end
	end
	

GO
DISABLE TRIGGER [dbo].[TR_UpdateParkiran]
    ON [dbo].[LogTransaksiListDetailPOS];

