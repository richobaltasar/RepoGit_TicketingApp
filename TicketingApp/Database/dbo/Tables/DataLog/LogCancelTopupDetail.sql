﻿CREATE TABLE [dbo].[LogCancelTopupDetail] (
    [IdTopup]           BIGINT         NOT NULL,
    [Datetime]          NVARCHAR (50)  NULL,
    [JenisPayment]      NVARCHAR (MAX) NULL,
    [AccountNumber]     NVARCHAR (50)  NULL,
    [NominalTopup]      FLOAT (53)     NULL,
    [TotalBayar]        FLOAT (53)     NULL,
    [PayCash]           FLOAT (53)     NULL,
    [TerimaUang]        FLOAT (53)     NULL,
    [Kembalian]         FLOAT (53)     NULL,
    [SaldoSebelum]      FLOAT (53)     NULL,
    [SaldoSetelah]      FLOAT (53)     NULL,
    [Chasierby]         NVARCHAR (MAX) NULL,
    [ComputerName]      NVARCHAR (MAX) NULL,
    [Status]            INT            NULL,
    [IdLogEDCTransaksi] BIGINT         NULL,
    [BankCode]          NVARCHAR (50)  NULL,
    [NamaBank]          NVARCHAR (MAX) NULL,
    [DiskonBank]        FLOAT (53)     NULL,
    [NominalDiskon]     FLOAT (53)     NULL,
    [AdminCharges]      FLOAT (53)     NULL,
    [TotalDebit]        FLOAT (53)     NULL,
    [PaymentMethod]     NVARCHAR (MAX) NULL,
    [StatusUpload]      INT            NULL
);
