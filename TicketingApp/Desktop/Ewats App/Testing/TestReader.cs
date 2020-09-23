﻿using SharedCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.Testing
{
    public partial class TestReader : Form
    {
        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        string readername = "ACS ACR122 0";

        private void button2_Click(object sender, EventArgs e)
        {
            SelectDevice();
            establishContext();
            if (connectCard())
            {
                string cardUID = getcardUID();
                mMsg.Text = mMsg.Text + cardUID + "\n";
            }
            else
            {
                mMsg.Text = "Smart Card tidak terdeteksi \n";
            }
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

        private void TestReader_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte tmpLong;
            string tmpStr;
            if (tKey1.Text == "" | !byte.TryParse(tKey1.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey1.Focus();
                tKey1.Text = "";
                return;

            }

            if (tKey2.Text == "" | !byte.TryParse(tKey2.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey2.Focus();
                tKey2.Text = "";
                return;
            }

            if (tKey3.Text == "" | !byte.TryParse(tKey3.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey3.Focus();
                tKey3.Text = "";
                return;

            }

            if (tKey4.Text == "" | !byte.TryParse(tKey4.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey4.Focus();
                tKey4.Text = "";
                return;
            }

            if (tKey5.Text == "" | !byte.TryParse(tKey5.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey5.Focus();
                tKey5.Text = "";
                return;

            }

            if (tKey6.Text == "" | !byte.TryParse(tKey6.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey6.Focus();
                tKey6.Text = "";
                return;
            }

            ClearBuffers();
            SendBuff[0] = 0xFF;                                                                        // Class
            SendBuff[1] = 0x82;                                                                        // INS
            SendBuff[2] = 0x00;                                                                        // P1 : Key Structure
            SendBuff[3] = byte.Parse(tKeyNum.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[4] = 0x06;                                                                        // P3 : Lc
            SendBuff[5] = byte.Parse(tKey1.Text, System.Globalization.NumberStyles.HexNumber);        // Key 1 value
            SendBuff[6] = byte.Parse(tKey2.Text, System.Globalization.NumberStyles.HexNumber);        // Key 2 value
            SendBuff[7] = byte.Parse(tKey3.Text, System.Globalization.NumberStyles.HexNumber);        // Key 3 value
            SendBuff[8] = byte.Parse(tKey4.Text, System.Globalization.NumberStyles.HexNumber);        // Key 4 value
            SendBuff[9] = byte.Parse(tKey5.Text, System.Globalization.NumberStyles.HexNumber);        // Key 5 value
            SendBuff[10] = byte.Parse(tKey6.Text, System.Globalization.NumberStyles.HexNumber);       // Key 6 value


            SendLen = 11;
            RecvLen = 2;

            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
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
                displayOut(4, 0, "Load authentication keys error!");
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
            displayOut(2, 0, tmpStr);
            retCode = Card.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0], SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != Card.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");
                return retCode;

            }

            tmpStr = "";
            for (indx = 0; indx <= RecvLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            displayOut(3, 0, tmpStr);
            return retCode;

        }

        private void bAuth_Click(object sender, EventArgs e)
        {
            int tempInt, indx;
            byte tmpLong;
            string tmpStr;

            // Validate input
            if (tBlkNo.Text == "" | !int.TryParse(tBlkNo.Text, out tempInt))
            {

                tBlkNo.Focus();
                tBlkNo.Text = "";
                return;

            }

            if (int.Parse(tBlkNo.Text) > 319)
            {

                tBlkNo.Text = "319";

            }

            if (tAuthenKeyNum.Text == "" | !byte.TryParse(tAuthenKeyNum.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tAuthenKeyNum.Focus();
                tAuthenKeyNum.Text = "";
                return;

            }
            else if (int.Parse(tAuthenKeyNum.Text) > 1)
            {
                tAuthenKeyNum.Text = "1";
                return;
            }

            ClearBuffers();

            SendBuff[0] = 0xFF;                             // Class
            SendBuff[1] = 0x86;                             // INS
            SendBuff[2] = 0x00;                             // P1
            SendBuff[3] = 0x00;                             // P2
            SendBuff[4] = 0x05;                             // Lc
            SendBuff[5] = 0x01;                             // Byte 1 : Version number
            SendBuff[6] = 0x00;                             // Byte 2
            SendBuff[7] = (byte)int.Parse(tBlkNo.Text);     // Byte 3 : Block number

            if (rbKType1.Checked == true)
            {
                SendBuff[8] = 0x60;

            }
            else if (rbKType2.Checked == true)
            {
                SendBuff[8] = 0x61;
            }

            SendBuff[9] = byte.Parse(tAuthenKeyNum.Text, System.Globalization.NumberStyles.HexNumber);        // Key 5 value

            SendLen = 10;
            RecvLen = 2;

            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
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
                displayOut(0, 0, "Authentication success!");
            }
            else
            {
                displayOut(4, 0, "Authentication failed!");
            }
        }

        private void bBinRead_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx;

            // Validate Inputs
            tBinData.Text = "";

            if (tBinBlk.Text == "")
            {

                tBinBlk.Focus();
                return;

            }

            if (int.Parse(tBinBlk.Text) > 319)
            {

                tBinBlk.Text = "319";
                return;
            }

            if (tBinLen.Text == "")
            {

                tBinLen.Focus();
                return;

            }

            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xB0;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text);
            SendBuff[4] = (byte)int.Parse(tBinLen.Text);

            SendLen = 5;
            RecvLen = SendBuff[4] + 2;

            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
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

                tBinData.Text = tmpStr;
            }
            else
            {
                displayOut(4, 0, "Read block error!");
            }

        }

        private void tBinLen_TextChanged(object sender, EventArgs e)
        {
            byte numbyte;

            if (byte.TryParse(tBinLen.Text, out numbyte))
                tBinData.MaxLength = numbyte;
        }

        private void bBinUpd_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx, tempInt;

            if (tBinBlk.Text == "" | !int.TryParse(tBinBlk.Text, out tempInt))
            {

                tBinBlk.Focus();
                tBinBlk.Text = "";
                return;

            }

            if (int.Parse(tBinBlk.Text) > 319)
            {

                tBinBlk.Text = "319";
                return;

            }

            if (tBinLen.Text == "" | !int.TryParse(tBinLen.Text, out tempInt))
            {

                tBinLen.Focus();
                tBinLen.Text = "";
                return;

            }


            if (tBinData.Text == "")
            {

                tBinData.Focus();
                return;

            }

            tmpStr = tBinData.Text;
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD6;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text);            // P2 : Starting Block No.
            SendBuff[4] = (byte)int.Parse(tBinLen.Text);            // P3 : Data length

            for (indx = 0; indx <= (tmpStr).Length - 1; indx++)
            {

                SendBuff[indx + 5] = (byte)tmpStr[indx];

            }
            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;

            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
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
                tBinData.Text = "";
            }
            else
            {
                displayOut(2, 0, "");
            }
        }

        private void bValRead_Click(object sender, EventArgs e)
        {

            long Amount;


            if (tValBlk.Text == "")
            {

                tValBlk.Focus();
                return;

            }


            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return;

            }

            tValAmt.Text = "";
            tValSrc.Text = "";
            tValTar.Text = "";

            ClearBuffers();
            SendBuff[0] = 0xFF;                                      // CLA     
            SendBuff[1] = 0xB1;                                      // INS
            SendBuff[2] = 0x00;                                      // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);             // P2 : Block No.
            SendBuff[4] = 0x04;                                      // Le

            SendLen = 0x05;
            RecvLen = 0x06;

            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {

                return;

            }

            Amount = RecvBuff[3];
            Amount = Amount + (RecvBuff[2] * 256);
            Amount = Amount + (RecvBuff[1] * 256 * 256);
            Amount = Amount + (RecvBuff[0] * 256 * 256 * 256);
            tValAmt.Text = (Amount).ToString();
        }

        private void bValStor_Click(object sender, EventArgs e)
        {

            long Amount;
            int tempInt, indx;
            string tmpStr;

            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {

                tValAmt.Focus();
                tValAmt.Text = "";
                return;

            }

            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {

                tValAmt.Text = "4294967295";
                tValAmt.Focus();
                return;

            }

            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return;

            }

            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return;

            }

            tValSrc.Text = "";
            tValTar.Text = "";

            Amount = Convert.ToInt64(tValAmt.Text);
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);            // P2 : Block No.
            SendBuff[4] = 0x05;                                     // Lc : Data length
            SendBuff[5] = 0x00;                                     // VB_OP Value
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);            // Amount MSByte
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);            // Amount middle byte
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);             // Amount middle byte
            SendBuff[9] = (byte)(Amount & 0xFF);                    // Amount LSByte

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;


            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
            }
            else
            {
                tmpStr = "";
                for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

            }

            if (tmpStr.Trim() != "90 00")
            {
                displayOut(4, 0, "Store value error!");
            }

        }

        private void bValInc_Click(object sender, EventArgs e)
        {

            long Amount;
            int tempInt, indx;
            string tmpStr;

            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {

                tValAmt.Focus();
                tValAmt.Text = "";
                return;

            }

            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {

                tValAmt.Text = "4294967295";
                tValAmt.Focus();
                return;

            }

            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return;

            }


            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return;

            }

            tValSrc.Text = "";
            tValTar.Text = "";

            Amount = Convert.ToInt64(tValAmt.Text);
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);            // P2 : Block No.
            SendBuff[4] = 0x05;                                     // Lc : Data length
            SendBuff[5] = 0x01;                                     // VB_OP Value
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);            // Amount MSByte
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);            // Amount middle byte
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);             // Amount middle byte
            SendBuff[9] = (byte)(Amount & 0xFF);                    // Amount LSByte

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;


            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
            }
            else
            {
                tmpStr = "";
                for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

            }

            if (tmpStr.Trim() != "90 00")
            {
                displayOut(4, 0, "Increment error!");
            }

        }

        private void bValDec_Click(object sender, EventArgs e)
        {
            long Amount;
            int tempInt, indx;
            string tmpStr;

            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {

                tValAmt.Focus();
                tValAmt.Text = "";
                return;

            }

            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {

                tValAmt.Text = "4294967295";
                tValAmt.Focus();
                return;

            }

            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return;

            }


            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return;

            }

            tValSrc.Text = "";
            tValTar.Text = "";

            Amount = Convert.ToInt64(tValAmt.Text);
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);            // P2 : Block No.
            SendBuff[4] = 0x05;                                     // Lc : Data length
            SendBuff[5] = 0x02;                                     // VB_OP Value
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);            // Amount MSByte
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);            // Amount middle byte         
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);             // Amount middle byte
            SendBuff[9] = (byte)(Amount & 0xFF);                    // Amount LSByte

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;


            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
            }
            else
            {
                tmpStr = "";
                for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

            }

            if (tmpStr.Trim() != "90 00")
            {
                displayOut(4, 0, "Decrement error!");
            }
        }

        private void bValRes_Click(object sender, EventArgs e)
        {

            int tempInt, indx;
            string tmpStr;

            // Validate inputs
            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return;

            }

            if (tValSrc.Text == "" | !int.TryParse(tValSrc.Text, out tempInt))
            {

                tValSrc.Focus();
                tValSrc.Text = "";
                return;

            }

            if (tValTar.Text == "" | !int.TryParse(tValTar.Text, out tempInt))
            {

                tValTar.Focus();
                tValTar.Text = "";
                return;

            }

            if (int.Parse(tValSrc.Text) > 319)
            {

                tValSrc.Text = "319";
                return;

            }

            if (int.Parse(tValTar.Text) > 319)
            {

                tValTar.Text = "319";
                return;

            }

            tValAmt.Text = "";
            tValBlk.Text = "";

            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValSrc.Text);            // P2 : Source Block No.
            SendBuff[4] = 0x02;                                     // Lc
            SendBuff[5] = 0x03;                                     // Data In Byte 1
            SendBuff[6] = (byte)int.Parse(tValTar.Text);            // P2 : Target Block No.

            SendLen = 0x07;
            RecvLen = 0x02;


            retCode = SendAPDU();

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                return;
            }
            else
            {
                tmpStr = "";
                for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

            }

            if (tmpStr.Trim() != "90 00")
            {
                displayOut(4, 0, "Restore value error!");
            }
        }

        public Card.SCARD_READERSTATE RdrState;
        public Card.SCARD_IO_REQUEST pioSendRequest;

        public TestReader()
        {
            InitializeComponent();
        }
        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:
                    mMsg.SelectionColor = Color.Green;
                    break;
                case 1:
                    mMsg.SelectionColor = Color.Red;
                    PrintText = Card.GetScardErrMsg(retVal);
                    break;
                case 2:
                    mMsg.SelectionColor = Color.Black;
                    PrintText = "<" + PrintText;
                    break;
                case 3:
                    mMsg.SelectionColor = Color.Black;
                    PrintText = ">" + PrintText;
                    break;
                case 4:
                    mMsg.SelectionColor = Color.Red;
                    break;

            }

            mMsg.AppendText(PrintText);
            mMsg.AppendText("\n");
            mMsg.SelectionColor = Color.Black;
            mMsg.Focus();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            //Establish Context
            retCode = Card.SCardEstablishContext(Card.SCARD_SCOPE_USER, 0, 0, ref hContext);

            if (retCode != Card.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "Error");

                return;

            }

            // 2. List PC/SC card readers installed in the system

            retCode = Card.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != Card.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "Error");

                return;
            }


            byte[] ReadersList = new byte[pcchReaders];

            // Fill reader list
            retCode = Card.SCardListReaders(this.hContext, null, ReadersList, ref pcchReaders);

            if (retCode != Card.SCARD_S_SUCCESS)
            {
                mMsg.AppendText("SCardListReaders Error: " + Card.GetScardErrMsg(retCode));

                return;
            }
            else
            {
                displayOut(0, 0, "Sukses");
            }

            rName = "";
            indx = 0;

            //Convert reader buffer to string
            while (ReadersList[indx] != 0)
            {

                while (ReadersList[indx] != 0)
                {
                    rName = rName + (char)ReadersList[indx];
                    indx = indx + 1;
                }

                //Add reader name to list
                cbReader.Items.Add(rName);
                rName = "";
                indx = indx + 1;

            }

            if (cbReader.Items.Count > 0)
            {
                cbReader.SelectedIndex = 0;

            }
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


    }
}
