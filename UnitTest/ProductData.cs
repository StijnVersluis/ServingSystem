using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public static class ProductData
    {
        public static Product Cola = new Product(2, "Cola", 2.8, 2);
        public static Product Ice_Tea = new Product(3, "Ice Tea", 3, 2);
        public static Product Pasta = new Product(7, "Pasta", 6.99, 1);
        public static Product Pizza = new Product(8, "Pizza", 5.99, 1);
    }
}
