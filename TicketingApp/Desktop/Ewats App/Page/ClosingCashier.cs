using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.Page
{
    public partial class ClosingCashier : Form
    {
        GlobalFunc f = new GlobalFunc();
        GeneralFunction ff = new GeneralFunction();
        public DashboardModel DataModel = new DashboardModel();
        public Dashboard2Model DataModel2 = new Dashboard2Model();


        public ClosingCashier()
        {
            InitializeComponent();
        }

        private void ClosingCashier_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            this.BringToFront();
            string ComputerName = f.GetComputerName();
            string NamaUser = f.GetNamaUser(General.IDUser);
            var data = f.GetDashboard_V2(ComputerName, NamaUser, "SP_GetDashboard");
            DataModel2 = data;

            txtTicketSalesCounting.Text = data.TicketSalesCounting;
            txtKartuBaruCounting.Text = data.KartuBaruCounting;
            txtTotalKartuRefundCounting.Text = data.TotalKartuRefundCounting;
            txtTotalKartuBaruNominal.Text = data.TotalKartuBaruNominal;
            txtTicketPayCash.Text = data.TicketPayCash;
            txtTicketPaySaldo.Text = data.TicketPaySaldo;
            txtTicketPaySaldoNCash.Text = data.TicketPaySaldoNCash;
            txtPayEDC.Text = data.PayEDC;
            txtPaySaldoEDC.Text = data.PaySaldoEDC;
            txtTicketTotalAmount.Text = data.TicketTotalAmount;
            txtTotalTopupCash.Text = data.TotalTopupCash;
            txtTotalTopupEDC.Text = data.TotalTopupEDC;
            txtTotalTopup.Text = data.TotalTopup;
            txtFNBPayCash.Text = data.FNBPayCash;
            txtFNBPaySaldo.Text = data.FNBPaySaldo;
            txtFNBAll.Text = data.FNBAll;
            txtRefundJaminan.Text = data.RefundJaminan;
            txtRefundSaldo.Text = data.RefundSaldo;
            txtTotalRefund.Text = data.TotalRefund;
            txtDanaModal.Text = data.DanaModal;
            txtTotalCashin.Text = data.TotalCashin;
            txtTotalCashOut.Text = data.TotalCashOut;
            txtTotalCashBox.Text = data.TotalCashBox;
            txtTotalEDC.Text = data.TotalEDC;
            txtTotalEmoney.Text = data.TotalEmoney;
            txtTotalTransaksiKasir.Text = data.TotalTransaksiKasir;
            f.loadGrid3(dtGrid_tenant);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        public void input_keyPad(string key, string Object)
        {
            TextBox txt = this.Controls.Find(Object, true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                if (key == "<-")
                {
                    if (txt.Text.Length > 0)
                    {
                        txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                        string data = txt.Text.Replace(".", "").Replace(",", "");
                        if (data != "")
                        {
                            decimal t = Convert.ToDecimal(data);
                            txt.Text = string.Format("{0:n0}", t);
                        }
                        else
                        {
                            txt.Text = "0";
                        }
                    }
                }
                else if (key == "Reset")
                {
                    txt.Text = "0";
                }
                else if (key == "Enter")
                {
                }
                else
                {
                    string data = (txt.Text + key).Replace(".", "").Replace(",", "");
                    if (data != "")
                    {
                        decimal t = Convert.ToDecimal(data);
                        txt.Text = string.Format("{0:n0}", t);
                    }
                    else
                    {
                        txt.Text = txt.Text + key;
                    }

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtControl.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtUangCashBox.Text != "" && txtUangCashBox.Text != "0")
            {
            Ulang:
                decimal input = f.ConvertDecimal(txtUangCashBox.Text);
                string ComputerName = f.GetComputerName();
                string NamaUser = f.GetNamaUser(General.IDUser);
                var data = f.GetDashboard_V2(ComputerName, NamaUser, "SP_GetDashboard");
                var dataTenant = f.GetDashTenantPerfomance(ComputerName, NamaUser, "SP_GetDashTenantPerfomance");
                if (input != f.ConvertDecimal(data.TotalCashBox))
                {
                    var res = MessageBox.Show("Uang yang anda masukkan tidak sesuai dengan jumlah Uang pada Cashbox, " +
                        "Tekan Yes untuk tetap melakukan tutup buku, dan tekan No untuk membatalkan permintaan",
                        "Reader not connected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        DataModel2 = data;
                        if (DataModel2 != null)
                        {
                            var save = f.SaveClosing_V2("SP_SaveClosing", f.GetComputerName(), f.GetNamaUser(General.IDUser), input);
                            if (save.Success == true)
                            {
                            PrintLagi1:
                                if (save.Message.Contains("LogId") == true)
                                {
                                    string LogId = save.Message.Split(',')[1].Trim().Split(':')[1].Trim();
                                    var print = ClosingPrintV2(LogId);
                                    if (print.Success == true)
                                    {
                                        var printFoodCourt = ClosingPrintFoodCourt(ComputerName, NamaUser, LogId, dataTenant);
                                        if (print.Success == true)
                                        {
                                            this.Close();
                                            Form fc2 = Application.OpenForms["Main"];
                                            if (fc2 != null)
                                            {
                                                fc2.Close();
                                            }
                                            f.PageControl("Login");
                                        }
                                        else
                                        {
                                            var res3 = MessageBox.Show("Print Closing FoodCourt Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                            if (res3 == DialogResult.Retry)
                                            {
                                                goto PrintLagi1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var res3 = MessageBox.Show("Print Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                        if (res3 == DialogResult.Retry)
                                        {
                                            goto PrintLagi1;
                                        }
                                    }
                                }
                                else
                                {
                                    var res3 = MessageBox.Show("Save Closing Gagal", "SaveClosing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                    if (res3 == DialogResult.Retry)
                                    {
                                        goto Ulang;
                                    }
                                }
                            }
                            else
                            {
                                var res3 = MessageBox.Show("Save Closing Gagal", "SaveClosing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                if (res3 == DialogResult.Retry)
                                {
                                    goto Ulang;
                                }
                            }
                        }
                        else
                        {
                            var res3 = MessageBox.Show("Data Closing Kosong", "SaveClosing Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                }
                else
                {
                    DataModel2 = data;
                    if (DataModel2 != null)
                    {
                        var save = f.SaveClosing_V2("SP_SaveClosing", ComputerName, NamaUser, input);
                        if (save.Success == true)
                        {
                        PrintLagi1:
                            if (save.Message.Contains("LogId") == true)
                            {
                                string LogId = save.Message.Split(',')[1].Trim().Split(':')[1].Trim();
                                var print = ClosingPrintV2(LogId);
                                if (print.Success == true)
                                {
                                    var printFoodCourt = ClosingPrintFoodCourt(ComputerName, NamaUser, LogId, dataTenant);
                                    if (print.Success == true)
                                    {
                                        this.Close();
                                        Form fc2 = Application.OpenForms["Main"];
                                        if (fc2 != null)
                                        {
                                            fc2.Close();
                                        }
                                        f.PageControl("Login");
                                    }
                                    else
                                    {
                                        var res3 = MessageBox.Show("Print Closing FoodCourt Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                        if (res3 == DialogResult.Retry)
                                        {
                                            goto PrintLagi1;
                                        }
                                    }
                                }
                                else
                                {
                                    var res3 = MessageBox.Show("Print Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                    if (res3 == DialogResult.Retry)
                                    {
                                        goto PrintLagi1;
                                    }
                                }
                            }
                            else
                            {
                                var res3 = MessageBox.Show("Save Closing Gagal", "SaveClosing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                if (res3 == DialogResult.Retry)
                                {
                                    goto Ulang;
                                }
                            }

                        }
                        else
                        {
                            var res3 = MessageBox.Show("Save Closing Gagal", "SaveClosing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            if (res3 == DialogResult.Retry)
                            {
                                goto Ulang;
                            }
                        }
                    }
                    else
                    {
                        var res3 = MessageBox.Show("Data Closing Kosong", "SaveClosing Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
            else
            {
                f.ShowMessagebox("Silahkan masukan jumlah uang cash yang telah dihitung", "Warning", MessageBoxButtons.OK);
            }

        }

        public ReturnResult ClosingPrint(DashboardModel data, decimal Input)
        {
            var res = new ReturnResult();
            try
            {
                string now = f.GetDatetime();
                string s = "Datetime \t: " + now + Environment.NewLine;
                s += "Merchant ID \t: " + f.GetComputerName() + Environment.NewLine;
                s += "Nama Petugas \t: " + f.GetNamaUser(General.IDUser) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Registrasi \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalRegis)) + Environment.NewLine;
                s += "Total Topup \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTopup)) + Environment.NewLine;
                s += "Total FoodCourt Cash \t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalFoodcourtCash)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total CashIn \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalRegis) + f.ConvertDecimal(data.TotalTopup) + f.ConvertDecimal(data.TotalFoodcourtCash)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total EDC Regis \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalNominalEdcRegis)) + Environment.NewLine;
                s += "Total EDC Topup \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalNominalEdcTopup)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total EDC \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalNominalDebit)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Ticket Emoney \t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTicketPayEmoney)) + Environment.NewLine;
                s += "Total FoodCourt Emoney \t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalFoodcourtEmoney)) + Environment.NewLine;
                s += "Total Refund \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalRefund)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Emoney Trasaksi \t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalFoodcourtEmoney) + f.ConvertDecimal(data.TotalRefund) + f.ConvertDecimal(data.TotalTicketPayEmoney)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Dana Modal \t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalDanaModal)) + Environment.NewLine;
                s += "Total Cash in \t\t: " + f.ConvertToRupiah((f.ConvertDecimal(data.TotalCashIn) - f.ConvertDecimal(data.TotalDanaModal))) + Environment.NewLine;
                s += "Total Cashout \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalCashOut)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                decimal TotalCasbox = ((f.ConvertDecimal(data.TotalDanaModal) + (f.ConvertDecimal(data.TotalCashIn) - f.ConvertDecimal(data.TotalDanaModal))) - f.ConvertDecimal(data.TotalCashOut));
                s += "Total Cashbox \t\t: " + f.ConvertToRupiah(TotalCasbox) + Environment.NewLine;
                s += "Cash Input by Kasir \t: " + f.ConvertToRupiah(Input) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                if (Input == TotalCasbox)
                {
                    s += "Closing Status \t\t: Matching" + Environment.NewLine;
                }
                else if (Input < TotalCasbox)
                {
                    s += "Closing Status \t: Kasir Minus Uang Cashbox" + Environment.NewLine;
                }
                else if (Input > TotalCasbox)
                {
                    s += "Closing Status \t: Kasir Kelebihan Uang Cashbox" + Environment.NewLine;
                }

                s += "-------------------------------------------------------" + Environment.NewLine;
                foreach (string pfoot in f.GetFooterPrint())
                {
                    s += pfoot + Environment.NewLine;
                }

                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    int HeadreX = 0;
                    int startY = 0;
                    int Offset = 0;
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

        public ReturnResult ClosingPrintV2(string LogId)
        {
            var res = new ReturnResult();
            try
            {
                var data = f.GetLogClosing_V2("SP_GetLogClosing", LogId);
                string s = "Datetime \t: " + data.Datetime + Environment.NewLine;
                s += "Merchant ID \t: " + data.ComputerName + Environment.NewLine;
                s += "Nama Petugas \t: " + data.Username + Environment.NewLine;
                s += "Log ID Closing \t: CLS-" + ff.ConvertLogDigit(f.ConvertDecimal(LogId)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Penjualan Kartu" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Kartu Terjual \t: " + data.KartuBaruCounting + Environment.NewLine;
                s += "Total Amount \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalKartuBaruNominal)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Transaksi Refund" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Kartu Refund \t: " + data.TotalKartuRefundCounting + Environment.NewLine;
                s += "Jaminan \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.RefundJaminan)) + Environment.NewLine;
                s += "Saldo Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.RefundSaldo)) + Environment.NewLine;
                s += "Total Amount \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalRefund)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Penjualan Ticket" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Qty Ticket \t\t: " + data.TicketSalesCounting + Environment.NewLine;
                s += "Cash \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TicketPayCash)) + Environment.NewLine;
                s += "Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TicketPaySaldo)) + Environment.NewLine;
                s += "Emoney + Cash  \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TicketPaySaldoNCash)) + Environment.NewLine;
                s += "EDC  \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.PayEDC)) + Environment.NewLine;
                s += "Emoney + EDC  \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.PaySaldoEDC)) + Environment.NewLine;
                s += "Total Amount \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TicketTotalAmount)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Penjualan Topup" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Cash \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTopupCash)) + Environment.NewLine;
                s += "EDC \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTopupEDC)) + Environment.NewLine;
                s += "Total Amount \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTopup)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Penjualan F&B" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Cash \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.FNBPayCash)) + Environment.NewLine;
                s += "Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.FNBPaySaldo)) + Environment.NewLine;
                s += "Total Amount \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.FNBAll)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Kasir Total Transaksi" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Dana Modal \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.DanaModal)) + Environment.NewLine;
                s += "Cash In \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalCashin)) + Environment.NewLine;
                s += "Cash Out \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalCashOut)) + Environment.NewLine;
                s += "Total Cash \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalCashBox)) + Environment.NewLine;
                s += "Total EDC \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalEDC)) + Environment.NewLine;
                s += "Total Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalEmoney)) + Environment.NewLine;
                s += "Total Cash+EDC+Emoney: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTransaksiKasir)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Kasir Closing" + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Real  \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.KasirInputNominal)) + Environment.NewLine;
                s += "Total By System \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalCashBox)) + Environment.NewLine;
                s += "Matching (+/-) \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.MoneyCashboxSelisih)) + Environment.NewLine;
                s += "Matching Status \t\t: " + data.MatchingStatus + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                foreach (string pfoot in f.GetFooterPrint())
                {
                    s += pfoot + Environment.NewLine;
                }

                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    int HeadreX = 0;
                    int startY = 0;
                    int Offset = 0;
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

        public ReturnResult ClosingPrintFoodCourt(string ComputerName, string Username, string LogId, List<DataDashTenant> data)
        {
            var res = new ReturnResult();
            try
            {
                var totalFoodcourt = f.GetLogClosing_V2("SP_GetLogClosing", LogId);
                string s = "Datetime \t: " + totalFoodcourt.Datetime + Environment.NewLine;
                s += "Merchant ID \t: " + ComputerName + Environment.NewLine;
                s += "Nama Petugas \t: " + Username + Environment.NewLine;
                s += "Log ID Closing \t: CLS-" + ff.ConvertLogDigit(f.ConvertDecimal(LogId)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += " Tenant Performance " + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Nama Tenant \t\t\t Total Penjualan" + Environment.NewLine;
                int count = 0;

                foreach (var d in data)
                {
                    count++;
                    s += " " + count + ". " + d.NamaTenant + "\t\t" + f.ConvertToRupiah(f.ConvertDecimal(d.TotalPenjualan)) + Environment.NewLine;
                }

                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Pay Cash \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(totalFoodcourt.FNBPayCash)) + Environment.NewLine;
                s += "Pay Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(totalFoodcourt.FNBPaySaldo)) + Environment.NewLine;
                s += "Total Transaksi \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(totalFoodcourt.FNBAll)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;

                foreach (string pfoot in f.GetFooterPrint())
                {
                    s += pfoot + Environment.NewLine;
                }

                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    int HeadreX = 0;
                    int startY = 0;
                    int Offset = 0;
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

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
