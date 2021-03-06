﻿using System;
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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                    var data = new ModuleData();
                    data.Status = 1;
                    model.ListData = await f.ModuleData_GetSearch(data);
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
                        return await Task.Run(() => Json(new { isValid = true }));
                    }
                    else
                    {
                        var Error = new ErrorViewModel();
                        Error.MessageContent = r.MessageContent;
                        Error.MessageTitle = r.MessageTitle;
                        Error.RequestId = r.RequestId;
                        data.Error = Error;
                        return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Form", data) }));
                    }
                }
                catch (Exception ex)
                {
                    var Error = new ErrorViewModel();
                    Error.MessageContent = ex.ToString();
                    Error.MessageTitle = "Error ";
                    Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Form", data) }));
                }
            }
            else
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = "State Model tidak valid";
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Form", data) }));
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
                return await Task.Run(() => Json(new { isValid = true }));
            }
            else
            {
                model.ListData = await f.ModuleData_Get();
                var Error = new ErrorViewModel();
                Error.MessageContent = r.MessageContent;
                Error.MessageTitle = r.MessageTitle;
                Error.RequestId = r.RequestId;
                model.Error = Error;
                return await Task.Run(() => Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) }));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModuleData_Search([Bind("IdModul,NamaModule,Action,Controller,Img,Status")] ModuleData data)
        {
            var model = new ModuleDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.ModuleData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.ModuleData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.ModuleData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "ModuleData_Table", model) }));
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
                    var data = new MenuData();
                    model.ListData = await f.MenuData_GetSearch(data);
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
                        return await Task.Run(() => Json(new { isValid = true}));
                    }
                    else
                    {
                        var Error = new ErrorViewModel();
                        Error.MessageContent = r.MessageContent;
                        Error.MessageTitle = r.MessageTitle;
                        Error.RequestId = r.RequestId;
                        data.Error = Error;
                        return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Form", data) }));
                    }
                }
                catch (Exception ex)
                {
                    var Error = new ErrorViewModel();
                    Error.MessageContent = ex.ToString();
                    Error.MessageTitle = "Error ";
                    Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Form", data) }));
                }
            }
            else
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = "State Model tidak valid";
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = Error.MessageContent, title = Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Form", data) }));
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
                return await Task.Run(() => Json(new { isValid = true }));
            }
            catch(Exception ex)
            {
                model.ListData = await f.MenuData_Get();
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = Error;
                return await Task.Run(() => Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "MenuData_Table", model) }));
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenuData_Search([Bind("idMenu,Action,Controller,NamaMenu,Img,Platform,Status")] MenuData data)
        {
            var model = new MenuDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.MenuData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "MenuData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.MenuData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Table", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.MenuData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "MenuData_Table", model) }));
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
                        return await Task.Run(() => Json(new { isValid = true }));
                    }
                    else
                    {
                        data.Error = r;
                        return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Form", data) }));
                    }
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Form", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Form", data) }));
            }
        }

        [HttpPost, ActionName("FormData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormData_Delete(int Id)
        {

            var model = new FormDataModel();
            try
            {
                model.Error = await f.FormData_Del(Id);
                if (model.Error.MessageStatus == "success")
                {
                    model.Error = null;
                    return await Task.Run(() => Json(new { isValid = true }));
                }
                else
                {
                    return await Task.Run(() => Json(new { isValid = false, message = model.Error.MessageContent, title = model.Error.MessageTitle }));
                }
            }
            catch (Exception ex)
            {
                var r = new ErrorViewModel();
                r.MessageContent = ex.ToString();
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle }));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormData_Search([Bind("idLog,NamaForm,Type,Id,TextLabel,Action,Controller,ValueInput,ListModel,Urutan,ShowHide,ReadOnly,Enable,Mandatory,IsNumber,FilterBy")] FormData data)
        {
            var model = new FormDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.FormData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "FormData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.FormData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Table", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.FormData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "FormData_Table", model) }));
            }
        }

        #endregion

        #region  ListItem Data
        public async Task<IActionResult> ListItemData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new ListItemDataModel();
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
                    var filter = new ListItemData();
                    model.ListData = await f.ListItemData_GetSearch(filter);
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
        public async Task<IActionResult> ListItemData_Form(int id = 0)
        {
            var model = new ListItemData();
            try
            {
                if (id == 0)
                {
                    model.id = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.ListItemData_GetById(id);

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
        public async Task<IActionResult> ListItemData_Save([Bind("id,ListName,Urutan,Text,Value")] ListItemData data)
        {
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    r = await f.ListItemData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        var model = new ListItemDataModel();
                        model.ListData = await f.ListItemData_GetSearch(data);
                        return await Task.Run(() => Json(new { isValid = true }));
                    }
                    else
                    {
                        data.Error = r;
                        return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle}));
                    }
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle}));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle}));
            }
        }

        [HttpPost, ActionName("ListItemData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListItemData_Delete(int Id)
        {
            var model = new ListItemDataModel();
            try
            {
                model.Error = await f.ListItemData_Del(Id);
                if (model.Error.MessageStatus == "success")
                {
                    model.Error = null;
                    return await Task.Run(() => Json(new { isValid = true }));
                }
                else
                {

                    return await Task.Run(() => Json(new { isValid = false, message = model.Error.MessageContent, title = model.Error.MessageTitle }));
                }
            }
            catch (Exception ex)
            {
                var r = new ErrorViewModel();
                r.MessageContent = ex.ToString();
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle}));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListItemData_Search([Bind("id,ListName,Urutan,Text,Value")] ListItemData data)
        {
            var model = new ListItemDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.ListItemData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ListItemData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.ListItemData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "ListItemData_Table", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.ListItemData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "ListItemData_Table", model) }));
            }
        }
        #endregion

        #region Role Menu
        public async Task<IActionResult> RoleMenuData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new RoleMenuDataModel();
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
                    var search = new RoleMenuData();
                    model.ListData = await f.RoleMenuData_GetSearch(search);
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
        public async Task<IActionResult> RoleMenuData_Form(int id = 0)
        {
            var model = new RoleMenuData();
            try
            {
                if (id == 0)
                {
                    model.IdRole = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.RoleMenuData_GetById(id);

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
        public async Task<IActionResult> RoleMenuData_Save([Bind("IdRole,IdModule,Posisi,IdParent,Urutan,IdMenu")] RoleMenuData data)
        {
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    r = await f.RoleMenuData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        return await Task.Run(() => Json(new { isValid = true}));
                    }
                    else
                    {
                        data.Error = r;
                        return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Form", data) }));
                    }
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Form", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Form", data) }));
            }
        }

        [HttpPost, ActionName("RoleMenuData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleMenuData_Delete(int Id)
        {
            var model = new RoleMenuDataModel();
            try
            {
                model.Error = await f.RoleMenuData_Del(Id);
                if (model.Error.MessageStatus == "success")
                {
                    model.Error = null;
                    return await Task.Run(() => Json(new { isValid = true }));
                }
                else
                {
                    model.ListData = await f.RoleMenuData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = model.Error.MessageContent, title = model.Error.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", model) }));
                }
            }
            catch (Exception ex)
            {
                var r = new ErrorViewModel();
                r.MessageContent = ex.ToString();
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", model) }));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleMenuData_Search([Bind("IdRole,IdModule,Posisi,IdParent,Urutan,IdMenu")] RoleMenuData data)
        {
            var model = new RoleMenuDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.RoleMenuData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.RoleMenuData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.RoleMenuData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", model) }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> RoleMenuData_GetMenu(int Id)
        {
            var model = new List<MenuData>();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model = await f.MenuData_GetMenuByIdModule(Id);
                    return await Task.Run(() => Json(new { isValid = true, model}));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle}));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> RoleMenuData_GetParent(int IdModule,int IdMenu,int IdPosisi)
        {
            var model = new List<SelectListItem>();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model = await f.MenuData_GetParent(IdModule,IdMenu,IdPosisi);
                    return await Task.Run(() => Json(new { isValid = true, model }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle }));
            }
        }

        #endregion

        
    }
}
