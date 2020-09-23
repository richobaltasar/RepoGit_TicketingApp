CREATE TABLE [dbo].[DataModule] (
    [IdModul]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaModule] NVARCHAR (MAX) NULL,
    [Action]     NVARCHAR (MAX) NULL,
    [Controller] NVARCHAR (MAX) NULL,
    [Img]        NVARCHAR (MAX) NULL,
    [Status]     INT            NULL
);

