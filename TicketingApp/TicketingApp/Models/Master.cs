using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingApp.Models
{
    #region Module Data

    public class ModuleDataModel
    {
        public List<ModuleData> ListData { get; set; }
        public ErrorViewModel Error { get; set; }
    }
    public class ModuleData
    {
        public int IdModul { get; set; }
        public string NamaModule { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Img { get; set; }
        public int Status { get; set; }

        public IFormFile File_Img { get; set; }
        public ErrorViewModel Error { get; set; }
    }

    #endregion

    #region Menu Data
    public class MenuDataModel
    {
        public List<MenuData> ListData { get; set; }
        public ErrorViewModel Error { get; set; }
    }
    public class MenuData
    {
        public int idMenu { get; set; }
        public string NamaMenu { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Platform { get; set; }
        public string Img { get; set; }
        public int Status { get; set; }

        public IFormFile File_Img { get; set; }
        public ErrorViewModel Error { get; set; }
    }
    #endregion

    #region FormData
    public class FormDataModel
    {
        public List<FormData> ListData { get; set; }
        public ErrorViewModel Error { get; set; }
    }
    public class FormData
    {
        public int idLog { get; set; }
        public string NamaForm { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string TextLabel { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string ValueInput { get; set; }
        public string ListModel { get; set; }
        public int Urutan { get; set; }
        public string ShowHide { get; set; }
        public string ReadOnly { get; set; }
        public string Enable { get; set; }
        public string Mandatory { get; set; }
        public int IsNumber { get; set; }
        public int FilterBy { get; set; }

        public ErrorViewModel Error { get; set; }
    }
    #endregion

    #region ListItemData
    public class ListItemDataModel
    {
        public List<ListItemData> ListData { get; set; }
        public ErrorViewModel Error { get; set; }
    }
    public class ListItemData
    {
        public int id { get; set; }
        public string ListName { get; set; }
        public string Urutan { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }

        public ErrorViewModel Error { get; set; }
    }
    #endregion



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
