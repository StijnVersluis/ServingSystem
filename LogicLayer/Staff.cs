using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UName { get; set; }
        public bool IsAdmin { get; set; }

        public Staff(int id, string name, string uName, string code, bool isAdmin)
        {
            Id = id;
            Name = name;
            UName = uName;
            IsAdmin = isAdmin;
        }

        public Staff(StaffDTO staff)
        {
            Id = staff.Id;
            Name = staff.Name;
            UName = staff.UName;
            IsAdmin = staff.IsAdmin;
        }

        public StaffDTO ToDTO()
        {
            return new StaffDTO(Id, Name, UName, IsAdmin);
        }

    }
}
