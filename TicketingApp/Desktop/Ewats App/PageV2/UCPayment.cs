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
    public partial class UCPayment : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();
        Sales s = new Sales();
        ACR_NFC NFC = new ACR_NFC();

        public UCPayment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
        }

        private void chkPakaiSaldo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPakaiSaldo.Checked)
            {
                //txtSaldo.Visible = true;
                txtPakaiSaldo.Visible = true;
                decimal sisa = g.ConvertToDecimal(txtSaldo.Text) - g.ConvertToDecimal(txtTotalTransaksi.Text);
                if (sisa > 0)
                {
                    txtPakaiSaldo.Text = g.ConvertToRupiah(-g.ConvertToDecimal(txtSaldo.Text) + sisa);
                    txtTotalBayar.Text = g.ConvertToRupiah(0);
                }
                else
                {
                    txtPakaiSaldo.Text = g.ConvertToRupiah(-(g.ConvertToDecimal(txtSaldo.Text)));
                    txtTotalBayar.Text = g.ConvertToRupiah(-sisa);
                }
            }
            else
            {
                txtSaldo.Visible = false;
                txtPakaiSaldo.Visible = false;
                txtTotalBayar.Text = g.ConvertToRupiah(g.ConvertToDecimal(txtTotalTransaksi.Text));

            }

            if (g.ConvertToDecimal(txtTotalBayar.Text) == 0)
            {
                btnBayarEmoney.Show();
                btnTunai.Hide();
                btnDebit.Hide();
                btnBayarEmoney.Location = new Point(22, 225);
                btnBack.Location = new Point(22, 291);
                panelScan.Height = 369;

            }
            else
            {
                btnBayarEmoney.Hide();
                btnTunai.Show();
                btnDebit.Show();
                btnTunai.Location = new Point(22, 225);
                btnDebit.Location = new Point(22, 291);
                btnBack.Location = new Point(22, 357);
                panelScan.Height = 433;
            }
        }

        private void UCPayment_Load(object sender, EventArgs e)
        {
            //panelScan.Location = new Point((this.Width - panelScan.Width) / 2, (this.Height - panelScan.Height) / 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (g.ConvertToDecimal(txtTotalBayar.Text) > 0)
            {
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCPayDebit"))
                {
                    UCPayDebit un = new UCPayDebit();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }
                Main.Instance.PnlContainer.Controls["UCPayDebit"].BringToFront();

                Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                UserControl fc = tbx.Controls.Find("UCPayDebit", true).FirstOrDefault() as UserControl;
                if (fc != null)
                {
                    TextBox txtTotalTransaksi = fc.Controls.Find("txtTotalTransaksi", true).FirstOrDefault() as TextBox;
                    if (txtTotalTransaksi != null)
                    {
                        txtTotalTransaksi.Text = txtTotalBayar.Text;
                    }
                }
            }
        }

        private void btnTunai_Click(object sender, EventArgs e)
        {
            if (g.ConvertToDecimal(txtTotalBayar.Text) > 0)
            {
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCPayTunai"))
                {
                    UCPayTunai un = new UCPayTunai();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }
                Main.Instance.PnlContainer.Controls["UCPayTunai"].BringToFront();

                Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                UserControl fc = tbx.Controls.Find("UCPayTunai", true).FirstOrDefault() as UserControl;
                if (fc != null)
                {
                    TextBox txtTotalTransaksi = fc.Controls.Find("txtTotalTransaksi", true).FirstOrDefault() as TextBox;
                    TextBox txtUangDiterima = fc.Controls.Find("txtUangDiterima", true).FirstOrDefault() as TextBox;

                    if (txtTotalTransaksi != null && txtUangDiterima != null)
                    {
                        txtTotalTransaksi.Text = txtTotalBayar.Text;
                        txtUangDiterima.Focus();
                    }
                }
            }
        }

        private void btnBayarEmoney_Click(object sender, EventArgs e)
        {
            if (txtTotalBayar.Text != "")
            {
                decimal TotalBayar = g.ConvertToDecimal(txtTotalBayar.Text);
                if (TotalBayar == 0 && chkPakaiSaldo.Checked == true)
                {
                    var list = new List<TransaksiListPay>();
                    var Trx = new TransaksiPaySave();
                    Trx.Emoney = g.ConvertToDecimal(txtPakaiSaldo.Text);
                    Trx.TotalBayar = TotalBayar;
                    Trx.PaymentMethod = "EMONEY";
                    Trx.AccountNumber = Main.Instance.LblCardId.Text + "-" + Main.Instance.lblCodeId.Text;
                    Trx.TotalTransaksi = g.ConvertToDecimal(txtTotalTransaksi.Text);
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
                                        list.Add(data);
                                    }
                                    dt_grid.Rows.Clear();
                                }
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
                                    if (d.AccountNumber == AccountNum)
                                    {
                                        var loadKey = NFC.LoaAuthoKey();
                                        if (loadKey.Success == true)
                                        {
                                            var SaldoEmoney = NFC.UpdateBlok("04", "04", d.Balanced);
                                            var Ticket = NFC.UpdateBlok("05", "04", d.Ticket);
                                            if (SaldoEmoney.Success == true && Ticket.Success == true)
                                            {
                                                txtTotalTransaksi.Text = "";
                                                txtTotalBayar.Text = "";
                                                txtPakaiSaldo.Text = "";

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
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }
    }
}
