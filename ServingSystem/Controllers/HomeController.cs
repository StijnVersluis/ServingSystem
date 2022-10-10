using DataLayer;
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
        private StaffContainer StaffContainer = new StaffContainer(new StaffDAL());

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (StaffContainer.IsLoggedIn() == false) return Redirect("/");
            else return View("Index");
        }

        public IActionResult Login()
        {
            if (StaffContainer.IsLoggedIn() == false) return View();
            else return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            if (StaffContainer.IsLoggedIn() == false) return Redirect("/");
            else return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
