using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingApp.Models
{
    public static class Config
    {
        public static string ConStr { get; set; }
    }

    public class DefaultPage
    {

    }

    public class UserProfile
    {
        public string Username { get; set; }
        public string NamaLengkap { get; set; }
        public string PhotoProfile { get; set; }
    }
    public class Logo
    {
        public string logo_icon { get; set; }
        public string logo_Image { get; set; }
    }
    public class alert
    {
        public string title { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }

    public class ResultData
    {
        public string Message { get; set; }
        public string CodeError { get; set; }
        public int status { get; set; }
        public string Output { get; set; }
    }

}
