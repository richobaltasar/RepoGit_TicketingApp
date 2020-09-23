using SharedCode;
using SharedCode.Function;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCParkir : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        Parkir f = new Parkir();

        public UCParkir()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
        }

        private void UCParkir_Load(object sender, EventArgs e)
        {
            panelScan.Location = new Point((this.Width - panelScan.Width) / 2, (this.Height - panelScan.Height) / 2);
            txtResult.Clear();
            txtResult.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var data = f.ReadParkirCheckin(txtResult.Text);
                if (data.Id != null)
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

                        Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                        UserControl fc = tbx.Controls.Find("UCParkirCheckin", true).FirstOrDefault() as UserControl;

                        if (fc != null)
                        {
                            PictureBox img1 = fc.Controls.Find("img1", true).FirstOrDefault() as PictureBox;
                            PictureBox img2 = fc.Controls.Find("img2", true).FirstOrDefault() as PictureBox;
                            PictureBox img3 = fc.Controls.Find("img3", true).FirstOrDefault() as PictureBox;
                            PictureBox img4 = fc.Controls.Find("img4", true).FirstOrDefault() as PictureBox;


                            TextBox txtTglMasuk = fc.Controls.Find("txtTglMasuk", true).FirstOrDefault() as TextBox;
                            TextBox txtJamMasuk = fc.Controls.Find("txtJamMasuk", true).FirstOrDefault() as TextBox;
                            TextBox txtCharges = fc.Controls.Find("txtCharges", true).FirstOrDefault() as TextBox;
                            TextBox txtTypeKendaraan = fc.Controls.Find("txtTypeKendaraan", true).FirstOrDefault() as TextBox;
                            TextBox txtNoPolis = fc.Controls.Find("txtNoPolis", true).FirstOrDefault() as TextBox;

                            TextBox txtBarcodeId = fc.Controls.Find("txtBarcodeId", true).FirstOrDefault() as TextBox;

                            if (img1 != null)
                            {
                                var request = System.Net.WebRequest.Create(data.Img1);

                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    img1.Image = Bitmap.FromStream(stream);
                                }
                            }
                            if (img2 != null)
                            {
                                var request = System.Net.WebRequest.Create(data.Img2);

                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    img2.Image = Bitmap.FromStream(stream);
                                }
                            }
                            if (img3 != null)
                            {
                                var request = System.Net.WebRequest.Create(data.Img3);

                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    img3.Image = Bitmap.FromStream(stream);
                                }
                            }
                            if (img4 != null)
                            {
                                var request = System.Net.WebRequest.Create(data.Img4);

                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    img4.Image = Bitmap.FromStream(stream);
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
                                txtCharges.Text = g.ConvertToRupiah(g.ConvertToDecimal(data.Charges));
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
                                txtBarcodeId.Text = txtResult.Text;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                txtResult.Clear();
                txtResult.Focus();
            }
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }
    }
}
