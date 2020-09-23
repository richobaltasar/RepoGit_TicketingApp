using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCOpenCasir : UserControl
    {

        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();

        public UCOpenCasir()
        {
            InitializeComponent();
        }


        private void button18_Click(object sender, EventArgs e)
        {
            OpenSave();
        }

        public void OpenSave()
        {
            if (txtDanaModal.Text != "")
            {
                decimal DanaModal = g.ConvertToDecimal(txtDanaModal.Text);
                if (DanaModal > 0)
                {
                ulang:
                    var data = new TambahModalCashbox();
                    data.ComputerName = f.GetComputerName();
                    data.NamaUser = f.GetNamaUser(General.IDUser);
                    data.Nominal = DanaModal;
                    var save = f.SaveDataTambahModal(data);
                    if (save.Success == true)
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
                        {
                            UCScanKartu un = new UCScanKartu();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
                    }
                    else
                    {
                        var res = MessageBox.Show("Terjadi Kesalahan pada SaveDataTambahModal, err:" + save.Message + "", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        if (res == DialogResult.Retry)
                        {
                            goto ulang;
                        }
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
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
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LoginV2 f = new LoginV2();
            f.Show();
            f.BringToFront();
            f.StartPosition = FormStartPosition.CenterScreen;
            Main.Instance.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            OpenSave();
        }

        private void UCOpenCasir_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point((this.Width - panel1.Width) / 2, (this.Height - panel1.Height) / 2);
        }

        private void button17_Click(object sender, EventArgs e)
        {

            LoginV2 f = new LoginV2();
            f.Show();
            f.BringToFront();
            f.StartPosition = FormStartPosition.CenterScreen;
            Main.Instance.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Location = new Point(ClientSize.Width / 2 - panel1.Size.Width / 2, ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;
        }
    }
}
