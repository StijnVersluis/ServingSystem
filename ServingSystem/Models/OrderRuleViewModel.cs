using LogicLayer;

namespace ServingSystem.Models
{
    public class OrderRuleViewModel
    {
        public int Id;
        public int OrderId;
        public int ProductId;
        public int Amount;
        public double ProductPrice;

        public Product LinkedProduct;

        public OrderRuleViewModel(OrderRule order)
        {
            Id = order.Id;
            OrderId = order.OrderId;
            ProductId = order.ProductId;
            Amount = order.Amount;
            ProductPrice = order.ProductPrice;
        }
    }
}
