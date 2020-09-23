using SharedCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCTopup : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        ACR_NFC NFC = new ACR_NFC();
        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;

        //string readername = "ACS ACR122 0";

        public UCTopup()
        {
            InitializeComponent();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
            txtBoxInput.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = button2.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = button5.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = button3.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = button6.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = button4.Text;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            txtBoxInput.Text = button23.Text;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");

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

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtBoxInput.Text != null)
            {
                var Card = NFC.ReadCardDataKey();
                if (Card.IdCard != "")
                {
                    decimal topup = g.ConvertToDecimal(txtBoxInput.Text);
                    if (topup > 0)
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                        {
                            MenuKasir un = new MenuKasir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        try
                        {

                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                            if (fc != null)
                            {
                                DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                                if (dt_grid != null)
                                {

                                    string[] row = new string[] { "x", "2","TOPUP",
                                    "Topup transaksi - Saldo sebelumnya : "+g.ConvertToRupiah(Card.SaldoEmoney),
                                    g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)), "1", g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)*g.ConvertToDecimal("1"))};
                                    dt_grid.Rows.Add(row);
                                }

                            }
                            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        txtBoxInput.Clear();
                    }
                }

            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();

            txtBoxInput.Text = "";
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();

            txtBoxInput.Text = "";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (txtBoxInput.Text != null)
            {
                var Card = NFC.ReadCardDataKey();
                if (Card.IdCard != "")
                {
                    decimal topup = g.ConvertToDecimal(txtBoxInput.Text);
                    if (topup > 0)
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                        {
                            MenuKasir un = new MenuKasir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        try
                        {
                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                            if (fc != null)
                            {
                                DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                                if (dt_grid != null)
                                {

                                    string[] row = new string[] { "x", "1","TOPUP",
                                    "Topup transaksi - Saldo sebelumnya : "+g.ConvertToRupiah(Card.SaldoEmoney),
                                    g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)), "1", g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)*g.ConvertToDecimal("1"))};
                                    dt_grid.Rows.Add(row);
                                }

                            }
                            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        txtBoxInput.Clear();
                    }
                }

            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtBoxInput");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBoxInput.Text != null)
            {
                var Card = NFC.ReadCardDataKey();
                if (Card.IdCard != "")
                {
                    decimal topup = g.ConvertToDecimal(txtBoxInput.Text);
                    if (topup > 0)
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                        {
                            MenuKasir un = new MenuKasir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        try
                        {
                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                            if (fc != null)
                            {
                                DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                                if (dt_grid != null)
                                {

                                    string[] row = new string[] { "x", "1","TOPUP",
                                    "Topup transaksi - Saldo sebelumnya : "+g.ConvertToRupiah(Card.SaldoEmoney),
                                    g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)), "1", g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)*g.ConvertToDecimal("1"))};
                                    dt_grid.Rows.Add(row);
                                }

                            }
                            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        txtBoxInput.Clear();
                    }
                }

            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (txtBoxInput.Text != null)
            {
                var Card = NFC.ReadCardDataKey();
                if (Card.IdCard != "")
                {
                    decimal topup = g.ConvertToDecimal(txtBoxInput.Text);
                    if (topup > 0)
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                        {
                            MenuKasir un = new MenuKasir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        try
                        {
                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                            if (fc != null)
                            {
                                DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                                if (dt_grid != null)
                                {

                                    string[] row = new string[] { "x", "1","TOPUP",
                                    "Topup transaksi - Saldo sebelumnya : "+g.ConvertToRupiah(Card.SaldoEmoney),
                                    g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)), "1", g.ConvertToNumber(g.ConvertToDecimal(txtBoxInput.Text)*g.ConvertToDecimal("1"))};
                                    dt_grid.Rows.Add(row);
                                }

                            }
                            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        txtBoxInput.Clear();
                    }
                }

            }
        }
    }
}
