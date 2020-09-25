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

        public async Task<IActionResult> ModuleData()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            var model = new ModuleDataModel();
            HttpContext.Session.SetString("_UserId", GF.GenID());
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
        
    }
}
