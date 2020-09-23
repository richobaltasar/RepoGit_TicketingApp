using SharedCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCVoucher : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        SharedCode.Ticketing f = new Ticketing();

        public UCVoucher()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var data = f.GetDataVoucher(txtResult.Text);
                if (data.id != null)
                {
                    if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                    {
                        MenuKasir un = new MenuKasir();
                        un.Dock = DockStyle.Fill;
                        Main.Instance.PnlContainer.Controls.Add(un);
                    }

                    try
                    {
                        Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();

                        Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                        UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                        if (fc != null)
                        {
                            DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                            if(dt_grid != null)
                            {
                                if (data.NominalVoucher.toInt() > 0)
                                {
                                    if (data.JenisVoucher.Contains("Semua Transaksi") == true)
                                    {
                                        string[] row = new string[] {
                                            "x",
                                            data.id.ToString()+"-"+data.CodeVoucher,
                                            "VOUCHER",
                                            "Voucher "+data.NamaVoucher+" untuk "+data.JenisVoucher,
                                            data.NominalVoucher.toNumber(),
                                            "1",
                                            "-"+ data.NominalVoucher.toNumber()};
                                        dt_grid.Rows.Add(row);
                                    }
                                    else if(data.JenisVoucher.Contains("Transaksi Tiket") == true)
                                    {
                                        int t = 0;
                                        decimal total = 0;
                                        foreach (DataGridViewRow rows in dt_grid.Rows)
                                        {
                                            if (rows.Cells[2].Value.ToString() != "TICKET")
                                            {
                                                t++;
                                                decimal ss = g.ConvertToDecimal(rows.Cells[6].Value.ToString());
                                                total = total + ss;
                                            }
                                        }

                                        if(t>0)
                                        {
                                            if(total>= data.NominalVoucher.toInt())
                                            {
                                                string[] row = new string[] {
                                                "x",
                                                data.id.ToString()+"-"+data.CodeVoucher,
                                                "VOUCHER",
                                                "Voucher "+data.NamaVoucher+" untuk "+data.JenisVoucher,
                                                data.NominalVoucher.toNumber(),
                                                "1",
                                                "-"+ data.NominalVoucher.toNumber()};
                                                dt_grid.Rows.Add(row);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Total transaksi tiket belum mencukupi , voucher ini hanya berlaku untuk total transaksi ticket >= Rp "+data.NominalVoucher.toNumber()+"", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Tidak ada transaksi tiket, voucher ini hanya berlaku untuk transaksi ticket","Sorry",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                txtResult.Clear();
                txtResult.Focus();
            }
        }

        public void input_keyPad(string key, string Object)
        {
            TextBox txt = this.Controls.Find(Object, true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                if (key == "<")
                {
                    if (txt.Text.Length > 0)
                    {
                        txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                    }
                }
                else if (key == ">")
                {
                    txt.SelectionStart = txt.Text.Length;
                    txt.SelectionLength = 0;
                }
                else if (key == "DEL")
                {
                    txt.Clear();
                }
                else if (key == "ENTER")
                {

                }
                else
                {
                    string data = (txt.Text + key);
                    txt.Text = txt.Text + key;
                }
            }

        }

        private void button53_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button54_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button52_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button55_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button57_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button58_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button46_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button45_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button48_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button43_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            var data = f.GetDataVoucher(txtResult.Text);
            if (data.id != null)
            {
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                {
                    MenuKasir un = new MenuKasir();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }

                try
                {
                    Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();

                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                    UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                    if (fc != null)
                    {
                        DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                        if (dt_grid != null)
                        {
                            if (data.NominalVoucher.toInt() > 0)
                            {
                                if (data.JenisVoucher.Contains("Semua Transaksi") == true)
                                {
                                    string[] row = new string[] {
                                            "x",
                                            data.id.ToString()+"-"+data.CodeVoucher,
                                            "VOUCHER",
                                            "Voucher "+data.NamaVoucher+" untuk "+data.JenisVoucher,
                                            data.NominalVoucher.toNumber(),
                                            "1",
                                            "-"+ data.NominalVoucher.toNumber()};
                                    dt_grid.Rows.Add(row);
                                }
                                else if (data.JenisVoucher.Contains("Transaksi Tiket") == true)
                                {
                                    int t = 0;
                                    decimal total = 0;
                                    foreach (DataGridViewRow rows in dt_grid.Rows)
                                    {
                                        if (rows.Cells[2].Value.ToString() != "TICKET")
                                        {
                                            t++;
                                            decimal ss = g.ConvertToDecimal(rows.Cells[6].Value.ToString());
                                            total = total + ss;
                                        }
                                    }

                                    if (t > 0)
                                    {
                                        if (total >= data.NominalVoucher.toInt())
                                        {
                                            string[] row = new string[] {
                                                "x",
                                                data.id.ToString()+"-"+data.CodeVoucher,
                                                "VOUCHER",
                                                "Voucher "+data.NamaVoucher+" untuk "+data.JenisVoucher,
                                                data.NominalVoucher.toNumber(),
                                                "1",
                                                "-"+ data.NominalVoucher.toNumber()};
                                            dt_grid.Rows.Add(row);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Total transaksi tiket belum mencukupi , voucher ini hanya berlaku untuk total transaksi ticket >= Rp " + data.NominalVoucher.toNumber() + "", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tidak ada transaksi tiket, voucher ini hanya berlaku untuk transaksi ticket", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            txtResult.Clear();
            txtResult.Focus();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtResult");
        }
    }
}
