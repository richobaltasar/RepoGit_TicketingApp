using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCPayDebit : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();
        Sales s = new Sales();

        ACR_NFC NFC = new ACR_NFC();

        public UCPayDebit()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCPayment"))
            {
                UCPayment un = new UCPayment();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["UCPayment"].BringToFront();

            txtBankName.Text = "";
            txtCardNum.Text = "";
            txtNoReff.Text = "";
        }

        private void UCPayDebit_Load(object sender, EventArgs e)
        {
            //panelScan.Location = new Point((this.Width - (panelScan.Width - 120)) / 2, (this.Height - (panelScan.Height - 120)) / 2);
        }

        private void txtTotalBayar_Click(object sender, EventArgs e)
        {

        }

        private void txtCardNum_Click(object sender, EventArgs e)
        {
            lblInput.Text = "txtCardNum";
        }

        private void txtNoReff_Click(object sender, EventArgs e)
        {
            lblInput.Text = "txtNoReff";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        public void input_keyPad(string key, string Object)
        {
            TextBox txt = this.Controls.Find(Object, true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                if (key == "BkSpc")
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
                else if (key == "Del")
                {
                    txt.Text = "0";
                }
                else if (key == "Enter")
                {
                }
                else if (key == "Cancel")
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

        private void button2_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCPayment"))
            {
                UCPayment un = new UCPayment();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["UCPayment"].BringToFront();

            txtBankName.Text = "";
            txtCardNum.Text = "";
            txtNoReff.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInput.Text);
        }

        private void btnBayarEmoney_Click(object sender, EventArgs e)
        {
            if (txtNoReff.Text != "" && txtCardNum.Text != "" && txtBankName.Text != "")
            {
                var list = new List<TransaksiListPay>();
                var Trx = new TransaksiPaySave();
                Trx.TotalBayar = g.ConvertToDecimal(txtTotalTransaksi.Text);
                Trx.PaymentMethod = "EDC";
                Trx.TotalTransaksi = g.ConvertToDecimal(txtTotalTransaksi.Text);

                Trx.BankName = txtBankName.Text;
                Trx.CardNumber = txtCardNum.Text;
                Trx.Noreff = txtNoReff.Text;

                int AdaKartuBaru = 0;
                decimal SaldoJaminanBaru = 0;

                if (Main.Instance.lblCodeId.Text == "0")
                {
                    Main.Instance.lblCodeId.Text = f.GenCodeID();
                }

                if (Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                {
                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                    UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;
                    if (fc != null)
                    {
                        DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;

                        if (dt_grid != null)
                        {
                            if (dt_grid.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow row in dt_grid.Rows)
                                {
                                    var data = new TransaksiListPay();
                                    data.Id = row.Cells[1].Value.ToString();
                                    data.Category = row.Cells[2].Value.ToString();
                                    data.NamaItem = row.Cells[3].Value.ToString();
                                    data.Harga = g.ConvertToDecimal(row.Cells[4].Value.ToString());
                                    data.Qtx = g.ConvertToDecimal(row.Cells[5].Value.ToString());
                                    data.Total = g.ConvertToDecimal(row.Cells[6].Value.ToString());

                                    if (data.Category == "CARD")
                                    {
                                        AdaKartuBaru++;
                                        SaldoJaminanBaru = data.Harga;
                                    }

                                    list.Add(data);
                                }
                                dt_grid.Rows.Clear();
                            }
                        }
                    }
                }
                if (Main.Instance.PnlContainer.Controls.ContainsKey("UCPayment"))
                {
                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                    UserControl fc = tbx.Controls.Find("UCPayment", true).FirstOrDefault() as UserControl;
                    if (fc != null)
                    {
                        CheckBox chkPakaiSaldo = fc.Controls.Find("chkPakaiSaldo", true).FirstOrDefault() as CheckBox;
                        if (chkPakaiSaldo != null)
                        {
                            if (chkPakaiSaldo.Checked == true)
                            {
                                Trx.PaymentMethod = Trx.PaymentMethod + "+EMONEY";
                                TextBox txtPakaiSaldo = fc.Controls.Find("txtPakaiSaldo", true).FirstOrDefault() as TextBox;
                                if (txtPakaiSaldo != null)
                                {
                                    Trx.Emoney = g.ConvertToDecimal(txtPakaiSaldo.Text);
                                    Trx.AccountNumber = Main.Instance.LblCardId.Text + "-" + Main.Instance.lblCodeId.Text;
                                }
                            }
                            else
                            {
                                Trx.AccountNumber = Main.Instance.LblCardId.Text + "-" + Main.Instance.lblCodeId.Text;
                            }
                        }

                        TextBox txtTotalTransaksi = fc.Controls.Find("txtTotalTransaksi", true).FirstOrDefault() as TextBox;
                        if (txtTotalTransaksi != null)
                        {
                            Trx.TotalTransaksi = g.ConvertToDecimal(txtTotalTransaksi.Text);
                        }
                    }
                }

                if (list.Count > 0)
                {
                    Trx.MerchantName = f.GetComputerName();
                    Trx.ChasierName = f.GetNamaUser(General.IDUser);
                    var res = s.SaveLogTransaksi(Trx);
                    if (res.status == "SUCCESS")
                    {
                        if (res.message != "")
                        {
                            Trx.IdTrx = res.message;
                        ulang:
                            decimal TotalListCheck = 0;
                            foreach (var l in list)
                            {
                                l.IdTrx = Trx.IdTrx;
                                var r = s.SaveTransaksiListDetail(l);
                                if (r.status == "SUCCESS")
                                {
                                    TotalListCheck = g.ConvertToDecimal(r.message);
                                }
                            }

                            if (TotalListCheck != list.Count())
                            {
                                goto ulang;
                            }

                            var d = s.GetDataAccount(Trx.AccountNumber);
                            var card = NFC.ReadCardDataKey();
                            if (card.IdCard != "")
                            {
                                string AccountNum = card.IdCard + "-" + card.CodeId;
                                if (d.AccountNumber == AccountNum && AdaKartuBaru == 0)
                                {
                                    var loadKey = NFC.LoaAuthoKey();
                                    if (loadKey.Success == true)
                                    {
                                        var SaldoEmoney = NFC.UpdateBlok("04", "04", d.Balanced);
                                        var Ticket = NFC.UpdateBlok("05", "04", d.Ticket);
                                        if (SaldoEmoney.Success == true && Ticket.Success == true)
                                        {
                                            txtTotalTransaksi.Text = "";
                                            txtBankName.Text = "";
                                            txtCardNum.Text = "";
                                            txtNoReff.Text = "";

                                            if (!Main.Instance.PnlContainer.Controls.ContainsKey("TransaksiBerhasil"))
                                            {
                                                TransaksiBerhasil un = new TransaksiBerhasil();
                                                un.Dock = DockStyle.Fill;
                                                Main.Instance.PnlContainer.Controls.Add(un);
                                            }

                                            Main.Instance.PnlContainer.Controls["TransaksiBerhasil"].BringToFront();
                                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                            UserControl fc = tbx.Controls.Find("TransaksiBerhasil", true).FirstOrDefault() as UserControl;
                                            if (fc != null)
                                            {
                                                RichTextBox rtResulPrint = fc.Controls.Find("rtResulPrint", true).FirstOrDefault() as RichTextBox;
                                                Panel panelNominal = fc.Controls.Find("panelNominal", true).FirstOrDefault() as Panel;
                                                if (rtResulPrint != null)
                                                {
                                                    ExtendedRichTextBox advRichTextBox = new ExtendedRichTextBox();
                                                    advRichTextBox.Size = rtResulPrint.Size;
                                                    advRichTextBox.Location = rtResulPrint.Location;
                                                    advRichTextBox.ScrollBars = RichTextBoxScrollBars.Both;
                                                    advRichTextBox.Rtf = f.DisplayRTFTransaksi(Trx.IdTrx);
                                                    if (panelNominal != null)
                                                    {
                                                        panelNominal.Controls.Add(advRichTextBox);
                                                    }

                                                    advRichTextBox.BringToFront();
                                                }

                                                Label lblTotalTransaksi = fc.Controls.Find("lblTotalTransaksi", true).FirstOrDefault() as Label;
                                                if (lblTotalTransaksi != null)
                                                {
                                                    lblTotalTransaksi.Text = g.ConvertToRupiah(Trx.TotalTransaksi);
                                                }

                                                Label lblIdTrx = fc.Controls.Find("lblIdTrx", true).FirstOrDefault() as Label;

                                                if (lblIdTrx != null)
                                                {
                                                    lblIdTrx.Text = Trx.IdTrx;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (d.AccountNumber != AccountNum && AdaKartuBaru > 0)
                                {
                                    var loadKey = NFC.LoaAuthoKey();
                                    if (loadKey.Success == true)
                                    {
                                        var SaldoEmoney = NFC.UpdateBlok("04", "04", d.Balanced);
                                        var Ticket = NFC.UpdateBlok("08", "08", d.UangJaminan);
                                        var CodeID = NFC.UpdateBlok("09", "08", d.AccountNumber.Split('-')[1]);
                                        if (SaldoEmoney.Success == true && Ticket.Success == true && CodeID.Success == true)
                                        {
                                            txtTotalTransaksi.Text = "";
                                            txtBankName.Text = "";
                                            txtCardNum.Text = "";
                                            txtNoReff.Text = "";

                                            if (!Main.Instance.PnlContainer.Controls.ContainsKey("TransaksiBerhasil"))
                                            {
                                                TransaksiBerhasil un = new TransaksiBerhasil();
                                                un.Dock = DockStyle.Fill;
                                                Main.Instance.PnlContainer.Controls.Add(un);
                                            }

                                            Main.Instance.PnlContainer.Controls["TransaksiBerhasil"].BringToFront();
                                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                            UserControl fc = tbx.Controls.Find("TransaksiBerhasil", true).FirstOrDefault() as UserControl;
                                            if (fc != null)
                                            {
                                                RichTextBox rtResulPrint = fc.Controls.Find("rtResulPrint", true).FirstOrDefault() as RichTextBox;
                                                Panel panelNominal = fc.Controls.Find("panelNominal", true).FirstOrDefault() as Panel;
                                                if (rtResulPrint != null)
                                                {
                                                    ExtendedRichTextBox advRichTextBox = new ExtendedRichTextBox();
                                                    advRichTextBox.Size = rtResulPrint.Size;
                                                    advRichTextBox.Location = rtResulPrint.Location;
                                                    advRichTextBox.ScrollBars = RichTextBoxScrollBars.Both;
                                                    advRichTextBox.Rtf = f.DisplayRTFTransaksi(Trx.IdTrx);
                                                    if (panelNominal != null)
                                                    {
                                                        panelNominal.Controls.Add(advRichTextBox);
                                                    }

                                                    advRichTextBox.BringToFront();
                                                }

                                                Label lblTotalTransaksi = fc.Controls.Find("lblTotalTransaksi", true).FirstOrDefault() as Label;
                                                if (lblTotalTransaksi != null)
                                                {
                                                    lblTotalTransaksi.Text = g.ConvertToRupiah(Trx.TotalTransaksi);
                                                }

                                                Label lblIdTrx = fc.Controls.Find("lblIdTrx", true).FirstOrDefault() as Label;

                                                if (lblIdTrx != null)
                                                {
                                                    lblIdTrx.Text = Trx.IdTrx;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        private void txtBankName_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["MasterBank"];
            if (fc != null)
            {
                fc.Show();
                fc.BringToFront();
                Label LblPage = fc.Controls.Find("LblPage", true).FirstOrDefault() as Label;
                if (LblPage != null)
                {
                    LblPage.Text = this.Name;
                }
            }
            else
            {
                PageV2.MasterBank frm = new PageV2.MasterBank();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.BringToFront();
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                Label LblPage = frm.Controls.Find("LblPage", true).FirstOrDefault() as Label;
                if (LblPage != null)
                {
                    LblPage.Text = this.Name;
                }
                frm.Show();
            }
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }
    }
}
