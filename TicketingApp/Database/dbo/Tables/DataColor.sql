CREATE TABLE [dbo].[DataColor] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaFunction]    NVARCHAR (MAX) NULL,
    [Urutan]          BIGINT         NULL,
    [Label]           NVARCHAR (MAX) NOT NULL,
    [BackgroundColor] NVARCHAR (MAX) NULL,
    [BorderColor]     NVARCHAR (MAX) NOT NULL
);

