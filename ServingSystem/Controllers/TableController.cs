using LogicLayer;
using DataLayer;
using InterfaceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ServingSystem.Models;
using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using System.Linq;

namespace ServingSystem.Controllers
{
    public class TableController : Controller
    {
        private static readonly TableDAL tDAL = new();
        private static readonly OrderDAL oDAL = new();
        private readonly TableContainer tableContainer = new(tDAL);
        private readonly ProductContainer productContainer = new(new ProductDAL());
        private readonly StaffContainer userContainer = new(new StaffDAL());

        // GET: TableController/Details/5
        public ActionResult Details(int id, string? error)
        {
            try
            {
                if (!IsLoggedIn()) return RedirectToAction("Login", "Home");

                Table table = tableContainer.GetTable(id);
                var openOrder = table.GetOpenOrder(tDAL);
                if (openOrder != null)
                {
                    ViewData["OpenOrder"] = new OrderViewModel(openOrder, openOrder.GetProducts(oDAL));
                }
                ViewData["Error"] = error;
                ViewData["Products"] = productContainer.GetAll().ConvertAll(x => new ProductViewModel(x));
                ViewData["AllOrders"] = table.GetOrders(tDAL).ConvertAll(x => new OrderViewModel(x, x.GetProducts(oDAL)));
                ViewData["TotalPrice"] = table.GetTotalPrice(tDAL);
                return View(new TableViewModel(table, table.Time_Arrived));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home", new { error = "Something went wrong, try again in a few seconds." });
            }
        }

        public ActionResult CloseTable(int id)
        {
            try
            {
                if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
                var order = tableContainer.GetTable(id).GetOpenOrder(tDAL);
                if (order == null)
                {
                    tableContainer.CloseTable(id);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Details", "Table", new { id = id, error = "Please Save or Remove last order!" });
                }
            } catch(Exception e)
            {
                return RedirectToAction("Details", "Table", new { id = id, error = "Something went wrong, try again in a few second." });
            }
        }

        public ActionResult CreateOrder(int id)
        {
            try
            {
                if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
                tableContainer.GetTable(id).CreateOrder(tDAL, (int)HttpContext.Session.GetInt32("UserId"));
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception e)
            {
                return RedirectToAction("Details", new { id = id, error = "Something went wrong, try again in a few seconds." });
            }
        }

        public ActionResult RemoveOrder(int id)
        {
            try
            {
                if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
                tableContainer.GetTable(id).RemoveOrder(tDAL);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception e)
            {
                return RedirectToAction("Details", new { id = id, error = "Something went wrong, try again in a few seconds." });
            }
        }

        // GET: TableController/Create
        public ActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            return View();
        }

        // POST: TableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            collection.TryGetValue("Name", out var name);
            try
            {
                tableContainer.CreateTable(name);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: TableController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            return View();
        }

        // POST: TableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: TableController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            return View();
        }

        // POST: TableController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetFilteredUnopenTables(string filter)
        {
            if (filter == null || filter == "") return View(tableContainer.GetAllNonSeatedTables().ConvertAll(table => new TableViewModel(table)));
            var tables = tableContainer.GetAllNonSeatedTables()
                .Where(table => table.Name.ToLower().Contains(filter.ToLower()))
                .ToList()
                .ConvertAll(table => new TableViewModel(table.Name));
            return View(tables);
        }

        public bool IsLoggedIn()
        {
            var loggedInId = HttpContext.Session.GetInt32("UserId");
            if (loggedInId != null && loggedInId != 0) return true;
            else return false;
        }
    }
    public class OrderProductJson
    {
        public int TableId { get; set; }
        public int ProductId { get; set; }
    }
}
