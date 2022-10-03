using InterfaceLayer;
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
        public int Type { private set; get; }
        
        public Product(int id, string name, double price, int type)
        {
            Id = id;
            Name = name;
            Price = price;
            Type = type;
        }

        public Product(ProductDTO product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Type = product.Type;
        }
        /// <summary>
        /// Convert this Product to a ProductDTO
        /// </summary>
        /// <returns>A new ProductDTO created from this Product.</returns>
        public ProductDTO ToDTOWithoutId()
        {
            return new ProductDTO(Name, Price, Type);
        }
    }
}
