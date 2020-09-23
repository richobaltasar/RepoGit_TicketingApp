using Ewats_App.Function;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Ewats_App.Page
{
    public partial class Dashboard : UserControl
    {
        GlobalFunc f = new GlobalFunc();

        public Dashboard()
        {
            InitializeComponent();
        }

        public void atur_grid()
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 2;
            dt_grid.Columns[0].Name = "Nama Ticket";
            dt_grid.Columns[1].Name = "Total";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];

            dt_grid.Dock = DockStyle.None;

            dt_grid.BorderStyle = BorderStyle.None;
            dt_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grid.ScrollBars = ScrollBars.Both;

            dt_grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dt_grid.DefaultCellStyle.Font = new Font("Tahoma", 6);
            dt_grid.DefaultCellStyle.ForeColor = Color.Black;

            dt_grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            var data = f.CheckOpenCashier();
            if (data.Success == true)
            {
                btnClosing.Enabled = true;
            }
            else
            {
                btnClosing.Enabled = false;
            }

            atur_grid();
            atur_grid2();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TambahDanaModal f = new TambahDanaModal();
            f.Show();
            f.BringToFront();
            f.StartPosition = FormStartPosition.CenterScreen;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void atur_grid2()
        {
            dt_grid2.Rows.Clear();
            dt_grid2.ColumnCount = 6;
            dt_grid2.Columns[0].Name = "X";
            dt_grid2.Columns[1].Name = "Id Trx";
            dt_grid2.Columns[2].Name = "Datetime";
            dt_grid2.Columns[3].Name = "Nama Transaksi";
            dt_grid2.Columns[4].Name = "Total Belanja";
            dt_grid2.Columns[5].Name = "Cashier by";

            dt_grid2.RowHeadersVisible = false;
            dt_grid2.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid2.Columns[0];
            DataGridViewColumn column2 = dt_grid2.Columns[1];
            DataGridViewColumn column3 = dt_grid2.Columns[2];
            DataGridViewColumn column4 = dt_grid2.Columns[3];
            DataGridViewColumn column5 = dt_grid2.Columns[4];
            DataGridViewColumn column6 = dt_grid2.Columns[5];

            // Initialize basic DataGridView properties.
            dt_grid2.Dock = DockStyle.None;

            dt_grid2.BorderStyle = BorderStyle.None;
            dt_grid2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grid2.ScrollBars = ScrollBars.Both;

            dt_grid2.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dt_grid2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dt_grid2.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dt_grid2.DefaultCellStyle.ForeColor = Color.Black;
            dt_grid2.Columns[0].Width = 40;
        }

        private void btnClosing_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["ClosingCashier"];
            if (fc != null)
            {
                fc.Show();
                fc.BringToFront();
            }
            else
            {
                ClosingCashier frm = new ClosingCashier();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.BringToFront();
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                frm.Show();
            }
        }

        private void dt_grid2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["StockOpname"];
            if (fc != null)
            {
                fc.Show();
                fc.BringToFront();
            }
            else
            {
                StockOpname frm = new StockOpname();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.BringToFront();
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                frm.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["HistoryFoodCourt"];
            if (fc != null)
            {
                fc.Show();
                fc.BringToFront();
            }
            else
            {
                HistoryFoodCourt frm = new HistoryFoodCourt();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.BringToFront();
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                frm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            f.loadGridSearch(dt_grid2, txtSearchTrx.Text);
        }

        private void txtSearchTrx_TextChanged(object sender, EventArgs e)
        {
            f.loadGridSearch(dt_grid2, txtSearchTrx.Text);
        }

        private void dt_grid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dt_grid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)// created column index (delete button)
            {
                if (e.RowIndex >= 0)
                {
                    int rowIndex = e.RowIndex;
                    DataGridViewRow row = dt_grid2.Rows[rowIndex];
                    var data = new AllTransaksiModel();

                    try
                    {

                        data.IdTrx = dt_grid2.Rows[rowIndex].Cells[1].Value.ToString();
                        data.Datetime = dt_grid2.Rows[rowIndex].Cells[2].Value.ToString();
                        data.JenisTransaksi = dt_grid2.Rows[rowIndex].Cells[3].Value.ToString();
                        data.Nominal = f.ConvertDecimal(dt_grid2.Rows[rowIndex].Cells[4].Value.ToString());
                        data.CashierBy = dt_grid2.Rows[rowIndex].Cells[5].Value.ToString();

                        if (data.IdTrx.Contains("REG") == true)
                        {
                            data.IdTrx = data.IdTrx.Replace("REG", "").Trim();
                            if (data.IdTrx != null && data.IdTrx != "")
                            {
                                TempAllTransaksiModel.IdTrx = data.IdTrx;
                                TempAllTransaksiModel.Datetime = data.Datetime;
                                TempAllTransaksiModel.CashierBy = data.CashierBy;
                                TempAllTransaksiModel.JenisTransaksi = data.JenisTransaksi;
                                TempAllTransaksiModel.Nominal = data.Nominal;

                                Form fc = Application.OpenForms["TrxRegistrasi"];
                                if (fc != null)
                                {
                                    fc.Show();
                                    fc.BringToFront();
                                }
                                else
                                {
                                    Page.TrxRegistrasi frm = new Page.TrxRegistrasi();
                                    frm.StartPosition = FormStartPosition.CenterScreen;
                                    frm.BringToFront();
                                    frm.MaximizeBox = false;
                                    frm.MinimizeBox = false;
                                    frm.Show();
                                    frm.BringToFront();
                                }

                            }

                        }
                        else if (data.IdTrx.Contains("TOPUP") == true)
                        {
                            data.IdTrx = data.IdTrx.Replace("TOPUP", "").Trim();
                            if (data.IdTrx != null && data.IdTrx != "")
                            {
                                TempAllTransaksiModel.IdTrx = data.IdTrx;
                                TempAllTransaksiModel.Datetime = data.Datetime;
                                TempAllTransaksiModel.CashierBy = data.CashierBy;
                                TempAllTransaksiModel.JenisTransaksi = data.JenisTransaksi;
                                TempAllTransaksiModel.Nominal = data.Nominal;

                                Form fc = Application.OpenForms["TrxTopup"];
                                if (fc != null)
                                {
                                    fc.Show();
                                    fc.BringToFront();
                                }
                                else
                                {
                                    Page.TrxTopup frm = new Page.TrxTopup();
                                    frm.StartPosition = FormStartPosition.CenterScreen;
                                    frm.BringToFront();
                                    frm.MaximizeBox = false;
                                    frm.MinimizeBox = false;
                                    frm.Show();
                                }

                            }
                        }
                        else if (data.IdTrx.Contains("REFUND") == true)
                        {
                            data.IdTrx = data.IdTrx.Replace("REFUND", "").Trim();
                            if (data.IdTrx != null && data.IdTrx != "")
                            {
                                TempAllTransaksiModel.IdTrx = data.IdTrx;
                                TempAllTransaksiModel.Datetime = data.Datetime;
                                TempAllTransaksiModel.CashierBy = data.CashierBy;
                                TempAllTransaksiModel.JenisTransaksi = data.JenisTransaksi;
                                TempAllTransaksiModel.Nominal = data.Nominal;

                                Form fc = Application.OpenForms["TrxRefund"];
                                if (fc != null)
                                {
                                    fc.Show();
                                    fc.BringToFront();
                                }
                                else
                                {
                                    Page.TrxRefund frm = new Page.TrxRefund();
                                    frm.StartPosition = FormStartPosition.CenterScreen;
                                    frm.BringToFront();
                                    frm.MaximizeBox = false;
                                    frm.MinimizeBox = false;
                                    frm.Show();
                                }

                            }
                        }
                        else if (data.IdTrx.Contains("FOODCOURT") == true)
                        {
                            data.IdTrx = data.IdTrx.Replace("FOODCOURT", "").Trim();
                            if (data.IdTrx != null && data.IdTrx != "")
                            {
                                TempAllTransaksiModel.IdTrx = data.IdTrx;
                                TempAllTransaksiModel.Datetime = data.Datetime;
                                TempAllTransaksiModel.CashierBy = data.CashierBy;
                                TempAllTransaksiModel.JenisTransaksi = data.JenisTransaksi;
                                TempAllTransaksiModel.Nominal = data.Nominal;

                                Form fc = Application.OpenForms["TrxFoodCourt"];
                                if (fc != null)
                                {
                                    //fc.Close();
                                    fc.Show();
                                    fc.BringToFront();
                                }
                                else
                                {
                                    TrxFoodCourt frm = new TrxFoodCourt();
                                    frm.StartPosition = FormStartPosition.CenterScreen;
                                    frm.BringToFront();
                                    frm.MaximizeBox = false;
                                    frm.MinimizeBox = false;
                                    frm.Show();
                                }


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        f.messageboxError(ex.Message);
                    }
                }
            }
        }

        private void txtSearchTrx_MouseClick(object sender, MouseEventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtSearchTrx_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
