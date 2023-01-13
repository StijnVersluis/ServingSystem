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
        public void CheckStaffDTOandStaff(StaffDTO staffDTO, Staff staff)
        {
            Assert.AreEqual(staffDTO.Id, staff.Id);
            Assert.AreEqual(staffDTO.Name, staff.Name);
            Assert.AreEqual(staffDTO.UName, staff.UName);
            Assert.AreEqual(staffDTO.IsAdmin, staff.IsAdmin);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            //Arrange
            var staff = new Staff(1, "Staff1", "Staff01", "0000", false);

            //Assert
            Assert.AreEqual(1, staff.Id);
            Assert.AreEqual("Staff1", staff.Name);
            Assert.AreEqual("Staff01", staff.UName);
            Assert.AreEqual("0000", staff.Code);
            Assert.IsFalse(staff.IsAdmin);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);
            var staffId = 1;
            var ExpectedStaff = sSTUB.staffList.Find(staff=>staff.Id == staffId);

            //Act
            Staff ResponseStaff = sCont.GetUserById(staffId);

            //Assert
            Assert.IsNotNull(ResponseStaff);
            CheckStaffDTOandStaff(ExpectedStaff, ResponseStaff);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);
            var staffName = "Kimby";
            var ExpectedStaff = sSTUB.staffList.Find(staff => staff.UName == staffName);

            //Act
            Staff ResponseStaff = sCont.GetUserByUserName(staffName);

            //Assert
            Assert.IsNotNull(ResponseStaff);
            CheckStaffDTOandStaff(ExpectedStaff, ResponseStaff);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff newUser = new Staff("TestMethodUser", "TMethUser", "200200", false);

            StaffDTO ExpectedStaff = newUser.ToDTO();
            
            //Act
            Staff ResponseStaff = sCont.CreateUser(newUser);
            ExpectedStaff.Id = ResponseStaff.Id;

            //Assert
            Assert.AreEqual(sSTUB.staffList.Count, sCont.GetAll().Count);
            Assert.IsNotNull(ResponseStaff);
            CheckStaffDTOandStaff(ExpectedStaff, ResponseStaff);
        }
        [TestMethod]
        public void EditUserTest()
        {
            //Act
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff oldStaff = sCont.GetUserById(1);
            Staff newStaff = new Staff(oldStaff.Id, "Kimberry", "Kimby", "10002", true);
            StaffDTO expectedStaff = newStaff.ToDTO();
            
            //Act
            Staff response = oldStaff.Edit(newStaff, sSTUB);

            //Assert
            Assert.IsNotNull(response);
            CheckStaffDTOandStaff(expectedStaff, response);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            Staff user = sCont.GetUserByUserName("User1");

            //Act
            var response = sCont.DeleteUser(user.Id);

            //Assert
            Assert.AreEqual(2, sCont.GetAll().Count);
            Assert.IsTrue(response);
        }
        [TestMethod]
        public void AttemptLoginTest()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            //Act
            int loggedInId = sCont.AttemptLogin("Kimby", "10001");
            int expected = sSTUB.loggedInStaff.Id;

            //Assert
            Assert.IsNotNull(loggedInId);
            Assert.AreEqual(expected, loggedInId);
        }
        [TestMethod]
        public void AttemptLoginFail()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            //Act
            int loggedInId = sCont.AttemptLogin("Kimby", "10002");

            //Assert
            Assert.IsNotNull(loggedInId);
            Assert.AreEqual(0, loggedInId);
        }
        [TestMethod]
        public void GetLoggedInStaffTest()
        {
            //Arrange
            StaffSTUB sSTUB = new StaffSTUB();
            StaffContainer sCont = new StaffContainer(sSTUB);

            //Act
            var loggedIn = sCont.GetLoggedInStaff(1);

            //Assert
            Assert.IsNotNull(loggedIn);
            Assert.AreEqual(1, loggedIn.Id);
            Assert.AreEqual("Kimberley", loggedIn.Name);
        }
    }
}
