using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class StaffContainer
    {
        private IStaffContainer sCont;

        public StaffContainer(IStaffContainer staffContainer)
        {
            sCont = staffContainer;
        }

        public bool AttemptLogin(string uName, string password)
        {
            return sCont.AttemptLogin(uName.ToLower(), password);
        }
        public bool IsLoggedIn()
        {
            return sCont.IsLoggedIn();
        }
        public bool Logout()
        {
            return sCont.Logout();
        }
        public Staff GetLoggedInStaff()
        {
            return new Staff(sCont.GetLoggedInStaff());
        }
    }
}
