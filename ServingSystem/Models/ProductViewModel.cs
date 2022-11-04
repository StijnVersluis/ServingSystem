using LogicLayer;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace ServingSystem.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int ProductType { get; set; }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            ProductType = product.Type;
        }
    }
}
