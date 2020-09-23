CREATE TABLE [dbo].[LogTransaksiCancel] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [TipeTransaksi]         NVARCHAR (MAX) NULL,
    [PaymentMethod]         NVARCHAR (MAX) NULL,
    [TotalTransaksi]        FLOAT (53)     NULL,
    [AccountNumber]         NVARCHAR (MAX) NULL,
    [NamaKasirYangInputTrx] NVARCHAR (MAX) NULL,
    [NamaKasirYangCancel]   NVARCHAR (MAX) NULL,
    [Authorize]             NVARCHAR (MAX) NULL,
    [TransactionDate]       NVARCHAR (MAX) NULL,
    [CancelDate]            NVARCHAR (50)  NULL,
    [IdTransaksi]           BIGINT         NOT NULL,
    [PayTunai]              FLOAT (53)     CONSTRAINT [DF_LogTransaksiCancel_PayTunai] DEFAULT ((0)) NOT NULL,
    [PayEmoney]             FLOAT (53)     CONSTRAINT [DF_LogTransaksiCancel_PayEmoney] DEFAULT ((0)) NOT NULL,
    [DescriptionTransaksi]  NVARCHAR (MAX) NULL,
    [PayEDC]                FLOAT (53)     NULL,
    CONSTRAINT [PK__LogTrans__3214EC07AC6C7A64] PRIMARY KEY CLUSTERED ([Id] ASC)
);

