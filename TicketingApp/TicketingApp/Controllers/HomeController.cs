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
    public class HomeController : Controller
    {
        GlobalFunction GF = new GlobalFunction();
        
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _env;
        public HomeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            Config.ConStr = _configuration.GetConnectionString("Db");
            HttpContext.Session.SetString("_UserId", GF.GenID());
            try
            {
                DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);
                BotParser botParser = new BotParser();
                var userAgent = Request.Headers["User-Agent"];
                var result = DeviceDetector.GetInfoFromUserAgent(userAgent);

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("_UserId")))
                {
                    //return RedirectToAction("SignIn", "Home");
                    var model = new LoginContent();
                    model.Alert = null;
                    return await Task.Run(() => RedirectToAction("SignIn", "Home",model));
                }
                else
                {
                    ViewBag.Device = result.Match.DeviceType.ToString();
                    Console.WriteLine(ViewBag.Device);
                    return await Task.Run(() => View());
                }
            }
            catch (Exception ex)
            {
                var Error = new ErrorViewModel();
                Error.MessageContent = ex.ToString();
                Error.MessageTitle = "Error ";
                Error.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return await Task.Run(() => View());
            }
        }

        public async Task<IActionResult> SignIn(LoginContent data)
        {
            return await Task.Run(() => View(data));
        }

        public async Task<IActionResult> ChatApp()
        {
            return await Task.Run(() => View());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error(ErrorViewModel Log)
        {
            return await Task.Run(() => View(Log));
        }
    }
}
