using System.ComponentModel.DataAnnotations;

namespace ServingSystem.Models
{
    public class TableViewModel
    {
        public int Id { private set; get; }
        [Required]
        public string Name { private set; get; }
    }
}
