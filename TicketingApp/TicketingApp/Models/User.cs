using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TicketingApp.Models
{
    public class UserData
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string hakakses { get; set; }
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string NoHp { get; set; }
        public string Photo { get; set; }

        public IFormFile File_Photo { get; set; }

        public string Alamat { get; set; }
        public int Status { get; set; }
        public string TanggalLahir { get; set; }
        public string TempatLahir { get; set; }
        public string Agama { get; set; }
        public string ScanKTP { get; set; }
        public IFormFile File_ScanKTP { get; set; }
        public string JenisIdentitas { get; set; }
        public string NoIdentitas { get; set; }
        public string NamaDivisi { get; set; }
        public string NamaPosisi { get; set; }
        public string TglAwalKerja { get; set; }
        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ImgLink { get; set; }




        public ErrorViewModel Error { get; set; }
    }

    public class UserDataModel
    {
        public List<UserData> ListData { get; set; }
        public ErrorViewModel Error { get; set; }
    }
}
