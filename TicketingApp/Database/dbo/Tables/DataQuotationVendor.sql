CREATE TABLE [dbo].[DataQuotationVendor] (
    [IdQuotation]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdRab]                   BIGINT         NULL,
    [NumberQuotationByVendor] NVARCHAR (MAX) NULL,
    [CategoryPerusahaan]      NVARCHAR (MAX) NULL,
    [CompanyName]             NVARCHAR (MAX) NULL,
    [AlamatPerusahaan]        NVARCHAR (MAX) NULL,
    [Contact]                 NVARCHAR (MAX) NULL,
    [Sales]                   NVARCHAR (MAX) NULL,
    [TotalPenawaran]          FLOAT (53)     NULL,
    [Perihal]                 NVARCHAR (MAX) NULL,
    [Description]             NVARCHAR (MAX) NULL,
    [CreateDate]              NVARCHAR (MAX) NULL,
    [StatusQuotation]         BIGINT         NULL,
    [Attachment1]             NVARCHAR (MAX) NULL
);

