using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCDashboard : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();
        Sales s = new Sales();

        public UCDashboard()
        {
            InitializeComponent();
        }

        private void UCDashboard_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            //this.MinimumSize = new Size(this.Width, this.Height);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            var data = s.ReloadDashboard(f.GetComputerName(), f.GetNamaUser(General.IDUser));
            txtNewCardCount.Text = data.QtyCard.ToString();
            txtKartuBaru.Text = g.ConvertToRupiah(data.TotalAmountCard);
            txtAsuransi.Text = data.QtyAsuransi.ToString();
            txtTotalAsuransi.Text = g.ConvertToRupiah(data.TotalAmountAsuransi);

            txtTicketCount.Text = data.QtyTicket.ToString();
            TotalTicketing.Text = g.ConvertToRupiah(data.TotalAmountTicket);

            txtTopup.Text = data.QtyTopup.ToString();
            txtTotalTopup.Text = g.ConvertToRupiah(data.TotalAmountTopup);
            txtQtymotorParkir.Text = data.QtyMotorParkir.ToString();
            txtQtyMobilParkir.Text = data.QtyMobilParkir.ToString();
            txtTotalParkir.Text = g.ConvertToRupiah(data.TotalAmountParkir);
            txtFoodcourt.Text = data.QtyFoodCourt.ToString();
            txtTotalFoodcourt.Text = g.ConvertToRupiah(data.TotalAmountFoodCourt);

            txtQtyTrx.Text = (data.QtyCard + data.QtyAsuransi + data.QtyTicket +
                data.QtyTopup + data.QtyMotorParkir + data.QtyMobilParkir + data.QtyFoodCourt).ToString();

            txtTunai.Text = g.ConvertToRupiah(data.Tunai);
            txtEDC.Text = g.ConvertToRupiah(data.EDC);
            txtEmoney.Text = g.ConvertToRupiah(data.Emoney);

            txtDanaModal.Text = g.ConvertToRupiah(data.DanaModal);
            txtPenjualan.Text = g.ConvertToRupiah(data.Tunai + data.EDC + data.Emoney);
        }
    }
}
