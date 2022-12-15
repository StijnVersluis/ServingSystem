using DataLayer;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServingSystem.Models;

namespace ServingSystem.Controllers
{
    public class StaffController : Controller
    {
        private static readonly StaffDAL sDAL = new();
        private readonly StaffContainer staffcontainer = new StaffContainer(sDAL);
        // GET: StaffController
        public ActionResult Index()
        {
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            return View(staffcontainer.GetAll().ConvertAll(Staff => new StaffViewModel(Staff)));
        }

        // GET: StaffController/Details/5
        public ActionResult Details(int id)
        {
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            return View(new StaffViewModel(staffcontainer.GetUserById(id)));
        }

        // GET: StaffController/Create
        public ActionResult Create()
        {
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            Staff GoCreate = new Staff(collection["Name"], collection["UName"], collection["code"], bool.Parse(collection["IsAdmin"][0]));
            try
            {
                bool UserNameExist = staffcontainer.GetUserByUserName(collection["UName"]) != null;

                if (!UserNameExist)
                {
                    Staff newStaff = staffcontainer.CreateUser(GoCreate);
                    return RedirectToAction(nameof(Details), new { id = newStaff.Id });
                }
                else
                {
                    ViewData["Error"] = "Username is already taken!";
                    return View(new StaffViewModel(collection["Name"], collection["UName"], collection["code"], bool.Parse(collection["IsAdmin"][0])));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            return View(new StaffViewModel(staffcontainer.GetUserById(id)));
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var code = collection["Code"];
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            try
            {
                Staff staf = staffcontainer.GetUserById(id).Edit(new Staff(id, collection["Name"], collection["UName"], collection["Code"], bool.Parse(collection["IsAdmin"][0])), sDAL);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new StaffViewModel(staffcontainer.GetUserById(id)));
            }
        }

        // GET: StaffController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!LoginActions.IsAdmin(this.HttpContext))
                return RedirectToAction("Index", "Home");
            return View(new StaffViewModel(staffcontainer.GetUserById(id)));
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (!LoginActions.IsAdmin(this.HttpContext)) return RedirectToAction("Index", "Home");
            try
            {
                staffcontainer.DeleteUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new StaffViewModel(staffcontainer.GetUserById(id)));
            }
        }
    }
}
