﻿CREATE TABLE [dbo].[LogClosingV2] (
    [LogId]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [Datetime]                 NVARCHAR (MAX) NULL,
    [KasirInputNominal]        FLOAT (53)     NULL,
    [ComputerName]             NVARCHAR (MAX) NULL,
    [Username]                 NVARCHAR (MAX) NULL,
    [MoneyCashboxSelisih]      FLOAT (53)     NULL,
    [MatchingStatus]           NVARCHAR (MAX) NULL,
    [TicketSalesCounting]      FLOAT (53)     NULL,
    [KartuBaruCounting]        FLOAT (53)     NULL,
    [TotalKartuRefundCounting] FLOAT (53)     NULL,
    [TotalKartuBaruNominal]    FLOAT (53)     NULL,
    [TicketPayCash]            FLOAT (53)     NULL,
    [TicketPaySaldo]           FLOAT (53)     NULL,
    [TicketPaySaldoNCash]      FLOAT (53)     NULL,
    [PayEDC]                   FLOAT (53)     NULL,
    [PaySaldoEDC]              FLOAT (53)     NULL,
    [TicketTotalAmount]        FLOAT (53)     NULL,
    [TotalTopupCash]           FLOAT (53)     NULL,
    [TotalTopupEDC]            FLOAT (53)     NULL,
    [TotalTopup]               FLOAT (53)     NULL,
    [FNBPayCash]               FLOAT (53)     NULL,
    [FNBPaySaldo]              FLOAT (53)     NULL,
    [FNBAll]                   FLOAT (53)     NULL,
    [RefundJaminan]            FLOAT (53)     NULL,
    [RefundSaldo]              FLOAT (53)     NULL,
    [TotalRefund]              FLOAT (53)     NULL,
    [DanaModal]                FLOAT (53)     NULL,
    [TotalCashin]              FLOAT (53)     NULL,
    [TotalCashOut]             FLOAT (53)     NULL,
    [TotalEDC]                 FLOAT (53)     NULL,
    [TotalEmoney]              FLOAT (53)     NULL,
    [TotalCashBox]             FLOAT (53)     NULL,
    [TotalTransaksiKasir]      FLOAT (53)     NULL,
    [StatusAcceptanceBySPV]    NVARCHAR (MAX) NULL,
    [KeteranganAcceptance]     NVARCHAR (MAX) NULL,
    [UangDiterimaFinnance]     FLOAT (53)     NULL,
    [TotalAmountStrukEDC]      FLOAT (53)     NULL,
    [Status]                   INT            NULL,
    [TanggalSetoran]           NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([LogId] ASC)
);

