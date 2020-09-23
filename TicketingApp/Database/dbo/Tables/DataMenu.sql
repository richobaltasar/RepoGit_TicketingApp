CREATE TABLE [dbo].[DataMenu] (
    [idMenu]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaMenu]   NVARCHAR (MAX) NULL,
    [Action]     NVARCHAR (MAX) NULL,
    [Controller] NVARCHAR (MAX) NULL,
    [Platform]   NVARCHAR (MAX) NULL,
    [Img]        NVARCHAR (MAX) NULL,
    [Status]     BIGINT         CONSTRAINT [DF_DataMenu_Status] DEFAULT ((1)) NOT NULL
);

