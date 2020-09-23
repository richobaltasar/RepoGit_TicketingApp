CREATE TABLE [dbo].[DataKotaPostalCode] (
    [Province]          NVARCHAR (MAX) NULL,
    [Kota_Kab]          NVARCHAR (MAX) NULL,
    [Kabupaten_Kota]    NVARCHAR (MAX) NULL,
    [Kec_subDistrict]   NVARCHAR (MAX) NULL,
    [Kelurahan_village] NVARCHAR (MAX) NULL,
    [PostalCode]        NVARCHAR (MAX) NULL,
    [id]                BIGINT         IDENTITY (1, 1) NOT NULL
);

