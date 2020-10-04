using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketingApp.Models;
using Microsoft.Extensions.Configuration;
using DeviceDetectorNET;
using DeviceDetectorNET.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using TicketingApp.Function;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace TicketingApp.Controllers
{
    public class MasterController : Controller
    {
        GlobalFunction GF = new GlobalFunction();
        MasterFunction f = new MasterFunction();
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public MasterController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        #region Module Data
        public async Task<IActionResult> ModuleData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new ModuleDataModel();
            try
            {
                DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);
                BotParser botParser = new BotParser();
                var userAgent = Request.Headers["User-Agent"];
                var result = DeviceDetector.GetInfoFromUserAgent(userAgent);

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_UserId")))
                {
                    var model2 = new alertLogin();
                    return await Task.Run(() => RedirectToAction("SignIn", "Home", model2));
                }
                else
                {
                    ViewBag.UserId = HttpContext.Session.GetString("_UserId");
                    ViewBag.Device = result.Match.DeviceType.ToString();
                    Console.WriteLine(ViewBag.Device);
                    model.ListData = await f.ModuleData_Get();
                    return await Task.Run(() => View(model));
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => View(model));
            }
        }

        [NoDirectAccess]
        public async Task<IActionResult> ModuleData_Form(int id = 0)
        {
            var model = new ModuleData();
            try
            {
                if (id == 0)
                {
                    model.IdModul = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.ModuleData_GetById(id);

                    if (model == null)
                    {
                        return NotFound();
                    }
                    return await Task.Run(() => View(model));
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => View(model));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModuleData_Save([Bind("IdModul,NamaModule,Action,Controller,Img,Status,File_Img")] ModuleData data)
        {
            if (ModelState.IsValid)
            {
                string NamaFolder = "FileMaster";
                var r = new ErrorViewModel();
                string webRootPath = _env.WebRootPath;
                var uploads = Path.Combine(webRootPath, NamaFolder);
                try
                {
                    if (data.File_Img != null)
                    {
                        if (data.File_Img.Length > 0)
                        {
                            string TypeFile = data.File_Img.ContentType;
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            string format = "." + TypeFile.Replace("image/", "");
                            string FileName = regex.Replace(data.IdModul.ToString(), "")+format;
                            string pathfiles = Path.Combine(uploads, regex.Replace(data.IdModul.ToString(), "") + ".*");
                            if (data.IdModul != 0)
                            {
                                DirectoryInfo directory = new DirectoryInfo(uploads);
                                var files = directory.GetFiles().ToList();
                                foreach(var img in files)
                                {
                                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(img.Name);

                                    if (fileNameWithoutExt == data.IdModul.ToString())
                                    {
                                        if (img.Exists.Equals(true))
                                        {
                                            img.Delete();
                                        }
                                    }
                                }
                            }

                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                await data.File_Img.CopyToAsync(fileStream);
                                data.Img = NamaFolder+"//"+FileName;
                            }
                            
                            
                        }
                    }
                    
                    r = await f.ModuleData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        var model = new ModuleDataModel();
                        model.ListData = await f.ModuleData_Get();
                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) });
                    }
                    else
                    {
                        var Error = new ErrorViewModel();
                        Error.MessageContent = r.MessageContent;
                        Error.MessageTitle = r.MessageTitle;
                        Error.RequestId = r.RequestId;
                        data.Error = Error;
                        return Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Form", data) });
                    }
                }
                catch (Exception ex)
                {
                    var Error = new ErrorViewModel();
                    Error.MessageContent = ex.ToString();
                    Error.MessageTitle = "Error ";
                    Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Form", data) });
                }
            }
            else
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = "State Model tidak valid";
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Form", data) });
            }
        }

        [HttpPost, ActionName("ModuleData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModuleData_Delete(int Id)
        {
            var model = new ModuleDataModel();
            var r =  await f.ModuleData_Del(Id);
            if (r.MessageStatus == "success")
            {
                model.ListData = await f.ModuleData_Get();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) });
            }
            else
            {
                model.ListData = await f.ModuleData_Get();
                var Error = new ErrorViewModel();
                Error.MessageContent = r.MessageContent;
                Error.MessageTitle = r.MessageTitle;
                Error.RequestId = r.RequestId;
                model.Error = Error;
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) });
            }
        }
        #endregion

        #region Menu Data
        public async Task<IActionResult> MenuData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new MenuDataModel();
            try
            {
                DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);
                BotParser botParser = new BotParser();
                var userAgent = Request.Headers["User-Agent"];
                var result = DeviceDetector.GetInfoFromUserAgent(userAgent);

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_UserId")))
                {
                    var model2 = new alertLogin();
                    return await Task.Run(() => RedirectToAction("SignIn", "Home", model2));
                }
                else
                {
                    ViewBag.UserId = HttpContext.Session.GetString("_UserId");
                    ViewBag.Device = result.Match.DeviceType.ToString();
                    Console.WriteLine(ViewBag.Device);
                    model.ListData = await f.MenuData_Get();
                    return await Task.Run(() => View(model));
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => View(model));
            }
        }

        [NoDirectAccess]
        public async Task<IActionResult> MenuData_Form(int id = 0)
        {
            var model = new MenuData();
            try
            {
                if (id == 0)
                {
                    model.idMenu = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.MenuData_GetById(id);

                    if (model == null)
                    {
                        return NotFound();
                    }
                    return await Task.Run(() => View(model));
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => View(model));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenuData_Save([Bind("idMenu,Action,Controller,NamaMenu,Img,Platform,Status,File_Img")] MenuData data)
        {
            if (ModelState.IsValid)
            {
                string NamaFolder = "FileMenu";
                var r = new ErrorViewModel();
                string webRootPath = _env.WebRootPath;
                var uploads = Path.Combine(webRootPath, NamaFolder);
                try
                {
                    if (data.File_Img != null)
                    {
                        if (data.File_Img.Length > 0)
                        {
                            string TypeFile = data.File_Img.ContentType;
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            string format = "." + TypeFile.Replace("image/", "");
                            string FileName = regex.Replace(data.idMenu.ToString(), "") + format;
                            string pathfiles = Path.Combine(uploads, regex.Replace(data.idMenu.ToString(), "") + ".*");
                            if (data.idMenu != 0)
                            {
                                DirectoryInfo directory = new DirectoryInfo(uploads);
                                var files = directory.GetFiles().ToList();
                                foreach (var img in files)
                                {
                                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(img.Name);

                                    if (fileNameWithoutExt == data.idMenu.ToString())
                                    {
                                        if (img.Exists.Equals(true))
                                        {
                                            img.Delete();
                                        }
                                    }
                                }
                            }

                            using (var fileStream = new FileStream(Path.Combine(uploads, FileName), FileMode.Create))
                            {
                                await data.File_Img.CopyToAsync(fileStream);
                                data.Img = NamaFolder + "//" + FileName;
                            }


                        }
                    }

                    r = await f.MenuData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        var model = new MenuDataModel();
                        model.ListData = await f.MenuData_Get();
                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "MenuData_Table", model) });
                    }
                    else
                    {
                        var Error = new ErrorViewModel();
                        Error.MessageContent = r.MessageContent;
                        Error.MessageTitle = r.MessageTitle;
                        Error.RequestId = r.RequestId;
                        data.Error = Error;
                        return Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Form", data) });
                    }
                }
                catch (Exception ex)
                {
                    var Error = new ErrorViewModel();
                    Error.MessageContent = ex.ToString();
                    Error.MessageTitle = "Error ";
                    Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Form", data) });
                }
            }
            else
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = "State Model tidak valid";
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Form", data) });
            }
        }

        [HttpPost, ActionName("MenuData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenuData_Delete(int Id)
        {
            var model = new MenuDataModel();
            try
            {
                var r = await f.MenuData_Del(Id);
                if (r.MessageStatus == "success")
                {
                    model.Error = null;
                }
                else
                {
                    var Error = new ErrorViewModel();
                    Error.MessageContent = r.MessageContent;
                    Error.MessageTitle = r.MessageTitle;
                    Error.RequestId = r.RequestId;
                    model.Error = Error;
                }

                model.ListData = await f.MenuData_Get();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "MenuData_Table", model) });
            }
            catch(Exception ex)
            {
                model.ListData = await f.MenuData_Get();
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "MenuData_Table", model) });
            }
            
        }
        #endregion

        #region Form Data
        public async Task<IActionResult> FormData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new FormDataModel();
            try
            {
                DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);
                BotParser botParser = new BotParser();
                var userAgent = Request.Headers["User-Agent"];
                var result = DeviceDetector.GetInfoFromUserAgent(userAgent);

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_UserId")))
                {
                    var model2 = new alertLogin();
                    return await Task.Run(() => RedirectToAction("SignIn", "Home", model2));
                }
                else
                {
                    ViewBag.UserId = HttpContext.Session.GetString("_UserId");
                    ViewBag.Device = result.Match.DeviceType.ToString();
                    Console.WriteLine(ViewBag.Device);
                    model.ListData = await f.FormData_Get();
                    return await Task.Run(() => View(model));
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => View(model));
            }
        }

        [NoDirectAccess]
        public async Task<IActionResult> FormData_Form(int id = 0)
        {
            var model = new FormData();
            try
            {
                if (id == 0)
                {
                    model.idLog = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.FormData_GetById(id);

                    if (model == null)
                    {
                        return NotFound();
                    }
                    return await Task.Run(() => View(model));
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => View(model));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormData_Save([Bind("idLog,NamaForm,Type,Id,TextLabel,Action,Controller,ValueInput,ListModel,Urutan,ShowHide,ReadOnly,Enable,Mandatory,IsNumber,FilterBy")] FormData data)
        {
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                string NamaFolder = "FileMenu";
                string webRootPath = _env.WebRootPath;
                var uploads = Path.Combine(webRootPath, NamaFolder);
                try
                {
                    r = await f.FormData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        var model = new FormDataModel();
                        model.ListData = await f.FormData_Get();
                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "FormData_Table", model) });
                    }
                    else
                    {
                        data.Error = r;
                        return Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Form", data) });
                    }
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Form", data) });
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Form", data) });
            }
        }

        [HttpPost, ActionName("FormData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormData_Delete(int Id)
        {
            var model = new FormDataModel();
            model.Error = await f.FormData_Del(Id);
            if (model.Error.MessageStatus == "success")
            {
                model.Error = null;
            }
            
            model.ListData = await f.FormData_Get();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "FormData_Table", model) });
        }
        #endregion
    }
}
