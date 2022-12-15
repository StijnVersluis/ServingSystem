using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.Frameworks;
using UnitTest.STUB;
using InterfaceLayer.DTO;

namespace UnitTest
{
    [TestClass]
    public class StaffTests
    {
        //Method for checking StaffDTO == Staff
        public bool CheckStaffDTOandStaff(StaffDTO staffDTO, Staff staff)
        {
            bool success = true;
            if (staffDTO.Id != 0 && staffDTO.Id != staff.Id) success = false;
            if (staffDTO.Name != staff.Name) success = false;
            if (staffDTO.UName != staff.UName) success = false;
            if (staffDTO.IsAdmin != staff.IsAdmin) success = false;
            return success;
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff ResponseStaff = sCont.GetUserById(1);
            var ExpectedStaff = sSTUB.staffList[0];

            Assert.IsNotNull(ResponseStaff);
            Assert.IsTrue(CheckStaffDTOandStaff(ExpectedStaff, ResponseStaff));
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff ResponseStaff = sCont.GetUserByUserName("Kimby");
            var ExpectedStaff = sSTUB.staffList[0];

            Assert.IsNotNull(ResponseStaff);
            Assert.IsTrue(CheckStaffDTOandStaff(ExpectedStaff, ResponseStaff));
        }

        [TestMethod]
        public void CreateUserTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff newUser = new Staff("TestMethodUser", "TMethUser", "200200", false);

            StaffDTO ExpectedStaff = newUser.ToDTO();
            Staff ResponseStaff = sCont.CreateUser(newUser);

            Assert.AreEqual(4, sCont.GetAll().Count);
            Assert.IsNotNull(ResponseStaff);
            Assert.IsTrue(CheckStaffDTOandStaff(ExpectedStaff, ResponseStaff));
        }
        [TestMethod]
        public void EditUserTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff oldStaff = sCont.GetUserById(1);
            Staff newStaff = new Staff(oldStaff.Id, "Kimberry", "Kimby", "10002", true);
            Staff expectedStaff = newStaff;

            Staff response = oldStaff.Edit(newStaff, sSTUB);

            Assert.IsNotNull(response);
            Assert.AreEqual(1, expectedStaff.Id);
            Assert.AreEqual(expectedStaff.Id, response.Id);
            Assert.AreEqual(expectedStaff.Name, response.Name);
            Assert.AreEqual(expectedStaff.UName, response.UName);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff user = sCont.GetUserByUserName("User1");

            var response = sCont.DeleteUser(user.Id);

            Assert.AreEqual(2, sCont.GetAll().Count);
            Assert.IsTrue(response);
        }
        [TestMethod]
        public void AttemptLoginTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            int loggedInId = sCont.AttemptLogin("Kimby", "10001");

            Assert.IsNotNull(loggedInId);
            Assert.AreEqual(1, loggedInId);
        }
        [TestMethod]
        public void AttemptLoginFail()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            int loggedInId = sCont.AttemptLogin("Kimby", "10002");

            Assert.IsNotNull(loggedInId);
            Assert.AreEqual(0, loggedInId);
        }
        [TestMethod]
        public void GetLoggedInStaffTest()
        {
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            var loggedIn = sCont.GetLoggedInStaff(1);

            Assert.IsNotNull(loggedIn);
            Assert.AreEqual(1, loggedIn.Id);
            Assert.AreEqual("Kimberley", loggedIn.Name);
        }
    }
}
