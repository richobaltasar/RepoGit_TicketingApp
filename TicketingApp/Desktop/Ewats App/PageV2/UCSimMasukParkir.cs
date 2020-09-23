using QRCoder;
using SharedCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCSimMasukParkir : UserControl
    {
        ACR_NFC NFC = new ACR_NFC();
        Ewats_App.Function.GlobalFunc f = new Function.GlobalFunc();
        GeneralFunction g = new GeneralFunction();
        Sales s = new Sales();

        static UCSimMasukParkir _obj;
        public static UCSimMasukParkir Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new UCSimMasukParkir();
                }
                return _obj;
            }
        }

        public UCSimMasukParkir()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            var data = s.ParkirInWithTombolTicket("Motor", "ParkirB3364BTT.jpeg", "ParkirB3364BTT.jpeg", "ParkirB3364BTT.jpeg", "ParkirB3364BTT.jpeg", "1");
            if (data.status != "ERROR")
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(data.message, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                if (qrCodeImage != null)
                {
                    picBarcode.Image = qrCodeImage;
                }

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
                        Label lblTimerOpening = fc.Controls.Find("lblTimerOpening", true).FirstOrDefault() as Label;
                        Label lblAction = fc.Controls.Find("lblAction", true).FirstOrDefault() as Label;

                        if (lblAction != null)
                        {
                            lblAction.Text = "Silahkan Masuk";
                        }

                        if (picShootKamera != null)
                        {
                            picShootKamera.Image = qrCodeImage;
                            picShootKamera.SizeMode = PictureBoxSizeMode.StretchImage;
                        }

                        if (lblTimerOpening != null)
                        {
                            lblTimerOpening.Text = "600";
                        }

                        UCOpeningGateParkir.Instance.TimerOpen.Start();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {

            }

        }

        private void timerScan_Tick(object sender, EventArgs e)
        {

        }

        private void UCSimMasukParkir_Load(object sender, EventArgs e)
        {

        }

        private void TimerOpening_Tick(object sender, EventArgs e)
        {

        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
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
                        var data = s.ParkirIn("Motor", Account.AccountNumber, "ParkirB3364BTT.jpeg", "ParkirB3364BTT.jpeg", "ParkirB3364BTT.jpeg", "ParkirB3364BTT.jpeg", "1");
                        if (data.status == "SUCCESS")
                        {
                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data.message, QRCodeGenerator.ECCLevel.Q);
                            QRCode qrCode = new QRCode(qrCodeData);
                            Bitmap qrCodeImage = qrCode.GetGraphic(20);

                            if (qrCodeImage != null)
                            {
                                picBarcode.Image = qrCodeImage;
                            }

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
                                    Label lblTimerOpening = fc.Controls.Find("lblTimerOpening", true).FirstOrDefault() as Label;

                                    Label lblAction = fc.Controls.Find("lblAction", true).FirstOrDefault() as Label;
                                    if (lblAction != null)
                                    {
                                        lblAction.Text = "Silahkan masuk";
                                    }

                                    if (picShootKamera != null)
                                    {
                                        picShootKamera.Image = qrCodeImage;
                                        picShootKamera.SizeMode = PictureBoxSizeMode.StretchImage;
                                    }

                                    if (lblTimerOpening != null)
                                    {
                                        lblTimerOpening.Text = "600";
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
