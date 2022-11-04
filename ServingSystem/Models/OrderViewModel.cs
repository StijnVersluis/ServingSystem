using DataLayer;
using InterfaceLayer;
using LogicLayer;
using System;
using System.Collections.Generic;

namespace ServingSystem.Models
{
    public class OrderViewModel
    {

        public int Id;
        public int SeatedTableId;
        public int StaffId;
        public DateTime CreatedAt;
        public List<OrderRule> OrderRules;

        private static readonly IProductContainer iProduct = new ProductDAL();

        public OrderViewModel(Order order, List<OrderRule> orderRules)
        {
            Id = order.Id;
            SeatedTableId = order.SeatedTableId;
            StaffId = order.StaffId;
            CreatedAt = order.CreatedAt;
            OrderRules = orderRules;
            foreach(OrderRule orderRule in OrderRules)
            {
                orderRule.LinkProduct(iProduct);
            }
        }
    }
}
