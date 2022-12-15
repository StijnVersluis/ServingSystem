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
        public string Code { get; set; }
        public bool IsAdmin { get; set; }

        public Staff(int id, string name, string uName, bool isAdmin)
        {
            Id = id;
            Name = name;
            UName = uName;
            IsAdmin = isAdmin;
        }
        public Staff(int id, string name, string uName, string code, bool isAdmin)
        {
            Id = id;
            Name = name;
            UName = uName;
            Code = code;
            IsAdmin = isAdmin;
        }
        public Staff(string name, string uName, string code, bool isAdmin)
        {
            Name = name;
            UName = uName;
            Code = code;
            IsAdmin = isAdmin;
        }

        public Staff(StaffDTO staff)
        {
            this.Id = staff.Id;
            Name = staff.Name;
            UName = staff.UName;
            IsAdmin = staff.IsAdmin;
        }

        public StaffDTO ToDTO()
        {
            return new StaffDTO(Id, Name, UName, Code, IsAdmin);
        }

        public Staff Edit(Staff staff, IStaff istaff)
        {
            var result = istaff.Edit(staff.ToDTO());
            return new Staff(result);
        }

    }
}
