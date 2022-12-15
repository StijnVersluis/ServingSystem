using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Product
    {
        public int Id { private set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public int Type { set; get; }

        public Product(int id, string name, double price, int type)
        {
            Id = id;
            Name = name;
            Price = price;
            Type = type;
        }
        public Product(string name, double price, int type)
        {
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
        public ProductDTO ToDTO()
        {
            return new ProductDTO(Id, Name, Price, Type);
        }
        public ProductDTO ToDTOWithoutId()
        {
            return new ProductDTO(Name, Price, Type);
        }

        public string GetProductTypeName(IProduct iproduct)
        {
            return iproduct.GetProductTypeName(this.Type);
        }

        public bool Edit(IProduct iproduct)
        {
            return iproduct.Edit(this.ToDTO());
        }
    }
}
