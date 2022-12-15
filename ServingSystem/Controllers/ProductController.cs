using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogicLayer;
using DataLayer;
using System.Collections;
using ServingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Globalization;

namespace ServingSystem.Controllers
{
    public class ProductController : Controller
    {
        ProductContainer productContainer = new(new ProductDAL());
        // GET: ProductController
        public ActionResult Index()
        {
            List<ProductViewModel> model = productContainer.GetAll().ConvertAll(product => new ProductViewModel(product)).OrderBy((product) => product.ProductType).ToList();

            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(new ProductViewModel(productContainer.GetProduct(id)));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View(new CRUDProductViewModel());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";

                var product = new Product(collection["Name"], Convert.ToDouble(collection["Price"], provider), int.Parse(collection["ProductType"]));
                var canCreate = productContainer.CheckProduct(product);
                if (canCreate) productContainer.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new CRUDProductViewModel(collection["Name"], double.Parse(collection["Price"])));
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productContainer.GetProduct(id);
            return View(new CRUDProductViewModel(product.Id, product.Name, product.Price));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            try
            {
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";

                var product = new Product(int.Parse(collection["Id"]),
                    collection["Name"],
                    Convert.ToDouble(collection["Price"], provider),
                    int.Parse(collection["ProductType"]));
                product.Edit(new ProductDAL());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new ProductViewModel(productContainer.GetProduct(id)));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                productContainer.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
