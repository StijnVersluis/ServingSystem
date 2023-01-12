using InterfaceLayer;
using InterfaceLayer.DTO;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.STUB
{
    public class StaffSTUB : IStaffContainer, IStaff
    {
        public StaffDTO loggedInStaff;
        /// <summary>A List of staff</summary>
        public List<StaffDTO> staffList;

        public StaffSTUB()
        {
            staffList = new List<StaffDTO>();
            StaffDTO[] list = {
                new StaffDTO(1, "Kimberley", "Kimby", "10001", false),
                new StaffDTO(2, "User1", "User1", "200200", false),
                new StaffDTO(3, "Timmothy", "tim01", "10010", true),
            };
            staffList.AddRange(list);
        }

        public int AttemptLogin(string uName, string password)
        {
            var userToLoginTo = GetUserByUserName(uName);
            if (userToLoginTo != null && userToLoginTo.Code == password)
            {
                loggedInStaff = userToLoginTo;
                return userToLoginTo.Id;
            }
            else return 0;
        }

        public StaffDTO CreateUser(StaffDTO newStaff)
        {
            int newId = staffList.Count;
            newStaff.Id = newId;
            staffList.Add(newStaff);
            return newStaff;
        }

        public bool DeleteUser(int id)
        {
            var remove = staffList.Single(x => x.Id == id);
            return staffList.Remove(remove);
        }

        public StaffDTO Edit(StaffDTO staffDTO)
        {
            staffList.Where(staff => staff.Id == staffDTO.Id).ToList().ForEach(staff => {
                staff.Name = staffDTO.Name;
                staff.UName = staffDTO.UName;
                staff.Code = staffDTO.Code;
                staff.IsAdmin = staffDTO.IsAdmin;
            });
            return staffList.FindLast(staff => staff.Id == staffDTO.Id);
        }

        public List<StaffDTO> GetAll()
        {
            return staffList;
        }

        public StaffDTO GetLoggedInStaff(int id)
        {
            return GetUserById(id);
        }

        public StaffDTO GetUserById(int id)
        {
            return staffList.FindLast(staff => staff.Id == id);
        }

        public StaffDTO GetUserByUserName(string userName)
        {
            return staffList.FindLast(staff => staff.UName.ToLower() == userName.ToLower());
        }
    }
}
