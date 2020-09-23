CREATE TABLE [dbo].[DataQuotationVendor_Item] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdQuotation] BIGINT         NULL,
    [IdRab]       BIGINT         NULL,
    [IdItemRAB]   BIGINT         NULL,
    [Category]    NVARCHAR (MAX) NULL,
    [NamaItem]    NVARCHAR (MAX) NULL,
    [Satuan]      NVARCHAR (MAX) NULL,
    [Unit]        NVARCHAR (MAX) NULL,
    [Harga]       FLOAT (53)     CONSTRAINT [DF_DataQuotationVendor_Item_Harga] DEFAULT ((0)) NOT NULL,
    [SubTotal]    FLOAT (53)     CONSTRAINT [DF_DataQuotationVendor_Item_SubTotal] DEFAULT ((0)) NOT NULL,
    [Attachment1] NVARCHAR (MAX) NULL,
    [Status]      BIGINT         NULL
);

