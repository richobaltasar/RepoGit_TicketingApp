using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class Main : Form
    {

        GeneralFunction g = new GeneralFunction();
        Function.GlobalFunc f = new Function.GlobalFunc();
        Sales s = new Sales();
        static Main _obj;
        ACR_NFC NFC = new ACR_NFC();

        public static Main Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new Main();
                }
                return _obj;
            }
        }

        public Panel PnlContainer
        {
            get { return PagePanel; }
            set { PagePanel = value; }
        }

        public Label LabelCardId
        {
            get { return LblCardId; }
            set { LblCardId = value; }
        }

        public Label LabelCodeId
        {
            get { return lblCodeId; }
            set { lblCodeId = value; }
        }

        public Label LabelSaldo
        {
            get { return LblSaldo; }
            set { LblSaldo = value; }
        }
        public Label labelTicket
        {
            get { return lblTicket; }
            set { lblTicket = value; }
        }

        public Main()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PagePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        public void GetMenu(string platform)
        {
            var dataMenu = f.GetMenuKasir(platform);
            ImageList il = new ImageList();
            int count = 0;
            ListMenu.Clear();
            foreach (var img in dataMenu)
            {
                try
                {
                    System.Net.WebRequest request = System.Net.WebRequest.Create(img.Img);
                    System.Net.WebResponse resp = request.GetResponse();
                    System.IO.Stream respStream = resp.GetResponseStream();
                    Bitmap bmp = new Bitmap(respStream);
                    respStream.Dispose();
                    il.ImageSize = new Size(50, 50);
                    il.Images.Add(bmp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.Message);
                    //throw ex;
                }
            }
            ListMenu.LargeImageList = il;
            foreach (var data in dataMenu)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = data.NamaMenu;
                lst.Name = data.idMenu + "~" + data.NamaMenu + "~" + data.Action;
                lst.ImageIndex = count++;
                ListMenu.Items.Add(lst);
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            GetMenu("SettingDesktop");
            btnMenu.BringToFront();
            panelSetting.Hide();
            PagePanel.Dock = DockStyle.Fill;
            _obj = this;
            var data = f.CheckOpenCashier();

            if (data.Success == true)
            {

                UCScanKartu Uc = new UCScanKartu();
                Uc.Dock = DockStyle.Fill;
                Uc.Width = this.PagePanel.Width;
                Uc.Height = this.PagePanel.Height;
                Uc.BringToFront();
                PagePanel.Controls.Add(Uc);
                LblCardId.Text = "-";
                lblCodeId.Text = "-";
                LblSaldo.Text = "-";
            }
            else
            {
                UCOpenCasir Uc = new UCOpenCasir();
                Uc.Dock = DockStyle.Fill;
                Uc.Width = this.PagePanel.Width;
                Uc.Height = this.PagePanel.Height;
                Uc.BringToFront();
                PagePanel.Controls.Add(Uc);
                LblCardId.Text = "-";
                lblCodeId.Text = "-";
                LblSaldo.Text = "-";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panelSetting.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panelSetting.Show();
            btnClosePanel.BringToFront();
        }

        private void PagePanel_Click(object sender, EventArgs e)
        {
            panelSetting.Hide();
        }

        private void panelProfile_MouseLeave(object sender, EventArgs e)
        {
            panelSetting.Hide();
        }

        private void panelSetting_MouseLeave(object sender, EventArgs e)
        {
            panelSetting.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginV2 f = new LoginV2();
            f.Show();
            f.BringToFront();
            f.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnClosePanel_Click(object sender, EventArgs e)
        {
            //btnClosePanel.BringToFront();
            btnMenu.BringToFront();
            panelSetting.Hide();

        }

        private void ListMenu_Click(object sender, EventArgs e)
        {
            var data = ListMenu.SelectedItems[0];
            if (data.Name != null)
            {
                var action = data.Name.Split('~');
                if (action[2] != null)
                {
                    if (action[1] == "Home")
                    {
                        var Card = NFC.ReadCardDataKey();
                        if (Card.IdCard != null)
                        {
                            if (lblCodeId.Text != "-")
                            {
                                action[2] = "MenuKasir";
                            }
                            else
                            {
                                action[2] = "UCScanKartu";
                            }
                        }
                        else
                        {
                            action[2] = "UCScanKartu";
                        }
                    }

                    if (!Main.Instance.PnlContainer.Controls.ContainsKey(action[2]))
                    {
                        if (action[2] == "UCDashboard")
                        {
                            UCDashboard un = new UCDashboard();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCTambahModal")
                        {
                            UCTambahModal un = new UCTambahModal();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCClosingMerchant")
                        {

                            UCClosingMerchant un = new UCClosingMerchant();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCHistoryTransactions")
                        {
                            UCHistoryTransactions un = new UCHistoryTransactions();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCStockOpname")
                        {
                            UCStockOpname un = new UCStockOpname();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCHistoryAccount")
                        {
                            UCHistoryAccount un = new UCHistoryAccount();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCProfile")
                        {
                            UCProfile un = new UCProfile();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "MenuKasir")
                        {
                            MenuKasir un = new MenuKasir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCScanKartu")
                        {
                            UCScanKartu un = new UCScanKartu();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCSimMasukParkir")
                        {
                            UCSimMasukParkir un = new UCSimMasukParkir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "UCSimKeluarParkir")
                        {
                            UCSimKeluarParkir un = new UCSimKeluarParkir();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                        else if (action[2] == "TransaksiBerhasil")
                        {
                            TransaksiBerhasil un = new TransaksiBerhasil();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }
                    }
                    try
                    {
                        panelSetting.Hide();
                        btnMenu.BringToFront();
                        Main.Instance.PnlContainer.Controls[action[2]].BringToFront();

                        if (action[2] == "UCClosingMerchant")
                        {
                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc = tbx.Controls.Find("UCClosingMerchant", true).FirstOrDefault() as UserControl;

                            if (fc != null)
                            {
                                TextBox txtQtyTrx = fc.Controls.Find("txtQtyTrx", true).FirstOrDefault() as TextBox;
                                TextBox txtTunai = fc.Controls.Find("txtTunai", true).FirstOrDefault() as TextBox;
                                TextBox txtEDC = fc.Controls.Find("txtEDC", true).FirstOrDefault() as TextBox;
                                TextBox txtEmoney = fc.Controls.Find("txtEmoney", true).FirstOrDefault() as TextBox;
                                TextBox txtDanaModal = fc.Controls.Find("txtDanaModal", true).FirstOrDefault() as TextBox;
                                TextBox txtPenjualan = fc.Controls.Find("txtPenjualan", true).FirstOrDefault() as TextBox;
                                TextBox txtCashinCashDrawer = fc.Controls.Find("txtCashinCashDrawer", true).FirstOrDefault() as TextBox;

                                var dataClosing = s.ReloadDashboard(f.GetComputerName(), f.GetNamaUser(General.IDUser));

                                txtQtyTrx.Text = (dataClosing.QtyCard + dataClosing.QtyAsuransi + dataClosing.QtyTicket +
                                    dataClosing.QtyTopup + dataClosing.QtyMotorParkir + dataClosing.QtyMobilParkir + dataClosing.QtyFoodCourt).ToString();

                                txtTunai.Text = g.ConvertToRupiah(dataClosing.Tunai);
                                txtEDC.Text = g.ConvertToRupiah(dataClosing.EDC);
                                txtEmoney.Text = g.ConvertToRupiah(dataClosing.Emoney);

                                txtDanaModal.Text = g.ConvertToRupiah(dataClosing.DanaModal);
                                txtPenjualan.Text = g.ConvertToRupiah(dataClosing.Tunai + dataClosing.EDC + dataClosing.Emoney);

                                txtCashinCashDrawer.Text = g.ConvertToRupiah(dataClosing.Tunai + dataClosing.DanaModal);
                            }
                        }
                        else if (action[2] == "UCProfile")
                        {
                            var profile = f.GetProfile(General.IDUser);
                            Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc = tbx.Controls.Find("UCProfile", true).FirstOrDefault() as UserControl;

                            if (fc != null)
                            {
                                PictureBox Img = fc.Controls.Find("Img", true).FirstOrDefault() as PictureBox;
                                TextBox txtNamaLengkap = fc.Controls.Find("txtNamaLengkap", true).FirstOrDefault() as TextBox;
                                TextBox txtUsername = fc.Controls.Find("txtUsername", true).FirstOrDefault() as TextBox;
                                TextBox txtAlamat = fc.Controls.Find("txtAlamat", true).FirstOrDefault() as TextBox;
                                TextBox txtContact = fc.Controls.Find("txtContact", true).FirstOrDefault() as TextBox;
                                TextBox txtEmail = fc.Controls.Find("txtEmail", true).FirstOrDefault() as TextBox;
                                TextBox txtNIK = fc.Controls.Find("txtNIK", true).FirstOrDefault() as TextBox;
                                TextBox txtPassword = fc.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
                                ComboBox cmbGender = fc.Controls.Find("cmbGender", true).FirstOrDefault() as ComboBox;

                                if (profile.ImgLink != null && Img != null)
                                {
                                    var request = System.Net.WebRequest.Create(ConfigurationFileStatic.PathImgWeb + "" + profile.ImgLink.Replace("//", "/"));
                                    try
                                    {
                                        using (var response = request.GetResponse())
                                        using (var stream = response.GetResponseStream())
                                        {
                                            Img.Image = Bitmap.FromStream(stream);
                                            Img.SizeMode = PictureBoxSizeMode.StretchImage;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    txtNamaLengkap.Text = profile.NamaLengkap;
                                    txtUsername.Text = profile.username;
                                    txtAlamat.Text = profile.Alamat;
                                    txtContact.Text = profile.NoHp;
                                    txtEmail.Text = profile.Email;
                                    txtNIK.Text = "NIK-" + profile.id;
                                    txtPassword.Text = profile.password;
                                    var listGender = f.GetListGender();
                                    foreach (var d in listGender)
                                    {
                                        cmbGender.Items.Add(d.Text);
                                    }
                                    cmbGender.SelectedIndex = cmbGender.FindStringExact(profile.Gender);
                                }

                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }
    }
}
