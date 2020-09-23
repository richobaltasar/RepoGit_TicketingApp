using Ewats_App.Function;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class MasterBank : Form
    {
        GlobalFunc f = new GlobalFunc();
        public MasterBank()
        {
            InitializeComponent();
        }

        private void MasterBank_Load(object sender, EventArgs e)
        {
            load_datagrid("");
        }
        public void atur_grid()
        {
            dt_grid.Rows.Clear();
            dt_grid.ColumnCount = 5;
            dt_grid.Columns[0].Name = "Id Log";
            dt_grid.Columns[1].Name = "Kode Bank";
            dt_grid.Columns[2].Name = "Nama Bank";
            dt_grid.Columns[3].Name = "Diskon Bank";
            dt_grid.Columns[4].Name = "Admin Charges";

            dt_grid.RowHeadersVisible = false;
            dt_grid.ColumnHeadersVisible = true;
            DataGridViewColumn column1 = dt_grid.Columns[0];
            DataGridViewColumn column2 = dt_grid.Columns[1];
            DataGridViewColumn column3 = dt_grid.Columns[2];
            DataGridViewColumn column4 = dt_grid.Columns[3];
            DataGridViewColumn column5 = dt_grid.Columns[4];

            // Initialize basic DataGridView properties.
            dt_grid.Dock = DockStyle.None;
            dt_grid.BorderStyle = BorderStyle.None;
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
            dt_grid.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            dt_grid.Columns[0].Width = 40;

        }

        public void load_datagrid(string search)
        {
            atur_grid();
            var data = f.GetDataBank(search);
            int a = 0;
            foreach (var r in data)
            {
                a++;
                string[] row = new string[] { r.idLog, r.KodeBank, r.NamaBank, r.DiskonBank, r.AdminCharges };
                this.dt_grid.Rows.Add(row);
                DataGridViewRow d = dt_grid.Rows[a - 1];
                d.MinimumHeight = 40;
            }
        }

        private void dt_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var data = new DataBank();
                DataGridViewRow row = this.dt_grid.Rows[e.RowIndex];

                data.idLog = row.Cells["Id Log"].Value.ToString();
                data.KodeBank = row.Cells["Kode Bank"].Value.ToString();
                data.NamaBank = row.Cells["Nama Bank"].Value.ToString();
                data.DiskonBank = row.Cells["Diskon Bank"].Value.ToString();
                data.AdminCharges = row.Cells["Admin Charges"].Value.ToString();

                if (data != null)
                {
                    this.Close();
                    if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCPayDebit"))
                    {
                        UCPayDebit un = new UCPayDebit();
                        un.Dock = DockStyle.Fill;
                        Main.Instance.PnlContainer.Controls.Add(un);
                    }
                    Main.Instance.PnlContainer.Controls["UCPayDebit"].BringToFront();

                    Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                    UserControl fc = tbx.Controls.Find("UCPayDebit", true).FirstOrDefault() as UserControl;
                    if (fc != null)
                    {
                        TextBox txtBankName = fc.Controls.Find("txtBankName", true).FirstOrDefault() as TextBox;

                        if (txtBankName != null)
                        {
                            txtBankName.Text = data.KodeBank + "-" + data.NamaBank;
                        }


                    }


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load_datagrid(txtSearch.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }
        public void input_keyPad(string key, string Object)
        {
            TextBox txt = this.Controls.Find(Object, true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                if (key == "<")
                {
                    if (txt.Text.Length > 0)
                    {
                        txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                    }
                }
                else if (key == ">")
                {
                    txt.SelectionStart = txt.Text.Length;
                    txt.SelectionLength = 0;
                }
                else if (key == "DEL")
                {
                    txt.Clear();
                }
                else if (key == "ENTER")
                {

                }
                else
                {
                    string data = (txt.Text + key);
                    txt.Text = txt.Text + key;
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            load_datagrid(txtSearch.Text);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtSearch");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
