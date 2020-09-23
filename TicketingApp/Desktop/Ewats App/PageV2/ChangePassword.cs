using SharedCode.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class ChangePassword : Form
    {
        Function.GlobalFunc f = new Function.GlobalFunc();

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
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

        private void txtCurrentPassword_Click(object sender, EventArgs e)
        {
            lblInputName.Text = "txtCurrentPassword";
        }

        private void txtNewPassword_Click(object sender, EventArgs e)
        {
            lblInputName.Text = "txtNewPassword";
        }

        private void txtConfPass_Click(object sender, EventArgs e)
        {
            lblInputName.Text = "txtConfPass";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, lblInputName.Text);
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            lblInputName.Text = "txtCurrentPassword";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Text != "" && txtNewPassword.Text != "" && txtConfPass.Text != "")
            {

                var data = new ChangePasswordModel();
                data.UserId = General.IDUser;
                data.CurrentPassword = txtCurrentPassword.Text;
                data.NewPassword = txtNewPassword.Text;
                data.ConfPassword = txtConfPass.Text;
                var result = f.SaveUpdateChangePasswordV2(data);
                if (result.status == "success")
                {
                    MessageBox.Show("Change password success", "Info", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.message, result.title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Field masih ada yang kosong", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
