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

        public int AttemptLogin(string uName, string password)
        {
            return sCont.AttemptLogin(uName.ToLower(), password);
        }

        public Staff GetLoggedInStaff(int id)
        {
            return new Staff(sCont.GetLoggedInStaff(id));
        }

        public List<Staff> GetAll()
        {
            return sCont.GetAll().ConvertAll(staffDTO => new Staff(staffDTO));
        }

        public Staff GetUserByUserName(string username)
        {
            var user = sCont.GetUserByUserName(username);
            if (user == null) return null;
            return new Staff(user);
        }

        public Staff GetUserById(int id)
        {
            var user = sCont.GetUserById(id);
            if (user == null) return null;
            return new Staff(user);
        }

        public Staff CreateUser(Staff newStaff)
        {
            var user = sCont.CreateUser(newStaff.ToDTO());
            if (user == null) return null;
            return new Staff(user);
        }

        public bool DeleteUser(int id)
        {
            return sCont.DeleteUser(id);
        }
    }
}
