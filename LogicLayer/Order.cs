using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Order
    {
        public int Id;
        public int SeatedTableId;
        public int StaffId;
        public List<Product> Products;
        public DateTime CreatedAt;

        private IOrder iOrder;

        public Order(OrderDTO order)
        {
            Id = order.Id;
            SeatedTableId = order.SeatedTableId;
            StaffId = order.StaffId;
            Products = order.Products.ConvertAll(x => new Product(x));
            CreatedAt = order.CreatedAt;
        }
        /// <summary>
        /// Get the products from the Order placed on a Table
        /// </summary>
        /// <returns>A List of Products</returns>
        public List<Product> GetProducts()
        {
            Products = new List<Product>();
            iOrder.GetProducts(this.Id).ForEach(product => Products.Add(new Product(product)));
            return Products;
        }
        public void AddProduct(Product product)
        {

        }

        public void RemoveProduct(Product product)
        {

        }

        public void SaveOrder()
        {

        }
    }
}
