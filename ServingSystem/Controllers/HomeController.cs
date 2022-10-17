using DataLayer;
using InterfaceLayer;
using LogicLayer;
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
            ViewData["CurrentStaff"] = new StaffViewModel(staffContainer.GetLoggedInStaff());
            ViewData["AllNonSeatedTables"] = tableContainer.GetAllNonSeatedTables().ConvertAll(x => new TableViewModel(x));
            ViewData["SeatedTables"] = tableContainer.GetAllSeatedTables().ConvertAll(x => new TableViewModel(x, x.Time_Arrived));
            if (staffContainer.IsLoggedIn() == false) return RedirectToAction("Login");
            else return View();
        }

        [HttpPost]
        public IActionResult OpenTable(int id)
        {
            tableContainer.OpenTable(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            if (staffContainer.IsLoggedIn() == false) return View();
            else return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            if (staffContainer.Logout() == false) return RedirectToAction(nameof(Index));
            else return RedirectToAction(nameof(Login));
        }


        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                return View(nameof(Index));
            }
            if (staffContainer.AttemptLogin(name, password)) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Index", "Login");
        }

        public IActionResult Privacy()
        {
            if (staffContainer.IsLoggedIn() == false) return Redirect("/");
            else return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
