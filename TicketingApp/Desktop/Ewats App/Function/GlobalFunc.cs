using Ewats_App.Model;
using Newtonsoft.Json;
using SharedCode;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ewats_App.Function
{
    public class ExtendedRichTextBox : RichTextBox
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr LoadLibrary(string dllName);
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                {
                    baseParams.ClassName = "RICHEDIT50W";
                }
                return baseParams;
            }
        }
    }
    public static class VFDPort
    {
        public static GlobalFunc f = new GlobalFunc();
        public static SerialPort sp = new SerialPort();
        internal static bool HasOpenPort(string portName)
        {
            bool portState = false;

            if (portName != string.Empty)
            {
                foreach (var itm in SerialPort.GetPortNames())
                {
                    if (itm.Contains(portName))
                    {
                        if (VFDPort.sp.IsOpen) { portState = true; }
                        else { portState = false; }
                    }
                }
            }

            else { System.Windows.Forms.MessageBox.Show("Error: No Port Specified."); }

            return portState;
        }
        internal static bool KonekPort(string portName)
        {
            bool portState = false;
            try
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                    sp = null;
                }
                sp.PortName = portName;
                sp.BaudRate = 9600;
                sp.Parity = Parity.None;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Open();
                if (HasOpenPort(portName) == true)
                {
                    portState = true;
                }
            }
            catch (Exception ex)
            {
                var res = f.ShowMessagebox("Serial port tidak ditemukan, silahkan tekan yes untuk melakukan settingan PORT VFD \n " + ex.Message, "Warning", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    f.PageControl("VFDConfig");
                }

            }
            return portState;
        }
        public static bool send(string Line1, string Line2, string Port)
        {
            bool res = false;
        ulang:
            if (HasOpenPort(Port) == false)
            {
                if (KonekPort(Port) == true)
                {
                    goto ulang;
                }
            }
            else
            {
                string title = Line1;
                byte[] bytesToSend = new byte[1] { 0x0C };
                VFDPort.sp.Write(bytesToSend, 0, 1);
                int d = title.Length;
                if (title.Length < 20)
                {
                    int sisa = 20 - d;
                    for (int a = 0; a < sisa; a++)
                    {
                        title = title + " ";
                    }
                }
                byte[] asciiBytes = Encoding.ASCII.GetBytes(title);
                VFDPort.sp.Write(asciiBytes, 0, asciiBytes.Length);
                byte[] enter = new byte[2] { 0x1F, 0x42 };
                VFDPort.sp.WriteLine(Line2);
                VFDPort.sp.Write(enter, 0, enter.Length);
                res = true;
            }

            return res;
        }
    }

    public class GlobalFunc
    {
        string server = ConfigurationFileStatic.ConnStrLog;
        string ImgPath = ConfigurationFileStatic.PathImgWeb;

        public SqlConnection conn = new SqlConnection();

        public SqlCommand cmd = new SqlCommand();

        GeneralFunction f = new GeneralFunction();
        Sales s = new Sales();


        public string DisplayRTFTransaksi(string idTrx)
        {
            var ListPrint = s.GetLogTransaksiListDetailPOS(idTrx);
            StringBuilder strRtfTable = new StringBuilder();

            //beginning of rich text format,dont customize this begining line
            strRtfTable.Append(@"{\rtf1 \fbidis\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 calibri;}}");
            //create 5 rows with 3 cells each.Write a method with parameters for accepting any no of rows and columns.
            int no = 0;
            strRtfTable.Append(@"\trowd");
            strRtfTable.Append(@"\cellx7300");
            strRtfTable.Append(@"\intbl " + "=========================================================");
            strRtfTable.Append(@"\intbl \cell \row"); //create row

            strRtfTable.Append(@"\trowd");
            strRtfTable.Append(@"\cellx7300");
            strRtfTable.Append(@"\intbl " + "WAHANA BERMAIN NUSANTARA".PadLeft(50));
            strRtfTable.Append(@"\intbl \cell \row"); //create row

            strRtfTable.Append(@"\trowd");
            strRtfTable.Append(@"\cellx7300");
            strRtfTable.Append(@"\intbl \cell \row"); //create row
            strRtfTable.Append(@"\trowd");
            strRtfTable.Append(@"\cellx7300");
            strRtfTable.Append(@"\intbl " + "=========================================================");
            strRtfTable.Append(@"\intbl \cell \row"); //create row
            decimal totalTrx = 0;
            foreach (var d in ListPrint)
            {
                no++;
                strRtfTable.Append(@"\trowd");

                strRtfTable.Append(@"\cellx300");
                strRtfTable.Append(@"\cellx3000");
                strRtfTable.Append(@"\cellx3500");
                //Last cell with width 2000.Last position is 4000 (which is 2000+2000)
                strRtfTable.Append(@"\cellx5300");
                strRtfTable.Append(@"\cellx7300");
                int pjg = 29;
                int tab2 = f.ConvertToRupiah(d.Total).Length;
                int tab1 = f.ConvertToRupiah(d.Harga).Length;
                strRtfTable.Append(@"\intbl " + no + @"\cell " + d.NamaItem + @"\cell  " + d.Qtx + @"\cell " + f.ConvertToRupiah(d.Harga).PadLeft(pjg - tab1) + @"\cell " + f.ConvertToRupiah(d.Total).PadLeft(pjg - tab2));
                strRtfTable.Append(@"\intbl \cell \row"); //create row
                totalTrx = totalTrx + d.Total;
            }

            int pjg2 = 29;
            int tab3 = f.ConvertToRupiah(totalTrx).Length;
            string TotalS = "Total Transaksi ";

            strRtfTable.Append(@"\trowd");
            strRtfTable.Append(@"\cellx5300");
            strRtfTable.Append(@"\cellx7300");
            strRtfTable.Append(@"\intbl  " + TotalS.PadLeft(73 - TotalS.Length) + @"\cell " + f.ConvertToRupiah(totalTrx).PadLeft(pjg2 - tab3));
            strRtfTable.Append(@"\intbl \cell \row"); //create row

            strRtfTable.Append(@"\pard");
            strRtfTable.Append(@"}");
            return strRtfTable.ToString();
        }

        public string VersionLabel
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Product Name: {4}, Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Product Name: {4}, Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
            }
        }

        public string VersiId
        {
            get
            {
                return VersionLabel.Split(',')[1].Split(':')[1].Trim().Replace(".", "_");
            }
        }

        #region keyboard_virtual
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(String sClassName, String sAppName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, String lpszClass, String lpszWindow);

        //private Process _p = null;
        public void ShowOnScreenKeyboard()
        {
            IntPtr parent = FindWindow("Shell_TrayWnd", null);
            IntPtr child1 = FindWindowEx(parent, IntPtr.Zero, "TrayNotifyWnd", "");
            IntPtr keyboardWnd = FindWindowEx(child1, IntPtr.Zero, null, "Touch keyboard");

            uint WM_LBUTTONDOWN = 0x0201;
            uint WM_LBUTTONUP = 0x0202;
            UIntPtr x = new UIntPtr(0x01);
            UIntPtr x1 = new UIntPtr(0);
            IntPtr y = new IntPtr(0x0240012);
            PostMessage(keyboardWnd, WM_LBUTTONDOWN, x, y);
            PostMessage(keyboardWnd, WM_LBUTTONUP, x1, y);

        }

        public void HideOnScreenKeyboard()
        {
            uint WM_SYSCOMMAND = 0x0112;
            UIntPtr SC_CLOSE = new UIntPtr(0xF060);
            IntPtr y = new IntPtr(0);
            IntPtr KeyboardWnd = FindWindow("IPTip_Main_Window", null);
            PostMessage(KeyboardWnd, WM_SYSCOMMAND, SC_CLOSE, y);

        }

        #endregion

        public bool CheckDBConnApp(string ConnStr)
        {
            bool res = false;
            try
            {
                string con = ConnStr;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    connection.Close();
                    res = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false;
            }
            return res;
        }
        public string CheckDBLocal(string Ipserver, string DBname, string username, string password)
        {
            string res = "";
            try
            {
                string con = "Data Source = " + Ipserver + "; Initial Catalog = " + DBname + "; User ID = " + username + "; Password = " + password + "";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    connection.Close();
                    res = con;
                }
            }
            catch (Exception ex)
            {
                res = "error:" + ex.Message;
            }
            return res;
        }
        public bool CheckDbAlreadyExists(string Connstring, string DbName)
        {
            bool res = false;
            try
            {
                conn.ConnectionString = Connstring;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + DbName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            int count = 0;
                            while (reader.Read())
                            {
                                count++;
                            }
                            if (count > 0)
                            {
                                res = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return res;
        }
        public bool CreateDB(string Connstring, string DBName)
        {
            bool res = false;
            try
            {
                conn.ConnectionString = Connstring;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "CREATE DATABASE " + DBName + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            res = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return res;
        }

        public void PageControl(string name)
        {

            switch (name)
            {
                case "Login":
                    #region LoginPage
                    Form fc1 = Application.OpenForms["Login"];
                    if (fc1 != null)
                    {
                        fc1.Show();
                        fc1.BringToFront();
                    }
                    else
                    {
                        Login frm = new Login();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "LoginV2":
                    #region LoginPageV2
                    Form fc11 = Application.OpenForms["LoginV2"];
                    if (fc11 != null)
                    {
                        fc11.Show();
                        fc11.BringToFront();
                    }
                    else
                    {
                        LoginV2 frm = new LoginV2();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "Main":
                    #region MainPage
                    Form fc2 = Application.OpenForms["Main"];
                    if (fc2 != null)
                    {
                        fc2.Show();
                        fc2.BringToFront();
                    }
                    else
                    {
                        Page.Main frm = new Page.Main();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "InsertLicense":
                    #region Insert License
                    Form fc3 = Application.OpenForms["InsertLicense"];
                    if (fc3 != null)
                    {
                        fc3.Show();
                        fc3.BringToFront();
                    }
                    else
                    {
                        InsertLicense frm = new InsertLicense();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "EwatsConfig":
                    #region Ewats Config
                    Form fc4 = Application.OpenForms["EwatsConfig"];
                    if (fc4 != null)
                    {
                        fc4.Show();
                        fc4.BringToFront();
                    }
                    else
                    {
                        EwatsConfig frm = new EwatsConfig();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "InitPage":
                    #region InitPage
                    Form fc5 = Application.OpenForms["InitPage"];
                    if (fc5 != null)
                    {
                        fc5.Show();
                        fc5.BringToFront();
                    }
                    else
                    {
                        InitPage frm = new InitPage();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "VFDConfig":
                    #region VFDConfig
                    Form fc6 = Application.OpenForms["VFDConfig"];
                    if (fc6 != null)
                    {
                        fc6.Show();
                        fc6.BringToFront();
                        fc6.TopMost = true;
                    }
                    else
                    {
                        VFDConfig frm = new VFDConfig();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion
                case "ChangePassword":
                    #region ChangePassword
                    Form fc7 = Application.OpenForms["ChangePassword"];
                    if (fc7 != null)
                    {
                        fc7.Show();
                        fc7.BringToFront();
                        fc7.TopMost = true;
                    }
                    else
                    {
                        Page.ChangePassword frm = new Page.ChangePassword();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                #endregion

                case "MainV2":
                    #region MainPageV2
                    Form fc8 = Application.OpenForms["Main"];
                    if (fc8 != null)
                    {
                        fc8.Show();
                        fc8.BringToFront();
                    }
                    else
                    {
                        PageV2.Main frm = new PageV2.Main();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.BringToFront();
                        frm.MaximizeBox = false;
                        frm.MinimizeBox = false;
                        frm.Show();
                    }
                    break;
                    #endregion
            }


        }

        public string GetNamaSPSesuaiVersiApp(string VersiID, string NamaSp)
        {
        ulang:
            string res = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetNamaSPSesuaiVersiApp @VersiID='" + VersiID + "', @NamaSp='" + NamaSp + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res = reader["NamaSP"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public OLogin LoginProc(string username, string password)
        {
        ulang:
            var res = new OLogin();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_Login @Username='" + username + "', @Password='" + password + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.ID = reader["id"].ToString();
                                res.HakAkses = reader["hakakses"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public void UpdatAccountData(ReaderCard ReadUpdate)
        {
            ReadUpdate.CodeId = ConvertDecimal(ReadUpdate.CodeId).ToString();
            if (ReadUpdate.CodeId.Length == 14)
            {
                var AccountUpdate = new DataAccount();
                AccountUpdate.AccountNumber = ReadUpdate.IdCard + "-" + ConvertDecimal(ReadUpdate.CodeId).ToString();
                AccountUpdate.BalancedSesudah = ReadUpdate.SaldoEmoney;
                AccountUpdate.Ticket = (ReadUpdate.TicketWeekDay + ReadUpdate.TicketWeekEnd);
                AccountUpdate.JaminanGelangYgTerbaca = ReadUpdate.SaldoJaminan;
                ReadUpdateAccountData(AccountUpdate);
            }

        }

        public List<DataTenant> GetTenant()
        {
        ulang:
            var data = new List<DataTenant>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetTenant";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new DataTenant();
                                d.Id = reader["idTenant"].ToString();
                                d.NamaTenant = reader["NamaTenant"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public ReturnResult CheckExpired(string AccountNum, string CodeId)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                CodeId = CodeId.Replace("\0", "");
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_CheckExpired " +
                        "@CodeId='" + AccountNum + "-" + CodeId + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["Status"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public string GetNamaUser(string IdUser)
        {
        ulang:
            string nama = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetNamaUser @IdUser=" + IdUser + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nama = reader["NamaLengkap"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return nama;
        }

        public DataKolomPrintInputVisitor GetPrintKolomVisitor()
        {
        ulang:
            var res = new DataKolomPrintInputVisitor();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetPrintKolomVisitor";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Visible = reader["Visible"].ToString();
                                res.Title = reader["Title"].ToString();
                                res.Nama = reader["Nama"].ToString();
                                res.MoKtp = reader["MoKtp"].ToString();
                                res.Alamat = reader["Alamat"].ToString();
                                res.NoTelp = reader["NoTelp"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public string GetComputerName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception ex)
            {
                return "Error :" + ex.Message;
            }
        }

        public UserMan GetProfile(string IdUser)
        {
        ulang:
            var res = new UserMan();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetProfile " +
                        "@IdUser=" + IdUser + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.id = reader["id"].ToString();
                                res.username = reader["username"].ToString();
                                res.NamaLengkap = reader["NamaLengkap"].ToString();
                                res.ImgLink = reader["Photo"].ToString();
                                res.Gender = reader["Gender"].ToString();
                                res.Alamat = reader["Alamat"].ToString();
                                res.Email = reader["Email"].ToString();
                                res.NoHp = reader["NoHp"].ToString();
                                res.password = reader["password"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public decimal ConvertDecimal(string data)
        {
            decimal res = 0;
            if (data != "" && data != null)
            {
                string filter = data.Replace("Rp", "").Replace(".", "").Replace(",", "").Replace("\0", "").Replace("\n", "").Replace("ÿ", "").Trim().Replace(":", "").Replace("/", "").Replace(" ", "");
                if (filter != "")
                {
                    if (filter.All(char.IsDigit) == true)
                    {
                        res = Convert.ToDecimal(filter);
                    }
                }
            }
            return res;
        }

        public ReturnResult SaveDataTambahModal(TambahModalCashbox data)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_SaveDataTambahModal " +
                        "@ComputerName='" + data.ComputerName + "'," +
                        "@NamaUser='" + data.NamaUser + "'," +
                        "@Nominal=" + data.Nominal + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveClosing(DashboardModel data, string ComputerName, string Username, decimal nominalCashierInput)
        {
        ulang:
            string sql = "";
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    sql = "exec  SP_Closing " +
                        "@TotalTransaksi=" + data.TotalTransaksi + "," +
                        "@TotalTopup=" + data.TotalTopup + "," +
                        "@TotalRegis=" + data.TotalRegis + "," +
                        "@TotalRefund=" + data.TotalRefund + "," +
                        "@TotalFoodcourt=" + (ConvertDecimal(data.TotalFoodcourtCash) + ConvertDecimal(data.TotalFoodcourtEmoney)) + "," +
                        "@TotalDanaModal=" + data.TotalDanaModal + "," +
                        "@TotalCashOut=" + data.TotalCashOut + "," +
                        "@TotalCashIn=" + data.TotalCashIn + "," +
                        "@TotalCashBox=" + data.TotalCashBox + "," +
                        "@TotalAllTicket=" + data.TotalAllTicket + "," +

                        "@TotalTrxEdc=" + data.TotalTrxEdc + "," +
                        "@TotalNominalDebit=" + ConvertDecimal(data.TotalNominalDebit) + "," +
                        "@TotalTrxEmoney=" + data.TotalTrxEmoney + "," +
                        "@TotalNominalDebitEmoney=" + ConvertDecimal(data.TotalNominalDebitEmoney) + "," +

                        "@TotalCashirInputMoneyCashbox=" + nominalCashierInput + "," +
                        "@ComputerName='" + ComputerName + "',@NamaUser='" + Username + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message + ": " + sql);

                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveClosing_V2(string SP_name, string ComputerName, string Username, decimal nominalCashierInput)
        {
        ulang:
            string sql = "";
            string NamaSP = GetNamaSPSesuaiVersiApp(VersiId, SP_name);
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    sql = "exec  " + NamaSP + " " +
                        "@TotalCashirInputMoneyCashbox=" + nominalCashierInput + "," +
                        "@ComputerName='" + ComputerName + "',@Username='" + Username + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message + ": " + sql);

                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveTransaksiTopup(SaveTopupTrx Data)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SaveTransaksiTopup " +
                        "@AccountNumber='" + Data.Card.IdCard + "-" + ConvertDecimal(Data.Card.CodeId).ToString() + "', " +
                        "@NominalTopup = " + Data.NominalTopup + ", " +
                        "@JenisPayment='" + Data.Pay.JenisTransaksi + "' ," +
                        "@PayCash= " + Data.Pay.PayCash + "," +
                        "@TotalBayar= " + Data.Pay.TotalBayar + "," +
                        "@TerimaUang= " + Data.Pay.TerimaUang + "," +
                        "@Kembalian= " + Data.Pay.Kembalian + "," +
                        "@SaldoSebelum= " + Data.Card.SaldoEmoney + "," +
                        "@SaldoSetelah= " + Data.Card.SaldoEmoneyAfter + "," +
                        "@Chasierby= '" + Data.NamaUser + "'," +
                        "@ComputerName= '" + Data.ComputerName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveTransaksiDebitTopup(SaveDebitTopupTrx Data)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SaveDebitTransaksiTopup " +
                        "@AccountNumber='" + Data.Card.IdCard + "-" + ConvertDecimal(Data.Card.CodeId).ToString() + "'," +
                        "@NominalTopup = " + Data.NominalTopup + "," +
                        "@JenisPayment='" + Data.Pay.JenisTransaksi + "'," +
                        "@TotalBayar=" + Data.Pay.TotalBayar + "," +
                        "@KodeBank='" + Data.Pay.KodeBank + "'," +
                        "@NamaBank='" + Data.Pay.NamaBank + "'," +

                        "@DiskonBank=" + Data.Pay.DiskonBank + "," +
                        "@NominalDiskonBank=" + Data.Pay.NominalDiskonBank + "," +
                        "@AdminCharges=" + Data.Pay.AdminCharges + "," +
                        "@DebitNominal=" + Data.Pay.DebitNominal + "," +

                        "@NoATM ='" + Data.Pay.NoATM + "' ," +
                        "@NoReff='" + Data.Pay.NoReff + "'," +
                        "@SaldoSebelum=" + Data.Card.SaldoEmoney + "," +
                        "@SaldoSetelah=" + Data.Card.SaldoEmoneyAfter + "," +
                        "@Chasierby= '" + Data.NamaUser + "'," +
                        "@ComputerName= '" + Data.ComputerName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveTransaksiRefund(SaveRefundCash Data)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_SaveTransaksiRefund " +
                        "@AccountNumber='" + Data.Card.IdCard + "-" + ConvertDecimal(Data.Card.CodeId).ToString() + "', " +
                        "@SaldoEmoney = " + Data.Card.SaldoEmoney + ", " +
                        "@SaldoJaminan=" + Data.Card.SaldoJaminan + " ," +
                        "@TicketWeekDay= " + Data.Card.TicketWeekDay + "," +
                        "@TicketWeekEnd= " + Data.Card.TicketWeekEnd + "," +
                        "@TotalNominalRefund= " + Data.NominalRefund + "," +
                        "@ChasierBy= '" + Data.NamaUser + "'," +
                        "@ComputerName= '" + Data.ComputerName + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveUpdateChangePassword(string UserId, string Password)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_SaveUpdateChangePassword " +
                        "@UserId=" + UserId + "," +
                        "@Password='" + Password + "'" +
                        "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public alert SaveUpdateChangePasswordV2(ChangePasswordModel data)
        {
        ulang:
            var res = new alert();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_SaveUpdateChangePasswordV2 " +
                        "@UserId=" + data.UserId + "," +
                        "@CurrentPassword='" + data.CurrentPassword + "'," +
                        "@NewPassword='" + data.NewPassword + "'," +
                        "@ConfPassword='" + data.ConfPassword + "'" +
                        "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.message = reader["message"].ToString();
                                res.status = reader["status"].ToString();
                                res.title = reader["title"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveUpdateStockOpname(KeranjangStockOpnameModel Data)
        {
            string namaUser = GetNamaUser(General.IDUser);
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_SaveUpdateStockOpname " +
                        "@idItem=" + Data.idItem + "," +
                        "@NamaTenant='" + Data.NamaTenant + "'," +
                        "@NamaItem='" + Data.NamaItem + "'," +
                        "@BykStok=" + Data.BykStok + "," +
                        "@BykStokUpdate=" + Data.BykStokUpdate + "," +
                        "@NamaUser='" + namaUser + "'" +
                        "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveTransaksiRegistrasi(SaveRegisTrx Data, string TicketId, string ComputerName, string Chasier)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();

                    string sql = "exec SP_SaveTransaksiRegistrasi " +
                        "@AccountNumber ='" + Data.Card.IdCard + "-" + ConvertDecimal(Data.Card.CodeIdAfter).ToString() + "'," +
                        "@SaldoEmoney = " + Data.Card.SaldoEmoney + "," +
                        "@SaldoEmoneyAfter = " + Data.Card.SaldoEmoneyAfter + "," +
                        "@TicketWeekDay = " + Data.Card.TicketWeekDay + "," +
                        "@TicketWeekDayAfter = " + Data.Card.TicketWeekDayAfter + "," +
                        "@TicketWeekEnd = " + Data.Card.TicketWeekEnd + "," +
                        "@TicketWeekEndAfter = " + Data.Card.TicketWeekEndAfter + "," +
                        "@SaldoJaminan = " + Data.Card.SaldoJaminan + "," +
                        "@SaldoJaminanAfter = " + Data.Card.SaldoJaminanAfter + "," +
                        "@IdTicketTrx = " + TicketId + "," +
                        "@Cashback = " + Data.Cashback + "," +
                        "@Topup = " + Data.Topup + "," +
                        "@Asuransi = " + Data.Asuransi + "," +
                        "@QtyTotalTiket = " + Data.QtyTotalTiket + "," +
                        "@TotalBeliTiket = " + Data.TotalBeliTiket + "," +
                        "@TotalAll=" + Data.TotalAll + "," +
                        "@JenisTransaksi='" + Data.Payment.JenisTransaksi + "'," +
                        "@TotalBayar=" + Data.Payment.TotalBayar + "," +
                        "@PayEmoney=" + Data.Payment.PayEmoney + "," +
                        "@PayCash=" + Data.Payment.PayCash + "," +
                        "@TerimaUang=" + Data.Payment.TerimaUang + "," +
                        "@Kembalian=" + Data.Payment.Kembalian + "," +
                        "@ComputerName='" + ComputerName + "'," +
                        "@Chasier='" + Chasier + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                string s = reader["_Success"].ToString();
                                if (s == "TRUE")
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message + ", " + ex.HResult);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveDebitTransaksiRegistrasi(SaveRegisDebitTrx Data, string TicketId, string ComputerName, string Chasier)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();

                    string sql = "exec SP_SaveDebitTransaksiRegistrasi " +
                        "@AccountNumber ='" + Data.Card.IdCard + "-" + ConvertDecimal(Data.Card.CodeIdAfter).ToString() + "'," +
                        "@SaldoEmoney = " + Data.Card.SaldoEmoney + "," +
                        "@SaldoEmoneyAfter = " + Data.Card.SaldoEmoneyAfter + "," +
                        "@TicketWeekDay = " + Data.Card.TicketWeekDay + "," +
                        "@TicketWeekDayAfter = " + Data.Card.TicketWeekDayAfter + "," +
                        "@TicketWeekEnd = " + Data.Card.TicketWeekEnd + "," +
                        "@TicketWeekEndAfter = " + Data.Card.TicketWeekEndAfter + "," +
                        "@SaldoJaminan = " + Data.Card.SaldoJaminan + "," +
                        "@SaldoJaminanAfter = " + Data.Card.SaldoJaminanAfter + "," +
                        "@IdTicketTrx = " + TicketId + "," +
                        "@Cashback = " + Data.Cashback + "," +
                        "@Topup = " + Data.Topup + "," +
                        "@Asuransi = " + Data.Asuransi + "," +
                        "@QtyTotalTiket = " + Data.QtyTotalTiket + "," +
                        "@TotalBeliTiket = " + Data.TotalBeliTiket + "," +
                        "@TotalAll=" + Data.TotalAll + "," +
                        "@JenisTransaksi='" + Data.Payment.JenisTransaksi + "'," +
                        "@TotalBayar=" + Data.Payment.TotalBayar + "," +
                        "@PayEmoney=" + Data.Payment.PayEmoney + "," +

                        "@KodeBank='" + Data.Payment.KodeBank + "'," +
                        "@NamaBank='" + Data.Payment.NamaBank + "'," +
                        "@DiskonBank=" + Data.Payment.DiskonBank + "," +
                        "@NominalDiskonBank=" + Data.Payment.NominalDiskonBank + "," +
                        "@AdminCharges=" + Data.Payment.AdminCharges + "," +
                        "@NoATM='" + Data.Payment.NoATM + "'," +
                        "@NoReff='" + Data.Payment.NoReff + "'," +
                        "@DebitNominal=" + Data.Payment.DebitNominal + "," +

                        "@ComputerName='" + ComputerName + "'," +
                        "@Chasier='" + Chasier + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveTicket(KeranjangTicket data, string AccountNumber, string IdTicket, string Chasier, string ComputerName)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    if (data.IdDiskon == "-")
                    {
                        data.IdDiskon = "0";
                    }
                    string sql = "exec SP_SaveTicketKeranjang " +
                        "@AccountNumber ='" + AccountNumber + "'," +
                        "@IdTicket =" + IdTicket + "," +
                        "@NamaTicket='" + data.NamaTicket + "'," +
                        "@Harga=" + data.Harga + "," +
                        "@Qty=" + data.Qty + "," +
                        "@Total=" + data.Total + "," +
                        "@IdDiskon=" + data.IdDiskon + "," +
                        "@NamaDiskon='" + data.NamaDiskon + "'," +
                        "@Diskon=" + data.Diskon.ToString().Replace(",", ".") + "," +
                        "@TotalDiskon=" + data.TotalDiskon + "," +
                        "@TotalAfterDiskon=" + data.TotalAfterDiskon + "," +
                        "@ChasierBy='" + Chasier + "'," +
                        "@ComputerName='" + ComputerName + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveSewa(KeranjangFoodcourt data, string AccountNumber, string IdTicket, string Chasier, string ComputerName)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveItemsFB(KeranjangFoodcourt data, string AccountNumber, string IdItems, string ChasierBy, string ComputerName)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_SaveItemsFB " +
                        "@IdItemsKeranjang =" + IdItems + "," +
                        "@KodeBarang =" + data.IdTrx + "," +
                        "@NamaItem='" + data.NamaItem + "'," +
                        "@Harga=" + data.Harga + "," +
                        "@Qtx=" + data.Qtx + "," +
                        "@AccountNumber='" + AccountNumber + "'," +
                        "@Chasierby='" + ChasierBy + "'," +
                        "@ComputerName='" + ComputerName + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveFoodCourtPayment(SaveFoodCourtPayment Data, string IdItemsKeranjang, string Chasier, string ComputerName)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                string CodeId = GenCodeID(); ;

                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "";
                    if (Data.Card == null)
                    {

                        sql = "exec SP_SaveFoodCourtPayment " +
                        "@AccountNumber='" + CodeId + "'," +
                        "@SaldoEmoney=0," +
                        "@SaldoEmoneyAfter=0," +
                        "@IdItemsKeranjang=" + IdItemsKeranjang + "," +
                        "@JenisTransaksi='" + Data.Pay.JenisTransaksi + "'," +
                        "@TotalBayar=" + Data.Pay.TotalBayar + "," +
                        "@PayEmoney=" + Data.Pay.PayEmoney + "," +
                        "@PayCash=" + Data.Pay.PayCash + "," +
                        "@TerimaUang=" + Data.Pay.TerimaUang + "," +
                        "@Kembalian=" + Data.Pay.Kembalian + "," +
                        "@ComputerName='" + ComputerName + "'," +
                        "@CashierBy='" + Chasier + "'";
                    }
                    else
                    {
                        CodeId = Data.Card.CodeId;
                        sql = "exec SP_SaveFoodCourtPayment " +
                        "@AccountNumber='" + Data.Card.IdCard + "-" + CodeId + "'," +
                        "@SaldoEmoney=" + Data.Card.SaldoEmoney + "," +
                        "@SaldoEmoneyAfter=" + Data.Card.SaldoEmoneyAfter + "," +
                        "@IdItemsKeranjang=" + IdItemsKeranjang + "," +
                        "@JenisTransaksi='" + Data.Pay.JenisTransaksi + "'," +
                        "@TotalBayar=" + Data.Pay.TotalBayar + "," +
                        "@PayEmoney=" + Data.Pay.PayEmoney + "," +
                        "@PayCash=" + Data.Pay.PayCash + "," +
                        "@TerimaUang=" + Data.Pay.TerimaUang + "," +
                        "@Kembalian=" + Data.Pay.Kembalian + "," +
                        "@ComputerName='" + ComputerName + "'," +
                        "@CashierBy='" + Chasier + "'";
                    }


                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveCancelTransakasiFoodcourt(string AuthorizeBy, string NamaKasir, string ComputerName, string IdTrx, string PaymentMethod, string TotalTransaksi, ReaderCard Datacard)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    if (TotalTransaksi != "")
                    {
                        TotalTransaksi = ConvertDecimal(TotalTransaksi).ToString();
                    }

                    connection.Open();
                    string sql = "";
                    if (PaymentMethod == "eMoney")
                    {
                        sql = "exec SP_SaveCancelTransakasiFoodcourt " +
                        "@IdTrx =" + IdTrx.Replace("FOODCOURT", "") + "," +
                        "@AccountNumber='" + Datacard.IdCard + '-' + Datacard.CodeId + "'," +
                        "@SaldoEmoney=" + Datacard.SaldoEmoney + "," +
                        "@AuthorizeBy='" + AuthorizeBy + "'," +
                        "@NamaKasir='" + NamaKasir + "'," +
                        "@ComputerName='" + ComputerName + "'" +
                        "";
                    }
                    else if (PaymentMethod == "Cash")
                    {
                        sql = "exec SP_SaveCancelTransakasiFoodcourt " +
                        "@IdTrx =" + IdTrx.Replace("FOODCOURT", "") + "," +
                        "@AccountNumber='0'," +
                        "@SaldoEmoney=0," +
                        "@AuthorizeBy='" + AuthorizeBy + "'," +
                        "@NamaKasir='" + NamaKasir + "'," +
                        "@ComputerName='" + ComputerName + "'" +
                        "";
                    }

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveCancelTransakasiTicket(string AuthorizeBy, string NamaKasir, string ComputerName, string IdTrx, string PaymentMethod, string TotalTransaksi, ReaderCard Datacard)
        {
        ulang:
            var res = new ReturnResult();
            string sql = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();

                    sql = "exec SP_SaveCancelTransakasiTicket " +
                        "@IdTrx =" + IdTrx.Replace("REG", "") + "," +
                        "@AccountNumber='" + Datacard.IdCard + '-' + Datacard.CodeId + "'," +
                        "@SaldoEmoney=" + Datacard.SaldoEmoney + "," +
                        "@AuthorizeBy='" + AuthorizeBy + "'," +
                        "@NamaKasir='" + NamaKasir + "'," +
                        "@ComputerName='" + ComputerName + "'" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message + Environment.NewLine + ", Tekan Retry untuk mengulang proses ini, atau tekan cancel untuk menutup aplikasi ini, dan silahkan contact your administrator");
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    InsertLogError(ex.Message, "SaveCancelTransakasiTicket", sql, NamaKasir);
                    Application.Exit();
                }
            }
            return res;
        }
        public ReturnResult SaveCancelTransakasiTopup(string AuthorizeBy, string NamaKasir, string ComputerName, string IdTrx, string PaymentMethod, string TotalTransaksi, ReaderCard Datacard)
        {
        ulang:
            var res = new ReturnResult();
            string sql = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();

                    sql = "exec SP_SaveCancelTransakasiTopup " +
                        "@IdTrx =" + IdTrx.Replace("TOPUP", "") + "," +
                        "@AccountNumber='" + Datacard.IdCard + '-' + Datacard.CodeId + "'," +
                        "@SaldoEmoney=" + Datacard.SaldoEmoney + "," +
                        "@AuthorizeBy='" + AuthorizeBy + "'," +
                        "@NamaKasir='" + NamaKasir + "'," +
                        "@ComputerName='" + ComputerName + "'" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message + Environment.NewLine + ", Tekan Retry untuk mengulang proses ini, atau tekan cancel untuk menutup aplikasi ini, dan silahkan contact your administrator");
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    InsertLogError(ex.Message, "SaveCancelTransakasiTicket", sql, NamaKasir);
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult SaveCancelTransakasiFnB(string AuthorizeBy, string NamaKasir, string ComputerName, string IdTrx, string PaymentMethod, string TotalTransaksi, ReaderCard Datacard)
        {
        ulang:
            var res = new ReturnResult();
            string sql = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();

                    sql = "exec SP_SaveCancelTransakasiFnB " +
                        "@IdTrx =" + IdTrx.Replace("FOODCOURT", "") + "," +
                        "@AccountNumber='" + Datacard.IdCard + '-' + Datacard.CodeId + "'," +
                        "@SaldoEmoney=" + Datacard.SaldoEmoney + "," +
                        "@AuthorizeBy='" + AuthorizeBy + "'," +
                        "@NamaKasir='" + NamaKasir + "'," +
                        "@ComputerName='" + ComputerName + "'" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message + Environment.NewLine + ", Tekan Retry untuk mengulang proses ini, atau tekan cancel untuk menutup aplikasi ini, dan silahkan contact your administrator");
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    InsertLogError(ex.Message, "SaveCancelTransakasiTicket", sql, NamaKasir);
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult InsertLogError(string MessageError, string FuncionName, string Parameter, string Username)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_InsertLogError @MessageError='',@FuncionName='',@Parameter='',@Username=''";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                                res.Message = reader["_Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public decimal GetAsuransi()
        {
        ulang:
            decimal data = 0;
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetAsuransi";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new Ticket();
                                if (reader["Harga"].ToString() != "")
                                {
                                    data = Convert.ToDecimal(reader["Harga"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public bool CheckNowWeekend()
        {
        ulang:
            bool res = true;
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_CheckNowWeekend";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string hr = reader["HariNow"].ToString();
                                if (hr != "" && hr != "Sunday" && hr != "Saturday")
                                {
                                    res = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public bool CheckPasswordCurrentValid(string Password, string User)
        {
        ulang:
            bool res = false;
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_CheckPassword " +
                        "@User=" + User + "," +
                        "@Password='" + Password + "'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["Result"].ToString().Contains("TRUE") == true)
                                {
                                    res = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult CheckOpenCashier()
        {
            string ComputerName = GetComputerName();
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_CheckOpenCashier '" + ComputerName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult CheckClosingCashier(string NamaUser)
        {
            string ComputerName = GetComputerName();
            string NamaSP = GetNamaSPSesuaiVersiApp(VersiId, "SP_CheckClosingMerchant");
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {

                    connection.Open();
                    string sql = "exec " + NamaSP + " '" + ComputerName + "','" + NamaUser + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Message = reader["_Message"].ToString();
                                if (reader["Success"].ToString().Contains("TRUE") == true)
                                {
                                    res.Success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public string GetIdTiket()
        {
        ulang:
            string Res = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GedtIdTiket";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Res = reader["IdTicket"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }

        public ReturnResult GetApakahiniKartubaru(string IdTrx, string AccountNumber)
        {
        ulang:
            var Res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetApakahiniKartubaru @IdTrx=" + IdTrx.Replace("REG", "") + ",@AccountNumber='" + AccountNumber + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                                Res.Message = reader["_Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }

        public ReturnResult CheckApakahIniTopupYangTerakhir(string Idtrx, string AccountNumber)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_CheckApakahIniTopupYangTerakhir @IdTrx=" + Idtrx.Replace("TOPUP", "") + ",@AccountNumber='" + AccountNumber + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                                res.Message = reader["_Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public ReturnResult CheckApakahAdaDataTrxFnB(string IdTrx, string AccountNumber)
        {
        ulang:
            var res = new ReturnResult();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_CheckApakahTrxFnBCancelValid @IdTrx=" + IdTrx.Replace("FOODCOURT", "") + ",@AccountNumber='" + AccountNumber + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                res.RetCode = Convert.ToInt16(reader["_Success"].ToString());
                                res.Message = reader["_Message"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public string GetIdTrx()
        {
        ulang:
            string Res = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec [dbo].[SP_GedtIdTrxF&B]";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Res = reader["IdItems"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }

        public List<Ticket> GetTicket(string Seacrh)
        {
        ulang:
            var data = new List<Ticket>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GETTICKETPRICE @search='" + Seacrh + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new Ticket();
                                if (reader["Harga"].ToString() != "")
                                {
                                    d.harga = Convert.ToDecimal(reader["Harga"].ToString());
                                }
                                d.IdTicket = reader["id"].ToString();
                                d.namaticket = reader["NamaTicket"].ToString();
                                d.ImgLink = ImgPath + reader["Img"].ToString();
                                if (reader["Asuransi"].ToString() != "")
                                {
                                    d.Asuransi = Convert.ToDecimal(reader["Asuransi"].ToString());
                                }
                                d.NamaDiskon = reader["NamaPromo"].ToString();
                                d.Diskon = Convert.ToDecimal(reader["Diskon"].ToString());
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public GetDataTransaksiRegistrasiModel GetDataTransaksiRegistrasi(AllTransaksiModel data)
        {
        ulang:
            var res = new GetDataTransaksiRegistrasiModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiRegistrasi " +
                        "@IdTrx=" + data.IdTrx + "," +
                        "@Datetime='" + data.Datetime + "'," +
                        "@JenisTransaksi='" + data.JenisTransaksi + "'," +
                        "@Nominal=" + data.Nominal + "," +
                        "@CashierBy='" + data.CashierBy + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.AccountNumber = reader["AccountNumber"].ToString();
                                res.Asuransi = ConvertDecimal(reader["Asuransi"].ToString());
                                res.Cashback = ConvertDecimal(reader["Cashback"].ToString());
                                res.CashierBy = reader["CashierBy"].ToString();
                                res.ComputerName = reader["ComputerName"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.IdTicketTrx = reader["IdTicketTrx"].ToString();
                                res.idTrx = "REG" + reader["idTrx"].ToString();
                                res.JenisTransaksi = reader["JenisTransaksi"].ToString();
                                res.Kembalian = ConvertDecimal(reader["Kembalian"].ToString());
                                res.PayCash = ConvertDecimal(reader["PayCash"].ToString());
                                res.PayEmoney = ConvertDecimal((reader["PayEmoney"].ToString()));
                                res.QtyTotalTiket = ConvertDecimal(reader["QtyTotalTiket"].ToString());
                                res.SaldoJaminan = ConvertDecimal(reader["SaldoJaminan"].ToString());
                                res.SaldoEmoneyBefore = ConvertDecimal(reader["SaldoEmoney"].ToString());
                                res.SaldoEmoneyAfter = ConvertDecimal(reader["SaldoEmoneyAfter"].ToString());
                                res.TerimaUang = ConvertDecimal(reader["TerimaUang"].ToString());
                                res.topup = ConvertDecimal(reader["topup"].ToString());
                                res.TotalAll = ConvertDecimal(reader["TotalAll"].ToString());
                                res.TotalBayar = ConvertDecimal(reader["TotalBayar"].ToString());
                                res.TotalBeliTiket = ConvertDecimal(reader["TotalBeliTiket"].ToString());
                                res.BankCode = reader["BankCode"].ToString();
                                res.NamaBank = reader["NamaBank"].ToString();
                                res.DiskonBank = ConvertDecimal(reader["DiskonBank"].ToString());
                                res.NominalDiskon = ConvertDecimal(reader["NominalDiskon"].ToString());
                                res.AdminCharges = ConvertDecimal(reader["AdminCharges"].ToString());
                                res.TotalDebit = ConvertDecimal(reader["TotalDebit"].ToString());
                                res.NoATM = reader["NoATM"].ToString();
                                res.NoReffEddPrint = reader["NoReffEddPrint"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public GetDataTransaksiRegistrasiModel GetDataTransaksiRegistrasiCancel(string IdTrx)
        {
        ulang:
            var res = new GetDataTransaksiRegistrasiModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiRegistrasiCancel " +
                        "@IdTrx=" + IdTrx.Replace("REG", "");
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.AccountNumber = reader["AccountNumber"].ToString();
                                res.Asuransi = ConvertDecimal(reader["Asuransi"].ToString());
                                res.Cashback = ConvertDecimal(reader["Cashback"].ToString());
                                res.CashierBy = reader["CashierBy"].ToString();
                                res.ComputerName = reader["ComputerName"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.IdTicketTrx = reader["IdTicketTrx"].ToString();
                                res.idTrx = "REG" + reader["idTrx"].ToString();
                                res.JenisTransaksi = reader["JenisTransaksi"].ToString();
                                res.Kembalian = ConvertDecimal(reader["Kembalian"].ToString());
                                res.PayCash = ConvertDecimal(reader["PayCash"].ToString());
                                res.PayEmoney = ConvertDecimal((reader["PayEmoney"].ToString()));
                                res.QtyTotalTiket = ConvertDecimal(reader["QtyTotalTiket"].ToString());
                                res.SaldoJaminan = ConvertDecimal(reader["SaldoJaminan"].ToString());
                                res.SaldoEmoneyBefore = ConvertDecimal(reader["SaldoEmoney"].ToString());
                                res.SaldoEmoneyAfter = ConvertDecimal(reader["SaldoEmoneyAfter"].ToString());
                                res.TerimaUang = ConvertDecimal(reader["TerimaUang"].ToString());
                                res.topup = ConvertDecimal(reader["topup"].ToString());
                                res.TotalAll = ConvertDecimal(reader["TotalAll"].ToString());
                                res.TotalBayar = ConvertDecimal(reader["TotalBayar"].ToString());
                                res.TotalBeliTiket = ConvertDecimal(reader["TotalBeliTiket"].ToString());
                                res.BankCode = reader["BankCode"].ToString();
                                res.NamaBank = reader["NamaBank"].ToString();
                                res.DiskonBank = ConvertDecimal(reader["DiskonBank"].ToString());
                                res.NominalDiskon = ConvertDecimal(reader["NominalDiskon"].ToString());
                                res.AdminCharges = ConvertDecimal(reader["AdminCharges"].ToString());
                                res.TotalDebit = ConvertDecimal(reader["TotalDebit"].ToString());
                                res.NoATM = reader["NoATM"].ToString();
                                res.NoReffEddPrint = reader["NoReffEddPrint"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public GetDataTransaksiTopupModel GetDataTransaksiTopupReprint(AllTransaksiModel data)
        {
        ulang:
            var res = new GetDataTransaksiTopupModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiTopup " +
                        "@IdTrx=" + data.IdTrx + "," +
                        "@Datetime='" + data.Datetime + "'," +
                        "@Nominal=" + data.Nominal.ToString() + "," +
                        "@CashierBy='" + data.CashierBy + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.AccountNumber = reader["AccountNumber"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.IdTransaction = "TOPUP" + reader["IdTopup"].ToString();
                                res.Kembalian = ConvertToRupiah(ConvertDecimal(reader["Kembalian"].ToString()));

                                res.MerchantName = reader["ComputerName"].ToString();
                                res.NamaKasir = reader["Chasierby"].ToString();
                                res.NominalTopup = ConvertToRupiah(ConvertDecimal(reader["NominalTopup"].ToString()));

                                res.SaldoSebelumnya = ConvertToRupiah(ConvertDecimal(reader["SaldoSebelum"].ToString()));
                                res.SaldoSetelahnya = ConvertToRupiah(ConvertDecimal(reader["SaldoSetelah"].ToString()));
                                res.UangDibayarkan = ConvertToRupiah(ConvertDecimal(reader["TerimaUang"].ToString()));

                                res.PaymentMethod = reader["PaymentMethod"].ToString();
                                res.IdLogEDCTransaksi = reader["IdLogEDCTransaksi"].ToString();
                                res.BankCode = reader["BankCode"].ToString();
                                res.NamaBank = reader["NamaBank"].ToString();
                                res.DiskonBank = reader["DiskonBank"].ToString();
                                res.NominalDiskon = reader["NominalDiskon"].ToString();
                                res.AdminCharges = reader["AdminCharges"].ToString();
                                res.TotalDebit = reader["TotalDebit"].ToString();
                                res.NoATM = reader["NoATM"].ToString();
                                res.NoReffEddPrint = reader["NoReffEddPrint"].ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public DataTransaksiCancelTopupModel getDataTransaksiTopupCancel(string IdTrx)
        {
        ulang:
            var res = new DataTransaksiCancelTopupModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_getDataTransaksiTopupCancel " +
                        "@IdTrx=" + IdTrx.Replace("TOPUP", "");
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Id = reader["Id"].ToString();
                                res.AccountNumber = reader["AccountNumber"].ToString();
                                res.PaymentMethod = reader["PaymentMethod"].ToString();
                                res.TipeTransaksi = reader["TipeTransaksi"].ToString();
                                res.TotalTransaksi = reader["TotalTransaksi"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }


        public GetDataTransaksiRefundReprintModel GetDataTransaksiRefundReprint(AllTransaksiModel data)
        {
        ulang:
            var res = new GetDataTransaksiRefundReprintModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiRefund " +
                        "@IdTrx=" + data.IdTrx + "," +
                        "@Datetime='" + data.Datetime + "'," +
                        "@Nominal=" + data.Nominal.ToString() + "," +
                        "@CashierBy='" + data.CashierBy + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.IdTransaction = "REFUND" + reader["IdRefund"].ToString();
                                res.AccounNumber = reader["AccountNumber"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.MerchantName = reader["ComputerName"].ToString();
                                res.NamaKasir = reader["ChasierBy"].ToString();
                                res.SaldoEmoney = ConvertToRupiah(ConvertDecimal(reader["SaldoEmoney"].ToString()));
                                res.SaldoJaminan = ConvertToRupiah(ConvertDecimal(reader["SaldoJaminan"].ToString()));
                                res.TotalRefund = ConvertToRupiah(ConvertDecimal(reader["TotalNominalRefund"].ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public GetDataTransaksiFoodCourtReprintModel GetDataTransaksiFoodCourtReprint(AllTransaksiModel data)
        {
        ulang:
            var res = new GetDataTransaksiFoodCourtReprintModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiFoodCourtReprint " +
                        "@IdTrx=" + data.IdTrx + "," +
                        "@Datetime='" + data.Datetime + "'," +
                        "@Nominal=" + data.Nominal.ToString() + "," +
                        "@CashierBy='" + data.CashierBy + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.IdTransaction = "FOODCOURT" + reader["IdTrx"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.MerchantName = reader["ComputerName"].ToString();
                                res.NamaKasir = reader["CashierBy"].ToString();
                                res.TotalBelanja = ConvertToRupiah(ConvertDecimal(reader["TotalBayar"].ToString()));
                                res.PaymentMethod = reader["JenisTransaksi"].ToString();
                                res.UseEmoney = ConvertToRupiah(ConvertDecimal(reader["PayEmoney"].ToString()));
                                res.AccountNumber = reader["AccountNumber"].ToString();
                                res.SaldoSebelum = ConvertToRupiah(ConvertDecimal(reader["SaldoEmoney"].ToString()));
                                res.SaldoSetelah = ConvertToRupiah(ConvertDecimal(reader["SaldoEmoneyAfter"].ToString()));
                                res.IdItemKeranjang = reader["IdItemsKeranjang"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public LogTransaksiCancelModel GetDataLogTransaksiCancel(string IdLog)
        {
        ulang:
            var res = new LogTransaksiCancelModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataLogTransaksiCancel " +
                        "@IdLog=" + IdLog + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Id TipeTransaksi   PaymentMethod TotalTransaksi  AccountNumber NamaKasirYangInputTrx   
                                //NamaKasirYangCancel Authorize   TransactionDate CancelDate  IdTransaksi PayTunai    
                                //PayEmoney DescriptionTransaksi    PayEDC

                                res.Id = reader["Id"].ToString();
                                res.TipeTransaksi = reader["TipeTransaksi"].ToString();
                                res.PaymentMethod = reader["PaymentMethod"].ToString();
                                res.TotalTransaksi = reader["TotalTransaksi"].ToString();
                                res.AccountNumber = reader["AccountNumber"].ToString();

                                res.NamaKasir = reader["NamaKasirYangInputTrx"].ToString();
                                res.NamaKasirYangCancel = reader["NamaKasirYangCancel"].ToString();

                                res.Authorize = reader["Authorize"].ToString();
                                res.TransactionDate = reader["TransactionDate"].ToString();
                                res.CancelDate = reader["CancelDate"].ToString();
                                res.IdTransaksi = reader["IdTransaksi"].ToString();

                                res.PayTunai = reader["PayTunai"].ToString();
                                res.PayEmoney = reader["PayEmoney"].ToString();
                                res.DescriptionTransaksi = reader["DescriptionTransaksi"].ToString();
                                res.PayEDC = reader["PayEDC"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }
        public GetDataTransaksiFoodCourtReprintModel GetDataTrxFoodCourt(string IdTrx)
        {
        ulang:
            var res = new GetDataTransaksiFoodCourtReprintModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTrxFoodCourt " +
                        "@IdTrx=" + IdTrx + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.IdTransaction = "FOODCOURT" + reader["IdTrx"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.MerchantName = reader["ComputerName"].ToString();
                                res.NamaKasir = reader["CashierBy"].ToString();
                                res.TotalBelanja = ConvertToRupiah(ConvertDecimal(reader["TotalBayar"].ToString()));
                                res.PaymentMethod = reader["JenisTransaksi"].ToString();
                                res.UseEmoney = ConvertToRupiah(ConvertDecimal(reader["PayEmoney"].ToString()));
                                res.AccountNumber = reader["AccountNumber"].ToString();
                                res.SaldoSebelum = ConvertToRupiah(ConvertDecimal(reader["SaldoEmoney"].ToString()));
                                res.SaldoSetelah = ConvertToRupiah(ConvertDecimal(reader["SaldoEmoneyAfter"].ToString()));
                                res.IdItemKeranjang = reader["IdItemsKeranjang"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public List<SelectListItem> GetListGender()
        {
        ulang:
            var res = new List<SelectListItem>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetGender ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new SelectListItem();
                                d.Text = reader["Text"].ToString();
                                d.Value = reader["Value"].ToString();
                                res.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;

        }

        public List<GetGridTicketModel> GetGridTicket(string IdTicketTrx)
        {
        ulang:
            var res = new List<GetGridTicketModel>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetGridTicket " +
                        "@IdTicket=" + IdTicketTrx + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new GetGridTicketModel();
                                d.NamaTicket = reader["NamaTicket"].ToString();
                                d.BesarDiskon = reader["Diskon"].ToString() + " %";
                                d.HargaSatuan = ConvertToRupiah(ConvertDecimal(reader["Harga"].ToString()));
                                d.NamaDiskon = reader["NamaDiskon"].ToString();
                                d.Qty = reader["Qty"].ToString();
                                d.Total = ConvertToRupiah(ConvertDecimal(reader["Total"].ToString()));
                                d.TotalAkhir = ConvertToRupiah(ConvertDecimal(reader["TotalAfterDiskon"].ToString()));
                                d.TotalDiskon = ConvertToRupiah(ConvertDecimal(reader["TotalDiskon"].ToString()));
                                res.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public List<GetGridKeranjangModel> GetGridKeranjangFoodCourtReprint(string ItemKeranjang)
        {
        ulang:
            var res = new List<GetGridKeranjangModel>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetGridKeranjangFoodCourtReprint " +
                        "@ItemKeranjang=" + ItemKeranjang + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new GetGridKeranjangModel();
                                d.HargaSatuan = ConvertToRupiah(ConvertDecimal(reader["Harga"].ToString()));
                                d.NamaItem = reader["NamaItem"].ToString();
                                d.Qty = reader["Qtx"].ToString();
                                d.Total = ConvertToRupiah(ConvertDecimal(reader["Total"].ToString()));
                                res.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public string GetDataTransaksiTopup(AllTransaksiModel data)
        {
        ulang:
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiTopup ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return "";
        }
        public string GetDataTransaksiRefund(AllTransaksiModel data)
        {
        ulang:
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiRefund ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return "";
        }

        public string GetDataTransaksiFoodcourt(AllTransaksiModel data)
        {
        ulang:
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataTransaksiRefund ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return "";
        }

        public decimal GetJaminan()
        {
        ulang:
            decimal data = 0;
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetJaminan";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new Ticket();
                                if (reader["Harga"].ToString() != "")
                                {
                                    data = Convert.ToDecimal(reader["Harga"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public string GetDatetime()
        {
        ulang:
            string data = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDateTime ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data = reader["tanggal"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public string GetDatetimeFake()
        {
        ulang:
            string data = "";
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["dbFake"].ConnectionString;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDateTime ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data = reader["tanggal"].ToString();
                                data = data.Left(10);
                                var d = data.Split('/');
                                data = d[2] + d[1] + d[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public string GenCodeID()
        {
        ulang:
            string data = "";
            try
            {

                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                    connection.Open();
                    string sql = "exec SP_GenCodeID ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data = reader["tanggal"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<string> GetFooterPrint()
        {
        ulang:
            var data = new List<string>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetFooterPrint ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string d = reader["val3"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public string GetFooterPrint(int line)
        {
        ulang:
            string d = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetHeaderPrint '" + line + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                d = reader["val3"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return d;
        }

        public List<DataPromo> GetPromo(string search)
        {
        ulang:
            var data = new List<DataPromo>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetPromo '" + search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //idPromo NamaPromo   CategoryPromo Diskon  Status BerlakuDari BerlakuSampai
                                var d = new DataPromo();
                                d.idPromo = reader["idPromo"].ToString();
                                d.NamaPromo = reader["NamaPromo"].ToString();
                                d.CatPromo = reader["CategoryPromo"].ToString();
                                d.Diskon = reader["Diskon"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<KeranjangPosTotal> GetLogFoodCourt(string search, string ComputerName)
        {
        ulang:
            var res = new List<KeranjangPosTotal>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GETFoodCourtSalesLog " +
                        "@Search='" + search + "'," +
                        "@ComputerName='" + ComputerName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new KeranjangPosTotal();
                                d.NamaTenant = reader["NamaTenant"].ToString();
                                d.NamaItem = reader["NamaItem"].ToString();
                                d.Qtx = ConvertDecimal(reader["Qty"].ToString());
                                d.HargaTotal = ConvertDecimal(reader["Total"].ToString());
                                d.Stok = ConvertDecimal(reader["Stok"].ToString());
                                d.HargaSatuan = ConvertDecimal(reader["Harga"].ToString());
                                res.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public string ConvertToRupiah(decimal Nominal)
        {
            string res = "Rp " + string.Format("{0:n0}", Nominal);
            return res;
        }

        public void RefreshDashboard()
        {
            string ComputerName = GetComputerName();
            string NamaUser = GetNamaUser(General.IDUser);
            string Versi = VersionLabel.Split(',')[1].Split(':')[1].Trim().Replace(".", "_");
            var data = GetDashboard_V2(ComputerName, NamaUser, "SP_GetDashboard");
            Form frm = Application.OpenForms["Main"];
            if (frm != null)
            {
                Panel tbx = frm.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                UserControl fc = tbx.Controls.Find("Dashboard", true).FirstOrDefault() as UserControl;
                if (fc != null)
                {
                    //New object
                    TextBox txtTicketSalesCounting = fc.Controls.Find("txtTicketSalesCounting", true).FirstOrDefault() as TextBox;
                    if (txtTicketSalesCounting != null)
                    {
                        txtTicketSalesCounting.Text = data.TicketSalesCounting;
                    }

                    TextBox txtKartuBaruCounting = fc.Controls.Find("txtKartuBaruCounting", true).FirstOrDefault() as TextBox;
                    if (txtKartuBaruCounting != null)
                    {
                        txtKartuBaruCounting.Text = data.KartuBaruCounting;
                    }

                    TextBox txtTotalKartuRefundCounting = fc.Controls.Find("txtTotalKartuRefundCounting", true).FirstOrDefault() as TextBox;
                    if (txtTotalKartuRefundCounting != null)
                    {
                        txtTotalKartuRefundCounting.Text = data.TotalKartuRefundCounting;
                    }

                    TextBox txtTotalKartuBaruNominal = fc.Controls.Find("txtTotalKartuBaruNominal", true).FirstOrDefault() as TextBox;
                    if (txtTotalKartuBaruNominal != null)
                    {
                        txtTotalKartuBaruNominal.Text = data.TotalKartuBaruNominal;
                    }

                    TextBox txtTicketPayCash = fc.Controls.Find("txtTicketPayCash", true).FirstOrDefault() as TextBox;
                    if (txtTicketPayCash != null)
                    {
                        txtTicketPayCash.Text = data.TicketPayCash;
                    }

                    TextBox txtTicketPaySaldo = fc.Controls.Find("txtTicketPaySaldo", true).FirstOrDefault() as TextBox;
                    if (txtTicketPaySaldo != null)
                    {
                        txtTicketPaySaldo.Text = data.TicketPaySaldo;
                    }

                    TextBox txtTicketPaySaldoNCash = fc.Controls.Find("txtTicketPaySaldoNCash", true).FirstOrDefault() as TextBox;
                    if (txtTicketPaySaldoNCash != null)
                    {
                        txtTicketPaySaldoNCash.Text = data.TicketPaySaldoNCash;
                    }

                    TextBox txtPayEDC = fc.Controls.Find("txtPayEDC", true).FirstOrDefault() as TextBox;
                    if (txtPayEDC != null)
                    {
                        txtPayEDC.Text = data.PayEDC;
                    }

                    TextBox txtPaySaldoEDC = fc.Controls.Find("txtPaySaldoEDC", true).FirstOrDefault() as TextBox;
                    if (txtPaySaldoEDC != null)
                    {
                        txtPaySaldoEDC.Text = data.PaySaldoEDC;
                    }

                    TextBox txtTicketTotalAmount = fc.Controls.Find("txtTicketTotalAmount", true).FirstOrDefault() as TextBox;
                    if (txtTicketTotalAmount != null)
                    {
                        txtTicketTotalAmount.Text = data.TicketTotalAmount;
                    }

                    TextBox txtTotalTopupCash = fc.Controls.Find("txtTotalTopupCash", true).FirstOrDefault() as TextBox;
                    if (txtTotalTopupCash != null)
                    {
                        txtTotalTopupCash.Text = data.TotalTopupCash;
                    }

                    TextBox txtTotalTopupEDC = fc.Controls.Find("txtTotalTopupEDC", true).FirstOrDefault() as TextBox;
                    if (txtTotalTopupEDC != null)
                    {
                        txtTotalTopupEDC.Text = data.TotalTopupEDC;
                    }

                    TextBox txtTotalTopup = fc.Controls.Find("txtTotalTopup", true).FirstOrDefault() as TextBox;
                    if (txtTotalTopup != null)
                    {
                        txtTotalTopup.Text = data.TotalTopup;
                    }

                    TextBox txtFNBPayCash = fc.Controls.Find("txtFNBPayCash", true).FirstOrDefault() as TextBox;
                    if (txtFNBPayCash != null)
                    {
                        txtFNBPayCash.Text = data.FNBPayCash;
                    }

                    TextBox txtFNBPaySaldo = fc.Controls.Find("txtFNBPaySaldo", true).FirstOrDefault() as TextBox;
                    if (txtFNBPaySaldo != null)
                    {
                        txtFNBPaySaldo.Text = data.FNBPaySaldo;
                    }

                    TextBox txtFNBAll = fc.Controls.Find("txtFNBAll", true).FirstOrDefault() as TextBox;
                    if (txtFNBAll != null)
                    {
                        txtFNBAll.Text = data.FNBAll;
                    }

                    TextBox txtRefundJaminan = fc.Controls.Find("txtRefundJaminan", true).FirstOrDefault() as TextBox;
                    if (txtRefundJaminan != null)
                    {
                        txtRefundJaminan.Text = data.RefundJaminan;
                    }

                    TextBox txtRefundSaldo = fc.Controls.Find("txtRefundSaldo", true).FirstOrDefault() as TextBox;
                    if (txtRefundSaldo != null)
                    {
                        txtRefundSaldo.Text = data.RefundSaldo;
                    }

                    TextBox txtTotalRefund = fc.Controls.Find("txtTotalRefund", true).FirstOrDefault() as TextBox;
                    if (txtTotalRefund != null)
                    {
                        txtTotalRefund.Text = data.TotalRefund;
                    }

                    TextBox txtDanaModal = fc.Controls.Find("txtDanaModal", true).FirstOrDefault() as TextBox;
                    if (txtDanaModal != null)
                    {
                        txtDanaModal.Text = data.DanaModal;
                    }

                    TextBox txtTotalCashin = fc.Controls.Find("txtTotalCashin", true).FirstOrDefault() as TextBox;
                    if (txtTotalCashin != null)
                    {
                        txtTotalCashin.Text = data.TotalCashin;
                    }

                    TextBox txtTotalCashOut = fc.Controls.Find("txtTotalCashOut", true).FirstOrDefault() as TextBox;
                    if (txtTotalCashOut != null)
                    {
                        txtTotalCashOut.Text = data.TotalCashOut;
                    }

                    TextBox txtTotalCashBox = fc.Controls.Find("txtTotalCashBox", true).FirstOrDefault() as TextBox;
                    if (txtTotalCashBox != null)
                    {
                        txtTotalCashBox.Text = data.TotalCashBox;
                    }

                    TextBox txtTotalEDC = fc.Controls.Find("txtTotalEDC", true).FirstOrDefault() as TextBox;
                    if (txtTotalEDC != null)
                    {
                        txtTotalEDC.Text = data.TotalEDC;
                    }

                    TextBox txtTotalEmoney = fc.Controls.Find("txtTotalEmoney", true).FirstOrDefault() as TextBox;
                    if (txtTotalEmoney != null)
                    {
                        txtTotalEmoney.Text = data.TotalEmoney;
                    }


                    TextBox txtTotalTransaksiKasir = fc.Controls.Find("txtTotalTransaksiKasir", true).FirstOrDefault() as TextBox;
                    if (txtTotalTransaksiKasir != null)
                    {
                        txtTotalTransaksiKasir.Text = data.TotalTransaksiKasir;
                    }


                    Button btnClosing = fc.Controls.Find("btnClosing", true).FirstOrDefault() as Button;
                    if (btnClosing != null)
                    {
                        var Open = CheckOpenCashier();
                        if (Open.Success == true)
                        {
                            btnClosing.Enabled = true;
                        }
                        else
                        {
                            btnClosing.Enabled = false;
                        }
                    }

                    DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                    if (dt_grid != null)
                    {
                        loadGrid(dt_grid);
                    }

                    DataGridView dt_grid2 = fc.Controls.Find("dt_grid2", true).FirstOrDefault() as DataGridView;
                    if (dt_grid2 != null)
                    {
                        loadGrid2(dt_grid2);
                    }

                    DataGridView dtGrid_tenant = fc.Controls.Find("dtGrid_tenant", true).FirstOrDefault() as DataGridView;
                    if (dtGrid_tenant != null)
                    {
                        loadGrid3(dtGrid_tenant);
                    }


                }
            }
        }

        public void loadGrid(DataGridView dt_grid)
        {
            dt_grid.Rows.Clear();
            atur_grid(dt_grid);
            var data = GetDashTicketCount(GetComputerName(), GetNamaUser(General.IDUser));
            int a = 0;
            foreach (var r in data)
            {
                a++;
                string[] row = new string[] { r.JenisTicket, r.Count };
                dt_grid.Rows.Add(row);
            }
        }

        public void loadGrid3(DataGridView dt_grid)
        {
            dt_grid.Rows.Clear();
            atur_grid3(dt_grid);
            string Versi = VersionLabel.Split(',')[1].Split(':')[1].Trim().Replace(".", "_");
            var data = GetDashTenantPerfomance(GetComputerName(), GetNamaUser(General.IDUser), "SP_GetDashTenantPerfomance");
            int a = 0;
            foreach (var r in data)
            {
                a++;
                string[] row = new string[] { r.NamaTenant, r.TotalPenjualan };
                dt_grid.Rows.Add(row);
            }
        }

        public void loadGrid2(DataGridView dt_grid)
        {
            dt_grid.Rows.Clear();
            atur_grid2(dt_grid);
            var data = GetDataAllTransaksi(GetComputerName(), GetNamaUser(General.IDUser));
            int a = 0;
            foreach (var r in data)
            {
                a++;
                string[] row = new string[] { "x", r.IdTrx, r.Datetime, r.JenisTransaksi, ConvertToRupiah(r.Nominal), r.CashierBy };
                dt_grid.Rows.Add(row);
            }
        }

        public void loadGridSearch(DataGridView dt_grid, string Search)
        {
            dt_grid.Rows.Clear();
            atur_grid2(dt_grid);
            var data = GetDataAllTransaksiSearch(GetComputerName(), GetNamaUser(General.IDUser), Search);
            int a = 0;
            foreach (var r in data)
            {
                a++;
                string[] row = new string[] { "x", r.IdTrx, r.Datetime, r.JenisTransaksi, ConvertToRupiah(r.Nominal), r.CashierBy };
                dt_grid.Rows.Add(row);
            }
        }

        public void atur_grid2(DataGridView dt_grid)
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 6;
            dt_grid.Columns[0].Name = "X";
            dt_grid.Columns[1].Name = "Id Trx";
            dt_grid.Columns[2].Name = "Datetime";
            dt_grid.Columns[3].Name = "Nama Transaksi";
            dt_grid.Columns[4].Name = "Total Belanja";
            dt_grid.Columns[5].Name = "Cashier by";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];
            DataGridViewColumn column3 = dt_grid.Columns[2];
            DataGridViewColumn column4 = dt_grid.Columns[3];
            DataGridViewColumn column5 = dt_grid.Columns[4];
            DataGridViewColumn column6 = dt_grid.Columns[5];

            // Initialize basic DataGridView properties.
            dt_grid.Dock = DockStyle.None;
            dt_grid.BorderStyle = BorderStyle.None;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grid.ScrollBars = ScrollBars.Both;
            dt_grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dt_grid.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dt_grid.DefaultCellStyle.ForeColor = Color.Black;
            dt_grid.Columns[0].Width = 40;
        }

        public void atur_grid(DataGridView dt_grid)
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 2;
            dt_grid.Columns[0].Name = "Nama Ticket";
            dt_grid.Columns[1].Name = "Total";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];

            // Initialize basic DataGridView properties.
            //dt_grid.Dock = DockStyle.None;
            //dt_grid.BorderStyle = BorderStyle.None;
            //dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dt_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dt_grid.DefaultCellStyle.Font = new Font("Tahoma", 14);
            //dt_grid.DefaultCellStyle.ForeColor = Color.Black;
            //dt_grid.Columns[0].Width = 200;
            dt_grid.Dock = DockStyle.None;
            dt_grid.BackgroundColor = Color.DimGray;
            dt_grid.DefaultCellStyle.BackColor = SystemColors.ControlDark;
            dt_grid.BorderStyle = BorderStyle.None;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grid.ScrollBars = ScrollBars.Both;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dt_grid.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            dt_grid.DefaultCellStyle.ForeColor = Color.Black;

            //dt_grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void atur_grid3(DataGridView dt_grid)
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 2;
            dt_grid.Columns[0].Name = "Nama Tenant";
            dt_grid.Columns[1].Name = "Total Penjualan";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];

            // Initialize basic DataGridView properties.
            //dt_grid.Dock = DockStyle.None;
            //dt_grid.BorderStyle = BorderStyle.None;
            //dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dt_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dt_grid.DefaultCellStyle.Font = new Font("Tahoma", 14);
            //dt_grid.DefaultCellStyle.ForeColor = Color.Black;
            //dt_grid.Columns[0].Width = 200;
            dt_grid.Dock = DockStyle.None;
            dt_grid.BackgroundColor = Color.DimGray;
            dt_grid.DefaultCellStyle.BackColor = SystemColors.ControlDark;
            dt_grid.BorderStyle = BorderStyle.None;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grid.ScrollBars = ScrollBars.Both;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dt_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dt_grid.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            dt_grid.DefaultCellStyle.ForeColor = Color.Black;

            //dt_grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public string GetTotalTopup(string ComputerName)
        {
        ulang:
            string Res = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDashTotalToup '" + ComputerName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Res = reader["TotalTopup"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }



        public string GetTotalRefund(string ComputerName)
        {
        ulang:
            string Res = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDashTotalRefund '" + ComputerName + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Res = reader["TotalRefund"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }

        public List<DataDashTicket> GetDashTicketCount(string ComputerName, string NamaUser)
        {
        ulang:
            var data = new List<DataDashTicket>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDashTicketCount '" + ComputerName + "','" + NamaUser + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ls = new DataDashTicket();
                                ls.JenisTicket = reader["NamaTicket"].ToString();
                                ls.Count = reader["Qty"].ToString();
                                data.Add(ls);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataDashTenant> GetDashTenantPerfomance(string ComputerName, string NamaUser, string SpVersion)
        {
        ulang:
            var data = new List<DataDashTenant>();
            string NamaSP = GetNamaSPSesuaiVersiApp(VersiId, SpVersion);
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec " + NamaSP + " '" + ComputerName + "','" + NamaUser + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ls = new DataDashTenant();
                                ls.NamaTenant = reader["NamaTenant"].ToString();
                                ls.TotalPenjualan = ConvertToRupiah(ConvertDecimal(reader["Qty"].ToString()));
                                data.Add(ls);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public Dashboard2Model GetDashboard_V2(string computername, string Username, string SP_name)
        {
        ulang:
            var res = new Dashboard2Model();
            string NamaSP = GetNamaSPSesuaiVersiApp(VersiId, SP_name);
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec " + NamaSP + " @ComputerName='" + computername + "',@Username='" + Username + "' ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.TicketSalesCounting = reader["TicketSalesCounting"].ToString();
                                res.KartuBaruCounting = reader["KartuBaruCounting"].ToString();
                                res.TotalKartuRefundCounting = reader["TotalKartuRefundCounting"].ToString();
                                res.TotalKartuBaruNominal = ConvertToRupiah(ConvertDecimal(reader["TotalKartuBaruNominal"].ToString()));
                                res.TicketPayCash = ConvertToRupiah(ConvertDecimal(reader["TicketPayCash"].ToString()));
                                res.TicketPaySaldo = ConvertToRupiah(ConvertDecimal(reader["TicketPaySaldo"].ToString()));
                                res.TicketPaySaldoNCash = ConvertToRupiah(ConvertDecimal(reader["TicketPaySaldoNCash"].ToString()));
                                res.PayEDC = ConvertToRupiah(ConvertDecimal(reader["PayEDC"].ToString()));
                                res.PaySaldoEDC = ConvertToRupiah(ConvertDecimal(reader["PaySaldoEDC"].ToString()));
                                res.TicketTotalAmount = ConvertToRupiah(ConvertDecimal(reader["TicketTotalAmount"].ToString()));

                                res.TotalTopupCash = ConvertToRupiah(ConvertDecimal(reader["TotalTopupCash"].ToString()));
                                res.TotalTopupEDC = ConvertToRupiah(ConvertDecimal(reader["TotalTopupEDC"].ToString()));
                                res.TotalTopup = ConvertToRupiah(ConvertDecimal(reader["TotalTopup"].ToString()));

                                res.FNBPayCash = ConvertToRupiah(ConvertDecimal(reader["FNBPayCash"].ToString()));
                                res.FNBPaySaldo = ConvertToRupiah(ConvertDecimal(reader["FNBPaySaldo"].ToString()));
                                res.FNBAll = ConvertToRupiah(ConvertDecimal(reader["FNBAll"].ToString()));

                                res.RefundSaldo = ConvertToRupiah(ConvertDecimal(reader["RefundSaldo"].ToString()));
                                res.RefundJaminan = ConvertToRupiah(ConvertDecimal(reader["RefundJaminan"].ToString()));
                                res.TotalRefund = ConvertToRupiah(ConvertDecimal(reader["TotalRefund"].ToString()));

                                res.DanaModal = ConvertToRupiah(ConvertDecimal(reader["DanaModal"].ToString()));
                                res.TotalCashin = ConvertToRupiah(ConvertDecimal(reader["TotalCashin"].ToString()));
                                res.TotalCashOut = ConvertToRupiah(ConvertDecimal(reader["TotalCashOut"].ToString()));
                                res.TotalCashBox = ConvertToRupiah(ConvertDecimal(reader["TotalCashBox"].ToString()));
                                res.TotalEDC = ConvertToRupiah(ConvertDecimal(reader["TotalEDC"].ToString()));
                                res.TotalEmoney = ConvertToRupiah(ConvertDecimal(reader["TotalEmoney"].ToString()));
                                res.TotalTransaksiKasir = ConvertToRupiah(ConvertDecimal(reader["TotalTransaksiKasir"].ToString()));

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public LogClosingV2 GetLogClosing_V2(string SP_name, string LogId)
        {
        ulang:
            var res = new LogClosingV2();
            string NamaSP = GetNamaSPSesuaiVersiApp(VersiId, SP_name);
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec " + NamaSP + " @LogId=" + LogId;
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.LogId = reader["LogId"].ToString();
                                res.Datetime = reader["Datetime"].ToString();
                                res.KasirInputNominal = reader["KasirInputNominal"].ToString();
                                res.ComputerName = reader["ComputerName"].ToString();
                                res.Username = reader["Username"].ToString();
                                res.MoneyCashboxSelisih = reader["MoneyCashboxSelisih"].ToString();
                                res.MatchingStatus = reader["MatchingStatus"].ToString();
                                res.TicketSalesCounting = reader["TicketSalesCounting"].ToString();
                                res.KartuBaruCounting = reader["KartuBaruCounting"].ToString();
                                res.TotalKartuRefundCounting = reader["TotalKartuRefundCounting"].ToString();
                                res.TotalKartuBaruNominal = reader["TotalKartuBaruNominal"].ToString();
                                res.TicketPayCash = reader["TicketPayCash"].ToString();
                                res.TicketPaySaldo = reader["TicketPaySaldo"].ToString();
                                res.TicketPaySaldoNCash = reader["TicketPaySaldoNCash"].ToString();
                                res.PayEDC = reader["PayEDC"].ToString();
                                res.PaySaldoEDC = reader["PaySaldoEDC"].ToString();
                                res.TicketTotalAmount = reader["TicketTotalAmount"].ToString();
                                res.TotalTopupCash = reader["TotalTopupCash"].ToString();
                                res.TotalTopupEDC = reader["TotalTopupEDC"].ToString();
                                res.TotalTopup = reader["TotalTopup"].ToString();
                                res.FNBPayCash = reader["FNBPayCash"].ToString();
                                res.FNBPaySaldo = reader["FNBPaySaldo"].ToString();
                                res.FNBAll = reader["FNBAll"].ToString();
                                res.RefundJaminan = reader["RefundJaminan"].ToString();
                                res.RefundSaldo = reader["RefundSaldo"].ToString();
                                res.TotalRefund = reader["TotalRefund"].ToString();
                                res.DanaModal = reader["DanaModal"].ToString();
                                res.TotalCashin = reader["TotalCashin"].ToString();
                                res.TotalCashOut = reader["TotalCashOut"].ToString();
                                res.TotalEDC = reader["TotalEDC"].ToString();
                                res.TotalEmoney = reader["TotalEmoney"].ToString();
                                res.TotalCashBox = reader["TotalCashBox"].ToString();
                                res.TotalTransaksiKasir = reader["TotalTransaksiKasir"].ToString();
                                res.StatusAcceptanceBySPV = reader["StatusAcceptanceBySPV"].ToString();
                                res.KeteranganAcceptance = reader["KeteranganAcceptance"].ToString();
                                res.UangDiterimaFinnance = reader["UangDiterimaFinnance"].ToString();
                                res.TotalAmountStrukEDC = reader["TotalAmountStrukEDC"].ToString();
                                res.Status = reader["Status"].ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public DashboardModel GetDashboard(string computername, string Username)
        {
        ulang:
            var res = new DashboardModel();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_Dashboard @ComputerName='" + computername + "',@Username='" + Username + "' ";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.TotalTopup = reader["TotalTopup"].ToString();
                                res.TotalRefund = reader["TotalRefund"].ToString();
                                res.TotalRegis = reader["TotalRegis"].ToString();
                                res.TotalFoodcourtCash = reader["TotalFoodcourtCash"].ToString();
                                res.TotalFoodcourtEmoney = reader["TotalFoodcourtEmoney"].ToString();
                                res.TotalTicketPayEmoney = reader["TotalTicketPayEmoney"].ToString();
                                res.TotalTransaksi = reader["TotalTransaksi"].ToString();

                                res.TotalDanaModal = reader["TotalDanaModal"].ToString();
                                res.TotalCashIn = reader["TotalCashIn"].ToString();
                                res.TotalCashOut = reader["TotalCashOut"].ToString();
                                res.TotalCashBox = reader["TotalCashBox"].ToString();
                                res.TotalAllTicket = reader["TotalAllTicket"].ToString();

                                res.TotalNominalEdcRegis = reader["TotalNominalEdcRegis"].ToString();
                                res.TotalNominalEdcTopup = reader["TotalNominalEdcTopup"].ToString();
                                res.TotalTrxEdc = reader["TotalTrxEDC"].ToString();
                                res.TotalNominalDebit = reader["TotalNominalDebit"].ToString();


                                res.TotalTrxEmoney = reader["TotalTrxEmoney"].ToString();
                                res.TotalNominalDebitEmoney = reader["TotalNominalDebitEmoney"].ToString();

                                res.TotalRegistCount = reader["TotalRegistCount"].ToString();
                                res.TotalRefundCount = reader["TotalRefundCount"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return res;
        }

        public List<DataMenu> GetMenuKasir(string Platform)
        {
        ulang:
            var data = new List<DataMenu>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();

                    string sql = "exec SP_GetMenu @Platform='" + Platform + "', @IdUser =" + General.IDUser + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new DataMenu();
                                d.idMenu = reader["idMenu"].ToString();
                                d.Action = reader["Action"].ToString();
                                d.NamaMenu = reader["NamaMenu"].ToString();
                                d.Img = ImgPath + reader["Img"].ToString();
                                d.Platform = reader["Platform"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataTenant> GetDataTenant(string search)
        {
        ulang:
            var data = new List<DataTenant>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetMenuV2 @search='" + search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //idPromo NamaPromo   CategoryPromo Diskon  Status BerlakuDari BerlakuSampai
                                var d = new DataTenant();
                                d.Id = reader["idTenant"].ToString();
                                d.NamaTenant = reader["NamaTenant"].ToString();
                                d.Img = ImgPath + reader["Img"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataBarang> GetBarang(string Tenant)
        {
        ulang:
            var data = new List<DataBarang>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetBarang " + Tenant;
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //idPromo NamaPromo   CategoryPromo Diskon  Status BerlakuDari BerlakuSampai
                                var d = new DataBarang();
                                d.IdMenu = reader["idMenu"].ToString();
                                d.NamaBarang = reader["NamaMenu"].ToString();
                                d.Harga = reader["HargaJual"].ToString();
                                d.LinkPic = ImgPath + reader["ImgLink"].ToString();
                                d.Stok = reader["Stok"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataBarang> GetBarangSearchMenu(string Tenant, string Search)
        {
        ulang:
            var data = new List<DataBarang>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec GetBarangSearchMenu @IdTenant='" + Tenant + "',@Search='" + Search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //idPromo NamaPromo   CategoryPromo Diskon  Status BerlakuDari BerlakuSampai
                                var d = new DataBarang();
                                d.IdMenu = reader["idMenu"].ToString();
                                d.NamaBarang = reader["NamaMenu"].ToString();
                                d.Harga = (reader["HargaJual"].ToString());
                                d.LinkPic = ImgPath + reader["ImgLink"].ToString();
                                d.Stok = reader["Stok"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<LogHistoryAccModel> GetHistoryAcc(string AccountNumber)
        {
        ulang:
            var data = new List<LogHistoryAccModel>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetHistoryAcc @AccountNumber='" + AccountNumber + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new LogHistoryAccModel();
                                d.idlog = reader["idlog"].ToString();
                                d.Datetime = reader["Datetime"].ToString();
                                d.JenisTransaksi = reader["JenisTransaksi"].ToString();
                                d.Uraian = reader["Uraian"].ToString();
                                d.Nominal = ConvertToRupiah(ConvertDecimal(reader["Nominal"].ToString()));
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataCashback> GetCashbacks(string search)
        {
        ulang:
            var data = new List<DataCashback>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetCashback '" + search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //idPromo NamaPromo   CategoryPromo Diskon  Status BerlakuDari BerlakuSampai
                                var d = new DataCashback();
                                d.id = reader["IdCashback"].ToString();
                                d.NamaCashback = reader["NamaCashback"].ToString();
                                if (reader["NominalCashback"].ToString() != "")
                                {
                                    d.Nominal = Convert.ToDecimal(reader["NominalCashback"].ToString());
                                }
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataBank> GetDataBank(string search)
        {
        ulang:
            var data = new List<DataBank>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataBank '" + search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new DataBank();
                                d.idLog = reader["idLog"].ToString();
                                d.AdminCharges = reader["AdminCharges"].ToString();
                                d.DiskonBank = reader["DiskonBank"].ToString();
                                d.KodeBank = reader["KodeBank"].ToString();
                                d.NamaBank = reader["NamaBank"].ToString();
                                d.status = reader["status"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<DataBank> GetLogAccount(string search)
        {
        ulang:
            var data = new List<DataBank>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataBank '" + search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new DataBank();
                                d.idLog = reader["idLog"].ToString();
                                d.AdminCharges = reader["AdminCharges"].ToString();
                                d.DiskonBank = reader["DiskonBank"].ToString();
                                d.KodeBank = reader["KodeBank"].ToString();
                                d.NamaBank = reader["NamaBank"].ToString();
                                d.status = reader["status"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<StockOpnameModel> GetDataStok(string search)
        {
        ulang:
            var data = new List<StockOpnameModel>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataStok '" + search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new StockOpnameModel();
                                d.idItem = reader["idMenu"].ToString();
                                d.NamaTenant = reader["NamaTenant"].ToString();
                                d.NamaItem = reader["NamaMenu"].ToString();
                                d.BykStok = ConvertDecimal(reader["Stok"].ToString());
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<AllTransaksiModel> GetDataAllTransaksi(string ComputerName, string NamaUser)
        {
        ulang:
            var data = new List<AllTransaksiModel>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataAllTransaksi @ComputerName='" + ComputerName + "',@NamaUser='" + NamaUser + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new AllTransaksiModel();
                                d.IdTrx = reader["IdTrx"].ToString();
                                d.Datetime = reader["Datetime"].ToString();
                                d.JenisTransaksi = reader["JenisTransaksi"].ToString();
                                d.Nominal = ConvertDecimal(reader["Nominal"].ToString());
                                d.CashierBy = reader["CashierBy"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<AllTransaksiModel> GetDataAllTransaksiSearch(string ComputerName, string NamaUser, string Search)
        {
        ulang:
            var data = new List<AllTransaksiModel>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDataAllTransaksiSearch @ComputerName='" + ComputerName + "',@NamaUser='" + NamaUser + "',@Search='" + Search + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new AllTransaksiModel();
                                d.IdTrx = reader["IdTrx"].ToString();
                                d.Datetime = reader["Datetime"].ToString();
                                d.JenisTransaksi = reader["JenisTransaksi"].ToString();
                                d.Nominal = ConvertDecimal(reader["Nominal"].ToString());
                                d.CashierBy = reader["CashierBy"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        public List<TambahModalCashbox> GetDanaModalLog(string ComputerName, string NamaUser)
        {
        ulang:
            var data = new List<TambahModalCashbox>();
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetDanaModalLog @ComputerName='" + ComputerName + "',@NamaUser='" + NamaUser + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new TambahModalCashbox();
                                d.Id = reader["idLog"].ToString();
                                d.ComputerName = reader["NamaComputer"].ToString();
                                d.NamaUser = reader["NamaUser"].ToString();
                                d.Nominal = ConvertDecimal(reader["NominalTambahModal"].ToString());
                                d.Datetime = reader["Datetime"].ToString();
                                data.Add(d);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return data;
        }

        private async Task<List<Ticket>> GetTicket2(string URI)
        {
            var data = new List<Ticket>();
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var productJsonString = await response.Content.ReadAsStringAsync();
                            data = JsonConvert.DeserializeObject<List<Ticket>>(productJsonString);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public DialogResult messageboxError(string content)
        {
            string message = "Do you want to abort this operation? \n" + content;
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            return result;
        }

        public DialogResult ShowMessagebox(string message, string title, MessageBoxButtons btn)
        {
            DialogResult result = MessageBox.Show(message, title, btn, MessageBoxIcon.Warning);
            return result;
        }

        public string ReadUpdateAccountData(DataAccount data)
        {
        ulang:
            string result = "";
            try
            {
                conn.ConnectionString = server;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_UpdateAccountDataRead " +
                        "@AccountNumber='" + data.AccountNumber + "'," +
                        "@Balanced=" + data.BalancedSesudah + "," +
                        "@Ticket=" + data.Ticket + "," +
                        "@JaminanGelang =" + data.JaminanGelangYgTerbaca;
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader["Result"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                var msg = messageboxError(ex.Message);
                if (msg == DialogResult.Retry)
                {
                    goto ulang;
                }
            }
            return result;
        }
    }

    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "RAW Document";
            // Win7
            di.pDataType = "RAW";

            // Win8+
            // di.pDataType = "XPS_PASS";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            fs.Close();
            fs.Dispose();
            fs = null;
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }

}
