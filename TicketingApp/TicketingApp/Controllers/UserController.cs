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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketingApp.Controllers
{
    public class UserController : Controller
    {
        GlobalFunction GF = new GlobalFunction();
        UserFuction f = new UserFuction();

        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public UserController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        #region UserData
        public async Task<IActionResult> UserData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new UserDataModel();
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
                    var Filter = new UserData();
                    model.ListData = await f.UserData_GetSearch(Filter);
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
        public async Task<IActionResult> UserData_Form(int id = 0)
        {
            var model = new UserData();
            try
            {
                if (id == 0)
                {
                    model.id = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.UserData_GetById(id);

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
        public async Task<IActionResult> UserData_Save([Bind("id,username,password,hakakses,NamaLengkap,Email,Gender,NoHp,Photo,Alamat,Status,TanggalLahir,TempatLahir,Agama," +
            "ScanKTP,JenisIdentitas,NoIdentitas,NamaDivisi,NamaPosisi,TglAwalKerja,CreateDate,ModifyDate,CreateBy,ImgLink,File_Photo,File_ScanKTP")] UserData data)
        {
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                string NamaFolder = "FileHRD";
                string webRootPath = _env.WebRootPath;
                var uploads = Path.Combine(webRootPath, NamaFolder);
                try
                {
                    if (data.File_Photo != null)
                    {
                        if (data.File_Photo.Length > 0)
                        {
                            string TypeFile = data.File_Photo.ContentType;
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            string format = "." + TypeFile.Replace("image/", "");
                            string FileName = "Photo_" + regex.Replace(data.id.ToString(), "") + format;

                            if (data.id != 0)
                            {
                                DirectoryInfo directory = new DirectoryInfo(uploads);
                                var files = directory.GetFiles().ToList();
                                foreach (var img in files)
                                {
                                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(img.Name);

                                    if (fileNameWithoutExt == "Photo_"+data.id.ToString())
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
                                await data.File_Photo.CopyToAsync(fileStream);
                                data.Photo = NamaFolder + "//" + FileName;
                            }
                        }
                    }

                    if (data.File_ScanKTP != null)
                    {
                        if (data.File_ScanKTP.Length > 0)
                        {
                            string TypeFile = data.File_ScanKTP.ContentType;
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            string format = "." + TypeFile.Replace("image/", "");
                            string FileName = "ScanKTP_" + regex.Replace(data.id.ToString(), "") + format;

                            if (data.id != 0)
                            {
                                DirectoryInfo directory = new DirectoryInfo(uploads);
                                var files = directory.GetFiles().ToList();
                                foreach (var img in files)
                                {
                                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(img.Name);

                                    if (fileNameWithoutExt == "ScanKTP_" + data.id.ToString())
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
                                await data.File_ScanKTP.CopyToAsync(fileStream);
                                data.ScanKTP = NamaFolder + "//" + FileName;
                            }
                        }
                    }

                    r = await f.UserData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        return await Task.Run(() => Json(new { isValid = true }));
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

        [HttpPost, ActionName("UserData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserData_Delete(int Id)
        {
            var model = new RoleMenuDataModel();
            try
            {
                model.Error = await f.UserData_Del(Id);
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
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle}));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserData_Search([Bind("id,username,password,hakakses,NamaLengkap,Email,Gender,NoHp,Photo,Alamat,Status,TanggalLahir,TempatLahir,Agama," +
            "ScanKTP,JenisIdentitas,NoIdentitas,NamaDivisi,NamaPosisi,TglAwalKerja,CreateDate,ModifyDate,CreateBy,ImgLink")] UserData data)
        {
            var model = new UserDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.UserData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "UserData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.UserData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.UserData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", model) }));
            }
        }
        #endregion

        #region UserAkses
        public async Task<IActionResult> UserAkses()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new UserDataModel();
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
                    var Filter = new UserData();
                    model.ListData = await f.UserData_GetSearch(Filter);
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
        public async Task<IActionResult> UserAkses_Form(int id = 0)
        {
            var model = new UserData();
            try
            {
                if (id == 0)
                {
                    model.id = id;
                    return await Task.Run(() => View(model));
                }
                else
                {
                    model = await f.UserData_GetById(id);

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
        public async Task<IActionResult> UserAkses_Save([Bind("id,username,password,hakakses,NamaLengkap,Email,Gender,NoHp,Photo,Alamat,Status,TanggalLahir,TempatLahir,Agama," +
            "ScanKTP,JenisIdentitas,NoIdentitas,NamaDivisi,NamaPosisi,TglAwalKerja,CreateDate,ModifyDate,CreateBy,ImgLink,File_Photo,File_ScanKTP")] UserData data)
        {
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                string NamaFolder = "FileHRD";
                string webRootPath = _env.WebRootPath;
                var uploads = Path.Combine(webRootPath, NamaFolder);
                try
                {
                    if (data.File_Photo != null)
                    {
                        if (data.File_Photo.Length > 0)
                        {
                            string TypeFile = data.File_Photo.ContentType;
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            string format = "." + TypeFile.Replace("image/", "");
                            string FileName = "Photo_" + regex.Replace(data.id.ToString(), "") + format;

                            if (data.id != 0)
                            {
                                DirectoryInfo directory = new DirectoryInfo(uploads);
                                var files = directory.GetFiles().ToList();
                                foreach (var img in files)
                                {
                                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(img.Name);

                                    if (fileNameWithoutExt == "Photo_" + data.id.ToString())
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
                                await data.File_Photo.CopyToAsync(fileStream);
                                data.Photo = NamaFolder + "//" + FileName;
                            }
                        }
                    }

                    if (data.File_ScanKTP != null)
                    {
                        if (data.File_ScanKTP.Length > 0)
                        {
                            string TypeFile = data.File_ScanKTP.ContentType;
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            string format = "." + TypeFile.Replace("image/", "");
                            string FileName = "ScanKTP_" + regex.Replace(data.id.ToString(), "") + format;

                            if (data.id != 0)
                            {
                                DirectoryInfo directory = new DirectoryInfo(uploads);
                                var files = directory.GetFiles().ToList();
                                foreach (var img in files)
                                {
                                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(img.Name);

                                    if (fileNameWithoutExt == "ScanKTP_" + data.id.ToString())
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
                                await data.File_ScanKTP.CopyToAsync(fileStream);
                                data.ScanKTP = NamaFolder + "//" + FileName;
                            }
                        }
                    }

                    r = await f.UserData_Save(data);
                    if (r.MessageStatus == "success")
                    {
                        return await Task.Run(() => Json(new { isValid = true }));
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

        [HttpPost, ActionName("UserData_Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAkses_Delete(int Id)
        {
            var model = new RoleMenuDataModel();
            try
            {
                model.Error = await f.UserData_Del(Id);
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
        public async Task<IActionResult> UserAkses_Search([Bind("id,username,password,hakakses,NamaLengkap,Email,Gender,NoHp,Photo,Alamat,Status,TanggalLahir,TempatLahir,Agama," +
            "ScanKTP,JenisIdentitas,NoIdentitas,NamaDivisi,NamaPosisi,TglAwalKerja,CreateDate,ModifyDate,CreateBy,ImgLink")] UserData data)
        {
            var model = new UserDataModel();
            var r = new ErrorViewModel();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ListData = await f.UserData_GetSearch(data);
                    return await Task.Run(() => Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "UserData_Table", model) }));
                }
                catch (Exception ex)
                {
                    r.MessageContent = ex.ToString();
                    r.MessageTitle = "Error ";
                    r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                    model.Error = r;
                    model.ListData = await f.UserData_Get();
                    return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", data) }));
                }
            }
            else
            {
                r.MessageContent = "State Model tidak valid";
                r.MessageTitle = "Error ";
                r.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                model.Error = r;
                model.ListData = await f.UserData_Get();
                return await Task.Run(() => Json(new { isValid = false, message = r.MessageContent, title = r.MessageTitle, html = Helper.RenderRazorViewToString(this, "RoleMenuData_Table", model) }));
            }
        }
        #endregion
    }
}
