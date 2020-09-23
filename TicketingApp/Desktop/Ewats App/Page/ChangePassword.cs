using Ewats_App.Function;
using SharedCode.Models;
using System;
using System.Windows.Forms;


namespace Ewats_App.Page
{
    public partial class ChangePassword : Form
    {
        Function.GlobalFunc f = new GlobalFunc();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Text == "Show")
            {
                if (checkBox1.Checked == true)
                {
                    checkBox1.Text = "Hide";
                    txtCurrentPass.PasswordChar = (char)0;
                    txtConfirmPass.PasswordChar = (char)0;
                    txtNewPass.PasswordChar = (char)0;
                }
            }
            else if (checkBox1.Text == "Hide")
            {
                if (checkBox1.Checked == false)
                {
                    checkBox1.Text = "Show";
                    txtCurrentPass.PasswordChar = '*';
                    txtConfirmPass.PasswordChar = '*';
                    txtNewPass.PasswordChar = '*';
                }
            }
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            txtConfirmPass.PasswordChar = '*';
            txtCurrentPass.PasswordChar = '*';
            txtNewPass.PasswordChar = '*';
            //this.TopMost = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtCurrentPass.Text != "" && txtConfirmPass.Text != "" && txtNewPass.Text != "")
            {
                if (f.CheckPasswordCurrentValid(txtCurrentPass.Text, General.IDUser) == true)
                {
                    if (txtConfirmPass.Text == txtNewPass.Text)
                    {
                        if (txtNewPass.Text.Length > 5)
                        {
                            var data = f.SaveUpdateChangePassword(General.IDUser, txtNewPass.Text);
                            if (data.Success == true)
                            {
                                var res = f.ShowMessagebox("Password telah berhasil dirubah", "Warning", MessageBoxButtons.OK);
                                if (res == DialogResult.OK)
                                {
                                    this.Close();
                                    f.PageControl("Main");
                                }
                            }
                            else
                            {
                                var res = f.ShowMessagebox("Password gagal dirubah", "Warning", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            f.ShowMessagebox("New Password tidak valid, min 5 Character", "Warning", MessageBoxButtons.OK);
                            txtNewPass.Focus();
                        }
                    }
                    else
                    {
                        f.ShowMessagebox("New Password not match", "Warning", MessageBoxButtons.OK);
                        txtConfirmPass.Focus();
                    }
                }
                else
                {
                    f.ShowMessagebox("Password yang diinput tidak valid", "Warning", MessageBoxButtons.OK);
                    txtCurrentPass.Focus();
                }
            }
            else
            {
                f.ShowMessagebox("silahkan input semua field", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPass_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNewPass_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtConfirmPass_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtNewPass_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtConfirmPass_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtCurrentPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LblNamaCompany_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
