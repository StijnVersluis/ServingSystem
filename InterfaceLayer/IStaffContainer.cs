using InterfaceLayer.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    public interface IStaffContainer
    {
        public bool AttemptLogin(string uName, string password);
        public StaffDTO GetLoggedInStaff();
        public bool IsLoggedIn();
    }
}
