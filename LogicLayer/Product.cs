using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Product
    {
        public int Id { private set; get; }
        public string Name { private set; get; }
        public double Price { private set; get; }
        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public Product(ProductDTO product)
        {
            Id = product.Id;
            Price = product.Price;
        }
    }
}
