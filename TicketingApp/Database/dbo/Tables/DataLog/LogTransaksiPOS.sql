CREATE TABLE [dbo].[LogTransaksiPOS] (
    [idTrx]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Datetime]       NVARCHAR (50)  NOT NULL,
    [MerchantName]   NVARCHAR (MAX) NOT NULL,
    [ChasierName]    NVARCHAR (MAX) NOT NULL,
    [PaymentMethod]  NVARCHAR (MAX) NOT NULL,
    [TotalTransaksi] FLOAT (53)     NOT NULL,
    [Emoney]         FLOAT (53)     NULL,
    [AccountNumber]  NVARCHAR (MAX) NULL,
    [TotalBayar]     FLOAT (53)     NOT NULL,
    [Tunai]          FLOAT (53)     NULL,
    [Kembalian]      FLOAT (53)     NULL,
    [BankName]       NVARCHAR (MAX) NULL,
    [CardNumber]     NVARCHAR (MAX) NULL,
    [Noreff]         NVARCHAR (MAX) NULL,
    [Status]         BIGINT         NULL
);


GO
CREATE TRIGGER [dbo].[TR_LogTransaksiPOS]  
ON [dbo].[LogTransaksiPOS]  
AFTER INSERT  
AS  
   declare @Emoney float
   declare @AccountNumber nvarchar(max)
   declare @IdTrx bigint

   select @IdTrx=IdTrx,@Emoney=Emoney,@AccountNumber = AccountNumber from Inserted  
   	
   if(@Emoney < 0)
   begin
		if exists(select*from DataAccount where AccountNumber = @AccountNumber)
		begin
			declare @Saldo float
			select @Saldo = Balanced from DataAccount where AccountNumber = @AccountNumber
			update DataAccount
			set Balanced = (@Saldo + @Emoney),
			UpdateDate = FORMAT(GETDATE() , 'dd/MM/yyyy HH:mm:ss')

			where AccountNumber = @AccountNumber
		end
   end

   