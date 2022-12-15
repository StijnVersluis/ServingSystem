using DataLayer;
using InterfaceLayer;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!LoginActions.IsLoggedIn(this.HttpContext)) return RedirectToAction("Login");
            ViewData["CurrentStaff"] = new StaffViewModel(staffContainer.GetLoggedInStaff((int)HttpContext.Session.GetInt32("UserId")));
            ViewData["AllNonSeatedTables"] = tableContainer.GetAllNonSeatedTables().ConvertAll(x => new TableViewModel(x));
            ViewData["SeatedTables"] = tableContainer.GetAllSeatedTables().ConvertAll(x => new TableViewModel(x, x.Time_Arrived));
            return View();
        }

        [HttpPost]
        public IActionResult OpenTable(int id)
        {
            if (!LoginActions.IsLoggedIn(this.HttpContext)) return RedirectToAction("Login");
            tableContainer.OpenTable(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login(string? error)
        {
            if (error != null) ViewData["error"] = error;
            if (LoginActions.IsLoggedIn(this.HttpContext)) return RedirectToAction(nameof(Index));
            else
            {
                ViewData["LoggedOut"] = true;
                return View();
            }
        }

        public IActionResult Logout()
        {
            LoginActions.LogoutUser(this.HttpContext);
            return RedirectToAction(nameof(Login));
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            ValidationResponse validLogin = LoginActions.Login(username, password, this.HttpContext);
            LoginViewModel login = new LoginViewModel(username, password);
            login.AddErrors(validLogin.Errors);
            if (validLogin.Success) return RedirectToAction(nameof(Index));
            else return View(login);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
