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
        public double GetTotalPrice();
        public bool AddProduct(ProductDTO product);
        public bool RemoveProduct(ProductDTO product);
        public bool RemoveProduct(ProductDTO product, int amount);
        public TableDTO Edit(TableDTO newTable);
        public bool Remove(TableDTO table);
    }
}
