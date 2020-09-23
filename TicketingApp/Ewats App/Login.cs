using Ewats_App.Function;
using Newtonsoft.Json;
using SharedCode;
using SharedCode.Models;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Ewats_App
{
    public static class TabletPCSupport
    {
        private static readonly int SM_CONVERTIBLESLATEMODE = 0x2003;
        private static readonly int SM_TABLETPC = 0x56;

        private static Boolean isTabletPC = false;

        public static Boolean SupportsTabletMode { get { return isTabletPC; } }

        public static Boolean IsTabletMode
        {
            get
            {
                return QueryTabletMode();
            }
        }

        static TabletPCSupport()
        {
            isTabletPC = (GetSystemMetrics(SM_TABLETPC) != 0);
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int nIndex);

        private static Boolean QueryTabletMode()
        {
            int state = GetSystemMetrics(SM_CONVERTIBLESLATEMODE);
            return (state == 0) && isTabletPC;
        }
    }
    public partial class Login : Form
    {
        GlobalFunc f = new GlobalFunc();
        ReadFromFile r = new ReadFromFile();
        private const int SM_CONVERTIBLESLATEMODE = 0x2003;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int nIndex);
        public Login()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {

        }
        private void Login_Load(object sender, EventArgs e)
        {
            lbl_version.Text = f.VersionLabel;
            if (ConfigurationFileStatic.VFDPort != null && ConfigurationFileStatic.VFDPort != "")
            {
                VFDPort.send("Login Kasir", "Please wait ....", ConfigurationFileStatic.VFDPort);
            }

            txtPassword.Text = "aqsw";
            txtUsername.Text = "root";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            LoginProc();
            f.HideOnScreenKeyboard();
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
                        lblAlert.Text = "Username / Password tidak valid";
                        lblAlert.Visible = true;
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
                lblAlert.Text = "Silahkan isi field yang masih kosong";
                lblAlert.Visible = true;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (ConfigurationFileStatic.VFDPort != null && ConfigurationFileStatic.VFDPort != "")
            {
                VFDPort.send("", "", VFDPort.sp.PortName);
            }

            if (VFDPort.sp.IsOpen)
            {
                VFDPort.sp.Close();
                VFDPort.sp.Dispose();
                VFDPort.sp = null;
            }
            Application.Exit();
        }

        private void LblNamaCompany_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)

            {
                f.HideOnScreenKeyboard();
                LoginProc();
            }
        }

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            f.PageControl("EwatsConfig");
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            if (ConfigurationFileStatic.VFDPort != null && ConfigurationFileStatic.VFDPort != "")
            {
                VFDPort.send("Login Kasir", "Please wait ....", ConfigurationFileStatic.VFDPort);
            }
        }

        private void txtUsername_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtPassword_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
