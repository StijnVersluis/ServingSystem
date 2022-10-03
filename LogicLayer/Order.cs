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
        public DateTime CreatedAt;

        public Order(OrderDTO order)
        {
            Id = order.Id;
            SeatedTableId = order.SeatedTableId;
            StaffId = order.StaffId;
            CreatedAt = order.CreatedAt;
        }

        /// <summary>
        /// Get the products from the Order placed on a Table.
        /// </summary>
        /// <returns>A List of Products.</returns>
        public List<Product> GetProducts(IOrder iOrder)
        {
            return iOrder.GetProducts(this.Id).ConvertAll(x=>new Product(x));
        }

        /// <summary>
        /// Add Product to an Order.
        /// </summary>
        /// <param name="product">The product that is going to be added to the order.</param>
        /// <returns>Successfulness (bool) of adding product to order.</returns>
        public bool AddProduct(IOrder iOrder, Product product)
        {
            return iOrder.AddProduct(this.Id, new ProductDTO(product.Id, product.Name, product.Price, product.Type));
        }

        /// <summary>
        /// Remove Product from an Order.
        /// </summary>
        /// <param name="product">The product that is going to be removed.</param>
        /// <returns>Succesfulness (bool) of remove the product from the order.</returns>
        public bool RemoveProduct(IOrder iOrder, int id)
        {
            return iOrder.RemoveProduct(id);
        }

        /// <summary>
        /// Save Order to Table.
        /// </summary>
        /// <returns>Successfulness (bool) of saving the Order.</returns>
        public bool SaveOrder(IOrder iOrder)
        {
            return iOrder.SaveOrder(this.Id);
        }
    }
}
