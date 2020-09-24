using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingApp.Models
{
    public class LoginContent
    {
        public string RightTitle { get; set; }
        public string RightDesc { get; set; }
        public FormComppro Compro { get; set; }
        public alertLogin Alert { get; set; }
    }
    public class FormComppro
    {
        public string NamaCompany { get; set; }
        public string NamaWahana { get; set; }
        public string NoTelpon { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string Status { get; set; }
        public string ImgLink { get; set; }
    }
    public class alertLogin
    {
        public string title { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public UserLogin data { get; set; }
        public string akses { get; set; }
    }
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Platform { get; set; }
    }
}
