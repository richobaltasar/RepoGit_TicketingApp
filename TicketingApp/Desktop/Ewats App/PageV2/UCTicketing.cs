using SharedCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCTicketing : UserControl
    {
        Function.GlobalFunc f = new Function.GlobalFunc();
        GeneralFunction g = new GeneralFunction();

        public UCTicketing()
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
        public void GetTicket(string search)
        {
            var dataMenu = f.GetTicket(search);
            ImageList il = new ImageList();
            int count = 0;
            ListMenu.Clear();
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
                    //throw ex;
                }
            }
            ListMenu.LargeImageList = il;
            foreach (var data in dataMenu)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = data.namaticket;
                lst.Name = data.IdTicket + "~" + data.namaticket + "~" + data.harga + "~" + data.Asuransi + "~" + data.NamaDiskon + "~" + data.Diskon;
                lst.ImageIndex = count++;
                ListMenu.Items.Add(lst);
            }

        }

        private void UCTicketing_Load(object sender, EventArgs e)
        {
            ListMenu.Dock = DockStyle.Fill;
            GetTicket(txtSearch.Text);
            tableLayoutPanel1.Hide();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

            }
            GetTicket(txtSearch.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {

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

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
            if (txtHargaTicket.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHargaTicket.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text);
                decimal d = g.ConvertToDecimal(txtAsuransi.Text);

                if (a > 0)
                {
                    decimal total = 0;
                    total = (a * b) - ((a * b) * c / 100);
                    total = total + (d * b);

                    txtTotal.Text = g.ConvertToRupiah(total);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
            if (txtHargaTicket.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHargaTicket.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text);
                decimal d = g.ConvertToDecimal(txtAsuransi.Text);

                if (a > 0)
                {
                    decimal total = 0;
                    total = (a * b) - ((a * b) * c / 100);
                    total = total + (d * b);

                    txtTotal.Text = g.ConvertToRupiah(total);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text != "")
            {
                decimal total = g.ConvertToDecimal(txtTotal.Text);
                decimal qty = g.ConvertToDecimal(txtQty.Text);
                if (qty > 0)
                {
                    if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
                    {
                        MenuKasir un = new MenuKasir();
                        un.Dock = DockStyle.Fill;
                        Main.Instance.PnlContainer.Controls.Add(un);
                    }
                    try
                    {
                        Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                        UserControl fc = tbx.Controls.Find("MenuKasir", true).FirstOrDefault() as UserControl;

                        if (fc != null)
                        {
                            DataGridView dt_grid = fc.Controls.Find("dt_grid", true).FirstOrDefault() as DataGridView;
                            if (dt_grid != null)
                            {
                                int IdTicket = 1;

                                foreach (DataGridViewRow row2 in dt_grid.Rows)
                                {
                                    if (row2.Cells[2].Value.ToString() == "TICKETING" && row2.Cells[3].Value.ToString().Contains("Nama Ticket") == true)
                                    {
                                        IdTicket++;
                                    }
                                }

                                string[] row = new string[] { "x", 
                                    IdTicket.ToString()+"-"+txtTicket.Text,
                                    "TICKETING",
                                    "Nama Ticket : "+txtNamaTicket.Text,
                                    g.ConvertToNumber(g.ConvertToDecimal(txtHargaTicket.Text)), 
                                    txtQty.Text, 
                                    g.ConvertToNumber(g.ConvertToDecimal(txtHargaTicket.Text)*g.ConvertToDecimal(txtQty.Text))};
                                dt_grid.Rows.Add(row);

                                decimal diskon = g.ConvertToDecimal(txtHargaTicket.Text) * g.ConvertToDecimal(txtDiskon.Text.Replace("%", "")) / 100;
                                if (diskon > 0)
                                {
                                    row = new string[] { "x", IdTicket.ToString()+"-"+txtTicket.Text,"TICKETING",
                                    "Diskon "+txtNamaDiskon.Text ,
                                    g.ConvertToNumber(diskon), txtQty.Text, "-"+ g.ConvertToNumber(diskon*(g.ConvertToDecimal(txtQty.Text)))};
                                    dt_grid.Rows.Add(row);
                                }

                                if (g.ConvertToDecimal(txtAsuransi.Text) > 0)
                                {
                                    row = new string[] { "x", IdTicket.ToString()+"-"+txtTicket.Text,"TICKETING",
                                    "Asuransi / Qty" ,
                                    g.ConvertToNumber(g.ConvertToDecimal(txtAsuransi.Text)), txtQty.Text, g.ConvertToNumber(g.ConvertToDecimal(txtAsuransi.Text)*(g.ConvertToDecimal(txtQty.Text)))};
                                    dt_grid.Rows.Add(row);
                                }


                            }

                        }
                        Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    ClearInput();
                }

            }
        }

        public void ClearInput()
        {
            txtAsuransi.Clear();
            txtTicket.Clear();
            txtNamaTicket.Clear();
            txtDiskon.Clear();
            txtHargaTicket.Clear();
            txtQty.Clear();
            txtSearch.Clear();
            txtTotal.Clear();

        }

        private void ListMenu_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            var data = ListMenu.SelectedItems[0];
            if (data.Name.Contains("~") == true)
            {
                var d = data.Name.Split('~');
                txtTicket.Text = d[0];
                txtNamaTicket.Text = d[1];
                txtHargaTicket.Text = g.ConvertToRupiah(g.ConvertToDecimal(d[2]));
                txtAsuransi.Text = g.ConvertToRupiah(g.ConvertToDecimal(d[3]));
                txtNamaDiskon.Text = d[4];
                txtDiskon.Text = g.ConvertToNumber(g.ConvertToDecimal(d[5])) + "%";
                if (txtQty.Text != "0" && txtQty.Text != "")
                {
                    decimal a = g.ConvertToDecimal(txtHargaTicket.Text);
                    decimal b = g.ConvertToDecimal(txtQty.Text);
                    decimal c = g.ConvertToDecimal(txtDiskon.Text.Replace("%", ""));
                    decimal dd = g.ConvertToDecimal(txtAsuransi.Text);
                    decimal totalSebelumdiskonnAsuransi = (a * b);
                    decimal totalDiskon = totalSebelumdiskonnAsuransi * c / 100;
                    decimal total = (totalSebelumdiskonnAsuransi - totalDiskon) + (dd * b);
                    txtTotalDiskon.Text = g.ConvertToRupiah(totalDiskon);
                    txtTotal.Text = g.ConvertToRupiah(total);
                }
            }
        }

        private void txtHargaTicket_TextChanged(object sender, EventArgs e)
        {
            if (txtHargaTicket.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHargaTicket.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text.Replace("%", ""));
                decimal d = g.ConvertToDecimal(txtAsuransi.Text);
                decimal totalSebelumdiskonnAsuransi = (a * b);
                decimal totalDiskon = totalSebelumdiskonnAsuransi * c / 100;
                decimal total = (totalSebelumdiskonnAsuransi - totalDiskon) + (d * b);
                txtTotalDiskon.Text = g.ConvertToRupiah(totalDiskon);
                txtTotal.Text = g.ConvertToRupiah(total);
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtHargaTicket.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHargaTicket.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text.Replace("%", ""));
                decimal d = g.ConvertToDecimal(txtAsuransi.Text);
                decimal totalSebelumdiskonnAsuransi = (a * b);
                decimal totalDiskon = totalSebelumdiskonnAsuransi * c / 100;
                decimal total = (totalSebelumdiskonnAsuransi - totalDiskon) + (d * b);
                txtTotalDiskon.Text = g.ConvertToRupiah(totalDiskon);
                txtTotal.Text = g.ConvertToRupiah(total);
            }
        }

        private void ListMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtAsuransi.Clear();
            txtDiskon.Clear();
            txtHargaTicket.Clear();
            txtNamaDiskon.Clear();
            txtNamaTicket.Clear();
            txtQty.Clear();
            txtSearch.Clear();
            txtTicket.Clear();
            txtTotal.Clear();
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
        }

        private void txtSearch_Click_1(object sender, EventArgs e)
        {
            tableLayoutPanel1.Show();
        }

        private void txtSearch_MouseLeave(object sender, EventArgs e)
        {
            //tableLayoutPanel1.Hide();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            //tableLayoutPanel1.Hide();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        public void input_keyPad2(string key, string Object)
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

        private void button54_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button52_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button55_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button57_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button58_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button46_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button45_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button48_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button43_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetTicket(txtSearch.Text);
        }
    }
}
