using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface ITable
    {
        public List<ProductDTO> GetProducts();
        public List<OrderDTO> GetOrders();
        public double GetTotalPrice();
        public OrderDTO CreateOrder(int id, int staffId);
        public TableDTO Edit(TableDTO newTable);
        public bool Remove(TableDTO table);
    }
}
