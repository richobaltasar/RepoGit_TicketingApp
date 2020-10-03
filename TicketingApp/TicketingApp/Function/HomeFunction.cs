using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketingApp.Models;
using System.Reflection;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc.Rendering;


namespace TicketingApp.Function
{
    
    public class HomeFunction
    {
        GlobalFunction GF = new GlobalFunction();
        public SqlConnection conn = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public alert LoginProc(UserLogin data)
        {
            var res = new alert();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_LoginProc " +
                        "@Username='" + data.Username + "'," +
                        "@Password='" + data.Password + "'," +
                        "@Category='" + data.Platform + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Type type = res.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error")
                                        {
                                            p.SetValue(res, reader[p.Name].ToString(), null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.message = "Function LoginProc, Error Syntax : " + ex.Message;
                res.status = "error";
                res.title = "Sorry we will fixing this bugs";
            }
            return res;
        }
        public string GetIDUser(UserLogin data)
        {
            string res = "";
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_LoginProc_GetID " +
                        "@Username='" + data.Username + "'," +
                        "@Password='" + data.Password + "'," +
                        "@Category='" + data.Platform + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res = reader["id"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
    }
}
