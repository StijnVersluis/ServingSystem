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
using Microsoft.AspNetCore.DataProtection;

namespace UnitTest
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            //Arrange
            var Product = new Product(1, "Cola", 2.99, 1);

            //Assert
            Assert.AreEqual(1, Product.Id);
            Assert.AreEqual("Cola", Product.Name);
            Assert.AreEqual(1, Product.Id);
            Assert.AreEqual(1, Product.Id);
        }
        [TestMethod]
        public void GetAllProductsTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var expected = stub.productList;

            //Act
            var productList = stub.GetAll();

            //Assert
            for (int i = 0; i < productList.Count; i++)
            {
                Assert.AreEqual(expected[i], productList[i]);
            }
        }
        [TestMethod]
        public void CreateProductTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);
            var previousListCount = stub.productList.Count;
            var product = new Product("Beer", 2.99, 3);

            //Act
            var success = container.CreateProduct(product);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(previousListCount, stub.GetAll().Count -1);
        }
        [TestMethod]
        public void DeleteProductTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);
            var previousListCount = stub.productList.Count;

            //Act
            var success = container.DeleteProduct(1);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(previousListCount, stub.GetAll().Count + 1);
        }
        [TestMethod]
        public void EditProductTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);
            var product = container.GetProduct(1);
            var previousPrice = product.Price;
            var minPrice = 1;
            product.Price = product.Price - minPrice;

            //Act
            var success = product.Edit(stub);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(stub.GetProduct(1).Price, previousPrice - minPrice);
        }
        [TestMethod]
        public void GetProductTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);

            //Act
            var product = container.GetProduct(1);

            //Assert
            Assert.AreEqual(stub.productList[0].Id, product.Id);
            Assert.AreEqual(stub.productList[0].Name, product.Name);
        }
        [TestMethod]
        public void GetProductTypeNameTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);
            var product = container.GetProduct(1);
            var expectedType = (stub.typeList.First(type => type.Id == 1));

            //Act
            var productTypeName = product.GetProductTypeName(stub);

            //Assert
            Assert.AreEqual(expectedType.Name, productTypeName);
        }
        [TestMethod]
        public void GetAllTypesTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);

            //Act
            var types = container.GetAllTypes();

            //Assert
            for (int i = 0; i < types.Count; i++)
            {
                Assert.AreEqual(stub.typeList[i].Id, types[i].Id);
                Assert.AreEqual(stub.typeList[i].Name, types[i].Name);
            }
        }
        [TestMethod]
        public void GetAllOfTypeTest()
        {
            //Arrange
            var stub = new ProductSTUB();
            var container = new ProductContainer(stub);
            var typeName = "Food";
            var typeId = stub.typeList.Find(type => type.Name == typeName).Id;
            var expectedProducts = stub.productList.FindAll(prod => prod.Type == typeId);

            //Act
            var productsOfType = container.GetAllOfType(typeId);

            //Assert
            Assert.AreEqual(expectedProducts.Count, productsOfType.Count);
            for (int i = 0; i < productsOfType.Count; i++)
            {
                Assert.AreEqual(expectedProducts[i].Id, productsOfType[i].Id);
                Assert.AreEqual(expectedProducts[i].Name, productsOfType[i].Name);
                Assert.AreEqual(expectedProducts[i].Price, productsOfType[i].Price);
            }
        }
    }
}
