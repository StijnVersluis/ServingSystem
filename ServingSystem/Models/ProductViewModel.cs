using System.ComponentModel.DataAnnotations;

namespace ServingSystem.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int ProductType { get; set; }
    }
}
