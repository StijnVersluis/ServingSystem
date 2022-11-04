using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class OrderRule
    {
        public int Id;
        public int OrderId;
        public int ProductId;
        public int Amount;
        public double ProductPrice;

        public Product LinkedProduct;
        public OrderRule(int id, int orderId, int productId, int amount, double productPrice)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            ProductPrice = productPrice;
        }
        public OrderRule(OrderRuleDTO order)
        {
            Id = order.Id;
            OrderId = order.OrderId;
            ProductId = order.ProductId;
            Amount = order.Amount;
            ProductPrice = order.ProductPrice;
        }
        public void LinkProduct(IProductContainer iProductContainer)
        {
            LinkedProduct = new Product(iProductContainer.GetProduct(ProductId));
        }

    }
}
