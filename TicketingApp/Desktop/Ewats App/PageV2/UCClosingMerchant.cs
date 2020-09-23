using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCClosingMerchant : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();
        Sales s = new Sales();

        ACR_NFC NFC = new ACR_NFC();

        public UCClosingMerchant()
        {
            InitializeComponent();
        }

        private void UCClosingMerchant_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            refresh();
        }
        private void refresh()
        {
            var data = s.ReloadDashboard(f.GetComputerName(), f.GetNamaUser(General.IDUser));

            txtQtyTrx.Text = (data.QtyCard + data.QtyAsuransi + data.QtyTicket +
                data.QtyTopup + data.QtyMotorParkir + data.QtyMobilParkir + data.QtyFoodCourt).ToString();

            txtTunai.Text = g.ConvertToRupiah(data.Tunai);
            txtEDC.Text = g.ConvertToRupiah(data.EDC);
            txtEmoney.Text = g.ConvertToRupiah(data.Emoney);

            txtDanaModal.Text = g.ConvertToRupiah(data.DanaModal);
            txtPenjualan.Text = g.ConvertToRupiah(data.Tunai + data.EDC + data.Emoney);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
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
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
            {
                UCScanKartu un = new UCScanKartu();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }

            try
            {
                Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            input_keyPad((sender as Button).Text, "txtInputClosing");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
            {
                UCScanKartu un = new UCScanKartu();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }

            try
            {
                Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (txtInputClosing.Text != "")
            {
                decimal sisa = g.ConvertToDecimal(txtCashinCashDrawer.Text) - g.ConvertToDecimal(txtInputClosing.Text);
                if (sisa == 0)
                {
                ulang:
                    var save = f.SaveClosing_V2("SP_SaveClosing", f.GetComputerName(), f.GetNamaUser(General.IDUser), g.ConvertToDecimal(txtInputClosing.Text));
                    if (save.Success == true)
                    {

                        if (save.Message.Contains("LogId") == true)
                        {
                            string LogId = save.Message.Split(',')[1].Trim().Split(':')[1].Trim();
                            //var print = ClosingPrintV2(LogId);
                            /*
                            if (print.Success == true)
                            {
                                var printFoodCourt = ClosingPrintFoodCourt(ComputerName, NamaUser, LogId, dataTenant);
                                if (print.Success == true)
                                {
                                    this.Close();
                                    Form fc2 = Application.OpenForms["Main"];
                                    if (fc2 != null)
                                    {
                                        fc2.Close();
                                    }
                                    f.PageControl("Login");
                                }
                                else
                                {
                                    var res3 = MessageBox.Show("Print Closing FoodCourt Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                    if (res3 == DialogResult.Retry)
                                    {
                                        goto PrintLagi1;
                                    }
                                }
                            }
                            else
                            {
                                var res3 = MessageBox.Show("Print Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                if (res3 == DialogResult.Retry)
                                {
                                    goto PrintLagi1;
                                }
                            }
                            */
                        }
                        else
                        {
                            var res3 = MessageBox.Show("Save Closing Gagal", "SaveClosing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            if (res3 == DialogResult.Retry)
                            {
                                goto ulang;
                            }
                        }
                    }
                    else
                    {
                        var res3 = MessageBox.Show("Save Closing Gagal", "SaveClosing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        if (res3 == DialogResult.Retry)
                        {
                            goto ulang;
                        }
                    }
                }
                else
                {

                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }
    }
}
