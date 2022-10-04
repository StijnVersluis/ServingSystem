using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.DTO
{
    public class OrderRuleDTO
    {
        public int Id;
        public int OrderId;
        public int ProductId;
        public int Amount;
        public double ProductPrice;

        public OrderRuleDTO(int id, int orderId, int productId, int amount, double productPrice)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            ProductPrice = productPrice;
        }

    }
}
