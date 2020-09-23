using SharedCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Ewats_App.Function
{
    public class DataFakeFunction
    {
        string server = ConfigurationFileStatic.ConnStrLog;
        string ImgPath = ConfigurationFileStatic.PathImgWeb;
        public SqlConnection conn = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();

        public string GenarateDataFake(string setTanggal, string Persentase)
        {
        ulang:
            string Res = "";
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["dbFake"].ConnectionString;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GenerateDataApp '" + setTanggal + "'," + Persentase + "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }
        public string GenarateDataFakeAuto(string setTanggal, string Persentase)
        {
        ulang:
            string Res = "";
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["dbFake"].ConnectionString;
                var ListTanggal = new List<string>();
                using (var connection = conn)
                {
                    connection.Open();
                    var now = DateTime.Now;
                    var first = new DateTime(now.Year, now.Month, 1);
                    var last = first.AddMonths(1).AddDays(-1);
                    string U1 = first.ToShortDateString();
                    string U2 = now.ToShortDateString();

                    string sql = "exec SP_GetDataSebelumnyaYgMasihKosong '" + U1 + "','" + U2 + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListTanggal.Add(reader["Tanggal"].ToString());
                            }
                        }
                    }
                }


                if (ListTanggal.Count() > 0)
                {
                    foreach (string Tanggal in ListTanggal)
                    {
                        GenarateDataFake(Tanggal, Persentase);
                    }
                }

            }
            catch (Exception ex)
            {
                var result = messageboxError(ex.Message);
                if (result == DialogResult.Retry)
                {
                    goto ulang;
                }
                else
                {
                    Application.Exit();
                }
            }
            return Res;
        }

        public DialogResult messageboxError(string content)
        {
            string message = "Do you want to abort this operation? \n" + content;
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            return result;
        }
    }
}
