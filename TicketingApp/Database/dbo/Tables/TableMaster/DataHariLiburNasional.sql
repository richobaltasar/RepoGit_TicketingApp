CREATE TABLE [dbo].[DataHariLiburNasional] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaHariLibur] NVARCHAR (MAX) NULL,
    [DariTanggal]   NVARCHAR (MAX) NULL,
    [SampaiTanggal] NVARCHAR (MAX) NULL,
    [Status]        BIGINT         NULL
);

