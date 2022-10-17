using DataLayer;
using LogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace ServingSystem.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
