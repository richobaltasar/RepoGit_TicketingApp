using Ewats_App.Function;
using SharedCode;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.Page
{
    public partial class CancelTransaksi : Form
    {
        GlobalFunc f = new GlobalFunc();
        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        string readername = "ACS ACR122 0";
        public Card.SCARD_READERSTATE RdrState;
        public Card.SCARD_IO_REQUEST pioSendRequest;

        public ReaderCard Carddata { set; get; }

        public CancelTransaksi()
        {
            InitializeComponent();
        }

        private void CancelTransaksi_Load(object sender, EventArgs e)
        {
            if (IdTransaction.Text != "")
            {
                if (IdTransaction.Text.Contains("FOODCOURT") == true)
                {
                    var res = f.GetDataTrxFoodCourt(IdTransaction.Text.Replace("FOODCOURT", ""));
                    txtTipeTransaksi.Text = "FOODCOURT";
                    txtPaymentMethod.Text = res.PaymentMethod;
                    txtTotalTransaksi.Text = res.TotalBelanja;


                    if (res.PaymentMethod == "eMoney")
                    {
                        txtAccountNumber.Text = res.AccountNumber;
                        PakeKartu.Visible = true;
                        var result = GetDataKartu();

                    }
                    else
                    {
                        PakeKartu.Visible = false;
                    }
                }
                else if (IdTransaction.Text.Contains("TOPUP") == true)
                {
                    var res = f.getDataTransaksiTopupCancel(IdTransaction.Text);
                    txtPaymentMethod.Text = res.PaymentMethod;
                    txtTipeTransaksi.Text = res.TipeTransaksi;
                    txtAccountNumber.Text = res.AccountNumber;
                    txtTotalTransaksi.Text = f.ConvertToRupiah(f.ConvertDecimal(res.TotalTransaksi));
                    PakeKartu.Visible = true;
                    var result = GetDataKartu();
                }
                else if (IdTransaction.Text.Contains("REG") == true)
                {
                    var res = f.GetDataTransaksiRegistrasiCancel(IdTransaction.Text);
                    txtPaymentMethod.Text = res.JenisTransaksi;
                    txtTipeTransaksi.Text = "REGISTRASI";
                    txtTotalTransaksi.Text = f.ConvertToRupiah(res.TotalBayar);
                    txtAccountNumber.Text = res.AccountNumber;
                    PakeKartu.Visible = true;
                    var result = GetDataKartu();

                }
                else
                {
                    f.ShowMessagebox("Sorry ID Transaction not found", "Sorry", MessageBoxButtons.OK);
                    this.Close();
                }
            }
            else
            {
                f.ShowMessagebox("Sorry ID Transaction not found", "Sorry", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button19_Click(object sender, EventArgs e)
        {
            GetDataKartu();
        }

        private ReaderCard GetDataKartu()
        {
            var Card = ReadCardDataKey();
            if (Card.Success == true)
            {
                f.UpdatAccountData(Card);
                if (Card.SaldoJaminan > 0)
                {
                    Carddata = Card;
                    TxtBacaKartu.Text = "======================";
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\nACCOUNT DETAIL";
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n======================";
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n Account Number : " + Card.IdCard;
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n Code ID \t\t: " + f.ConvertDecimal(Card.CodeId).ToString();
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n Saldo Emoney \t: Rp " + string.Format("{0:n0}", Card.SaldoEmoney);
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n Ticket WeekDay \t: " + string.Format("{0:n0}", Card.TicketWeekDay);
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n Ticket WeekEnd \t: " + string.Format("{0:n0}", Card.TicketWeekEnd);
                    TxtBacaKartu.Text = TxtBacaKartu.Text + "\n SaldoJaminan \t: Rp " + string.Format("{0:n0}", Card.SaldoJaminan);
                    if (ConfigurationFileStatic.VFDPort != null && ConfigurationFileStatic.VFDPort != "")
                    {
                        Function.VFDPort.send("Saldo Kartu Anda :", f.ConvertToRupiah(Card.SaldoEmoney), Function.VFDPort.sp.PortName);
                    }
                }
                else
                {
                    TxtBacaKartu.Text = "Gelang belum registrasi, silahkan melakukan registrasi Ticket";
                }
            }
            else
            {
                TxtBacaKartu.Text = "Gagal membaca data kartu";
            }
            return Card;
        }

        private void button4_Click(object sender, EventArgs e)
        {
        Submit:
            if (txtTipeTransaksi.Text == "FOODCOURT")
            {
                if (txtPaymentMethod.Text.ToUpper() == "EMONEY" || txtPaymentMethod.Text.ToUpper() == "CASH" || txtPaymentMethod.Text.ToUpper() == "EMONEY & CASH")
                {
                    var result = GetDataKartu();
                    if (result.Success == false)
                    {
                        var dialog = f.ShowMessagebox("Kartu tidak terdeteksi", "Sorry", MessageBoxButtons.RetryCancel);
                        if (dialog == DialogResult.Retry)
                        {
                            goto Submit;
                        }
                    }
                    else
                    {
                        string AkunNumber = result.IdCard + "-" + result.CodeId;
                        if (AkunNumber != txtAccountNumber.Text)
                        {
                            var dialog = f.ShowMessagebox("No Akun kartu tidak sama dengan No Akun transaksi ini", "Sorry", MessageBoxButtons.RetryCancel);
                            if (dialog == DialogResult.Retry)
                            {
                                goto Submit;
                            }
                        }
                        else
                        {
                            var ApakahOkeBuatCancel = f.CheckApakahAdaDataTrxFnB(IdTransaction.Text, AkunNumber);
                            if (ApakahOkeBuatCancel.RetCode > 0)
                            {
                                var dialog = f.ShowMessagebox(ApakahOkeBuatCancel.Message + " ?", "warning", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    var r = f.SaveCancelTransakasiFnB(f.GetNamaUser(LblAuthorizeBy.Text), f.GetNamaUser(General.IDUser), f.GetComputerName(), IdTransaction.Text, txtPaymentMethod.Text, txtTotalTransaksi.Text, result);
                                    if (r.RetCode == 1)
                                    {
                                    ulangTulisKartu:
                                        var d = r.Message.Split('~');
                                        var Res = new ResultSaveCancelTransakasiTicketModel();
                                        foreach (string rr in d)
                                        {
                                            var rrr = rr.Split(':');
                                            if (rrr[0] == "Id")
                                            {
                                                Res.Id = rrr[1];
                                            }
                                            else if (rrr[0] == "AccountNumber")
                                            {
                                                Res.AccountNumber = rrr[1];
                                            }
                                            else if (rrr[0] == "SaldoEmoney")
                                            {
                                                Res.SaldoEmoney = rrr[1];
                                            }
                                            else if (rrr[0] == "SaldoJaminan")
                                            {
                                                Res.SaldoJaminan = rrr[1];
                                            }
                                            else if (rrr[0] == "Ticket")
                                            {
                                                Res.Ticket = rrr[1];
                                            }
                                            else if (rrr[0] == "TipeTransaksi")
                                            {
                                                Res.TipeTransaksi = rrr[1];
                                            }
                                        }

                                        var DataKartu = GetDataKartu();
                                        string AkunKartu = DataKartu.IdCard + "-" + DataKartu.CodeId;
                                        if (AkunKartu == AkunNumber)
                                        {
                                        ulangSaldoEmoney:
                                            var SaldoEmoney = UpdateBlok("04", "04", Res.SaldoEmoney);
                                            if (SaldoEmoney.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Saldo eMoney Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var tiketWeekDay = UpdateBlok("05", "04", Res.Ticket);
                                            if (tiketWeekDay.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Ticket Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var JaminanSaldo = UpdateBlok("08", "08", Res.SaldoJaminan);
                                            if (tiketWeekDay.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Ticket Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var UpdateAccount = ReadCardDataKey();
                                            f.UpdatAccountData(UpdateAccount);
                                            string IdLog = Res.Id;
                                        PrintLagi:
                                            var print = PrintTicketCancel(IdLog, r.Message);
                                            if (print.Success == true)
                                            {
                                                f.RefreshDashboard();
                                                General.Page = "Registrasi";
                                                Print fp = new Print();
                                                fp.Show();
                                                fp.BringToFront();
                                                fp.StartPosition = FormStartPosition.CenterScreen;
                                                fp.txtMessageBox.Text = "Transaksi ID:" + IdTransaction.Text + " Berhasil dihapus";
                                                this.Close();
                                            }
                                            else
                                            {
                                                var res = MessageBox.Show("Print Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto PrintLagi;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var res = f.ShowMessagebox("Akun Kartu yang ditempelkan pada reader tidak sesuai, silahkan tempelkan kartu yang sesuai", "warning", MessageBoxButtons.RetryCancel);
                                            if (dialog == DialogResult.Retry)
                                            {
                                                goto ulangTulisKartu;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        f.ShowMessagebox(r.Message, "Warning", MessageBoxButtons.OK);
                                    }
                                }
                            }
                            else
                            {
                                var dialog = f.ShowMessagebox(ApakahOkeBuatCancel.Message, "warning", MessageBoxButtons.RetryCancel);
                                if (dialog == DialogResult.Retry)
                                {
                                    goto Submit;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txtPaymentMethod.Text.Contains("EDC") == true)
                    {
                        f.ShowMessagebox("Sorry, untuk transaksi EDC tidak bisa dicancel", "Sorry", MessageBoxButtons.OK);
                    }
                    else
                    {
                        f.ShowMessagebox("Sorry, Transaksi tidak dikenali", "Sorry", MessageBoxButtons.OK);
                    }
                }
            }
            else if (txtTipeTransaksi.Text == "REGISTRASI")
            {
                if (txtPaymentMethod.Text.ToUpper() == "EMONEY" || txtPaymentMethod.Text.ToUpper() == "CASH" || txtPaymentMethod.Text.ToUpper() == "EMONEY & CASH")
                {
                    var result = GetDataKartu();
                    if (result.Success == false)
                    {
                        var dialog = f.ShowMessagebox("Kartu tidak terdeteksi", "Sorry", MessageBoxButtons.RetryCancel);
                        if (dialog == DialogResult.Retry)
                        {
                            goto Submit;
                        }
                    }
                    else
                    {
                        string AkunNumber = result.IdCard + "-" + result.CodeId;
                        if (AkunNumber != txtAccountNumber.Text)
                        {
                            var dialog = f.ShowMessagebox("No Akun kartu tidak sama dengan No Akun transaksi ini", "Sorry", MessageBoxButtons.RetryCancel);
                            if (dialog == DialogResult.Retry)
                            {
                                goto Submit;
                            }
                        }
                        else
                        {
                            var apakahkartubaru = f.GetApakahiniKartubaru(IdTransaction.Text, AkunNumber);
                            if (apakahkartubaru.RetCode > 0)
                            {
                                var dialog = f.ShowMessagebox(apakahkartubaru.Message + " ?", "warning", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    var r = f.SaveCancelTransakasiTicket(f.GetNamaUser(LblAuthorizeBy.Text), f.GetNamaUser(General.IDUser), f.GetComputerName(), IdTransaction.Text, txtPaymentMethod.Text, txtTotalTransaksi.Text, result);
                                    if (r.RetCode == 1)
                                    {
                                    ulangTulisKartu:
                                        var d = r.Message.Split('~');
                                        var Res = new ResultSaveCancelTransakasiTicketModel();
                                        foreach (string rr in d)
                                        {
                                            var rrr = rr.Split(':');
                                            if (rrr[0] == "Id")
                                            {
                                                Res.Id = rrr[1];
                                            }
                                            else if (rrr[0] == "AccountNumber")
                                            {
                                                Res.AccountNumber = rrr[1];
                                            }
                                            else if (rrr[0] == "SaldoEmoney")
                                            {
                                                Res.SaldoEmoney = rrr[1];
                                            }
                                            else if (rrr[0] == "SaldoJaminan")
                                            {
                                                Res.SaldoJaminan = rrr[1];
                                            }
                                            else if (rrr[0] == "Ticket")
                                            {
                                                Res.Ticket = rrr[1];
                                            }
                                            else if (rrr[0] == "TipeTransaksi")
                                            {
                                                Res.TipeTransaksi = rrr[1];
                                            }
                                        }

                                        var DataKartu = GetDataKartu();
                                        string AkunKartu = DataKartu.IdCard + "-" + DataKartu.CodeId;
                                        if (AkunKartu == AkunNumber)
                                        {
                                        ulangSaldoEmoney:
                                            var SaldoEmoney = UpdateBlok("04", "04", Res.SaldoEmoney);
                                            if (SaldoEmoney.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Saldo eMoney Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var tiketWeekDay = UpdateBlok("05", "04", Res.Ticket);
                                            if (tiketWeekDay.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Ticket Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var JaminanSaldo = UpdateBlok("08", "08", Res.SaldoJaminan);
                                            if (tiketWeekDay.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Ticket Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            if (Res.TipeTransaksi == "TICKET+KARTU")
                                            {
                                                var CodeId = UpdateBlok("09", "08", "");
                                                if (CodeId.Success == false)
                                                {
                                                    var res = MessageBox.Show("Update Code ID Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                    if (res == DialogResult.Retry)
                                                    {
                                                        goto ulangSaldoEmoney;
                                                    }
                                                }
                                            }

                                            var UpdateAccount = ReadCardDataKey();
                                            f.UpdatAccountData(UpdateAccount);
                                            string IdLog = Res.Id;
                                        PrintLagi:
                                            var print = PrintTicketCancel(IdLog, r.Message);
                                            if (print.Success == true)
                                            {
                                                f.RefreshDashboard();
                                                General.Page = "Registrasi";
                                                Print fp = new Print();
                                                fp.Show();
                                                fp.BringToFront();
                                                fp.StartPosition = FormStartPosition.CenterScreen;
                                                fp.txtMessageBox.Text = "Transaksi ID:" + IdTransaction.Text + " Berhasil dihapus";
                                                this.Close();
                                            }
                                            else
                                            {
                                                var res = MessageBox.Show("Print Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto PrintLagi;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var res = f.ShowMessagebox("Akun Kartu yang ditempelkan pada reader tidak sesuai, silahkan tempelkan kartu yang sesuai", "warning", MessageBoxButtons.RetryCancel);
                                            if (dialog == DialogResult.Retry)
                                            {
                                                goto ulangTulisKartu;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        f.ShowMessagebox(r.Message, "Warning", MessageBoxButtons.OK);
                                    }
                                }
                            }
                            else
                            {
                                var dialog = f.ShowMessagebox(apakahkartubaru.Message, "warning", MessageBoxButtons.RetryCancel);
                                if (dialog == DialogResult.Retry)
                                {
                                    goto Submit;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txtPaymentMethod.Text.Contains("EDC") == true)
                    {
                        f.messageboxError("Sorry untuk transaksi EDC tidak bisa dicancel");
                    }
                    else
                    {
                        f.messageboxError("Sorry, Transaksi tidak dikenali");
                    }
                }
            }
            else if (txtTipeTransaksi.Text == "TOPUP")
            {
                if (txtPaymentMethod.Text.ToUpper() == "EMONEY" || txtPaymentMethod.Text.ToUpper() == "CASH" || txtPaymentMethod.Text.ToUpper() == "EMONEY & CASH")
                {
                    var result = GetDataKartu();
                    if (result.Success == false)
                    {
                        var dialog = f.ShowMessagebox("Kartu tidak terdeteksi", "Sorry", MessageBoxButtons.RetryCancel);
                        if (dialog == DialogResult.Retry)
                        {
                            goto Submit;
                        }
                    }
                    else
                    {
                        string AkunNumber = result.IdCard + "-" + result.CodeId;
                        if (AkunNumber != txtAccountNumber.Text)
                        {
                            var dialog = f.ShowMessagebox("No Akun kartu tidak sama dengan No Akun transaksi ini", "Sorry", MessageBoxButtons.RetryCancel);
                            if (dialog == DialogResult.Retry)
                            {
                                goto Submit;
                            }
                        }
                        else
                        {
                            var ApakahOkeBuatCancel = f.CheckApakahIniTopupYangTerakhir(IdTransaction.Text, AkunNumber);
                            if (ApakahOkeBuatCancel.RetCode > 0)
                            {
                                var dialog = f.ShowMessagebox(ApakahOkeBuatCancel.Message + " ?", "warning", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    var r = f.SaveCancelTransakasiTopup(f.GetNamaUser(LblAuthorizeBy.Text), f.GetNamaUser(General.IDUser), f.GetComputerName(), IdTransaction.Text, txtPaymentMethod.Text, txtTotalTransaksi.Text, result);
                                    if (r.RetCode == 1)
                                    {
                                    ulangTulisKartu:
                                        var d = r.Message.Split('~');
                                        var Res = new ResultSaveCancelTransakasiTicketModel();
                                        foreach (string rr in d)
                                        {
                                            var rrr = rr.Split(':');
                                            if (rrr[0] == "Id")
                                            {
                                                Res.Id = rrr[1];
                                            }
                                            else if (rrr[0] == "AccountNumber")
                                            {
                                                Res.AccountNumber = rrr[1];
                                            }
                                            else if (rrr[0] == "SaldoEmoney")
                                            {
                                                Res.SaldoEmoney = rrr[1];
                                            }
                                            else if (rrr[0] == "SaldoJaminan")
                                            {
                                                Res.SaldoJaminan = rrr[1];
                                            }
                                            else if (rrr[0] == "Ticket")
                                            {
                                                Res.Ticket = rrr[1];
                                            }
                                            else if (rrr[0] == "TipeTransaksi")
                                            {
                                                Res.TipeTransaksi = rrr[1];
                                            }
                                        }

                                        var DataKartu = GetDataKartu();
                                        string AkunKartu = DataKartu.IdCard + "-" + DataKartu.CodeId;
                                        if (AkunKartu == AkunNumber)
                                        {
                                        ulangSaldoEmoney:
                                            var SaldoEmoney = UpdateBlok("04", "04", Res.SaldoEmoney);
                                            if (SaldoEmoney.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Saldo eMoney Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var tiketWeekDay = UpdateBlok("05", "04", Res.Ticket);
                                            if (tiketWeekDay.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Ticket Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var JaminanSaldo = UpdateBlok("08", "08", Res.SaldoJaminan);
                                            if (tiketWeekDay.Success == false)
                                            {
                                                var res = MessageBox.Show("Update Ticket Gagal", "Encode Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto ulangSaldoEmoney;
                                                }
                                            }

                                            var UpdateAccount = ReadCardDataKey();
                                            f.UpdatAccountData(UpdateAccount);
                                            string IdLog = Res.Id;
                                        PrintLagi:
                                            var print = PrintTicketCancel(IdLog, r.Message);
                                            if (print.Success == true)
                                            {
                                                f.RefreshDashboard();
                                                General.Page = "Registrasi";
                                                Print fp = new Print();
                                                fp.Show();
                                                fp.BringToFront();
                                                fp.StartPosition = FormStartPosition.CenterScreen;
                                                fp.txtMessageBox.Text = "Transaksi ID:" + IdTransaction.Text + " Berhasil dihapus";
                                                this.Close();
                                            }
                                            else
                                            {
                                                var res = MessageBox.Show("Print Gagal", "Printing Fail", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                                                if (res == DialogResult.Retry)
                                                {
                                                    goto PrintLagi;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var res = f.ShowMessagebox("Akun Kartu yang ditempelkan pada reader tidak sesuai, silahkan tempelkan kartu yang sesuai", "warning", MessageBoxButtons.RetryCancel);
                                            if (dialog == DialogResult.Retry)
                                            {
                                                goto ulangTulisKartu;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        f.ShowMessagebox(r.Message, "Warning", MessageBoxButtons.OK);
                                    }
                                }
                            }
                            else
                            {
                                var dialog = f.ShowMessagebox(ApakahOkeBuatCancel.Message, "warning", MessageBoxButtons.RetryCancel);
                                if (dialog == DialogResult.Retry)
                                {
                                    goto Submit;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txtPaymentMethod.Text.Contains("EDC") == true)
                    {
                        f.ShowMessagebox("Sorry, untuk transaksi EDC tidak bisa dicancel", "Sorry", MessageBoxButtons.OK);
                    }
                    else
                    {
                        f.ShowMessagebox("Sorry, Transaksi tidak dikenali", "Sorry", MessageBoxButtons.OK);
                    }
                }
            }
        }

        #region CardFunction
        internal void establishContext()
        {
            retCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_SYSTEM, 0, 0, ref hContext);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                MessageBox.Show("Check your device and please restart again", "Reader not connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connActive = false;
                return;
            }
        }
        public void SelectDevice()
        {
            try
            {
                List<string> availableReaders = this.ListReaders();
                this.RdrState = new Card.SCARD_READERSTATE();
                readername = availableReaders[0].ToString();//selecting first device
                this.RdrState.RdrName = readername;
            }

            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                f.ShowMessagebox("Error :" + e.Message, "Reader Error", MessageBoxButtons.OK);
            }


        }
        public bool connectCard()
        {
            connActive = true;

            retCode = Card.SCardConnect(hContext, readername, Card.SCARD_SHARE_SHARED,
                      Card.SCARD_PROTOCOL_T0 | Card.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                connActive = false;
                return false;
            }
            return true;
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            GetDataKartu();
        }

        public List<string> ListReaders()
        {
            int ReaderCount = 0;
            List<string> AvailableReaderList = new List<string>();

            //Make sure a context has been established before 
            //retrieving the list of smartcard readers.
            retCode = Card.SCardListReaders(hContext, null, null, ref ReaderCount);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                MessageBox.Show("Silahkan Pasangkan RFID Reader", Card.GetScardErrMsg(retCode));
            }

            byte[] ReadersList = new byte[ReaderCount];

            retCode = Card.SCardListReaders(hContext, null, ReadersList, ref ReaderCount);
            if (retCode != Card.SCARD_S_SUCCESS)
            {
                MessageBox.Show("Silahkan Pasangkan RFID Reader", Card.GetScardErrMsg(retCode));
            }

            string rName = "";
            int indx = 0;
            if (ReaderCount > 0)
            {
                // Convert reader buffer to string
                while (ReadersList[indx] != 0)
                {

                    while (ReadersList[indx] != 0)
                    {
                        rName = rName + (char)ReadersList[indx];
                        indx = indx + 1;
                    }

                    //Add reader name to list
                    AvailableReaderList.Add(rName);
                    rName = "";
                    indx = indx + 1;

                }
            }
            return AvailableReaderList;
        }
        private string getcardUID()//only for mifare 1k cards
        {
            string cardUID = "";
            byte[] receivedUID = new byte[256];
            Card.SCARD_IO_REQUEST request = new Card.SCARD_IO_REQUEST();
            request.dwProtocol = Card.SCARD_PROTOCOL_T1;
            request.cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Card.SCARD_IO_REQUEST));
            byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 }; //get UID command      for Mifare cards
            int outBytes = receivedUID.Length;
            int status = Card.SCardTransmit(hCard, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

            if (status != Card.SCARD_S_SUCCESS)
            {
                cardUID = "Error";
            }
            else
            {
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToUpper();
            }

            return cardUID;
        }
        private void ClearBuffers()
        {

            long indx;

            for (indx = 0; indx <= 262; indx++)
            {

                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;

            }

        }
        public int SendAPDU()
        {
            int indx;
            string tmpStr;

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            // Display Apdu In
            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }
            //displayOut(2, 0, tmpStr);
            retCode = Card.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0], SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != Card.SCARD_S_SUCCESS)
            {

                //displayOut(1, retCode, "");
                return retCode;

            }

            tmpStr = "";
            for (indx = 0; indx <= RecvLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            //displayOut(3, 0, tmpStr);
            return retCode;

        }
        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:
                    TxtBacaKartu.SelectionColor = Color.Green;
                    break;
                case 1:
                    TxtBacaKartu.SelectionColor = Color.Red;
                    PrintText = Card.GetScardErrMsg(retVal);
                    break;
                case 2:
                    TxtBacaKartu.SelectionColor = Color.Black;
                    PrintText = "<" + PrintText;
                    break;
                case 3:
                    TxtBacaKartu.SelectionColor = Color.Black;
                    PrintText = ">" + PrintText;
                    break;
                case 4:
                    TxtBacaKartu.SelectionColor = Color.Red;
                    break;

            }

            TxtBacaKartu.AppendText(PrintText);
            TxtBacaKartu.AppendText("\n");
            TxtBacaKartu.SelectionColor = Color.Black;
            TxtBacaKartu.Focus();

        }
        private ReturnResult LoaAuthoKey()
        {
            var data = new ReturnResult();
            try
            {
                string tmpStr = "";
                ClearBuffers();
                SendBuff[0] = 0xFF;                                                                        // Class
                SendBuff[1] = 0x82;                                                                        // INS
                SendBuff[2] = 0x00;                                                                        // P1 : Key Structure
                SendBuff[3] = 0x00;
                SendBuff[4] = 0x06;                                                                        // P3 : Lc
                SendBuff[5] = 0xFF;        // Key 1 value
                SendBuff[6] = 0xFF;        // Key 2 value
                SendBuff[7] = 0xFF;        // Key 3 value
                SendBuff[8] = 0xFF;        // Key 4 value
                SendBuff[9] = 0xFF;        // Key 5 value
                SendBuff[10] = 0xFF;       // Key 6 value

                SendLen = 16;
                RecvLen = 2;

                retCode = SendAPDU();

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    data.RetCode = retCode;
                    data.Success = false;
                    data.Message = "LoaAuthoKey Failed";
                }
                else
                {
                    tmpStr = "";
                    for (int indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
                    {
                        tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                    }
                }
                if (tmpStr.Trim() != "90 00")
                {
                    data.RetCode = retCode;
                    data.Success = true;
                    data.Message = "LoaAuthoKey Succes";
                }
            }
            catch (Exception ex)
            {
                data.RetCode = retCode;
                data.Success = false;
                data.Message = "Load authentication keys error!" + ex.Message;
            }
            return data;
        }
        private ReturnResult Authenticate(string Blocknumber)
        {
            var res = new ReturnResult();
            int indx;
            string tmpStr = "";

            ClearBuffers();

            SendBuff[0] = 0xFF;                             // Class
            SendBuff[1] = 0x86;                             // INS
            SendBuff[2] = 0x00;                             // P1
            SendBuff[3] = 0x00;                             // P2
            SendBuff[4] = 0x05;                             // Lc
            SendBuff[5] = 0x01;                             // Byte 1 : Version number
            SendBuff[6] = 0x00;                             // Byte 2
            SendBuff[7] = (byte)int.Parse("" + Blocknumber + "");     // Byte 3 : Block number
            SendBuff[8] = 0x60;

            SendBuff[9] = byte.Parse("00", System.Globalization.NumberStyles.HexNumber);        // Key 5 value

            SendLen = 10;
            RecvLen = 2;

            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                res.Message = "card_function error :" + retCode;
                res.RetCode = retCode;
                res.Success = false;
            }
            else
            {
                tmpStr = "";
                for (indx = 0; indx <= RecvLen - 1; indx++)
                {
                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                }
            }
            if (tmpStr.Trim() == "90 00")
            {
                //displayOut(0, 0, "Authentication success!");
                res.Message = "Authentication success";
                res.Success = true;
            }
            else
            {
                //displayOut(4, 0, "Authentication failed!");
                res.Message = "Authentication failed";
                res.Success = false;
            }

            return res;
        }
        private ReturnResult ReadBlock(string BinBlk, string Autho)
        {
            var data = new ReturnResult();
            string tmpStr = "";
            try
            {
                Authenticate(Autho);
                int indx;
                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0xB0;
                SendBuff[2] = 0x00;
                SendBuff[3] = (byte)int.Parse(BinBlk);
                SendBuff[4] = (byte)int.Parse("16");

                SendLen = 5;
                RecvLen = SendBuff[4] + 2;

                retCode = SendAPDU();

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    data.RetCode = retCode;
                    data.Success = false;
                    data.Message = "Send Request Error";
                }
                else
                {
                    tmpStr = "";
                    for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
                    {
                        tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                    }

                }
                if (tmpStr.Trim() == "90 00")
                {
                    tmpStr = "";
                    for (indx = 0; indx <= RecvLen - 3; indx++)
                    {

                        tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);
                    }

                    data.RetCode = retCode;
                    data.Success = true;
                    data.Message = tmpStr;
                }
                else
                {
                    data.RetCode = retCode;
                    data.Success = false;
                    data.Message = "Read block error!";
                }
            }
            catch (Exception ex)
            {
                data.RetCode = retCode;
                data.Success = false;
                data.Message = "Read block error! - " + ex.Message;
            }
            return data;
        }
        public ReaderCard ReadCardDataKey()
        {
            var res = new ReaderCard();
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                string cardUID = getcardUID();
                if (cardUID != "Error" || cardUID != "99")
                {
                    int count = (cardUID.Length / 2) - 1;
                    string[] array_data = new string[cardUID.Length / 2];
                    int itung = 0;
                    for (int a = 0; a < cardUID.Length; a++)
                    {
                        int c = a % 2;
                        if (c == 0)
                        {
                            array_data[itung] = cardUID.Substring(a, 2);
                            itung++;
                        }
                    }
                    string id_card = "";
                    for (int a = array_data.Count() - 1; a >= 0; a--)
                    {
                        if (a == array_data.Count() - 1)
                        {
                            id_card = id_card + array_data[a];
                        }
                        else
                        {
                            id_card = id_card + array_data[a];
                        }

                    }
                    uint num = uint.Parse(id_card, System.Globalization.NumberStyles.AllowHexSpecifier);
                    if (num != 0)
                    {
                        res.IdCard = num.ToString();
                        var loadKey = LoaAuthoKey();
                        if (loadKey.Success == true)
                        {
                            var SaldoEmoney = ReadBlock("04", "04");
                            var tiketWeekDay = ReadBlock("05", "04");
                            var tiketWeekEnd = ReadBlock("06", "04");
                            var JaminanSaldo = ReadBlock("08", "08");
                            var CodeId = ReadBlock("09", "08");
                            if (SaldoEmoney.Success == true && tiketWeekDay.Success == true
                                && tiketWeekEnd.Success == true && JaminanSaldo.Success == true
                                && CodeId.Success == true)
                            {
                                res.SaldoEmoney = f.ConvertDecimal(SaldoEmoney.Message);
                                res.TicketWeekDay = f.ConvertDecimal(tiketWeekDay.Message);
                                res.TicketWeekEnd = f.ConvertDecimal(tiketWeekEnd.Message);
                                res.SaldoJaminan = f.ConvertDecimal(JaminanSaldo.Message);
                                res.CodeId = f.ConvertDecimal(CodeId.Message).ToString();
                                res.Success = true;
                                res.Message = "Reading Card success";
                            }
                            else
                            {
                                res.Success = false;
                                res.Message = "Reading Card fail";
                            }
                        }
                        else
                        {
                            res.Success = false;
                            res.Message = "loadKey Card fail";
                        }
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Smart Card UID tidak terdeteksi";
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Smart Card UID tidak terdeteksi";
                }
            }
            else
            {
                res.Success = false;
                res.Message = "Smart Card UID tidak terdeteksi";
            }
            return res;
        }
        private ReturnResult UpdateBlok(string BinBlk, string AuthoBin, string BinData)
        {
            var res = new ReturnResult();
            var Authorize = Authenticate(AuthoBin);
            if (Authorize.Success == true)
            {
                string tmpStr;
                int indx;
                tmpStr = BinData;
                ClearBuffers();
                SendBuff[0] = 0xFF;                                     // CLA
                SendBuff[1] = 0xD6;                                     // INS
                SendBuff[2] = 0x00;                                     // P1
                SendBuff[3] = (byte)int.Parse(BinBlk);            // P2 : Starting Block No.
                SendBuff[4] = (byte)int.Parse("16");            // P3 : Data length

                for (indx = 0; indx <= (tmpStr).Length - 1; indx++)
                {

                    SendBuff[indx + 5] = (byte)tmpStr[indx];

                }
                SendLen = SendBuff[4] + 5;
                RecvLen = 0x02;

                retCode = SendAPDU();

                if (retCode != Card.SCARD_S_SUCCESS)
                {
                    res.Success = false;
                    res.Message = "Card Function Fail : " + retCode;
                }
                else
                {
                    tmpStr = "";
                    for (indx = 0; indx <= RecvLen - 1; indx++)
                    {

                        tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                    }

                }
                if (tmpStr.Trim() == "90 00")
                {
                    res.Success = true;
                    res.Message = "Encode Berhasil";
                }
                else
                {
                    //displayOut(2, 0, "");
                    res.Success = false;
                    res.Message = "Encode Gagal";
                }
            }
            else
            {
                res.Success = false;
                res.Message = "Encode Gagal";
            }
            return res;
        }
        #endregion

        #region Print
        public ReturnResult PrintFoodCourtCancel(string Idlog, string Message)
        {
            var res = new ReturnResult();
            try
            {
                var data = f.GetDataLogTransaksiCancel(Idlog);
                string s = "Datetime \t: " + data.CancelDate + Environment.NewLine;
                s += "ID Transaction\t: FOODCOURT" + data.IdTransaksi + Environment.NewLine;
                s += "Merchant ID \t: " + f.GetComputerName() + Environment.NewLine;
                s += "Nama Petugas \t: " + f.GetNamaUser(General.IDUser) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Transaksi Foodcourt " + Environment.NewLine;

                if (Message.Contains(",") == true)
                {
                    var datae = Message.Split(',');
                    var listKeranjang = new List<string>();
                    foreach (string datanyo in datae)
                    {
                        if (datanyo.Contains("Desc :") == true)
                        {
                            string dDatanyo = datanyo.Replace("Desc :", "");
                            if (dDatanyo.Contains("*") == true)
                            {
                                var LK = dDatanyo.Split('*');
                                foreach (string Lst in LK)
                                {
                                    listKeranjang.Add(Lst);
                                }
                            }
                        }
                    }
                    if (listKeranjang.Count > 0)
                    {
                        decimal d = 0;
                        foreach (string Items in listKeranjang)
                        {
                            d++;
                            if (Items.Contains("|") == true)
                            {
                                var content = Items.Split('|');
                                s += d + ". " + content[0] + " - " + content[1] + "\t : " + f.ConvertToRupiah(f.ConvertDecimal(content[3])) + Environment.NewLine;
                            }
                        }
                    }
                }

                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Belanja \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTransaksi)) + Environment.NewLine;
                s += "Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.PayEmoney)) + Environment.NewLine;
                s += "Cash \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.PayTunai)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Transaksi berhasil dibatalkan " + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                foreach (string pfoot in f.GetFooterPrint())
                {
                    s += pfoot + Environment.NewLine;
                }

                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    int HeadreX = 0;
                    int startY = 0;
                    int Offset = 0;
                    string underLine1 = "======================================";
                    e1.Graphics.DrawString(underLine1, new Font("calibri", 7), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    e1.Graphics.DrawString(f.GetFooterPrint(1), new Font("Arial", 8, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    Offset = Offset + 15;
                    //e1.Graphics.DrawString(f.GetFooterPrint(2), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    e1.Graphics.DrawString(f.GetFooterPrint(2), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                    Offset = Offset + 15;
                    e1.Graphics.DrawString(f.GetFooterPrint(3), new Font("Arial", 5, FontStyle.Italic), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                    Offset = Offset + 15;
                    string underLine = "======================================";
                    e1.Graphics.DrawString(underLine, new Font("Arial", 7), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    e1.Graphics.DrawString(s, new Font("Arial", 7), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                };
                p.Print();
                res.Success = true;
                res.Message = "Print Success";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = "Exception Occured While Printing " + ex.Message;
            }
            return res;
        }
        public ReturnResult PrintTicketCancel(string Idlog, string Message)
        {
            var res = new ReturnResult();
            try
            {
                var data = f.GetDataLogTransaksiCancel(Idlog);
                string s = "Datetime \t: " + data.CancelDate + Environment.NewLine;
                if (data.TipeTransaksi == "TICKET+KARTU" || data.TipeTransaksi == "TICKET")
                {
                    s += "ID Transaction\t: REG" + data.IdTransaksi + Environment.NewLine;
                }
                else if (data.TipeTransaksi == "TOPUP")
                {
                    s += "ID Transaction\t: TOPUP" + data.IdTransaksi + Environment.NewLine;
                }
                else if (data.TipeTransaksi == "FOODCOURT")
                {
                    s += "ID Transaction\t: FOOODCOURT" + data.IdTransaksi + Environment.NewLine;
                }

                s += "Transaction Date\t: " + data.TransactionDate + Environment.NewLine;
                s += "Merchant ID \t: " + f.GetComputerName() + Environment.NewLine;
                s += "Nama Petugas \t: " + f.GetNamaUser(General.IDUser) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Transaksi Detail " + Environment.NewLine;

                if (data.DescriptionTransaksi.Contains("~") == true)
                {
                    var ListTicket = data.DescriptionTransaksi.Split('~');
                    decimal d = 0;
                    foreach (string dlist in ListTicket)
                    {
                        d++;
                        if (data.TipeTransaksi != "FOODCOURT")
                        {
                            if (dlist != "" && dlist.Contains("|") == true)
                            {
                                var dkolom = dlist.Split('|');
                                s += d + ". " + dkolom[0] + " - " + dkolom[2] + "\t : " + f.ConvertToRupiah(f.ConvertDecimal(dkolom[6])) + Environment.NewLine;
                            }
                        }
                        else
                        {
                            s += d + ". " + dlist + Environment.NewLine;
                        }

                    }
                }
                s += "- " + data.DescriptionTransaksi + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Total Belanja \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.TotalTransaksi)) + Environment.NewLine;
                s += "Emoney \t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.PayEmoney)) + Environment.NewLine;
                s += "Cash \t\t\t: " + f.ConvertToRupiah(f.ConvertDecimal(data.PayTunai)) + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                s += "Transaksi berhasil dibatalkan " + Environment.NewLine;
                s += "-------------------------------------------------------" + Environment.NewLine;
                foreach (string pfoot in f.GetFooterPrint())
                {
                    s += pfoot + Environment.NewLine;
                }

                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    int HeadreX = 0;
                    int startY = 0;
                    int Offset = 0;
                    string underLine1 = "======================================";
                    e1.Graphics.DrawString(underLine1, new Font("calibri", 7), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    e1.Graphics.DrawString(f.GetFooterPrint(1), new Font("Arial", 8, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    Offset = Offset + 15;
                    //e1.Graphics.DrawString(f.GetFooterPrint(2), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), HeadreX, startY + Offset);
                    e1.Graphics.DrawString(f.GetFooterPrint(2), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                    Offset = Offset + 15;
                    e1.Graphics.DrawString(f.GetFooterPrint(3), new Font("Arial", 5, FontStyle.Italic), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                    Offset = Offset + 15;
                    string underLine = "======================================";
                    e1.Graphics.DrawString(underLine, new Font("Arial", 7), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    e1.Graphics.DrawString(s, new Font("Arial", 7), new SolidBrush(Color.Black), new RectangleF(0, startY + Offset, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                };
                p.Print();
                res.Success = true;
                res.Message = "Print Success";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = "Exception Occured While Printing " + ex.Message;
            }
            return res;
        }
        #endregion
    }
}
