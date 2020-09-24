using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingApp.Models
{
    public class ModuleDataModel
    {
        public List<ModuleData> ListData { get; set; }
    }
    public class ModuleData
    {
        public string IdModul { get; set; }
        public string NamaModule { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
    }
}
