CREATE TABLE [dbo].[Master_ListItem] (
    [id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ListName] NVARCHAR (MAX) NOT NULL,
    [Urutan]   NVARCHAR (MAX) NULL,
    [Text]     NVARCHAR (MAX) NULL,
    [Value]    NVARCHAR (MAX) NULL
);