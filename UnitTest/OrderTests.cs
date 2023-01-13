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
using System.Security.Cryptography.Xml;
using System.Linq.Expressions;

namespace UnitTest
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            //Arrange
            var date = DateTime.Now;
            var order = new Order(1, 1, 1, date);
            var orderRule = new OrderRule(1, 1, 1, 2, 2.99);

            //Assert
            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(1, order.SeatedTableId);
            Assert.AreEqual(1, order.StaffId);
            Assert.AreEqual(date, order.CreatedAt);

            Assert.AreEqual(1, orderRule.Id);
            Assert.AreEqual(order.Id, orderRule.OrderId);
            Assert.AreEqual(1, orderRule.ProductId);
            Assert.AreEqual(2, orderRule.Amount);
            Assert.AreEqual(2.99, orderRule.ProductPrice);
        }
        [TestMethod]
        public void GetProductsTest()
        {
            //Arrange
            var stub = new OrderSTUB();
            var orderId = 1;

            var order = new Order(stub.orders.Find(order => order.Id == orderId));

            //Act
            var products = order.GetProducts(stub);

            //Assert
            Assert.AreEqual(stub.orderRules.Where(rule => rule.OrderId == orderId).Count(), products.Count);
        }

        [TestMethod]
        public void AddProductTest()
        {
            //Arrange
            var stub = new OrderSTUB();

            var order = new Order(stub.orders[0]);
            int productId = 1;
            OrderRule oldRule = new OrderRule(stub.orderRules.Where(rule => rule.OrderId == order.Id && rule.ProductId == productId).ToList().First());

            //Act
            order.AddProduct(stub, new Product(productId, "", 5, 1));

            OrderRule newRule = new OrderRule(stub.orderRules.Where(rule => rule.OrderId == order.Id && rule.ProductId == productId).ToList().First());

            //Assert
            Assert.AreEqual(oldRule.Amount + 1, newRule.Amount);
        }

        [TestMethod]
        public void RemoveProductTest()
        {
            //Arrange
            var stub = new OrderSTUB();

            var order = new Order(stub.orders[0]);
            int productId = 1;
            OrderRule oldRule = new OrderRule(stub.orderRules.Where(rule => rule.OrderId == order.Id && rule.ProductId == productId).ToList().First());

            //Act
            order.RemoveProduct(stub, productId);

            OrderRule newRule = new OrderRule(stub.orderRules.Where(rule => rule.OrderId == order.Id && rule.ProductId == productId).ToList().First());

            //Assert
            Assert.AreEqual(oldRule.Amount - 1, newRule.Amount);
        }
    }
}
