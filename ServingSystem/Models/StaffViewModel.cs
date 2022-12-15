using LogicLayer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServingSystem.Models
{
    public class StaffViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Username")]
        [MinLength(5)]
        public string UName { get; set; }
        [DisplayName("Passcode")]
        [MinLength(5)]
        [MaxLength(10)]
        public string Code { get; private set; }
        public bool IsAdmin { get; set; }

        public StaffViewModel(int id, string name, string uName, string code, bool isAdmin)
        {
            Id = id;
            Name = name;
            UName = uName;
            Code = code;
            IsAdmin = isAdmin;
        }
        public StaffViewModel(string name, string uName, string code, bool isAdmin)
        {
            Name = name;
            UName = uName;
            Code = code;
            IsAdmin = isAdmin;
        }

        public StaffViewModel(Staff staff)
        {
            Id = staff.Id;
            Name = staff.Name;
            UName = staff.UName;
            IsAdmin = staff.IsAdmin;
        }
    }
}
