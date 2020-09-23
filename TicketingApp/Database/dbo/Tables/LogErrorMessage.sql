CREATE TABLE [dbo].[LogErrorMessage] (
    [logId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [MessageError] NVARCHAR (MAX) NULL,
    [FunctionName] NVARCHAR (MAX) NULL,
    [Parameter]    NVARCHAR (MAX) NULL,
    [Datetime]     NVARCHAR (MAX) NULL,
    [Username]     NVARCHAR (MAX) NULL
);

