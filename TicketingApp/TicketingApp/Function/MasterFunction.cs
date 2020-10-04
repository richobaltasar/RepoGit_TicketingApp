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
    public class MasterFunction
    {
        GlobalFunction GF = new GlobalFunction();
        public SqlConnection conn = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        #region Module Data
        public async Task<List<ModuleData>> ModuleData_Get()
        {
            var res = new List<ModuleData>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ModuleData_Get" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new ModuleData();
                                Type type = d.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                                        {
                                            if(p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                p.SetValue(d, reader[p.Name].ToString().ToInt(), null);
                                            }
                                            else
                                            {
                                                p.SetValue(d, reader[p.Name].ToString(), null);
                                            }
                                        }
                                    }
                                }
                                res.Add(d);
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
        public async Task<ErrorViewModel> ModuleData_Save(ModuleData Data)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ModuleData_Save ";
                    Type type = Data.GetType();
                    PropertyInfo[] props = type.GetProperties();
                    foreach (var p in props)
                    {
                        if (null != p && p.CanWrite)
                        {
                            if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                            {
                                string param = "";
                                if (p.PropertyType.Name.ToString() == "String")
                                {
                                     param= "@"+p.Name+"='"+ p.GetValue(Data).ToString()+"',";
                                }
                                else
                                {
                                    param = "@" + p.Name + "=" + p.GetValue(Data).ToString() + ",";
                                }
                                sql = sql + param;
                            }
                        }
                    }

                    sql = sql.RemoveLast(",");

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                res.MessageTitle = reader["Title"].ToString();
                                res.MessageContent = reader["Message"].ToString();
                                res.MessageStatus = reader["Status"].ToString();
                                res.RequestId = reader["Id"].ToString();
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
        public async Task<ErrorViewModel> ModuleData_Del(int Id)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ModuleData_Del @Id="+Id.ToString()+"" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                res.MessageTitle = reader["Title"].ToString();
                                res.MessageContent = reader["Message"].ToString();
                                res.MessageStatus = reader["Status"].ToString();
                                res.RequestId = reader["Id"].ToString();
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
        public async Task<ModuleData> ModuleData_GetById(int Id)
        {
            var res = new ModuleData();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ModuleData_Get_ById @Id="+Id+"" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Type type = res.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                                        {
                                            if (p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                p.SetValue(res, reader[p.Name].ToString().ToInt(), null);
                                            }
                                            else
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        #endregion


        #region Menu Data
        public async Task<List<MenuData>> MenuData_Get()
        {
            var res = new List<MenuData>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_Get" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new MenuData();
                                Type type = d.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                                        {
                                            if (p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                p.SetValue(d, reader[p.Name].ToString().ToInt(), null);
                                            }
                                            else
                                            {
                                                p.SetValue(d, reader[p.Name].ToString(), null);
                                            }
                                        }
                                    }
                                }
                                res.Add(d);
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
        public async Task<ErrorViewModel> MenuData_Save(MenuData Data)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_Save ";
                    Type type = Data.GetType();
                    PropertyInfo[] props = type.GetProperties();
                    foreach (var p in props)
                    {
                        if (null != p && p.CanWrite)
                        {
                            if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                            {
                                string param = "";
                                if (p.PropertyType.Name.ToString() == "String")
                                {
                                    var val = p.GetValue(Data) ?? "";
                                    param = "@" + p.Name + "='" + val.ToString() + "',";
                                }
                                else
                                {
                                    param = "@" + p.Name + "=" + p.GetValue(Data).ToString() + ",";
                                }
                                sql = sql + param;
                            }
                        }
                    }

                    sql = sql.RemoveLast(",");

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                res.MessageTitle = reader["Title"].ToString();
                                res.MessageContent = reader["Message"].ToString();
                                res.MessageStatus = reader["Status"].ToString();
                                res.RequestId = reader["Id"].ToString();
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
        public async Task<ErrorViewModel> MenuData_Del(int Id)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_Del @Id=" + Id.ToString() + "" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                res.MessageTitle = reader["Title"].ToString();
                                res.MessageContent = reader["Message"].ToString();
                                res.MessageStatus = reader["Status"].ToString();
                                res.RequestId = reader["Id"].ToString();
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
        public async Task<MenuData> MenuData_GetById(int Id)
        {
            var res = new MenuData();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_Get_ById @Id=" + Id + "" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Type type = res.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                                        {
                                            if (p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                p.SetValue(res, reader[p.Name].ToString().ToInt(), null);
                                            }
                                            else
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        #endregion

        #region Form Data

        public async Task<List<FormData>> FormData_Get()
        {
            var res = new List<FormData>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_FormData_Get" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new FormData();
                                Type type = d.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                                        {
                                            if (p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                var val = reader[p.Name].ToString() ?? "0";
                                                p.SetValue(d, val.ToInt(), null);
                                            }
                                            else
                                            {
                                                p.SetValue(d, reader[p.Name].ToString(), null);
                                            }
                                            if(p.Name == "NamaForm")
                                            {
                                                Console.WriteLine("Data : " + reader[p.Name].ToString());
                                            }
                                            
                                        }
                                    }
                                }
                                res.Add(d);
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
        public async Task<ErrorViewModel> FormData_Save(FormData Data)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_FormData_Save ";
                    Type type = Data.GetType();
                    PropertyInfo[] props = type.GetProperties();
                    foreach (var p in props)
                    {
                        if (null != p && p.CanWrite)
                        {
                            if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                            {
                                string param = "";
                                if (p.PropertyType.Name.ToString() == "String")
                                {
                                    var val = p.GetValue(Data) ?? "";
                                    param = "@" + p.Name + "='" + val.ToString() + "',";
                                }
                                else
                                {
                                    var val = p.GetValue(Data) ?? "";
                                    param = "@" + p.Name + "=" + val.ToString() + ",";
                                }
                                sql = sql + param;
                            }
                        }
                    }

                    sql = sql.RemoveLast(",");

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                res.MessageTitle = reader["Title"].ToString();
                                res.MessageContent = reader["Message"].ToString();
                                res.MessageStatus = reader["Status"].ToString();
                                res.RequestId = reader["Id"].ToString();
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
        public async Task<ErrorViewModel> FormData_Del(int Id)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_FormData_Del @Id=" + Id.ToString() + "" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                res.MessageTitle = reader["Title"].ToString();
                                res.MessageContent = reader["Message"].ToString();
                                res.MessageStatus = reader["Status"].ToString();
                                res.RequestId = reader["Id"].ToString();
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
        public async Task<FormData> FormData_GetById(int Id)
        {
            var res = new FormData();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_FormData_GetById @Id=" + Id + "" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Type type = res.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile")
                                        {
                                            if (p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                p.SetValue(res, reader[p.Name].ToString().ToInt(), null);
                                            }
                                            else
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }


        public List<FormMaster> GetFormLayout(string Page)
        {
            var res = new List<FormMaster>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "select*from Master_Form where NamaForm='" + Page + "' order by Urutan asc";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new FormMaster();
                                Type type = d.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "")
                                        {
                                            p.SetValue(d, reader[p.Name].ToString(), null);
                                        }
                                    }
                                }
                                res.Add(d);
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
        public List<FormMaster> GetFormLayoutForFilter(string Page)
        {
            var res = new List<FormMaster>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "select*from Master_Form where NamaForm='" + Page + "' and FilterBy=1 order by Urutan asc";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var d = new FormMaster();
                                Type type = d.GetType();
                                PropertyInfo[] props = type.GetProperties();
                                foreach (var p in props)
                                {
                                    if (null != p && p.CanWrite)
                                    {
                                        if (p.Name != "")
                                        {
                                            p.SetValue(d, reader[p.Name].ToString(), null);
                                        }
                                    }
                                }
                                res.Add(d);
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
        #endregion

        #region Master List
        public List<SelectListItem> GetListDataMaster(string Data)
        {
            var res = new List<SelectListItem>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetListDataMaster @Data='" + Data + "'";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var data = new SelectListItem();
                                data.Text = reader["Text"].ToString();
                                data.Value = reader["Value"].ToString();
                                res.Add(data);
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
        #endregion
    }
}
