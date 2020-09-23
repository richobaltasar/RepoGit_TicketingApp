using SharedCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCSimKeluarParkir : UserControl
    {
        ACR_NFC NFC = new ACR_NFC();
        Ewats_App.Function.GlobalFunc f = new Function.GlobalFunc();
        GeneralFunction g = new GeneralFunction();
        Sales s = new Sales();

        static UCSimKeluarParkir _obj;
        public static UCSimKeluarParkir Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new UCSimKeluarParkir();
                }
                return _obj;
            }
        }


        public UCSimKeluarParkir()
        {
            InitializeComponent();
        }

        private void timerScan_Tick(object sender, EventArgs e)
        {
            var Card = NFC.ReadCardDataKey();
            if (Card.IdCard != null)
            {
                if (Card.CodeId != null)
                {
                    string AccountNumber = Card.IdCard + "-" + Card.CodeId;
                    var Account = s.GetDataAccount(AccountNumber);
                    if (Account.AccountNumber != "" && Account.AccountNumber != null)
                    {
                        var data = s.GetDataParkir(Account.AccountNumber);
                        if (data.Status == "2")
                        {
                            var r = s.ParkirOut(data.AccountNumber, "2");
                            if (r.status == "SUCCESS")
                            {
                                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCOpeningGateParkir"))
                                {
                                    UCOpeningGateParkir un = new UCOpeningGateParkir();
                                    un.Dock = DockStyle.Fill;
                                    Main.Instance.PnlContainer.Controls.Add(un);
                                }

                                try
                                {
                                    Main.Instance.PnlContainer.Controls["UCOpeningGateParkir"].BringToFront();
                                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                    UserControl fc = tbx.Controls.Find("UCOpeningGateParkir", true).FirstOrDefault() as UserControl;
                                    if (fc != null)
                                    {
                                        PictureBox picShootKamera = fc.Controls.Find("picShootKamera", true).FirstOrDefault() as PictureBox;
                                        if (picShootKamera != null)
                                        {
                                            var request = System.Net.WebRequest.Create(data.Img1);

                                            using (var response = request.GetResponse())
                                            using (var stream = response.GetResponseStream())
                                            {
                                                picShootKamera.Image = Bitmap.FromStream(stream);
                                            }
                                        }

                                        UCOpeningGateParkir.Instance.TimerOpen.Start();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
            {
                UCScanKartu un = new UCScanKartu();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }

        private void UCSimKeluarParkir_Load(object sender, EventArgs e)
        {
            panelScan.Location = new Point((this.Width - panelScan.Width) / 2, (this.Height - panelScan.Height) / 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Card = NFC.ReadCardDataKey();
            if (Card.IdCard != null)
            {
                if (Card.CodeId != null)
                {
                    string AccountNumber = Card.IdCard + "-" + Card.CodeId;
                    var Account = s.GetDataAccount(AccountNumber);
                    if (Account.AccountNumber != "" && Account.AccountNumber != null)
                    {
                        var data = s.GetDataParkir(Account.AccountNumber);
                        if (data.Status == "2")
                        {
                            var r = s.ParkirOut(data.AccountNumber, "2");
                            if (r.status == "SUCCESS")
                            {
                                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCOpeningGateParkir"))
                                {
                                    UCOpeningGateParkir un = new UCOpeningGateParkir();
                                    un.Dock = DockStyle.Fill;
                                    Main.Instance.PnlContainer.Controls.Add(un);
                                }

                                try
                                {
                                    Main.Instance.PnlContainer.Controls["UCOpeningGateParkir"].BringToFront();
                                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                    UserControl fc = tbx.Controls.Find("UCOpeningGateParkir", true).FirstOrDefault() as UserControl;
                                    if (fc != null)
                                    {
                                        PictureBox picShootKamera = fc.Controls.Find("picShootKamera", true).FirstOrDefault() as PictureBox;

                                        if (picShootKamera != null)
                                        {
                                            var request = System.Net.WebRequest.Create(general.ImgPath + data.Img1);

                                            using (var response = request.GetResponse())
                                            using (var stream = response.GetResponseStream())
                                            {
                                                picShootKamera.Image = Bitmap.FromStream(stream);
                                            }
                                        }

                                        Label lblAction = fc.Controls.Find("lblAction", true).FirstOrDefault() as Label;
                                        if (lblAction != null)
                                        {
                                            lblAction.Text = "Silahkan keluar";
                                        }


                                        UCOpeningGateParkir.Instance.TimerOpen.Start();
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
        }
    }
}
