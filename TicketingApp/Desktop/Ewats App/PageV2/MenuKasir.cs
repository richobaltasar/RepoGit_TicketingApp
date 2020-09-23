using SharedCode;
using SharedCode.Function;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class MenuKasir : UserControl
    {
        ACR_NFC NFC = new ACR_NFC();
        GeneralFunction G = new GeneralFunction();
        Function.GlobalFunc f = new Function.GlobalFunc();
        Sales s = new Sales();
        Parkir p = new Parkir();

        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;

        private void dt_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            //{
            //    string Id = dt_grid.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            //    string Category = dt_grid.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            //    if (Category != "CARD")
            //    {
            //        for (int v = 0; v < dt_grid.Rows.Count; v++)
            //        {
            //            if (string.Equals(dt_grid[1, v].Value as string, Id) && string.Equals(dt_grid[2, v].Value as string, Category))
            //            {
            //                dt_grid.Rows.RemoveAt(v);
            //                v--;
            //            }
            //        }
            //    }
            //}
        }

        private void dt_grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dt_grid.Rows[e.RowIndex];
                if (row.Cells.Count > 1)
                {
                    decimal totalbelanja = 0;
                    foreach (DataGridViewRow rows in dt_grid.Rows)
                    {
                        if (rows.Cells[6].Value.ToString() != "")
                        {
                            if (rows.Cells[6].Value.ToString().Contains("-") == true)
                            {
                                totalbelanja = totalbelanja - f.ConvertDecimal(rows.Cells[6].Value.ToString().Replace("-", ""));
                            }
                            else
                            {
                                totalbelanja = totalbelanja + f.ConvertDecimal(rows.Cells[6].Value.ToString());
                            }
                        }
                    }
                    txtTotalTrx.Text = G.ConvertToRupiah(totalbelanja);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dt_grid.Rows.Count > 0 && txtTotalTrx.Text != "" && f.ConvertDecimal(txtTotalTrx.Text) > 0)
            {
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCPayment"))
                {
                    UCPayment un = new UCPayment();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }

                try
                {
                    Main.Instance.PnlContainer.Controls["UCPayment"].BringToFront();

                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                    UserControl fc = tbx.Controls.Find("UCPayment", true).FirstOrDefault() as UserControl;

                    if (fc != null)
                    {
                        TextBox txtTotalTransaksi = fc.Controls.Find("txtTotalTransaksi", true).FirstOrDefault() as TextBox;
                        TextBox txtSaldo = fc.Controls.Find("txtSaldo", true).FirstOrDefault() as TextBox;
                        CheckBox chkPakaiSaldo = fc.Controls.Find("chkPakaiSaldo", true).FirstOrDefault() as CheckBox;
                        TextBox txtTotalBayar = fc.Controls.Find("txtTotalBayar", true).FirstOrDefault() as TextBox;
                        TextBox txtPakaiSaldo = fc.Controls.Find("txtPakaiSaldo", true).FirstOrDefault() as TextBox;

                        Button btnBayarEmoney = fc.Controls.Find("btnBayarEmoney", true).FirstOrDefault() as Button;
                        Button btnTunai = fc.Controls.Find("btnTunai", true).FirstOrDefault() as Button;
                        Button btnDebit = fc.Controls.Find("btnDebit", true).FirstOrDefault() as Button;
                        Button btnBack = fc.Controls.Find("btnBack", true).FirstOrDefault() as Button;

                        Panel panelScan = fc.Controls.Find("panelScan", true).FirstOrDefault() as Panel;

                        int HarusTunai = 0;
                        int HarusEmoney = 0;

                        if (txtTotalTransaksi != null)
                        {
                            txtTotalTransaksi.Text = txtTotalTrx.Text;
                        }

                        if (txtSaldo != null)
                        {
                            var Card = NFC.ReadCardDataKey();
                            if (Card.CodeId != "" && Card.CodeId != "0")
                            {
                                int checkParkirAdadiList = 0;
                                string AccountNum = Card.IdCard + "-" + Card.CodeId;
                                var d = s.GetDataParkir(AccountNum);
                            lanjut:
                                if (d.AccountNumber != null && checkParkirAdadiList == 0 && d.Status == "1")
                                {
                                    var data = p.ReadParkirCheckin(d.BarcodeReciptCode);
                                    if (data.Id != null)
                                    {

                                        if (dt_grid.Rows.Count > 0)
                                        {
                                            foreach (DataGridViewRow row in dt_grid.Rows)
                                            {
                                                if (row.Cells[2].Value.ToString() == "PARKIR")
                                                {
                                                    checkParkirAdadiList++;
                                                }
                                            }
                                            if (checkParkirAdadiList == 0)
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
                                                        TextBox txtSaldo2 = fc2.Controls.Find("txtSaldo", true).FirstOrDefault() as TextBox;

                                                        TextBox txtBarcodeId = fc2.Controls.Find("txtBarcodeId", true).FirstOrDefault() as TextBox;

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
                                                            txtCharges.Text = G.ConvertToRupiah(G.ConvertToDecimal(data.Charges));
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

                                                        if (txtSaldo2 != null)
                                                        {
                                                            txtSaldo2.Text = f.ConvertToRupiah(Card.SaldoEmoney);
                                                        }
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            else
                                            {
                                                goto lanjut;
                                            }
                                        }
                                    }
                                }
                                else if (d.AccountNumber != null && checkParkirAdadiList > 0 && d.Status == "1")
                                {
                                    if (Card.CodeId != "" && Card.CodeId != "0")
                                    {
                                        if (Card.SaldoEmoney > 0)
                                        {
                                            if (chkPakaiSaldo != null)
                                            {

                                                if (dt_grid.Rows.Count > 0)
                                                {
                                                    foreach (DataGridViewRow row in dt_grid.Rows)
                                                    {
                                                        if (row.Cells[2].Value.ToString() == "TOPUP")
                                                        {
                                                            HarusTunai++;
                                                        }
                                                        else if (row.Cells[2].Value.ToString() == "FOODCOURT")
                                                        {
                                                            HarusEmoney++;
                                                        }
                                                    }
                                                }
                                                if (HarusTunai > 0)
                                                {
                                                    chkPakaiSaldo.Enabled = false;
                                                    chkPakaiSaldo.Checked = false;
                                                    txtPakaiSaldo.Visible = false;
                                                    if (txtTotalBayar != null)
                                                    {
                                                        txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                                    }
                                                }
                                                else if (HarusEmoney > 0)
                                                {
                                                    txtSaldo.Text = G.ConvertToRupiah(Card.SaldoEmoney);
                                                    chkPakaiSaldo.Enabled = false;
                                                    chkPakaiSaldo.Checked = true;
                                                    txtSaldo.Visible = false;
                                                    txtPakaiSaldo.Visible = true;
                                                    decimal sisa = G.ConvertToDecimal(txtSaldo.Text) - G.ConvertToDecimal(txtTotalTransaksi.Text);
                                                    if (sisa > 0)
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-G.ConvertToDecimal(txtSaldo.Text) + sisa); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(0); }
                                                    }
                                                    else
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-(G.ConvertToDecimal(txtSaldo.Text))); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(-sisa); }
                                                    }
                                                }
                                                else
                                                {
                                                    txtSaldo.Text = G.ConvertToRupiah(Card.SaldoEmoney);
                                                    chkPakaiSaldo.Enabled = true;
                                                    chkPakaiSaldo.Checked = true;
                                                    txtSaldo.Visible = false;
                                                    txtPakaiSaldo.Visible = true;
                                                    decimal sisa = G.ConvertToDecimal(txtSaldo.Text) - G.ConvertToDecimal(txtTotalTransaksi.Text);
                                                    if (sisa > 0)
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-G.ConvertToDecimal(txtSaldo.Text) + sisa); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(0); }
                                                    }
                                                    else
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-(G.ConvertToDecimal(txtSaldo.Text))); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(-sisa); }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (chkPakaiSaldo != null)
                                            {
                                                chkPakaiSaldo.Enabled = false;
                                                chkPakaiSaldo.Checked = false;
                                                txtPakaiSaldo.Visible = false;
                                                if (txtTotalBayar != null)
                                                {
                                                    txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (chkPakaiSaldo != null)
                                        {
                                            chkPakaiSaldo.Enabled = false;
                                            chkPakaiSaldo.Checked = false;
                                            if (txtTotalBayar != null)
                                            {
                                                txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                            }
                                        }
                                    }

                                    if (btnBayarEmoney != null && btnTunai != null && btnDebit != null && btnBack != null && panelScan != null)
                                    {
                                        if (G.ConvertToDecimal(txtTotalBayar.Text) == 0)
                                        {
                                            btnBayarEmoney.Show();
                                            btnTunai.Hide();
                                            btnDebit.Hide();
                                            btnBayarEmoney.Location = new Point(22, 225);
                                            btnBack.Location = new Point(22, 291);
                                            panelScan.Height = 369;
                                        }
                                        else
                                        {
                                            if (HarusEmoney > 0)
                                            {
                                                btnBayarEmoney.Show();
                                                btnTunai.Hide();
                                                btnDebit.Hide();
                                                btnBayarEmoney.Location = new Point(22, 225);
                                                btnBack.Location = new Point(22, 291);
                                                panelScan.Height = 369;
                                            }
                                            else
                                            {
                                                btnBayarEmoney.Hide();
                                                btnTunai.Show();
                                                btnDebit.Show();
                                                btnTunai.Location = new Point(22, 225);
                                                btnDebit.Location = new Point(22, 291);
                                                btnBack.Location = new Point(22, 357);
                                                panelScan.Height = 433;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    if (Card.CodeId != "" && Card.CodeId != "0")
                                    {
                                        if (Card.SaldoEmoney > 0)
                                        {
                                            if (chkPakaiSaldo != null)
                                            {

                                                if (dt_grid.Rows.Count > 0)
                                                {
                                                    foreach (DataGridViewRow row in dt_grid.Rows)
                                                    {
                                                        if (row.Cells[2].Value.ToString() == "TOPUP")
                                                        {
                                                            HarusTunai++;
                                                        }
                                                        else if (row.Cells[2].Value.ToString() == "FOODCOURT")
                                                        {
                                                            HarusEmoney++;
                                                        }
                                                    }
                                                }
                                                if (HarusTunai > 0)
                                                {
                                                    chkPakaiSaldo.Enabled = false;
                                                    chkPakaiSaldo.Checked = false;
                                                    txtPakaiSaldo.Visible = false;
                                                    if (txtTotalBayar != null)
                                                    {
                                                        txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                                    }
                                                }
                                                else if (HarusEmoney > 0)
                                                {
                                                    txtSaldo.Text = G.ConvertToRupiah(Card.SaldoEmoney);
                                                    chkPakaiSaldo.Enabled = false;
                                                    chkPakaiSaldo.Checked = true;
                                                    txtSaldo.Visible = false;
                                                    txtPakaiSaldo.Visible = true;
                                                    decimal sisa = G.ConvertToDecimal(txtSaldo.Text) - G.ConvertToDecimal(txtTotalTransaksi.Text);
                                                    if (sisa > 0)
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-G.ConvertToDecimal(txtSaldo.Text) + sisa); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(0); }
                                                    }
                                                    else
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-(G.ConvertToDecimal(txtSaldo.Text))); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(-sisa); }
                                                    }
                                                }
                                                else
                                                {
                                                    txtSaldo.Text = G.ConvertToRupiah(Card.SaldoEmoney);
                                                    chkPakaiSaldo.Enabled = true;
                                                    chkPakaiSaldo.Checked = true;
                                                    txtSaldo.Visible = false;
                                                    txtPakaiSaldo.Visible = true;
                                                    decimal sisa = G.ConvertToDecimal(txtSaldo.Text) - G.ConvertToDecimal(txtTotalTransaksi.Text);
                                                    if (sisa > 0)
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-G.ConvertToDecimal(txtSaldo.Text) + sisa); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(0); }
                                                    }
                                                    else
                                                    {
                                                        if (txtPakaiSaldo != null)
                                                        { txtPakaiSaldo.Text = G.ConvertToRupiah(-(G.ConvertToDecimal(txtSaldo.Text))); }
                                                        if (txtTotalBayar != null)
                                                        { txtTotalBayar.Text = G.ConvertToRupiah(-sisa); }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (chkPakaiSaldo != null)
                                            {
                                                chkPakaiSaldo.Enabled = false;
                                                chkPakaiSaldo.Checked = false;
                                                txtPakaiSaldo.Visible = false;
                                                if (txtTotalBayar != null)
                                                {
                                                    txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (chkPakaiSaldo != null)
                                        {
                                            chkPakaiSaldo.Enabled = false;
                                            chkPakaiSaldo.Checked = false;
                                            if (txtTotalBayar != null)
                                            {
                                                txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                            }
                                        }
                                    }

                                    if (btnBayarEmoney != null && btnTunai != null && btnDebit != null && btnBack != null && panelScan != null)
                                    {
                                        if (G.ConvertToDecimal(txtTotalBayar.Text) == 0)
                                        {
                                            btnBayarEmoney.Show();
                                            btnTunai.Hide();
                                            btnDebit.Hide();
                                            btnBayarEmoney.Location = new Point(22, 225);
                                            btnBack.Location = new Point(22, 291);
                                            panelScan.Height = 369;
                                        }
                                        else
                                        {
                                            if (HarusEmoney > 0)
                                            {
                                                btnBayarEmoney.Show();
                                                btnTunai.Hide();
                                                btnDebit.Hide();
                                                btnBayarEmoney.Location = new Point(22, 225);
                                                btnBack.Location = new Point(22, 291);
                                                panelScan.Height = 369;
                                            }
                                            else
                                            {
                                                btnBayarEmoney.Hide();
                                                btnTunai.Show();
                                                btnDebit.Show();
                                                btnTunai.Location = new Point(22, 225);
                                                btnDebit.Location = new Point(22, 291);
                                                btnBack.Location = new Point(22, 357);
                                                panelScan.Height = 433;
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (chkPakaiSaldo != null)
                                {
                                    chkPakaiSaldo.Enabled = false;
                                    chkPakaiSaldo.Checked = false;
                                    if (txtTotalBayar != null)
                                    {
                                        txtTotalBayar.Text = G.ConvertToRupiah(G.ConvertToDecimal(txtTotalTransaksi.Text));
                                    }
                                }
                                if (btnBayarEmoney != null && btnTunai != null && btnDebit != null && btnBack != null && panelScan != null)
                                {
                                    if (G.ConvertToDecimal(txtTotalBayar.Text) == 0)
                                    {
                                        btnBayarEmoney.Show();
                                        btnTunai.Hide();
                                        btnDebit.Hide();
                                        btnBayarEmoney.Location = new Point(22, 225);
                                        btnBack.Location = new Point(22, 291);
                                        panelScan.Height = 369;
                                    }
                                    else
                                    {
                                        if (HarusEmoney > 0)
                                        {
                                            btnBayarEmoney.Show();
                                            btnTunai.Hide();
                                            btnDebit.Hide();
                                            btnBayarEmoney.Location = new Point(22, 225);
                                            btnBack.Location = new Point(22, 291);
                                            panelScan.Height = 369;
                                        }
                                        else
                                        {
                                            btnBayarEmoney.Hide();
                                            btnTunai.Show();
                                            btnDebit.Show();
                                            btnTunai.Location = new Point(22, 225);
                                            btnDebit.Location = new Point(22, 291);
                                            btnBack.Location = new Point(22, 357);
                                            panelScan.Height = 433;
                                        }

                                    }
                                }
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

        private void dt_grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                string Id = dt_grid.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                string Category = dt_grid.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                if (Category != "CARD")
                {
                    dt_grid.Rows.RemoveAt(e.RowIndex);
                    ulang:
                    foreach (DataGridViewRow rows in dt_grid.Rows)
                    {
                        if (rows.Cells[2].Value.ToString().Contains(Category) == true && rows.Cells[1].Value.ToString().Contains(Id) ==true)
                        {
                            dt_grid.Rows.RemoveAt(rows.Index);
                            goto ulang;
                        }
                    }



                    if (Category == "TICKETING")
                    {
                        int row = 0;
                        foreach (DataGridViewRow rows in dt_grid.Rows)
                        {
                            if (rows.Cells[2].Value.ToString().Contains("TICKETING") == true)
                            {
                                row++;
                            }
                        }
                        if(row == 0)
                        {
                            foreach (DataGridViewRow rows in dt_grid.Rows)
                            {
                                if (rows.Cells[2].Value.ToString().Contains("VOUCHER")==true && (rows.Cells[3].Value.ToString().Contains("Ticket") == true || rows.Cells[3].Value.ToString().Contains("Tiket") == true))
                                {
                                    dt_grid.Rows.RemoveAt(rows.Index);
                                }
                            }
                        }
                        else
                        {
                            decimal totalTrx = 0;
                            foreach (DataGridViewRow rows in dt_grid.Rows)
                            {
                                if (rows.Cells[2].Value.ToString().Contains("TICKETING") == true)
                                {
                                    totalTrx = totalTrx + G.ConvertToDecimal(rows.Cells[6].Value.ToString());
                                }
                            }
                            decimal TotalVoucher = 0;
                            foreach (DataGridViewRow rows in dt_grid.Rows)
                            {
                                if (rows.Cells[2].Value.ToString().Contains("VOUCHER") == true && (rows.Cells[3].Value.ToString().Contains("Ticket") == true || rows.Cells[3].Value.ToString().Contains("Tiket") == true))
                                {
                                    TotalVoucher = TotalVoucher + G.ConvertToDecimal(rows.Cells[6].Value.ToString().toNumber());
                                }
                            }

                            if (TotalVoucher >= totalTrx)
                            {
                                foreach (DataGridViewRow rows in dt_grid.Rows)
                                {
                                    if (rows.Cells[2].Value.ToString().Contains("VOUCHER") == true && (rows.Cells[3].Value.ToString().Contains("Ticket") == true || rows.Cells[3].Value.ToString().Contains("Tiket") == true))
                                    {
                                        dt_grid.Rows.RemoveAt(rows.Index);
                                    }
                                }
                            }
                        }
                    }

                    if (dt_grid.Rows.Count > 0)
                    {
                        decimal totalbelanja = 0;
                        foreach (DataGridViewRow rows in dt_grid.Rows)
                        {
                            if (rows.Cells[6].Value.ToString() != "")
                            {
                                if (rows.Cells[6].Value.ToString().Contains("-") == true)
                                {
                                    totalbelanja = totalbelanja - f.ConvertDecimal(rows.Cells[6].Value.ToString().Replace("-", ""));
                                }
                                else
                                {
                                    totalbelanja = totalbelanja + f.ConvertDecimal(rows.Cells[6].Value.ToString());
                                }
                            }
                        }
                        txtTotalTrx.Text = G.ConvertToRupiah(totalbelanja);
                    }
                    else
                    {
                        txtTotalTrx.Text = G.ConvertToRupiah(0);
                    }

                }
            }
        }

        private void dt_grid_Click(object sender, EventArgs e)
        {

        }

        //string readername = "ACS ACR122 0";


        public MenuKasir()
        {
            InitializeComponent();
        }

        private void MenuKasir_Load(object sender, EventArgs e)
        {
            GetMenu("Desktop");
            atur_grid();
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

                    il.ImageSize = new Size(80, 80);
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

        private void ListMenu_Click(object sender, EventArgs e)
        {
            try
            {
                var data = ListMenu.SelectedItems[0];
                if (data.Name != null)
                {
                    var action = data.Name.Split('~');
                    if (action[2] != null)
                    {
                        if (!Main.Instance.PnlContainer.Controls.ContainsKey(action[2]))
                        {
                            if (action[2] == "UCVoucher")
                            {
                                UCVoucher un = new UCVoucher();
                                un.Dock = DockStyle.Fill;
                                Main.Instance.PnlContainer.Controls.Add(un);
                            }
                            else if (action[2] == "UCTicketing")
                            {
                                UCTicketing un = new UCTicketing();
                                un.Dock = DockStyle.Fill;
                                Main.Instance.PnlContainer.Controls.Add(un);
                            }
                            else if (action[2] == "UCTopup")
                            {
                                UCTopup un = new UCTopup();
                                un.Dock = DockStyle.Fill;
                                Main.Instance.PnlContainer.Controls.Add(un);
                            }
                            else if (action[2] == "UCRefund")
                            {
                                var Card = NFC.ReadCardDataKey();
                                if (Card.IdCard != null)
                                {
                                    UCRefund un = new UCRefund();
                                    un.Dock = DockStyle.Fill;
                                    Main.Instance.PnlContainer.Controls.Add(un);
                                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                    UserControl fc = tbx.Controls.Find("UCRefund", true).FirstOrDefault() as UserControl;
                                    if (fc != null)
                                    {
                                        TextBox txtJaminan = fc.Controls.Find("txtJaminan", true).FirstOrDefault() as TextBox;
                                        TextBox txtSaldo = fc.Controls.Find("txtSaldo", true).FirstOrDefault() as TextBox;
                                        TextBox txtTotalRefund = fc.Controls.Find("txtTotalRefund", true).FirstOrDefault() as TextBox;
                                        var d = s.GetDataAccount(Card.IdCard + "-" + Card.CodeId);
                                        if (txtJaminan != null)
                                        {
                                            txtJaminan.Text = G.ConvertToNumber(G.ConvertToDecimal(d.UangJaminan));
                                        }

                                        if (txtSaldo != null)
                                        {
                                            txtSaldo.Text = G.ConvertToNumber(G.ConvertToDecimal(d.Balanced));
                                        }

                                        if (txtTotalRefund != null)
                                        {
                                            txtTotalRefund.Text = G.ConvertToNumber(G.ConvertToDecimal(d.UangJaminan) + G.ConvertToDecimal(d.Balanced));
                                        }

                                    }
                                }
                            }
                            else if (action[2] == "UCFoodCourt")
                            {
                                UCFoodCourt un = new UCFoodCourt();
                                un.Dock = DockStyle.Fill;
                                Main.Instance.PnlContainer.Controls.Add(un);
                            }
                            else if (action[2] == "UCParkir")
                            {
                                UCParkir un = new UCParkir();
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
                                UCSimMasukParkir un = new UCSimMasukParkir();
                                un.Dock = DockStyle.Fill;
                                Main.Instance.PnlContainer.Controls.Add(un);
                            }

                        }
                        try
                        {
                            Main.Instance.PnlContainer.Controls[action[2]].BringToFront();

                            if (action[2] == "UCRefund")
                            {
                                var Card = NFC.ReadCardDataKey();
                                if (Card.IdCard != null)
                                {
                                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                    UserControl fc = tbx.Controls.Find("UCRefund", true).FirstOrDefault() as UserControl;
                                    if (fc != null)
                                    {
                                        TextBox txtJaminan = fc.Controls.Find("txtJaminan", true).FirstOrDefault() as TextBox;
                                        TextBox txtSaldo = fc.Controls.Find("txtSaldo", true).FirstOrDefault() as TextBox;
                                        TextBox txtTotalRefund = fc.Controls.Find("txtTotalRefund", true).FirstOrDefault() as TextBox;
                                        var d = s.GetDataAccount(Card.IdCard + "-" + Card.CodeId);
                                        if (txtJaminan != null)
                                        {
                                            txtJaminan.Text = G.ConvertToNumber(G.ConvertToDecimal(d.UangJaminan));
                                        }

                                        if (txtSaldo != null)
                                        {
                                            txtSaldo.Text = G.ConvertToNumber(G.ConvertToDecimal(d.Balanced));
                                        }

                                        if (txtTotalRefund != null)
                                        {
                                            txtTotalRefund.Text = G.ConvertToNumber(G.ConvertToDecimal(d.UangJaminan) + G.ConvertToDecimal(d.Balanced));
                                        }

                                    }
                                }
                            }
                            else if (action[2] == "UCParkir")
                            {
                                Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                UserControl fc = tbx.Controls.Find("UCParkir", true).FirstOrDefault() as UserControl;
                                if (fc != null)
                                {
                                    TextBox txtResult = fc.Controls.Find("txtResult", true).FirstOrDefault() as TextBox;
                                    if (txtResult != null)
                                    {
                                        txtResult.Focus();
                                    }
                                }
                            }
                            else if (action[2] == "UCTicketing")
                            {
                                Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                UserControl fc = tbx.Controls.Find("UCTicketing", true).FirstOrDefault() as UserControl;

                                if (fc != null)
                                {
                                    ListView ListMenu2 = fc.Controls.Find("ListMenu", true).FirstOrDefault() as ListView;

                                    if (ListMenu2 != null)
                                    {
                                        var dataMenu = f.GetTicket("");
                                        ImageList il = new ImageList();
                                        int count = 0;
                                        ListMenu2.Clear();
                                        foreach (var img in dataMenu)
                                        {
                                            try
                                            {
                                                System.Net.WebRequest request = System.Net.WebRequest.Create(img.ImgLink);
                                                System.Net.WebResponse resp = request.GetResponse();
                                                System.IO.Stream respStream = resp.GetResponseStream();
                                                Bitmap bmp = new Bitmap(respStream);
                                                respStream.Dispose();

                                                il.ImageSize = new Size(80, 80);
                                                il.Images.Add(bmp);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error : " + ex.Message);
                                            }
                                        }
                                        ListMenu2.LargeImageList = il;
                                        foreach (var d in dataMenu)
                                        {
                                            ListViewItem lst = new ListViewItem();
                                            lst.Text = d.namaticket;
                                            lst.Name = d.IdTicket + "~" + d.namaticket + "~" + d.harga + "~" + d.Asuransi + "~" + d.NamaDiskon + "~" + d.Diskon;
                                            lst.ImageIndex = count++;
                                            ListMenu2.Items.Add(lst);
                                        }

                                    }
                                }
                            }
                            else if (action[2] == "UCVoucher")
                            {
                                Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                                UserControl fc = tbx.Controls.Find("UCVoucher", true).FirstOrDefault() as UserControl;

                                if (fc != null)
                                {
                                    TextBox txtResult = fc.Controls.Find("txtResult", true).FirstOrDefault() as TextBox;

                                    if (txtResult != null)
                                    {
                                        txtResult.Focus();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            Main.Instance.labelTicket.Text = "";
            Main.Instance.LabelCardId.Text = "";
            Main.Instance.LabelCodeId.Text = "";
            Main.Instance.LabelSaldo.Text = "";
            dt_grid.Rows.Clear();
            txtTotalTrx.Clear();

        }

        public void atur_grid()
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 7;
            dt_grid.Columns[0].Name = "X";
            dt_grid.Columns[1].Name = "Id";
            dt_grid.Columns[2].Name = "Category";
            dt_grid.Columns[3].Name = "Nama Item";
            dt_grid.Columns[4].Name = "Harga";
            dt_grid.Columns[5].Name = "Qtx";
            dt_grid.Columns[6].Name = "Total";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];
            DataGridViewColumn column3 = dt_grid.Columns[2];
            DataGridViewColumn column4 = dt_grid.Columns[3];
            DataGridViewColumn column5 = dt_grid.Columns[4];
            DataGridViewColumn column6 = dt_grid.Columns[5];
            DataGridViewColumn column7 = dt_grid.Columns[6];

            // Initialize basic DataGridView properties.
            dt_grid.Dock = DockStyle.None;

            // Set property values appropriate for read-only display and 
            // limited interactivity. 
            dt_grid.AllowUserToAddRows = false;
            dt_grid.AllowUserToDeleteRows = false;
            dt_grid.AllowUserToOrderColumns = true;
            dt_grid.ReadOnly = true;
            dt_grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dt_grid.MultiSelect = true;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            dt_grid.AllowUserToResizeColumns = true;
            dt_grid.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dt_grid.AllowUserToResizeRows = false;
            dt_grid.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // Set the selection background color for all the cells.
            dt_grid.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dt_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dt_grid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            dt_grid.RowsDefaultCellStyle.BackColor = Color.White;
            dt_grid.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            // Set the row and column header styles.
            dt_grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dt_grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dt_grid.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            //dt_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dt_grid.DefaultCellStyle.Font = new Font("Calibri", 10);
            dt_grid.Columns[0].Width = 10;
            dt_grid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dt_grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dt_grid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dt_grid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dt_grid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dt_grid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dt_grid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dt_grid.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dt_grid.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dt_grid.Columns[1].Width = 50;
            dt_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dt_grid.Columns[3].Width = 150;
            column2.Width = 0;
        }
    }
}
