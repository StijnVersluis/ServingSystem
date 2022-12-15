using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IStaff
    {
        //public string GetCode(StaffDTO staff);
        public StaffDTO Edit(StaffDTO staff);
    }
}
