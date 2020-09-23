CREATE TABLE [dbo].[LogActivityUser] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaTable] NVARCHAR (MAX) NULL,
    [Message]   NVARCHAR (MAX) NULL,
    [IdRow]     BIGINT         NULL,
    [Action]    NVARCHAR (MAX) NULL,
    [UserBy]    NVARCHAR (MAX) NULL,
    [Datetime]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_LogActivityUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);

