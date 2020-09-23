using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCRefund : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        ACR_NFC NFC = new ACR_NFC();
        Sales s = new Sales();
        GlobalFunc f = new GlobalFunc();

        public UCRefund()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("MenuKasir"))
            {
                MenuKasir un = new MenuKasir();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["MenuKasir"].BringToFront();
        }

        private void UCRefund_Load(object sender, EventArgs e)
        {
            panelRefund.Location = new Point((this.Width - panelRefund.Width) / 2, (this.Height - panelRefund.Height) / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (g.ConvertToDecimal(txtTotalRefund.Text) > 0)
            {
                var card = NFC.ReadCardDataKey();
                if (card != null)
                {
                    if (card.IdCard != null)
                    {
                        string AccountNum = card.IdCard + "-" + card.CodeId;
                        var data = s.GetDataAccount(AccountNum);
                        if (data != null)
                        {
                            decimal TotalRefund = g.ConvertToDecimal(data.UangJaminan) + g.ConvertToDecimal(data.Balanced);
                            if (TotalRefund > 0)
                            {
                                var SaldoEmoneyAfter = NFC.UpdateBlok("04", "04", "0");
                                var SaldoJaminanAfter = NFC.UpdateBlok("05", "04", "0");
                                var TicketWeekDayAfter = NFC.UpdateBlok("06", "04", "0");
                                var TicketWeekEndAfter = NFC.UpdateBlok("08", "08", "0");
                                var CodeId = NFC.UpdateBlok("09", "08", "");

                                if (SaldoEmoneyAfter.Success == true &&
                                    SaldoJaminanAfter.Success == true &&
                                    TicketWeekDayAfter.Success == true &&
                                    TicketWeekEndAfter.Success == true &&
                                    CodeId.Success == true)
                                {
                                    var dataRefund = new SaveRefundCashV2();
                                    dataRefund.AccountNumber = AccountNum;
                                    dataRefund.ChasierBy = f.GetNamaUser(General.IDUser);
                                    dataRefund.ComputerName = f.GetComputerName();
                                    dataRefund.SaldoEmoney = g.ConvertToDecimal(data.Balanced);
                                    dataRefund.SaldoJaminan = g.ConvertToDecimal(data.UangJaminan);
                                    dataRefund.TicketWeekDay = g.ConvertToDecimal(data.Ticket);
                                    dataRefund.TicketWeekEnd = 0;
                                    dataRefund.TotalNominalRefund = TotalRefund;
                                    var save = s.SaveTransaksiRefund(dataRefund);
                                    if (save.status == "SUCCESS")
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
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
        }

        private void panelRefund_Paint(object sender, PaintEventArgs e)
        {
            panelRefund.Location = new Point(ClientSize.Width / 2 - panelRefund.Size.Width / 2, ClientSize.Height / 2 - panelRefund.Size.Height / 2);
            panelRefund.Anchor = AnchorStyles.None;
        }
    }
}
