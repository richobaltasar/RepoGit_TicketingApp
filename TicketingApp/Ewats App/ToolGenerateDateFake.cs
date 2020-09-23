using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App
{
    public partial class ToolGenerateDateFake : Form
    {
        Function.GlobalFunc f = new Function.GlobalFunc();
        Function.DataFakeFunction ff = new Function.DataFakeFunction();

        public int Counter;
        public int durasi;
        public int SatuanJam = 3600;
        DateTime dt = new DateTime();
        public ToolGenerateDateFake()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Generate")
            {
                backgroundWorker1.RunWorkerAsync();
                panel2.BringToFront();
                setFake.Enabled = false;
                SetTanggal.Enabled = false;
                button1.Text = "Stop";
                this.Height = 499 - panel3.Height;
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(workingArea.Right - Size.Width,
                                          workingArea.Bottom - Size.Height);
            }
            else
            {
                backgroundWorker1.CancelAsync();
                this.Height = 499;
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(workingArea.Right - Size.Width,
                                          workingArea.Bottom - Size.Height);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            pictureBox1.Visible = false;
        }

        private void ToolGenerateDateFake_Load(object sender, EventArgs e)
        {
            check_running();
            CounterDown.Stop();
            setFake2.Focus();
            panel1.Location = new Point(0, 0);
            panel2.Location = new Point(0, 0);
            panel5.Location = new Point(0, 0);
            panel1.BringToFront();
            textBox3.Text = "........................  ???";
            this.Size = new Size(234, 499);
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void ToolGenerateDateFake_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string Tanggal = SetTanggal.Value.ToString("yyyyMMdd");
            if (setFake.Text == "")
            {
                backgroundWorker1.CancelAsync();

            }
            else
            {
                ff.GenarateDataFake(Tanggal, setFake.Text);
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                if (setFake.Text == "")
                {
                    textBox1.Text = "Persante Data Masih kosong";
                }
                else
                {
                    textBox1.Text = "Silahkan tekan Generate untuk memulai Permainan";
                }

                panel1.BringToFront();
                button1.Text = "Generate";
                this.Height = 499;
            }
            else
            {
                panel1.BringToFront();
                button1.Text = "Generate";
                setFake.Enabled = true;
                SetTanggal.Enabled = true;
                this.Height = 499;
            }
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void setFake_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (f.ConvertDecimal(setFake.Text) >= 100)
            {
                setFake.Text = "100";
            }
            else
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

        }

        private void setFake_TextChanged(object sender, EventArgs e)
        {
            if (f.ConvertDecimal(setFake.Text) >= 100)
            {
                setFake.Text = "100";
            }

        }
        public void check_running()
        {
            try
            {
                var processExists = Process.GetProcesses().Any(p => p.ProcessName.Contains("kiosk"));

                if (processExists == true)
                {
                    var data = Process.GetProcessesByName("Ewats App");
                    int count = 0;
                    if (data.Count() > 1)
                    {
                        foreach (Process proc in Process.GetProcessesByName("Ewats App"))
                        {
                            if (count < data.Count() - 1)
                            {
                                proc.Kill();
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox3.Text = "........................  ???";
            if (button2.Text == "Auto Generate")
            {
                if (setPeriodik.Text != "" && setFake2.Text != "")
                {
                    if (f.ConvertDecimal(setPeriodik.Text) > 0 && f.ConvertDecimal(setFake2.Text) > 0)
                    {
                        setPeriodik.Enabled = false;
                        setFake2.Enabled = false;
                        button2.Text = "Stop";
                        CounterDown.Start();
                        durasi = Convert.ToInt16(setPeriodik.Text);

                        Counter = durasi * SatuanJam;
                        panel3.Top = panel1.Height;
                        panel3.BringToFront();
                        panel5.BringToFront();
                        this.Height = 499 - panel4.Height;
                        Rectangle workingArea = Screen.GetWorkingArea(this);
                        this.Location = new Point(workingArea.Right - Size.Width,
                                                  workingArea.Bottom - Size.Height);
                    }
                }
            }
            else
            {
                WorkerAutoGenerate.CancelAsync();
                CounterDown.Stop();
                panel3.Top = panel1.Height + panel4.Height;
                panel3.BringToFront();
                button2.Text = "Auto Generate";
                Counter = durasi * SatuanJam;
                txtCountDown.Text = dt.AddSeconds(Counter).ToString("HH:mm:ss");
                this.Height = 499;
                setPeriodik.Enabled = true;
                setFake2.Enabled = true;
                panel1.BringToFront();
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(workingArea.Right - Size.Width,
                                          workingArea.Bottom - Size.Height);
            }

        }

        private void WorkerAutoGenerate_DoWork(object sender, DoWorkEventArgs e)
        {
            var now = DateTime.Now;
            string Datetime = f.GetDatetimeFake();
            var Tanggal = new DateTime(now.Year, now.Month, 1).ToShortDateString();
            ff.GenarateDataFake(Datetime, setFake2.Text);
        }

        private void WorkerAutoGenerate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                setPeriodik.Enabled = true;
                setFake2.Enabled = true;
                button2.Text = "Auto Generate";
                CounterDown.Start();
                Counter = durasi * SatuanJam;
            }
            else
            {
                CounterDown.Start();
                Counter = durasi * SatuanJam;
            }
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (f.ConvertDecimal(setPeriodik.Text) >= 24)
            {
                txtCountDown.Text = "24";
            }
            else
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void counter_Tick(object sender, EventArgs e)
        {
            Counter--;
            txtCountDown.Text = dt.AddSeconds(Counter).ToString("HH:mm:ss");
            textBox3.Text = "santuy aja nunggu timer : " + txtCountDown.Text;
            string c = txtCountDown.Text.Split('.')[0];
            panel5.BringToFront();
            if (Counter == 0)
            {
                CounterDown.Stop();
                setPeriodik.Enabled = false;
                setFake2.Enabled = false;
                panel2.BringToFront();
                WorkerAutoGenerate.RunWorkerAsync();
                button2.Text = "Stop";
            }

        }

        private void setPeriodik_TextChanged(object sender, EventArgs e)
        {
            if (f.ConvertDecimal(setPeriodik.Text) >= 24)
            {
                setPeriodik.Text = "24";
            }
        }

        private void setFake2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (f.ConvertDecimal(setFake2.Text) >= 100)
            {
                setFake2.Text = "100";
            }
            else
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void setFake2_TextChanged(object sender, EventArgs e)
        {
            if (f.ConvertDecimal(setFake2.Text) >= 100)
            {
                setFake2.Text = "100";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCountDown_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
