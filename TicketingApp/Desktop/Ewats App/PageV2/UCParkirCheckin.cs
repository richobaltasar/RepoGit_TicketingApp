using SharedCode;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCParkirCheckin : UserControl
    {
        GeneralFunction g = new GeneralFunction();

        public UCParkirCheckin()
        {
            InitializeComponent();
        }

        private void UCParkirCheckin_Load(object sender, EventArgs e)
        {

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

            ClearInput();
        }

        public void ClearInput()
        {
            txtCharges.Clear();
            txtJamMasuk.Clear();
            txtNoPolis.Clear();
            txtTglMasuk.Clear();
            txtTypeKendaraan.Clear();
            txtBarcodeId.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtBarcodeId.Text != "" && txtCharges.Text != "" && txtNoPolis.Text != "")
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
                            string[] row = new string[] { "x", txtBarcodeId.Text,"PARKIR",
                                    "Parkir Masuk : "+ txtTglMasuk.Text+" "+ txtJamMasuk.Text+" - "+txtTypeKendaraan.Text+" - "+txtNoPolis.Text,
                                    g.ConvertToNumber(g.ConvertToDecimal(txtCharges.Text)),
                                    "1",
                                    g.ConvertToNumber(g.ConvertToDecimal(txtCharges.Text)*g.ConvertToDecimal("1"))};
                            dt_grid.Rows.Add(row);
                        }

                    }
                    Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                ClearInput();
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtNoPolis");
        }
    }
}
