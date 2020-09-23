CREATE TABLE [dbo].[DataPO_Item] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdPO]            BIGINT         NULL,
    [IdQuotation]     BIGINT         NULL,
    [IdItemQuotation] BIGINT         NULL,
    [Category]        NVARCHAR (MAX) NULL,
    [NamaItem]        NVARCHAR (MAX) NULL,
    [Satuan]          NVARCHAR (MAX) NULL,
    [Unit]            FLOAT (53)     NULL,
    [Harga]           FLOAT (53)     NULL,
    [SubTotal]        FLOAT (53)     NULL,
    [Attachment1]     NVARCHAR (MAX) NULL,
    [Status]          BIGINT         NULL,
    CONSTRAINT [PK_DataPO_Item] PRIMARY KEY CLUSTERED ([Id] ASC)
);

