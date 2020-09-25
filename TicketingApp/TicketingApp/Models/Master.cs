using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingApp.Models
{
    public class ModuleDataModel
    {
        public List<ModuleData> ListData { get; set; }
        public ErrorViewModel Error { get; set; }
    }
    public class ModuleData
    {
        public string IdModul { get; set; }
        public string NamaModule { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }

        public ErrorViewModel Error { get; set; }
    }

    public class FormMasterData
    {
        public List<FormMaster> Template { get; set; }
        public dynamic dbContext { get; set; }
        public string classNameWithNameSpace { get; set; }
        public string ShowSubmit { get; set; }
        public int ColField { get; set; }
    }
    public class FormMaster
    {
        public string idLog { get; set; }
        public string NamaForm { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string TextLabel { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string ValueInput { get; set; }
        public string ListModel { get; set; }
        public string Urutan { get; set; }
        public string ShowHide { get; set; }
        public string ReadOnly { get; set; }
        public string Enable { get; set; }
        public string Mandatory { get; set; }
        public string IsNumber { get; set; }
        public string FilterBy { get; set; }


    }
}
