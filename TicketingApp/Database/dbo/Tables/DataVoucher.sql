CREATE TABLE [dbo].[DataVoucher] (
    [id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaVoucher]    NVARCHAR (MAX) NULL,
    [JenisVoucher]   NVARCHAR (MAX) NULL,
    [NominalVoucher] FLOAT (53)     NULL,
    [CodeVoucher]    NVARCHAR (MAX) NULL,
    [Img]            NVARCHAR (MAX) NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [Status]         BIGINT         NULL,
    [BerlakuDari]    NVARCHAR (MAX) NULL,
    [BerlakuSampai]  NVARCHAR (MAX) NULL,
    [CreateDate]     NVARCHAR (MAX) NULL,
    [ModifyDate]     NVARCHAR (MAX) NULL,
    [CreateBy]       NVARCHAR (MAX) NULL
);

