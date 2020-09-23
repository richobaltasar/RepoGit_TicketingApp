using SharedCode;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCListMenuFoodCourt : UserControl
    {
        static UCListMenuFoodCourt _obj;

        public static UCListMenuFoodCourt Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new UCListMenuFoodCourt();
                }
                return _obj;
            }
        }

        public Label LabelIDTenant
        {
            get { return lblIDTenant; }
            set { lblIDTenant = value; }
        }


        Function.GlobalFunc f = new Function.GlobalFunc();
        GeneralFunction g = new GeneralFunction();

        public UCListMenuFoodCourt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCFoodCourt"))
            {
                UCFoodCourt un = new UCFoodCourt();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["UCFoodCourt"].BringToFront();

            ClearInput();
        }


        public void ClearInput()
        {
            txtDiskon.Clear();
            txtHarga.Clear();
            txtIdMenu.Clear();
            txtNamaMenu.Clear();
            txtQty.Clear();
            txtSearch.Clear();
            txtTotal.Clear();
        }
        private void UCListMenuFoodCourt_Load(object sender, EventArgs e)
        {
            GetMenuFoodCourt(lblNamaTenant.Text, txtSearch.Text);
            tableLayoutPanel1.Hide();

        }

        public void GetMenuFoodCourt(string IdTenant, string search)
        {
            var dataMenu = f.GetBarangSearchMenu(IdTenant, search);
            ImageList il = new ImageList();
            int count = 0;
            ListMenu.Clear();
            foreach (var img in dataMenu)
            {
                try
                {
                    System.Net.WebRequest request = System.Net.WebRequest.Create(img.LinkPic);
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
                lst.Text = data.NamaBarang;
                lst.Name = data.IdMenu + "~" + data.NamaBarang + "~" + data.Harga + "~" + data.Stok;
                lst.ImageIndex = count++;
                ListMenu.Items.Add(lst);
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

            }
            GetMenuFoodCourt(lblNamaTenant.Text, txtSearch.Text);
        }


        private void txtSearch_Click(object sender, EventArgs e)
        {
            f.ShowOnScreenKeyboard();
        }

        private void ListMenu_Click(object sender, EventArgs e)
        {
            var data = ListMenu.SelectedItems[0];
            if (data.Name.Contains("~") == true)
            {
                var d = data.Name.Split('~');
                txtIdMenu.Text = lblIDTenant.Text + "-" + d[0];
                txtNamaMenu.Text = d[1];
                txtHarga.Text = g.ConvertToRupiah(g.ConvertToDecimal(d[2]));
                txtDiskon.Text = "0";
            }
            tableLayoutPanel1.Hide();
            //txtSearch.Clear();
        }

        public void input_keyPad(string key, string Object)
        {
            TextBox txt = this.Controls.Find(Object, true).FirstOrDefault() as TextBox;
            if (txt != null)
            {
                if (key == "<-")
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
                else if (key == "Reset")
                {
                    txt.Text = "0";
                }
                else if (key == "Enter")
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

        private void txtHarga_TextChanged(object sender, EventArgs e)
        {
            if (txtHarga.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHarga.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text);
                decimal total = 0;
                total = (a * b) - ((a * b) * c / 100);
                txtTotal.Text = g.ConvertToRupiah(total);
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtHarga.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHarga.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text);
                decimal total = 0;
                total = (a * b) - ((a * b) * c / 100);
                txtTotal.Text = g.ConvertToRupiah(total);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
            if (txtHarga.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHarga.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text);
                decimal total = 0;
                total = (a * b) - ((a * b) * c / 100);
                txtTotal.Text = g.ConvertToRupiah(total);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");
            if (txtHarga.Text != "")
            {
                decimal a = g.ConvertToDecimal(txtHarga.Text);
                decimal b = g.ConvertToDecimal(txtQty.Text);
                decimal c = g.ConvertToDecimal(txtDiskon.Text);
                decimal total = 0;
                total = (a * b) - ((a * b) * c / 100);
                txtTotal.Text = g.ConvertToRupiah(total);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtQty");

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
                                string[] row = new string[] { "x", txtIdMenu.Text,"FOODCOURT",
                                    txtNamaMenu.Text,
                                    g.ConvertToNumber(g.ConvertToDecimal(txtHarga.Text)),
                                    txtQty.Text,
                                    g.ConvertToNumber(g.ConvertToDecimal(txtHarga.Text)*g.ConvertToDecimal(txtQty.Text))};
                                dt_grid.Rows.Add(row);

                                decimal diskon = g.ConvertToDecimal(txtHarga.Text) * g.ConvertToDecimal(txtDiskon.Text) / 100;
                                if (diskon > 0)
                                {
                                    row = new string[] { "x", txtIdMenu.Text,"FOODCOURT",
                                    "Diskon" ,
                                    g.ConvertToNumber(diskon), txtQty.Text, "-"+ g.ConvertToNumber(diskon*(g.ConvertToDecimal(txtQty.Text)))};
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetMenuFoodCourt(lblNamaTenant.Text, txtSearch.Text);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
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

        private void button29_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button30_Click(object sender, EventArgs e)
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

        private void button28_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button25_Click(object sender, EventArgs e)
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

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button18_Click(object sender, EventArgs e)
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

        private void txtSearch_Click_1(object sender, EventArgs e)
        {
            tableLayoutPanel1.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GetMenuFoodCourt(lblNamaTenant.Text, txtSearch.Text);
            tableLayoutPanel1.Hide();
            txtSearch.Clear();
        }
    }
}
