using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer.DTO
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UName { get; set; }
        public string Code { get; set; }
        public bool IsAdmin { get; set; }

        public StaffDTO(int id, string name, string uName, bool isAdmin)
        {
            Id = id;
            Name = name;
            UName = uName;
            IsAdmin = isAdmin;
        }
        public StaffDTO(int id, string name, string uName, string code, bool isAdmin)
        {
            Id = id;
            Name = name;
            UName = uName;
            Code = code;
            IsAdmin = isAdmin;
        }
    }
}
