using LogicLayer;
using DataLayer;
using InterfaceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ServingSystem.Models;
using System;

namespace ServingSystem.Controllers
{
    public class TableController : Controller
    {
        private readonly TableContainer tableContainer = new TableContainer(new TableDAL());

        // GET: TableController/Details/5
        public ActionResult Details(int id)
        {
            Table table = tableContainer.GetTable(id);
            return View(new TableViewModel(table, table.Time_Arrived));
        }

        // GET: TableController/Create
        public ActionResult CloseTable(int id)
        {
            tableContainer.CloseTable(id);
            return RedirectToAction("Index", "Home");
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
}
