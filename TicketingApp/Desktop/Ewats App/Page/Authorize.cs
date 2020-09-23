using System;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.Page
{
    public partial class Authorize : Form
    {
        Function.GlobalFunc f = new Function.GlobalFunc();

        public Authorize()
        {
            InitializeComponent();
        }

        private void txtCurrentPass_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtCurrentPass_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtUsername_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtCurrentPass.Text != "")
            {
                var data = f.LoginProc(txtUsername.Text, txtCurrentPass.Text);
                if (data.HakAkses == "Admin")
                {
                    Form fc = Application.OpenForms["CancelTransaksi"];
                    if (fc != null)
                    {
                        fc.Show();
                        fc.BringToFront();
                        TextBox IdTransaction = fc.Controls.Find("IdTransaction", true).FirstOrDefault() as TextBox;
                        Label LblAuthorizeBy = fc.Controls.Find("LblAuthorizeBy", true).FirstOrDefault() as Label;

                        if (IdTransaction != null)
                        {
                            IdTransaction.Text = IdTrx.Text;
                        }

                        if (LblAuthorizeBy != null)
                        {
                            LblAuthorizeBy.Text = data.ID;
                        }
                    }
                    else
                    {
                        Page.CancelTransaksi frm = new Page.CancelTransaksi();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;

                        TextBox IdTransaction = frm.Controls.Find("IdTransaction", true).FirstOrDefault() as TextBox;
                        if (IdTransaction != null)
                        {
                            IdTransaction.Text = IdTrx.Text;
                        }
                        Label LblAuthorizeBy = frm.Controls.Find("LblAuthorizeBy", true).FirstOrDefault() as Label;
                        if (LblAuthorizeBy != null)
                        {
                            LblAuthorizeBy.Text = data.ID;
                        }
                        frm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    f.ShowMessagebox("Maaf anda tidak mendapat akses", "Sorry", MessageBoxButtons.OK);
                }
            }
            else
            {
                f.ShowMessagebox("Username atau password masih kosong", "Sorry", MessageBoxButtons.OK);
            }
        }

        private void Authorize_Load(object sender, EventArgs e)
        {

        }

        private void Authorize_Activated(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtCurrentPass.Text = "";
        }
    }
}
