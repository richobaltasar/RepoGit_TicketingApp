using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketingApp.Models;
using System.Reflection;
using System.Data.SqlClient;

namespace TicketingApp.Function
{
    public class GlobalFunction
    {
        public SqlConnection conn = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();

        public string GenID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string id = builder.ToString();
            return id;
        }
        public string GuidSha256(string url)
        {
            SHA256 shaAlgorithm = new SHA256Managed();
            byte[] shaDigest = shaAlgorithm.ComputeHash(ASCIIEncoding.ASCII.GetBytes(url));
            return BitConverter.ToString(shaDigest);
        }
        public object GetPropValue(object source, string propertyName)
        {
            var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            if (property != null)
            {
                if (property.GetValue(source) != null)
                {
                    return property?.GetValue(source);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        #region UserPP
        public UserData GetProfileUser(string IdUser)
        {
            var res = new UserData();
            try
            {
                conn.ConnectionString = Config.ConStr;
                using (var connection = conn)
                {
                    connection.Open();
                    string sql = "exec SP_GetProfileUser " +
                        "@IdUser=" + IdUser + "";
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
                throw ex;
            }
            return res;
        }
        #endregion

    }
}
