using SharedCode;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class UCOpeningGateParkir : UserControl
    {
        Ewats_App.Function.GlobalFunc f = new Function.GlobalFunc();
        GeneralFunction g = new GeneralFunction();
        Sales s = new Sales();
        static UCOpeningGateParkir _obj;
        public int countTimer = 30;

        public static UCOpeningGateParkir Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new UCOpeningGateParkir();
                }
                return _obj;
            }
        }

        public Timer TimerOpen
        {
            get { return TimerOpening; }
            set { TimerOpening = value; }
        }



        public UCOpeningGateParkir()
        {
            InitializeComponent();
        }

        private void TimerOpening_Tick(object sender, EventArgs e)
        {
            countTimer--;
            lblTimerOpening.Text = countTimer.ToString();
            if (countTimer == 0)
            {
                TimerOpening.Stop();
                if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
                {
                    UCScanKartu un = new UCScanKartu();
                    un.Dock = DockStyle.Fill;
                    Main.Instance.PnlContainer.Controls.Add(un);
                }
                Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
            }

        }

        private void panelOpening_Paint(object sender, PaintEventArgs e)
        {
            panelOpening.Location = new Point(ClientSize.Width / 2 - panelOpening.Size.Width / 2, ClientSize.Height / 2 - panelOpening.Size.Height / 2);
            panelOpening.Anchor = AnchorStyles.None;
        }

        private void UCOpeningGateParkir_Load(object sender, EventArgs e)
        {

        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            TimerOpening.Stop();
            if (!Main.Instance.PnlContainer.Controls.ContainsKey("UCScanKartu"))
            {
                UCScanKartu un = new UCScanKartu();
                un.Dock = DockStyle.Fill;
                Main.Instance.PnlContainer.Controls.Add(un);
            }
            Main.Instance.PnlContainer.Controls["UCScanKartu"].BringToFront();
        }
    }
}
