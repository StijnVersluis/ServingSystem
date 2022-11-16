using DataLayer;
using InterfaceLayer;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using ServingSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ServingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private StaffContainer staffContainer = new(new StaffDAL());
        private TableContainer tableContainer = new(new TableDAL());
        //private readonly LoginActions LA = new(HttpContext);

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login");
            ViewData["CurrentStaff"] = new StaffViewModel(staffContainer.GetLoggedInStaff((int)HttpContext.Session.GetInt32("UserId")));
            ViewData["AllNonSeatedTables"] = tableContainer.GetAllNonSeatedTables().ConvertAll(x => new TableViewModel(x));
            ViewData["SeatedTables"] = tableContainer.GetAllSeatedTables().ConvertAll(x => new TableViewModel(x, x.Time_Arrived));
            return View();
        }

        [HttpPost]
        public IActionResult OpenTable(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login");
            tableContainer.OpenTable(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login(string? error)
        {
            if (error != null) ViewData["error"] = error;
            if (IsLoggedIn()) return RedirectToAction(nameof(Index));
            else return View();
        }

        public IActionResult Logout()
        {
            LogoutUser();
            return RedirectToAction(nameof(Login));
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
            {
                return View(nameof(Login));
            }
            var staffId = staffContainer.AttemptLogin(username, password);


            if (staffId != 0)
            {
                HttpContext.Session.SetInt32("UserId", staffId);
                return RedirectToAction(nameof(Index));
            }
            else return RedirectToAction(nameof(Login), new { error = "Inloggegevens niet gevonden" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool IsLoggedIn()
        {
            var loggedInId = HttpContext.Session.GetInt32("UserId");
            if (loggedInId != null && loggedInId != 0) return true;
            else return false;
        }

        public void LogoutUser()
        {
            HttpContext.Session.Remove("UserId");
        }
    }
}
