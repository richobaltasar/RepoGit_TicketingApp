using SharedCode;
using SharedCode.Function;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCScanKartu : UserControl
    {
        ACR_NFC NFC = new ACR_NFC();
        GeneralFunction f = new GeneralFunction();
        Function.GlobalFunc ff = new Function.GlobalFunc();
        Sales s = new Sales();
        Parkir p = new Parkir();

        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;

        //string readername = "ACS ACR122 0";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelScanCard_Paint(object sender, PaintEventArgs e)
        {
            panelScanCard.Location = new Point(ClientSize.Width / 2 - panelScanCard.Size.Width / 2, ClientSize.Height / 2 - panelScanCard.Size.Height / 2);
            panelScanCard.Anchor = AnchorStyles.None;
        }

        public Card.SCARD_READERSTATE RdrState;
        public Card.SCARD_IO_REQUEST pioSendRequest;

        public UCScanKartu()
        {
            InitializeComponent();
        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            //panelScanCard.Location = new Point((this.Width - (panelScanCard.Width)) / 2, (this.Height - panelScanCard.Height) / 2);
            //this.panelScanCard.Left = this.Width / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var Card = NFC.ReadCardDataKey();
            if (Card.IdCard != null)
            {
                Main.Instance.LabelCardId.Text = Card.IdCard;
                Main.Instance.LabelCodeId.Text = Card.CodeId;
                Main.Instance.LabelSaldo.Text = f.ConvertToRupiah(Card.SaldoEmoney);
                Main.Instance.labelTicket.Text = Card.TicketWeekDay.ToString();

                if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                {
                    MenuKasir un = new MenuKasir();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }
                Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;
                DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                TextBox txtTotalTrx = fc.Controls.Find("txtTotalTrx", true).FirstOrDefault() as TextBox;
                if (Card.CodeId == "0" || Card.CodeId == null)
                {
                    if (fc != null)
                    {

                        if (dt_grid != null)
                        {
                            decimal Harga = ff.GetJaminan();
                            string[] row = new string[] { "x", "0","CARD",
                                        "New Member ",
                                        f.ConvertToNumber(Harga), "1", f.ConvertToNumber(Harga*1)};
                            dt_grid.Rows.Add(row);
                        }

                    }
                }
                else
                {
                    if (dt_grid != null)
                    {
                        dt_grid.Rows.Clear();
                    }
                    if (txtTotalTrx != null)
                    {
                        txtTotalTrx.Text = "";
                    }

                    string AccountNum = Card.IdCard + "-" + Card.CodeId;
                    var d = s.GetDataParkir(AccountNum);
                    var data = p.ReadParkirCheckin(d.BarcodeReciptCode);
                    if (data.Id != null && d.Status == "1")
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCParkirCheckin"))
                        {
                            UCParkirCheckin un = new UCParkirCheckin();
                            un.Dock = DockStyle.Fill;
                            Main.Instance.PnlContainer.Controls.Add(un);
                        }

                        try
                        {
                            Main.Instance.PnlContainer.Controls["UCParkirCheckin"].BringToFront();

                            Panel tbx2 = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                            UserControl fc2 = tbx2.Controls.Find("UCParkirCheckin", true).FirstOrDefault() as UserControl;

                            if (fc2 != null)
                            {
                                PictureBox img1 = fc2.Controls.Find("img1", true).FirstOrDefault() as PictureBox;
                                PictureBox img2 = fc2.Controls.Find("img2", true).FirstOrDefault() as PictureBox;
                                PictureBox img3 = fc2.Controls.Find("img3", true).FirstOrDefault() as PictureBox;
                                PictureBox img4 = fc2.Controls.Find("img4", true).FirstOrDefault() as PictureBox;


                                TextBox txtTglMasuk = fc2.Controls.Find("txtTglMasuk", true).FirstOrDefault() as TextBox;
                                TextBox txtJamMasuk = fc2.Controls.Find("txtJamMasuk", true).FirstOrDefault() as TextBox;
                                TextBox txtCharges = fc2.Controls.Find("txtCharges", true).FirstOrDefault() as TextBox;
                                TextBox txtTypeKendaraan = fc2.Controls.Find("txtTypeKendaraan", true).FirstOrDefault() as TextBox;
                                TextBox txtNoPolis = fc2.Controls.Find("txtNoPolis", true).FirstOrDefault() as TextBox;

                                TextBox txtAccountNumber = fc2.Controls.Find("txtAccountNumber", true).FirstOrDefault() as TextBox;
                                TextBox txtSaldo = fc2.Controls.Find("txtSaldo", true).FirstOrDefault() as TextBox;

                                TextBox txtBarcodeId = fc2.Controls.Find("txtBarcodeId", true).FirstOrDefault() as TextBox;

                                if (img1 != null)
                                {
                                    var request = System.Net.WebRequest.Create(data.Img1);
                                    try
                                    {
                                        using (var response = request.GetResponse())
                                        using (var stream = response.GetResponseStream())
                                        {
                                            img1.Image = Bitmap.FromStream(stream);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                if (img2 != null)
                                {
                                    var request = System.Net.WebRequest.Create(data.Img2);
                                    try
                                    {
                                        using (var response = request.GetResponse())
                                        using (var stream = response.GetResponseStream())
                                        {
                                            img2.Image = Bitmap.FromStream(stream);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                if (img3 != null)
                                {
                                    var request = System.Net.WebRequest.Create(data.Img3);
                                    try
                                    {
                                        using (var response = request.GetResponse())
                                        using (var stream = response.GetResponseStream())
                                        {
                                            img3.Image = Bitmap.FromStream(stream);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                if (img4 != null)
                                {
                                    var request = System.Net.WebRequest.Create(data.Img4);
                                    try
                                    {
                                        using (var response = request.GetResponse())
                                        using (var stream = response.GetResponseStream())
                                        {
                                            img4.Image = Bitmap.FromStream(stream);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                                if (txtJamMasuk != null)
                                {
                                    txtJamMasuk.Text = data.Datetime.Right(8);
                                }

                                if (txtTglMasuk != null)
                                {
                                    txtTglMasuk.Text = data.Datetime.Left(10);
                                }

                                if (txtCharges != null)
                                {
                                    txtCharges.Text = f.ConvertToRupiah(f.ConvertToDecimal(data.Charges));
                                }

                                if (txtNoPolis != null)
                                {
                                    txtNoPolis.Text = data.PolisNum;
                                }

                                if (txtTypeKendaraan != null)
                                {
                                    txtTypeKendaraan.Text = data.TypeKendaraan;
                                }

                                if (txtBarcodeId != null)
                                {
                                    txtBarcodeId.Text = d.BarcodeReciptCode;
                                }

                                if (txtAccountNumber.Text != null)
                                {
                                    txtAccountNumber.Text = d.AccountNumber;
                                }

                                if (txtSaldo != null)
                                {
                                    txtSaldo.Text = f.ConvertToRupiah(Card.SaldoEmoney);
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
}
