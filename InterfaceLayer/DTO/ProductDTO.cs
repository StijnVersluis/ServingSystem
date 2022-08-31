using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.DTO
{
    public class ProductDTO
    {
        public int Id { private set; get; }
        public string Name { private set; get; }
        public double Price;

        public ProductDTO(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
