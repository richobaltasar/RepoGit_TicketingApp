CREATE TABLE [dbo].[Transactions] (
    [TransactionId]   INT            IDENTITY (1, 1) NOT NULL,
    [AccountNumber]   NVARCHAR (12)  NOT NULL,
    [BeneficiaryName] NVARCHAR (100) NOT NULL,
    [BankName]        NVARCHAR (100) NOT NULL,
    [SWIFTCode]       NVARCHAR (11)  NOT NULL,
    [Amount]          INT            NOT NULL,
    [Date]            DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([TransactionId] ASC)
);

