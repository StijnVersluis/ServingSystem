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
        public int AttemptLogin(string uName, string password);
        public StaffDTO GetLoggedInStaff(int id);
        public List<StaffDTO> GetAll();
        public StaffDTO GetUserByUserName(string userName);
        public StaffDTO GetUserById(int id);
        public StaffDTO CreateUser(StaffDTO newStaff);
        public bool DeleteUser(int id);
    }
}
