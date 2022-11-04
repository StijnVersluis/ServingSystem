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
        public List<OrderDTO> GetOrders(int id);
        public double GetTotalPrice(int id);
        public string GetLastOrderText(int id);
        public OrderDTO GetOpenOrder(int id);
        public OrderDTO CreateOrder(int id, int staffId);
        public TableDTO Edit(TableDTO newTable);
    }
}
