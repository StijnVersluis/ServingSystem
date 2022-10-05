using DataLayer;
using LogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace ServingSystem.Controllers
{
    public class LoginController : Controller
    {
        private StaffContainer staffContainer = new StaffContainer(new StaffDAL());

        public IActionResult Index()
        {
            return View();
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
    }
}
