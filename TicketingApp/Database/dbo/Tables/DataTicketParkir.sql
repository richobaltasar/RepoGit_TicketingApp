CREATE TABLE [dbo].[DataTicketParkir] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [JenisTicket]  NVARCHAR (MAX) NULL,
    [Category]     NVARCHAR (MAX) NULL,
    [Code]         NVARCHAR (MAX) NULL,
    [Harga]        FLOAT (53)     NULL,
    [Periode]      NVARCHAR (MAX) NULL,
    [Img]          NVARCHAR (MAX) NULL,
    [Status]       BIGINT         NULL,
    [CreateDate]   NVARCHAR (MAX) NULL,
    [ModifyDate]   NVARCHAR (MAX) NULL,
    [CreateBy]     NVARCHAR (MAX) NULL,
    [HargaWeekDay] FLOAT (53)     NULL,
    [HargaWeekEnd] FLOAT (53)     NULL,
    [HargaHoliday] FLOAT (53)     NULL
);

