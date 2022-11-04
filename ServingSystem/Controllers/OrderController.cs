using DataLayer;
using LogicLayer;
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
            return View();
        }


        [HttpPost]
        public ActionResult AddProduct([FromBody] OrderProductJson collection)
        {
            var order = tableContainer.GetTable(collection.TableId).GetOpenOrder(tDAL);
            order.AddProduct(oDAL, productContainer.GetProduct(collection.ProductId));
            return View("GetProducts", new {id=order.Id});
        }

        public ActionResult GetProducts(int id)
        {
            return View();
        }

        public void Save(int id)
        {
            tableContainer.GetTable(id).GetOpenOrder(tDAL).SaveOrder(oDAL);
        }
    }
}
