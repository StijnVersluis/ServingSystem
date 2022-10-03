using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IOrder
    {
        public List<ProductDTO> GetProducts(int id);
        public bool AddProduct(int orderId, ProductDTO product);
        public bool RemoveProduct(int id);
        public bool SaveOrder(int id);
    }
}
