CREATE TABLE [dbo].[DataPO] (
    [IdPO]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdQuotation]     BIGINT         NULL,
    [LastPrint]       NVARCHAR (MAX) NULL,
    [TotalPO]         FLOAT (53)     NULL,
    [CreateDatePO]    NVARCHAR (MAX) NULL,
    [SendPODate]      NVARCHAR (MAX) NULL,
    [PhoneCode]       NVARCHAR (MAX) NULL,
    [NoWhatsAppSales] NVARCHAR (MAX) NULL,
    [StatusPO]        BIGINT         NULL,
    CONSTRAINT [PK_DataPO] PRIMARY KEY CLUSTERED ([IdPO] ASC)
);

