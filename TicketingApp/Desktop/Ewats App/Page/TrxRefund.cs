﻿using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Ewats_App.Page
{
    public partial class TrxRefund : Form
    {
        GlobalFunc f = new GlobalFunc();
        public TrxRefund()
        {
            InitializeComponent();
        }
        private void TrxRefund_Load(object sender, EventArgs e)
        {
            if (TempAllTransaksiModel.IdTrx != "")
            {
                var data = new AllTransaksiModel();
                data.IdTrx = TempAllTransaksiModel.IdTrx;
                data.CashierBy = TempAllTransaksiModel.CashierBy;
                data.Datetime = TempAllTransaksiModel.Datetime;
                data.JenisTransaksi = TempAllTransaksiModel.JenisTransaksi;
                data.Nominal = TempAllTransaksiModel.Nominal;

                var res = f.GetDataTransaksiRefundReprint(data);
                if (res != null)
                {
                    IdTransaction.Text = res.IdTransaction;
                    Datetime.Text = res.Datetime;
                    MerchantName.Text = res.MerchantName;
                    NamaKasir.Text = res.NamaKasir;
                    SaldoEmoney.Text = res.SaldoEmoney;
                    SaldoJaminan.Text = res.SaldoJaminan;
                    TotalRefund.Text = res.TotalRefund;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            if (TempAllTransaksiModel.IdTrx != "")
            {
                var data = new AllTransaksiModel();
                data.IdTrx = TempAllTransaksiModel.IdTrx;
                data.CashierBy = TempAllTransaksiModel.CashierBy;
                data.Datetime = TempAllTransaksiModel.Datetime;
                data.JenisTransaksi = TempAllTransaksiModel.JenisTransaksi;
                data.Nominal = TempAllTransaksiModel.Nominal;

                var res = f.GetDataTransaksiRefundReprint(data);
                var print = PrintRefund(res);
            }
        }
        public ReturnResult PrintRefund(GetDataTransaksiRefundReprintModel data)
        {
            var res = new ReturnResult();
            try
            {
                string s = "Datetime \t: " + data.Datetime + Environment.NewLine;
                s += "ID Transaction\t: " + data.IdTransaction + Environment.NewLine;
                s += "Merchant ID \t: " + data.MerchantName + Environment.NewLine;
                s += "Nama Petugas \t: " + data.NamaKasir + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Transaksi Refund " + Environment.NewLine;
                s += "Saldo Emoney \t: " + data.SaldoEmoney + Environment.NewLine;
                s += "Saldo Jaminan \t: " + data.SaldoJaminan + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Refund \t: Rp " + data.TotalRefund + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                foreach (string pfoot in f.GetFooterPrint())
                {
                    s += pfoot + Environment.NewLine;
                }
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "*****   RECEIPT COPY   *****" + Environment.NewLine;


                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    int HeadreX = 0;
                    int startY = 0;
                    int Offset = 0;
                    e1.Graphics.DrawString("*****   RECEIPT COPY   *****", new Font("Arial", 8, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    Offset = Offset + 15;
                    string underLine1 = "======================================";
                    e1.Graphics.DrawString(underLine1, new Font("calibri", 7), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    e1.Graphics.DrawString(f.GetFooterPrint(1), new Font("Arial", 8, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    Offset = Offset + 15;
                    //e1.Graphics.DrawString(f.GetFooterPrint(2), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    e1.Graphics.DrawString(f.GetFooterPrint(2), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                    Offset = Offset + 15;
                    e1.Graphics.DrawString(f.GetFooterPrint(3), new Font("Arial", 5, FontStyle.Italic), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                    Offset = Offset + 15;
                    string underLine = "======================================";
                    e1.Graphics.DrawString(underLine, new Font("Arial", 6), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    e1.Graphics.DrawString(s, new Font("Arial", 7), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                };
                p.Print();
                res.Success = true;
                res.Message = "Print Success";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = "Exception Occured While Printing " + ex.Message;
            }
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
