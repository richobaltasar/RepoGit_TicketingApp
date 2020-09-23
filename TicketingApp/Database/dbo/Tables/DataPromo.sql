CREATE TABLE [dbo].[DataPromo] (
    [idPromo]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [NamaPromo]     NVARCHAR (MAX) NULL,
    [CategoryPromo] NVARCHAR (MAX) NULL,
    [Diskon]        FLOAT (53)     NULL,
    [Status]        BIGINT         NULL,
    [BerlakuDari]   NVARCHAR (50)  NULL,
    [BerlakuSampai] NVARCHAR (50)  NULL,
    [Img]           NVARCHAR (MAX) NULL,
    [IdJenisTicket] BIGINT         NULL,
    [JenisTicket]   NVARCHAR (MAX) NULL,
    [CreateDate]    NVARCHAR (50)  NULL,
    [ModifyDate]    NVARCHAR (50)  NULL, 
    [ImgLink] NVARCHAR(MAX) NULL
);

