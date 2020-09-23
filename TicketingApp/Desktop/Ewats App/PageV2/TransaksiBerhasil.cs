using Ewats_App.Function;
using SharedCode;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ewats_App.PageV2
{
    public partial class TransaksiBerhasil : UserControl
    {
        GeneralFunction g = new GeneralFunction();
        GlobalFunc f = new GlobalFunc();
        Sales s = new Sales();

        public TransaksiBerhasil()
        {
            InitializeComponent();
        }

        private void TransaksiBerhasil_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
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

        private static String InsertTableInRichTextBox(DataTable dtbl, int width)
        {
            //Since too much string appending go for string builder
            StringBuilder sringTableRtf = new StringBuilder();

            //beginning of rich text format,dont customize this begining line
            sringTableRtf.Append(@"{\rtf1 ");

            //create 5 rows with 3 cells each
            int cellWidth;

            //Start the Row
            sringTableRtf.Append(@"\trowd");

            //Populate the Table header from DataTable column headings.
            for (int j = 0; j < dtbl.Columns.Count; j++)
            {
                //A cell with width 1000.
                sringTableRtf.Append(@"\cellx" + ((j + 1) * width).ToString());

                if (j == 0)
                    sringTableRtf.Append(@"\intbl  " + dtbl.Columns[j].ColumnName);
                else
                    sringTableRtf.Append(@"\cell   " + dtbl.Columns[j].ColumnName);
            }

            //Add the table header row
            sringTableRtf.Append(@"\intbl \cell \row");

            //Loop to populate the table cell data from DataTable
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                //Start the Row
                sringTableRtf.Append(@"\trowd");

                for (int j = 0; j < dtbl.Columns.Count; j++)
                {
                    cellWidth = (j + 1) * width;

                    //A cell with width 1000.
                    sringTableRtf.Append(@"\cellx" + cellWidth.ToString());

                    if (j == 0)
                        sringTableRtf.Append(@"\intbl  " + dtbl.Rows[i][j].ToString());
                    else
                        sringTableRtf.Append(@"\cell   " + dtbl.Rows[i][j].ToString());
                }

                //Insert data row
                sringTableRtf.Append(@"\intbl \cell \row");
            }

            sringTableRtf.Append(@"\pard");
            sringTableRtf.Append(@"}");

            //convert the string builder to string
            return sringTableRtf.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //var ListPrint = s.GetLogTransaksiListDetailPOS("84");
            ExtendedRichTextBox advRichTextBox = new ExtendedRichTextBox();
            advRichTextBox.Size = rtResulPrint.Size;
            advRichTextBox.Location = rtResulPrint.Location;
            advRichTextBox.ScrollBars = RichTextBoxScrollBars.Both;
            advRichTextBox.Rtf = f.DisplayRTFTransaksi("84");

            //Add the programatically created richtextbox to the form's control collection.
            //Else it won't be displayed in form.
            this.panelNominal.Controls.Add(advRichTextBox);

            //Bring the control to top level,else will be appearing in back.
            advRichTextBox.BringToFront();
        }

        private void panelScan_Paint(object sender, PaintEventArgs e)
        {
            panelScan.Location = new Point(ClientSize.Width / 2 - panelScan.Size.Width / 2, ClientSize.Height / 2 - panelScan.Size.Height / 2);
            panelScan.Anchor = AnchorStyles.None;
        }
    }
}
