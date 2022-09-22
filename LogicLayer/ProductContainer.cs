using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProductContainer
    {
        IProductContainer iProductContainer;

        public ProductContainer (IProductContainer iProd)
        {
            iProductContainer = iProd;
        }
        public List<Product> GetAll()
        {
            return iProductContainer.GetAll().ConvertAll(x => new Product(x));
        }
        public List<Product> GetAllOfType(int type)
        {
            return iProductContainer.GetAllOfType(type).ConvertAll(x => new Product(x));
        }
    }

}
