using LogicLayer;
using DataLayer;
using InterfaceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ServingSystem.Models;
using System;
using System.Runtime.InteropServices;

namespace ServingSystem.Controllers
{
    public class TableController : Controller
    {
        private static readonly TableDAL tDAL = new();
        private static readonly OrderDAL oDAL = new();
        private readonly TableContainer tableContainer = new (tDAL);
        private readonly ProductContainer productContainer = new (new ProductDAL());
        private readonly StaffContainer userContainer = new(new StaffDAL());

        // GET: TableController/Details/5
        public ActionResult Details(int id)
        {
            Table table = tableContainer.GetTable(id);
            var openOrder = table.GetOpenOrder(tDAL);
            if (openOrder != null)
            {
                ViewData["OpenOrder"] = new OrderViewModel(openOrder, openOrder.GetProducts(oDAL));
            }
            ViewData["Products"] = productContainer.GetAll().ConvertAll(x => new ProductViewModel(x));
            ViewData["AllOrders"] = table.GetOrders(tDAL).ConvertAll(x => new OrderViewModel(x, x.GetProducts(oDAL)));
            return View(new TableViewModel(table, table.Time_Arrived));
        }

        // GET: TableController/Create
        public ActionResult CloseTable(int id)
        {
            tableContainer.CloseTable(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public void CreateOrder([FromBody] OrderProductJson collection)
        {
            tableContainer.GetTable(collection.TableId).CreateOrder(tDAL, (int)HttpContext.Session.GetInt32("UserId"));
        }

        // GET: TableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
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
            return View();
        }

        // POST: TableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
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
            return View();
        }

        // POST: TableController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
    public class OrderProductJson
    {
        public int TableId { get; set; }
        public int ProductId { get; set; }
    }
}
