CREATE TABLE [dbo].[DataRAB_Item] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdRAB]    BIGINT         NULL,
    [Category] NVARCHAR (MAX) NULL,
    [NamaItem] NVARCHAR (MAX) NULL,
    [Satuan]   NVARCHAR (MAX) NULL,
    [Unit]     FLOAT (53)     NULL,
    [Harga]    FLOAT (53)     NULL,
    [SubTotal] FLOAT (53)     NULL,
    [Status]   BIGINT         NULL
);

