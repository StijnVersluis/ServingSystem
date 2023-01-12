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
            var table = new Table(1, "T-01");

            Assert.AreEqual(1, table.Id);
            Assert.AreEqual("T-01", table.Name);
        }
        [TestMethod]
        public void GetAllTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var expected = stub.tables.ConvertAll(tableDTO => new Table(tableDTO));
            var actual = container.GetAll();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
        [TestMethod]
        public void GetTableTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;

            var expected = new Table(stub.tables.Find(table => table.Id == tableId));
            var actual = container.GetTable(1);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }
        [TestMethod]
        public void GetAllNonSeatedTablesTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var expected = stub.tables.FindAll(table => table.Time_Arrived.Year != 2022).ConvertAll(tableDTO => new Table(tableDTO));
            var actual = container.GetAllNonSeatedTables();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
        [TestMethod]
        public void GetAllSeatedTablesTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var expected = stub.tables.FindAll(table => table.Time_Arrived.Year == 2022).ConvertAll(tableDTO => new Table(tableDTO));
            var actual = container.GetAllSeatedTables();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
        [TestMethod]
        public void GetOrdersTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var table = container.GetTable(tableId);

            var expected = stub.orders.FindAll(order => order.SeatedTableId == tableId).ConvertAll(orderDTO => new Order(orderDTO));
            var actual = table.GetOrders(stub);

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
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var expected = new Order(stub.orders.OrderByDescending(order => order.CreatedAt).ToList().Find(order => order.SeatedTableId == tableId));
            var actual = container.GetTable(tableId).GetOpenOrder(stub);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.SeatedTableId, actual.SeatedTableId);
            Assert.AreEqual(expected.StaffId, actual.StaffId);
            Assert.AreEqual(expected.CreatedAt, actual.CreatedAt);
        }
        [TestMethod]
        public void GetTotalPriceTest()
        {
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
            double actual = table.GetTotalPrice(stub);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void OpenTableTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 3;
            container.OpenTable(tableId);

            Assert.IsTrue(stub.tables.Find(table => table.Id == tableId).Time_Arrived.Year == DateTime.Now.Year);
        }
        [TestMethod]
        public void CloseTableTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            container.CloseTable(tableId);

            Assert.IsTrue(stub.tables.Find(table => table.Id == tableId).Time_Arrived.Year != DateTime.Now.Year);
        }
        [TestMethod]
        public void EditTableTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

        }
        [TestMethod]
        public void DeleteTableTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 5;
            container.DeleteTable(tableId);

            Assert.IsTrue(!stub.tables.Any(table => table.Id == tableId));
        }
        [TestMethod]
        public void CreateTableTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            container.CreateTable("T-10");

            Assert.IsTrue(stub.tables.Any(table => table.Name == "T-10"));
        }
        [TestMethod]
        public void CreateOrderTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;

            var expected = stub.orders.Count + 1;
            var table = container.GetTable(tableId);
            var order = table.CreateOrder(stub, 1);

            Assert.AreEqual(expected, stub.orders.Count);
        }

        [TestMethod]
        public void RemoveOrderTest()
        {
            var stub = new TableSTUB();
            var container = new TableContainer(stub);

            var tableId = 1;
            var table = container.GetTable(tableId);

            var expected = stub.orders.Count - 1;
            table.RemoveOrder(stub);

            Assert.AreEqual(expected, stub.orders.Count);
        }
    }
}
