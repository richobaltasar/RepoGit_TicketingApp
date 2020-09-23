using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCFoodCourt : UserControl
    {
        Function.GlobalFunc f = new Function.GlobalFunc();
        public UCFoodCourt()
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

        private void UCFoodCourt_Load(object sender, EventArgs e)
        {
            GetTenant(txtSearch.Text);
            tableLayoutPanel1.Hide();
        }

        public void GetTenant(string search)
        {
            var dataMenu = f.GetDataTenant(search);
            ImageList il = new ImageList();
            int count = 0;
            ListTenant.Clear();
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
            ListTenant.LargeImageList = il;
            foreach (var data in dataMenu)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = data.NamaTenant;
                lst.Name = data.Id + "~" + data.NamaTenant;
                lst.ImageIndex = count++;
                ListTenant.Items.Add(lst);
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

            }
            GetTenant(txtSearch.Text);
        }

        private void ListTenant_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();

            var data = ListTenant.SelectedItems[0];
            if (data.Name != null)
            {
                var action = data.Name.Split('~');
                if (action[0] != null)
                {
                    txtSearch.Text = "";

                    if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCListMenuFoodCourt"))
                    {
                        UCListMenuFoodCourt un = new UCListMenuFoodCourt();
                        un.Dock = DockStyle.Fill;
                        Main.Instance.PnlContainer.Controls.Add(un);
                    }
                    try
                    {
                        Main.Instance.PnlContainer.Controls["UCListMenuFoodCourt"].BringToFront();

                        Panel tbx = Main.Instance.Controls.Find("PagePanel", true).FirstOrDefault() as Panel;
                        UserControl fc = tbx.Controls.Find("UCListMenuFoodCourt", true).FirstOrDefault() as UserControl;

                        if (fc != null)
                        {
                            Label lblIDTenant = fc.Controls.Find("lblIDTenant", true).FirstOrDefault() as Label;
                            if (lblIDTenant != null)
                            {
                                lblIDTenant.Text = action[0];
                            }

                            Label lblNamaTenant = fc.Controls.Find("lblNamaTenant", true).FirstOrDefault() as Label;
                            if (lblNamaTenant != null)
                            {
                                lblNamaTenant.Text = action[1];
                            }

                            ListView ListMenu = fc.Controls.Find("ListMenu", true).FirstOrDefault() as ListView;
                            if (ListMenu != null)
                            {
                                var dataMenu = f.GetBarangSearchMenu(action[1], "");
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
                                foreach (var d in dataMenu)
                                {
                                    ListViewItem lst = new ListViewItem();
                                    lst.Text = d.NamaBarang;
                                    lst.Name = d.IdMenu + "~" + d.NamaBarang + "~" + d.Harga + "~" + d.Stok;
                                    lst.ImageIndex = count++;
                                    ListMenu.Items.Add(lst);
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

        private void txtSearch_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Show();
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

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button16_Click(object sender, EventArgs e)
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

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button10_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad2((sender as Button).Text, "txtSearch");
        }

        private void button3_Click(object sender, EventArgs e)
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
            GetTenant(txtSearch.Text);
        }
    }
}
