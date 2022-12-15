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
        public string Name { set; get; }
        public double Price { set; get; }
        public int Type { set; get; }

        public ProductDTO(int id, string name, double price, int type)
        {
            Id = id;
            Name = name;
            Price = price;
            Type = type;
        }
        public ProductDTO(string name, double price, int type)
        {
            Name = name;
            Price = price;
            Type = type;
        }
    }
}
