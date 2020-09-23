CREATE TABLE [dbo].[LogParkir] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [BarcodeReciptCode] NVARCHAR (MAX) NULL,
    [Datetime]          NVARCHAR (50)  NULL,
    [DatetimeOut]       NVARCHAR (MAX) NULL,
    [TypeKendaraan]     NVARCHAR (MAX) NULL,
    [PolisNum]          NVARCHAR (MAX) NULL,
    [GateID]            BIGINT         NULL,
    [GateOut]           BIGINT         NULL,
    [In_Out_Status]     NCHAR (10)     NULL,
    [Img1]              NVARCHAR (MAX) NULL,
    [Img2]              NVARCHAR (MAX) NULL,
    [Img3]              NVARCHAR (MAX) NULL,
    [Img4]              NVARCHAR (MAX) NULL,
    [Img1Out]           NVARCHAR (MAX) NULL,
    [Img2Out]           NVARCHAR (MAX) NULL,
    [Img3Out]           NVARCHAR (MAX) NULL,
    [Img4Out]           NVARCHAR (MAX) NULL,
    [AccountNumber]     NVARCHAR (MAX) NULL,
    [Charges]           FLOAT (53)     NULL,
    [Status]            INT            NULL
);

