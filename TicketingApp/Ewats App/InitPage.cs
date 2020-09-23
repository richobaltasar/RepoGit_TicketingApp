using Ewats_App.Function;
using SharedCode;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ewats_App
{
    public partial class InitPage : Form
    {
        public int countTimer = 30;

        GlobalFunc f = new GlobalFunc();
        ReadFromFile r = new ReadFromFile();
        [DllImport("user32")] private static extern bool HideCaret(IntPtr hWnd);

        public InitPage()
        {
            InitializeComponent();
        }

        private void InitPage_Load(object sender, EventArgs e)
        {
            lbl_version.Text = f.VersionLabel;
            check_running();
            timer1.Interval = 1000;
            timer1.Start();
            txtMessage.Text = "Checking License Key...";
            txtMessage.TabStop = false;
            HideCaret(txtMessage.Handle);
        }
        public void check_running()
        {
            try
            {
                var processExists = Process.GetProcesses().Any(p => p.ProcessName.Contains("kiosk"));

                if (processExists == true)
                {
                    var data = Process.GetProcessesByName("Ewats App");
                    int count = 0;
                    if (data.Count() > 1)
                    {
                        foreach (Process proc in Process.GetProcessesByName("Ewats App"))
                        {
                            if (count < data.Count() - 1)
                            {
                                proc.Kill();
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            countTimer--;
            lblCount.Text = countTimer.ToString();
            if (countTimer == 28)
            {
                timer1.Stop();
                if (CheckKey() == true)
                {
                    timer1.Start();
                }
            }
            else if (countTimer == 26)
            {
                txtMessage.Text = "Checking Configuration...";
                timer1.Stop();
            CheckDBConnAppLagi:
                if (CheckConfig() == true)
                {
                    if (f.CheckDBConnApp(ConfigurationFileStatic.ConnStrLog) == true)
                    {
                        timer1.Start();
                        timer1.Interval = 1;
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Tidak dapat terhubung ke server, silahkan tekan retry untuk mencoba lagi atau tekan cancel dan check konfigurasi database pada setting DB", "Important Question", MessageBoxButtons.RetryCancel);
                        if (dialogResult == DialogResult.Retry)
                        {
                            goto CheckDBConnAppLagi;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
            }
            else if (countTimer == 0)
            {
                this.Hide();
                this.Opacity = 0.0f;
                this.ShowInTaskbar = false;
                timer1.Stop();
                f.PageControl("LoginV2");
            }
        }
        public bool CheckKey()
        {
            bool res = false;
            if (r.CheckFileKey() == true)
            {
                string key = Encrypt.DecryptString(r.ReadFileKey(), "BISMILLAH");
                string Result = Encrypt.DecryptString(key, "BISMILLAH");
                if (Result.Length == 16)
                {
                    if (Result.Contains("TENTAKEL") == true)
                    {
                        string dateExp = Result.Replace("TENTAKEL", "");
                        decimal now = f.ConvertDecimal(DateTime.Now.ToString("yyyyMMdd"));
                        if (f.ConvertDecimal(dateExp) >= now)
                        {
                            res = true;
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show("Please, input new lincense key, your key had been expired, please click Yes, if you want input new Key", "Important Question", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                this.Hide();
                                this.Opacity = 0.0f;
                                this.ShowInTaskbar = false;
                                f.PageControl("InsertLicense");
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Please, input new lincense key, your key has not valid, please click Yes, if you want input new Key", "Important Question", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            this.Hide();
                            this.Opacity = 0.0f;
                            this.ShowInTaskbar = false;
                            f.PageControl("InsertLicense");
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Please, input new lincense key, your key has not valid, please click Yes, if you want input new Key", "Important Question", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.Hide();
                        this.Opacity = 0.0f;
                        this.ShowInTaskbar = false;
                        f.PageControl("InsertLicense");
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            else
            {
                string Key = ConfigurationManager.ConnectionStrings["Key"].ConnectionString;
                if (Key != "")
                {
                    string key = Encrypt.DecryptString(Key, "BISMILLAH");
                    string Result = Encrypt.DecryptString(key, "BISMILLAH");
                    if (Result.Length == 16)
                    {
                        if (Result.Contains("TENTAKEL") == true)
                        {
                            string dateExp = Result.Replace("TENTAKEL", "");
                            decimal now = f.ConvertDecimal(DateTime.Now.ToString("yyyyMMdd"));
                            if (f.ConvertDecimal(dateExp) >= now)
                            {
                                res = true;
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show("Please, input new lincense key, your key had been expired, please click Yes, if you want input new Key", "Important Question", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    this.Hide();
                                    this.Opacity = 0.0f;
                                    this.ShowInTaskbar = false;
                                    f.PageControl("InsertLicense");
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show("Please, input new lincense key, your key has not valid, please click Yes, if you want input new Key", "Important Question", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                this.Hide();
                                this.Opacity = 0.0f;
                                this.ShowInTaskbar = false;
                                f.PageControl("InsertLicense");
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Please, input new lincense key, your key has not valid, please click Yes, if you want input new Key", "Important Question", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            this.Hide();
                            this.Opacity = 0.0f;
                            this.ShowInTaskbar = false;
                            f.PageControl("InsertLicense");
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    this.Hide();
                    this.Opacity = 0.0f;
                    this.ShowInTaskbar = false;
                    this.Hide();
                    f.PageControl("InsertLicense");
                }
            }
            return res;
        }

        public bool CheckConfig()
        {
            bool res = false;
            if (r.CheckFileConfig() == true)
            {
                var data = new ConfigurationFile();
                data = r.ReadFileConfig();
                if (data.ConnStrLog != null && data.ConnStrLog != ""
                        && data.IpServer != null && data.IpServer != ""
                        && data.DBServer != null && data.DBServer != ""
                        && data.UsernameServer != null && data.UsernameServer != ""
                        && data.PasswordServer != null && data.PasswordServer != ""
                        && data.PathImgWeb != null && data.PathImgWeb != ""
                )
                {
                    ConfigurationFileStatic.ConnStrLog = Encrypt.DecryptString(data.ConnStrLog, "BISMILLAH");
                    ConfigurationFileStatic.IpServer = Encrypt.DecryptString(data.IpServer, "BISMILLAH");
                    ConfigurationFileStatic.DBServer = Encrypt.DecryptString(data.DBServer, "BISMILLAH");
                    ConfigurationFileStatic.UsernameServer = Encrypt.DecryptString(data.UsernameServer, "BISMILLAH");
                    ConfigurationFileStatic.PasswordServer = Encrypt.DecryptString(data.PasswordServer, "BISMILLAH");
                    ConfigurationFileStatic.PathImgWeb = Encrypt.DecryptString(data.PathImgWeb, "BISMILLAH");
                    if (data.VFDPort != "")
                    {
                        ConfigurationFileStatic.VFDPort = Encrypt.DecryptString(data.VFDPort, "BISMILLAH");
                    }
                    else
                    {
                        ConfigurationFileStatic.VFDPort = null;
                    }

                    txtMessage.Text = "Configuration files already exists";
                    res = true;
                }
                else
                {
                    timer1.Stop();
                    DialogResult dialogResult = MessageBox.Show("Please, input Configuration file, click Yes, if you want to input", "Important Question", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.Hide();
                        this.Opacity = 0.0f;
                        this.ShowInTaskbar = false;
                        timer1.Stop();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }

            }
            else
            {
                string conn = ConfigurationManager.ConnectionStrings["dblocal"].ConnectionString;
                if (conn != null && conn != "")
                {
                    if (f.CheckDBConnApp(conn) == true)
                    {
                        ConfigurationFileStatic.ConnStrLog = conn;
                        ConfigurationFileStatic.PathImgWeb = ConfigurationManager.ConnectionStrings["ImgCloud"].ConnectionString;
                        txtMessage.Text = "Configuration files already exists";
                        res = true;
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Please, input Configuration file, click Yes, if you want to input", "Important Question", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            this.Hide();
                            this.Opacity = 0.0f;
                            this.ShowInTaskbar = false;
                            timer1.Stop();
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    this.Hide();
                    this.Opacity = 0.0f;
                    this.ShowInTaskbar = false;
                    f.PageControl("EwatsConfig");
                }
            }
            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            this.Opacity = 0.0f;
            this.ShowInTaskbar = false;
            f.PageControl("EwatsConfig");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            this.Opacity = 0.0f;
            this.ShowInTaskbar = false;
            f.PageControl("EwatsConfig");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
