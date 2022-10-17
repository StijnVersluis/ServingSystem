using LogicLayer;

namespace ServingSystem.Models
{
    public class StaffViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UName { get; set; }
        public bool IsAdmin { get; set; }

        public StaffViewModel(Staff staff)
        {
            Id = staff.Id;
            Name = staff.Name;
            UName = staff.UName;
            IsAdmin = staff.IsAdmin;
        }
    }
}
