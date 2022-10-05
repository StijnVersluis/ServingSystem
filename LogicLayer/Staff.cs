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

        public Staff(int id, string name, string uName, string code)
        {
            Id = id;
            Name = name;
            UName = uName;
        }

        public Staff(StaffDTO staff)
        {
            Id = staff.Id;
            Name = staff.Name;
            UName = staff.UName;
        }

        public StaffDTO ToDTO()
        {
            return new StaffDTO(Id, Name, UName);
        }

        //public string GetCode(IStaff istaff)
        //{
        //    return istaff.GetCode(this.ToDTO());
        //}

    }
}
