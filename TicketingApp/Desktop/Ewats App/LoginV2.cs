using Ewats_App.Function;
using Newtonsoft.Json;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ewats_App
{
    public partial class LoginV2 : Form
    {
        GlobalFunc f = new GlobalFunc();
        ReadFromFile r = new ReadFromFile();
        private const int SM_CONVERTIBLESLATEMODE = 0x2003;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int nIndex);
        public LoginV2()
        {
            InitializeComponent();
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
            txtUsername.Focus();
            txtInputSelected.Text = "txtUsername";
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.PasswordChar = '*';
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtInputSelected.Text = "txtUsername";
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtInputSelected.Text = "txtPassword";
        }

        private void button53_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
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

        private void button54_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button42_Click(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, txtInputSelected.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //this.txtPassword.PasswordChar = this.checkBox1.Checked ? char.MinValue : '*';
                txtPassword.UseSystemPasswordChar = false;
                checkBox1.Text = "Hide Password";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                //txtPassword.PasswordChar = '*';
                checkBox1.Text = "Show Password";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginProc()
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
            ulang:
                var fe = new UserLib();
                var res = fe.LoginProc(txtUsername.Text, txtPassword.Text);
                if (res.status == 1)
                {
                    var data = JsonConvert.DeserializeObject<OLogin>(res.Output);
                    if (data.ID != null)
                    {
                        General.IDUser = data.ID;
                        var close = f.CheckClosingCashier(f.GetNamaUser(General.IDUser));
                        if (close.Success == false)
                        {
                            f.PageControl("MainV2");
                            //f.PageControl("Main");
                            this.Hide();
                        }
                        else
                        {
                            var msgbox = MessageBox.Show(close.Message + ", Silahkan melakukan Approval oleh bagian Keuangan",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        //lblAlert.Text = "Username / Password tidak valid";
                        //lblAlert.Visible = true;
                    }
                }
                else
                {
                    var result = f.messageboxError(res.Message);
                    if (result == DialogResult.Retry)
                    {
                        goto ulang;
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            else
            {
                //lblAlert.Text = "Silahkan isi field yang masih kosong";
                //lblAlert.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginProc();
        }
    }
}
