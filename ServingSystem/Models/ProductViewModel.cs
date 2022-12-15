using DataLayer;
using LogicLayer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServingSystem.Models
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        [Required]
        [MinLength(5)]
        public string Name { set; get; }
        [Required]
        public double Price { set; get; }
        [Range(1,3)]
        public int ProductType { set; get; }

        [DisplayName("Product Type")]
        public string ProductTypeName { set; get; }

        public ProductViewModel(int id, string name, double price, int producttype)
        {
            Id = id;
            Name = name;
            Price = price;
            ProductType = producttype;
        }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            ProductType = product.Type;
            ProductTypeName = product.GetProductTypeName(new ProductDAL());
        }
    }
}
