using InterfaceLayer;
using InterfaceLayer.DTO;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.STUB
{
    internal class OrderSTUB : IOrder
    {
        public List<OrderDTO> orders;
        public List<OrderRuleDTO> orderRules;
        public OrderSTUB()
        {
            orders = new List<OrderDTO>()
            {
                new OrderDTO(1, 1, 1, new(2022, 12, 23, 10, 1, 0)),
                new OrderDTO(2, 1, 1, new(2022, 12, 23, 10, 30, 0)),
                new OrderDTO(3, 2, 1, new(2022, 12, 23, 10, 2, 0)),
            };
            orderRules = new List<OrderRuleDTO>()
            {
                new OrderRuleDTO(1, orders[0].Id, 1, 2, 5),
                new OrderRuleDTO(2, orders[0].Id, 2, 4, 4),
                new OrderRuleDTO(3, orders[1].Id, 1, 3, 5),
                new OrderRuleDTO(4, orders[1].Id, 2, 1, 4),
                new OrderRuleDTO(5, orders[2].Id, 1, 1, 4),
            };
        }

        public List<OrderRuleDTO> GetProducts(int id)
        {
            return orderRules.Where(rule => rule.OrderId == id).ToList();
        }

        public bool AddProduct(int orderId, ProductDTO product)
        {
            var success = false;
            OrderRuleDTO existingRule = orderRules.Find(order=>order.OrderId == orderId && order.ProductId == product.Id);
            if (existingRule != null) { existingRule.Amount++; success = true; }
            else
            {
                OrderRuleDTO orderRule = new OrderRuleDTO(orderRules.Count, orderId, product.Id, 1, product.Price);
                orderRules.Add(orderRule);
                success = true;
            }
            return success;
        }

        public bool RemoveProduct(int id, int productId)
        {
            orderRules.Find(rule => rule.OrderId == id && rule.ProductId == productId).Amount--;
            if (orderRules.Find(rule => rule.OrderId == id && rule.ProductId == productId).Amount == 0) return orderRules.Remove(orderRules.Find(rule => rule.OrderId == id && rule.ProductId == productId));
            else return false;
        }

        public bool SaveOrder(int id)
        {
            return orders.Any(order => order.Id == id);
        }
    }
}
