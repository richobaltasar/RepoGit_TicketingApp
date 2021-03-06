﻿CREATE TABLE [dbo].[UserData] (
    [id]             INT            IDENTITY (1, 1) NOT NULL,
    [username]       NVARCHAR (MAX) NULL,
    [password]       NVARCHAR (MAX) NULL,
    [hakakses]       NVARCHAR (MAX) NULL,
    [NamaLengkap]    NVARCHAR (MAX) NULL,
    [Email]          NVARCHAR (MAX) NULL,
    [Gender]         NVARCHAR (50)  NULL,
    [NoHp]           NVARCHAR (50)  NULL,
    [Photo]          NVARCHAR (MAX) NULL,
    [Alamat]         NVARCHAR (MAX) NULL,
    [Status]         BIGINT         NULL,
    [TanggalLahir]   NVARCHAR (MAX) NULL,
    [TempatLahir]    NVARCHAR (MAX) NULL,
    [Agama]          NVARCHAR (MAX) NULL,
    [ScanKTP]        NVARCHAR (MAX) NULL,
    [JenisIdentitas] NVARCHAR (MAX) NULL,
    [NoIdentitas]    NVARCHAR (MAX) NULL,
    [NamaDivisi]     NVARCHAR (MAX) NULL,
    [NamaPosisi]     NVARCHAR (MAX) NULL,
    [TglAwalKerja]   NVARCHAR (MAX) NULL,
    [CreateDate]     NVARCHAR (MAX) NULL,
    [ModifyDate]     NVARCHAR (MAX) NULL,
    [CreateBy]       NVARCHAR (MAX) NULL, 
    [ImgLink] NVARCHAR(MAX) NULL
);

