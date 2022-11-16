using DataLayer;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServingSystem.Controllers
{
    public class OrderController : Controller
    {
        private static readonly TableDAL tDAL = new();
        private static readonly OrderDAL oDAL = new();
        private readonly TableContainer tableContainer = new(tDAL);
        private readonly ProductContainer productContainer = new(new ProductDAL());
        private readonly StaffContainer userContainer = new(new StaffDAL());
        public IActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct([FromBody] OrderProductJson collection)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            var order = tableContainer.GetTable(collection.TableId).GetOpenOrder(tDAL);
            if (order != null)
            {
                order.AddProduct(oDAL, productContainer.GetProduct(collection.ProductId));
                return View("GetProducts", new { id = order.Id });
            } else
            {
                return RedirectToAction("Details", "Table", new { id = collection.TableId, error = "Please create an order!" });
            }
        }

        [HttpPost]
        public ActionResult RemoveProduct([FromBody] OrderProductJson collection)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            var order = tableContainer.GetTable(collection.TableId).GetOpenOrder(tDAL);
            if (order != null)
            {
                order.RemoveProduct(oDAL, collection.ProductId);
                return View("GetProducts", new { id = order.Id });
            } else
            {
                return RedirectToAction("Details", "Table", new { id = collection.TableId, error = "Please create an order!" });
            }
        }

        public ActionResult GetProducts(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult Save(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            var order = tableContainer.GetTable(id).GetOpenOrder(tDAL);
            if (order != null )
            {
                order.SaveOrder(oDAL);
            }
            return RedirectToAction("Details", "Table", new { id });
        }

        public bool IsLoggedIn()
        {
            var loggedInId = HttpContext.Session.GetInt32("UserId");
            if (loggedInId != null && loggedInId != 0) return true;
            else return false;
        }
    }
}
