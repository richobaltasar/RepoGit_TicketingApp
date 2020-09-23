using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.Page
{
    public partial class TrxTopup : Form
    {
        GlobalFunc f = new GlobalFunc();
        public TrxTopup()
        {
            InitializeComponent();
        }

        private void TrxTopup_Load(object sender, EventArgs e)
        {
            if (TempAllTransaksiModel.IdTrx != "")
            {
                var data = new AllTransaksiModel();
                data.IdTrx = TempAllTransaksiModel.IdTrx;
                data.CashierBy = TempAllTransaksiModel.CashierBy;
                data.Datetime = TempAllTransaksiModel.Datetime;
                data.JenisTransaksi = TempAllTransaksiModel.JenisTransaksi;
                data.Nominal = TempAllTransaksiModel.Nominal;

                var res = f.GetDataTransaksiTopupReprint(data);
                if (res != null)
                {
                    IdTransaction.Text = res.IdTransaction;
                    Datetime.Text = res.Datetime;
                    MerchantName.Text = res.MerchantName;
                    NamaKasir.Text = res.NamaKasir;
                    NominalTopup.Text = res.NominalTopup;
                    UangDibayarkan.Text = res.UangDibayarkan;
                    Kembalian.Text = res.Kembalian;

                    AccountNumber.Text = res.AccountNumber;
                    SaldoSebelumnya.Text = res.SaldoSebelumnya;
                    SaldoSetelahnya.Text = res.SaldoSetelahnya;

                    txtAccountNumberCash.Text = res.AccountNumber;
                    txtSaldoSebelumnyaCash.Text = res.SaldoSebelumnya;
                    txtSaldoSetelahnyaCash.Text = res.SaldoSetelahnya;

                    txtNamaBank.Text = res.NamaBank;
                    txtNoATM.Text = res.NoATM;
                    txtNominalDebit.Text = f.ConvertToRupiah(f.ConvertDecimal(res.TotalDebit));
                    txtNoReff.Text = res.NoReffEddPrint;
                    txtPaymentMethode.Text = res.PaymentMethod;
                    if (res.PaymentMethod == "CASH")
                    {
                        PanelCash.Visible = true;
                        PanelEDC.Visible = false;
                    }
                    else
                    {
                        PanelCash.Visible = false;
                        PanelEDC.Visible = true;
                    }
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

                var res = f.GetDataTransaksiTopupReprint(data);
                var print = PrintTopup(res);
            }
        }
        public ReturnResult PrintTopup(GetDataTransaksiTopupModel data)
        {
            var res = new ReturnResult();
            try
            {
                string s = "Datetime \t: " + data.Datetime + Environment.NewLine;
                s += "ID Transaction\t: " + data.IdTransaction + Environment.NewLine;
                s += "Merchant ID \t: " + data.MerchantName + Environment.NewLine;
                s += "Nama Petugas \t: " + data.NamaKasir + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Transaksi Topup " + Environment.NewLine;
                s += "Nominal Topup \t: " + data.NominalTopup + Environment.NewLine;
                s += "Uang dibayarkan : " + data.UangDibayarkan + Environment.NewLine;
                s += "Uang kembalian \t: " + data.Kembalian + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Account Number \t: " + data.AccountNumber + Environment.NewLine;
                s += "Previous Balance : " + data.SaldoSebelumnya + Environment.NewLine;
                s += "Current Balance \t: " + data.SaldoSetelahnya + Environment.NewLine;

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

            Form fc = Application.OpenForms["Authorize"];
            if (fc != null)
            {
                fc.Show();
                fc.BringToFront();
                TextBox txtUsername = fc.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox;
                TextBox txtCurrentPass = fc.Controls.Find("txtCurrentPass", true).FirstOrDefault() as TextBox;
                Label IdTrx = fc.Controls.Find("IdTrx", true).FirstOrDefault() as Label;
                Label txtTransactionType = fc.Controls.Find("txtTransactionType", true).FirstOrDefault() as Label;
                if (txtUsername != null)
                {
                    txtUsername.Focus();
                }
                if (IdTrx != null)
                {
                    IdTrx.Text = IdTransaction.Text;
                }
                if (txtTransactionType != null)
                {
                    txtTransactionType.Text = "REG";
                }
            }
            else
            {
                Page.Authorize frm = new Page.Authorize();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.BringToFront();
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                frm.Show();
                TextBox txtCurrentPass = frm.Controls.Find("txtCurrentPass", true).FirstOrDefault() as TextBox;
                TextBox txtUsername = frm.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox;
                Label IdTrx = frm.Controls.Find("IdTrx", true).FirstOrDefault() as Label;
                Label txtTransactionType = frm.Controls.Find("txtTransactionType", true).FirstOrDefault() as Label;


                if (txtUsername != null)
                {
                    txtUsername.Focus();
                }
                if (IdTrx != null)
                {
                    IdTrx.Text = IdTransaction.Text;
                }
                if (txtTransactionType != null)
                {
                    txtTransactionType.Text = "REG";
                }
            }
            this.Close();
        }
    }
}
