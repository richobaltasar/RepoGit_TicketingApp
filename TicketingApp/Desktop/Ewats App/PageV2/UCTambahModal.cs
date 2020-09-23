using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCTambahModal : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();
        Ticketing t = new Ticketing();
        Sales s = new Sales();

        public UCTambahModal()
        {
            InitializeComponent();
        }

        private void UCTambahModal_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            dt_grid.Dock = DockStyle.Fill;
            panelScanCard.Location = new Point((panel1.Width - (panelScanCard.Width - 100)) / 2, (panel1.Height - (panelScanCard.Height + 100)) / 2);
            load_datagrid();
        }

        public void atur_grid()
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 4;
            dt_grid.Columns[0].Name = "X";
            dt_grid.Columns[1].Name = "Datetime";
            dt_grid.Columns[2].Name = "Nama Kasir";
            dt_grid.Columns[3].Name = "Nominal";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];
            DataGridViewColumn column3 = dt_grid.Columns[2];
            DataGridViewColumn column4 = dt_grid.Columns[3];

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

            dt_grid.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dt_grid.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dt_grid.Columns[1].Width = 50;
            dt_grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dt_grid.Columns[3].Width = 150;
            column2.Width = 0;

        }

        public void load_datagrid()
        {
            atur_grid();
            var data = s.GetCashierDanaModalHistory(f.GetComputerName());
            int a = 0;
            decimal totalDanaModal = 0;
            foreach (var r in data)
            {
                a++;
                string[] row = new string[] { a.ToString(), r.Datetime, r.NamaUser, f.ConvertToRupiah(f.ConvertDecimal(r.NominalTambahModal)) };
                totalDanaModal = totalDanaModal + f.ConvertDecimal(r.NominalTambahModal);
                this.dt_grid.Rows.Add(row);
                DataGridViewRow d = dt_grid.Rows[a - 1];
                d.MinimumHeight = 40;
            }
            lblTotalDanaModal.Text = f.ConvertToRupiah(totalDanaModal);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        public void input_keyPad(string key, string Object)
        {
            TextBox txt = this.Controls.Find(Object, true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                if (key == "BkSpc")
                {
                    if (txt.Text.Length > 0)
                    {
                        txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                        string data = txt.Text.Replace(".", "").Replace(",", "");
                        if (data != "")
                        {
                            decimal t = Convert.ToDecimal(data);
                            txt.Text = string.Format("{0:n0}", t);
                        }
                        else
                        {
                            txt.Text = "0";
                        }
                    }
                }
                else if (key == "Del")
                {
                    txt.Text = "0";
                }
                else if (key == "Enter")
                {
                }
                else if (key == "Cancel")
                {
                }
                else
                {
                    string data = (txt.Text + key).Replace(".", "").Replace(",", "");
                    if (data != "")
                    {
                        decimal t = Convert.ToDecimal(data);
                        txt.Text = string.Format("{0:n0}", t);
                    }
                    else
                    {
                        txt.Text = txt.Text + key;
                    }

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtDanaModal");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SaveDanaModal();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            backToHome();
        }

        public void backToHome()
        {
            if (Main.Instance.LabelCardId.Text != "-")
            {
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                {
                    MenuKasir un = new MenuKasir();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }
                Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
            }
            else
            {
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
                {
                    UCScanKartu un = new UCScanKartu();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }
                Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            SaveDanaModal();
        }

        public void SaveDanaModal()
        {
            if (txtDanaModal.Text != "")
            {
                var res = MessageBox.Show("Apakah Anda yakin untuk melakukan Penambahan Modal Cash Box sebesar : " + f.ConvertToRupiah(f.ConvertDecimal(txtDanaModal.Text)) + " ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    decimal DanaModal = g.ConvertToDecimal(txtDanaModal.Text);
                    if (DanaModal > 0)
                    {
                    ulang:
                        var data = new TambahModalCashbox();
                        data.ComputerName = f.GetComputerName();
                        data.NamaUser = f.GetNamaUser(General.IDUser);
                        data.Nominal = DanaModal;
                        var save = f.SaveDataTambahModal(data);
                        if (save.Success == true)
                        {
                            load_datagrid();
                            txtDanaModal.Text = "";
                        }
                        else
                        {
                            var res2 = MessageBox.Show("Terjadi Kesalahan pada SaveDataTambahModal, err:" + save.Message + "", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            if (res2 == DialogResult.Retry)
                            {
                                goto ulang;
                            }
                        }
                    }
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
