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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(res, val, null);
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

        public async Task<List<ModuleData>> ModuleData_GetSearch(ModuleData Data)
        {
            var res = new List<ModuleData>();
            try
            {
                var filter = GetFormLayoutForFilter("Form Master CRUD");
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ModuleData_GetSearch ";
                    

                    Type type1 = Data.GetType();
                    PropertyInfo[] props1 = type1.GetProperties();
                    foreach(var d in filter)
                    {
                        foreach (var p in props1)
                        {
                            if (null != p && p.CanWrite)
                            {
                                if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile" && p.Name == d.Id)
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
                    }

                    
                    sql = sql.RemoveLast(",");
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
                                            if (p.PropertyType.Name.ToString() == "Int32")
                                            {
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(res, val, null);
                                                
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
        public async Task<List<MenuData>> MenuData_GetSearch(MenuData Data)
        {
            var res = new List<MenuData>();
            try
            {
                var filter = GetFormLayoutForFilter("Form Master Menu");
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_GetSearch ";
                    Type type1 = Data.GetType();
                    PropertyInfo[] props1 = type1.GetProperties();
                    foreach (var d in filter)
                    {
                        foreach (var p in props1)
                        {
                            if (null != p && p.CanWrite)
                            {
                                if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile" && p.Name == d.Id)
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
                    }
                    sql = sql.RemoveLast(",");
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
        public async Task<List<MenuData>> MenuData_GetMenuByIdModule(int IdModule)
        {
            var res = new List<MenuData>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_GetMenuByIdModule @Id="+IdModule+"" +
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
        public async Task<List<SelectListItem>> MenuData_GetParent(int IdModule, int IdMenu, int IdPosisi)
        {
            var res = new List<SelectListItem>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_MenuData_GetParent " +
                        "@IdModule=" + IdModule + "," +
                        "@IdMenu="+ IdMenu + "," +
                        "@IdPosisi=" + IdPosisi + "" +
                        "";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new SelectListItem();
                                d.Text = reader["Text"].ToString();
                                d.Value = reader["Value"].ToString();
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

        #region Form Data
        public async Task<List<FormData>> FormData_GetSearch(FormData Data)
        {
            var res = new List<FormData>();
            try
            {
                var filter = GetFormLayoutForFilter("Form FormData");
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_FormData_GetSearch ";

                    Type type1 = Data.GetType();
                    PropertyInfo[] props1 = type1.GetProperties();
                    foreach (var d in filter)
                    {
                        foreach (var p in props1)
                        {
                            if (null != p && p.CanWrite)
                            {
                                if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile" && p.Name == d.Id)
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
                    }

                    sql = sql.RemoveLast(",");
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(res, val, null);
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

        #region ListItemData

        public async Task<List<ListItemData>> ListItemData_Get()
        {
            var res = new List<ListItemData>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ListItemData_Get" +
                        "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new ListItemData();
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
        public async Task<ErrorViewModel> ListItemData_Save(ListItemData Data)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ListItemData_Save ";
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
        public async Task<ErrorViewModel> ListItemData_Del(int Id)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ListItemData_Del @Id=" + Id.ToString() + "" +
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
        public async Task<ListItemData> ListItemData_GetById(int Id)
        {
            var res = new ListItemData();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ListItemData_GetById @Id=" + Id + "" +
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(res, val, null);
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
        public async Task<List<ListItemData>> ListItemData_GetSearch(ListItemData Data)
        {
            var res = new List<ListItemData>();
            try
            {
                var filter = GetFormLayoutForFilter("Form ListItemData");
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_ListItemData_GetSearch ";
                    Type type1 = Data.GetType();
                    PropertyInfo[] props1 = type1.GetProperties();
                    foreach (var d in filter)
                    {
                        foreach (var p in props1)
                        {
                            if (null != p && p.CanWrite)
                            {
                                if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile" && p.Name==d.Id)
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
                    }

                    sql = sql.RemoveLast(",");
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new ListItemData();
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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

        #region RoleMenuData

        public async Task<List<RoleMenuData>> RoleMenuData_Get()
        {
            var res = new List<RoleMenuData>();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_RoleMenuData_Get" +
                        "";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new RoleMenuData();
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
        public async Task<ErrorViewModel> RoleMenuData_Save(RoleMenuData Data)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_RoleMenuData_Save ";
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
                                    if(p.GetValue(Data) != null)
                                    {
                                        var val = p.GetValue(Data) ?? "";
                                        param = "@" + p.Name + "='" + val.ToString() + "',";
                                    }
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
        public async Task<ErrorViewModel> RoleMenuData_Del(int Id)
        {
            var res = new ErrorViewModel();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_RoleMenuData_Del @Id=" + Id.ToString() + "" +
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
        public async Task<RoleMenuData> RoleMenuData_GetById(int Id)
        {
            var res = new RoleMenuData();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_RoleMenuData_GetById @Id=" + Id + "" +
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(res, val, null);
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
        public async Task<List<RoleMenuData>> RoleMenuData_GetSearch(RoleMenuData Data)
        {
            var res = new List<RoleMenuData>();
            try
            {
                var filter = GetFormLayoutForFilter("Form RoleMenuData");
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_RoleMenuData_GetSearch ";
                    Type type1 = Data.GetType();
                    PropertyInfo[] props1 = type1.GetProperties();
                    foreach (var d in filter)
                    {
                        foreach (var p in props1)
                        {
                            if (null != p && p.CanWrite)
                            {
                                if (p.Name != "" && p.Name != "Error" && p.PropertyType.Name.ToString() != "IFormFile" && p.Name == d.Id)
                                {
                                    string param = "";
                                    if (p.PropertyType.Name.ToString() == "String")
                                    {
                                        if (p.GetValue(Data) != null)
                                        {
                                            var val = p.GetValue(Data) ?? "";
                                            param = "@" + p.Name + "='" + val.ToString() + "',";
                                        }
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
                    }
                    sql = sql.RemoveLast(",");
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 0;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var d = new RoleMenuData();
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
                                                int val = reader[p.Name].ToString().AsInt() ?? 0;
                                                p.SetValue(d, val, null);
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
        #endregion
    }
}
