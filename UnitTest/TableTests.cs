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
using System.ComponentModel;
using System.Security.Permissions;
using InterfaceLayer;

namespace UnitTest
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            //Arrange
            var table = new Table(1, "T-01");

            //Assert
            Assert.AreEqual(1, table.Id);
            Assert.AreEqual("T-01", table.Name);
        }
        [TestMethod]
        public void GetAllTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);
            var expected = stub.tables.ConvertAll(tableDTO => new Table(tableDTO));

            //Act
            var actual = container.GetAll();

            //Assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
        [TestMethod]
        public void GetTableTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;

            var expected = new Table(stub.tables.Find(table => table.Id == tableId));

            //Act
            var actual = container.GetTable(1);

            //Assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }
        [TestMethod]
        public void GetAllNonSeatedTablesTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var expected = stub.tables.FindAll(table => table.Time_Arrived.Year != 2022).ConvertAll(tableDTO => new Table(tableDTO));

            //Act
            var actual = container.GetAllNonSeatedTables();

            //Assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
        [TestMethod]
        public void GetAllSeatedTablesTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var expected = stub.tables.FindAll(table => table.Time_Arrived.Year == 2022).ConvertAll(tableDTO => new Table(tableDTO));

            //Act
            var actual = container.GetAllSeatedTables();

            //Assert
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
        [TestMethod]
        public void GetOrdersTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var table = container.GetTable(tableId);

            var expected = stub.orders.FindAll(order => order.SeatedTableId == tableId).ConvertAll(orderDTO => new Order(orderDTO));

            //Act
            var actual = table.GetOrders(stub);

            //Assert
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].SeatedTableId, actual[i].SeatedTableId);
                Assert.AreEqual(expected[i].StaffId, actual[i].StaffId);
            }
        }
        [TestMethod]
        public void GetOpenOrderTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var expected = new Order(stub.orders.OrderByDescending(order => order.CreatedAt).ToList().Find(order => order.SeatedTableId == tableId));

            //Act
            var actual = container.GetTable(tableId).GetOpenOrder(stub);

            //Assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.SeatedTableId, actual.SeatedTableId);
            Assert.AreEqual(expected.StaffId, actual.StaffId);
            Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
        }
        [TestMethod]
        public void GetTotalPriceTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var table = container.GetTable(1);
            var tableOrders = stub.orders.FindAll(order => order.SeatedTableId == tableId);

            double expected = 0;
            stub.orderRules.ForEach(rule =>
            {
                if (tableOrders.Any(order => order.Id == rule.OrderId)) expected += rule.ProductPrice * rule.Amount;
            });

            //Act
            double actual = table.GetTotalPrice(stub);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OpenTableTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 3;

            //Act
            container.OpenTable(tableId);

            //Assert
            Assert.IsTrue(stub.tables.Find(table => table.Id == tableId).Time_Arrived.Year == DateTime.Now.Year);
        }
        [TestMethod]
        public void CloseTableTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            //Act
            container.CloseTable(tableId);

            //Assert
            Assert.IsTrue(stub.tables.Find(table => table.Id == tableId).Time_Arrived.Year != DateTime.Now.Year);
        }
        [TestMethod]
        public void EditTableTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            //Act

            //Assert
        }
        [TestMethod]
        public void DeleteTableTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 5;

            //Act
            container.DeleteTable(tableId);

            //Assert
            Assert.IsTrue(!stub.tables.Any(table => table.Id == tableId));
        }
        [TestMethod]
        public void CreateTableTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            //Act
            container.CreateTable("T-10");

            //Assert
            Assert.IsTrue(stub.tables.Any(table => table.Name == "T-10"));
        }
        [TestMethod]
        public void CreateOrderTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;

            var expected = stub.orders.Count + 1;
            var table = container.GetTable(tableId);

            //Act
            var order = table.CreateOrder(stub, 1);

            //Assert
            Assert.AreEqual(expected, stub.orders.Count);
        }

        [TestMethod]
        public void RemoveOrderTest()
        {
            //Arrange
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var table = container.GetTable(tableId);

            var expected = stub.orders.Count - 1;

            //Act
            table.RemoveOrder(stub);

            //Assert
            Assert.AreEqual(expected, stub.orders.Count);
        }
    }
}
