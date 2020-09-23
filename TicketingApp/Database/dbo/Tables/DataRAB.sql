CREATE TABLE [dbo].[DataRAB] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaRAB]             NVARCHAR (MAX) NULL,
    [JenisRAB]            NVARCHAR (MAX) NULL,
    [DescriptionRAB]      NVARCHAR (MAX) NULL,
    [CreateDate]          NVARCHAR (MAX) NULL,
    [TargetImplementDate] NVARCHAR (MAX) NULL,
    [PIC1]                BIGINT         NULL,
    [PIC2]                BIGINT         NULL,
    [DisetujuiOleh]       BIGINT         NULL,
    [Attachment1]         NVARCHAR (MAX) NULL,
    [Attachment2]         NVARCHAR (MAX) NULL,
    [Attachment3]         NVARCHAR (MAX) NULL,
    [TotalBudget]         FLOAT (53)     NULL,
    [EstimasiPurchase]    FLOAT (53)     NULL,
    [StatusRAB]           NVARCHAR (MAX) NOT NULL,
    [IdUserBy]            BIGINT         NOT NULL
);

