CREATE TABLE [dbo].[DataSettingReportforExternal] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaLaporan] NVARCHAR (MAX) NULL,
    [Category]    NVARCHAR (MAX) NULL,
    [Persentase]  FLOAT (53)     NULL,
    [Status]      INT            NULL
);

