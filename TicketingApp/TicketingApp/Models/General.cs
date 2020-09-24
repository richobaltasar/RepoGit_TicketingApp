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

}
