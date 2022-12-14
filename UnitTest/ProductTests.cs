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
        public void GetAllProductsTest()
        {
            var stub = new ProductSTUB();

            var expected = stub.productList;
            var productList = stub.GetAll();

            for (int i = 0; i < productList.Count; i++)
            {
                productList[i] = stub.productList[i];
            }
        }
        [TestMethod]
        public void CreateProductTest()
        {
            var stub = new ProductSTUB();
            var previousListCount = stub.productList.Count;
            var product = new Product("Beer", 2.99, 3);

            Assert.IsTrue(stub.CreateProduct(product.ToDTOWithoutId()));
            Assert.IsTrue(previousListCount < stub.GetAll().Count);
        }
        [TestMethod]
        public void DeleteProductTest()
        {
            var stub = new ProductSTUB();
            var previousListCount = stub.productList.Count;

            Assert.IsTrue(stub.DeleteProduct(1));
            Assert.IsTrue(previousListCount > stub.GetAll().Count);
        }
        [TestMethod]
        public void EditProductTest()
        {
            var stub = new ProductSTUB();
            var product = new Product(stub.GetProduct(1));
            var previousPrice = product.Price;
            product.Price = product.Price - 1;

            Assert.IsTrue(product.Edit(stub));
            Assert.IsTrue(stub.GetProduct(1).Price != previousPrice);
        }
        [TestMethod]
        public void GetProductTest()
        {
            var stub = new ProductSTUB();
            var product = new Product(stub.GetProduct(1));

            Assert.AreEqual(stub.productList[0].Id, product.Id);
            Assert.AreEqual(stub.productList[0].Name, product.Name);
        }
        [TestMethod]
        public void GetProductTypeNameTest()
        {
            var stub = new ProductSTUB();
            var product = new Product(stub.GetProduct(1));
            var productTypeName = product.GetProductTypeName(stub);
            var expectedType = (stub.typeList.First(type => type.Id == 1));

            Assert.AreEqual(expectedType.Name, productTypeName);
        }
        [TestMethod]
        public void GetAllTypesTest()
        {
            var stub = new ProductSTUB();
            var types = stub.GetAllTypes();

            for (int i = 0; i < types.Count; i++)
            {
                Assert.AreEqual(stub.typeList[i].Id, types[i].Id);
                Assert.AreEqual(stub.typeList[i].Name, types[i].Name);
            }
        }
        [TestMethod]
        public void GetAllOfTypeTest()
        {
            var stub = new ProductSTUB();
            var productsOfType = stub.GetAllOfType(stub.typeList.Find(type => type.Name == "Food").Id);
            var expectedProducts = stub.productList.FindAll(prod => prod.Type == stub.typeList.Find(type => type.Name == "Food").Id);

            Assert.AreEqual(expectedProducts.Count, productsOfType.Count);
            for (int i = 0; i < productsOfType.Count; i++)
            {
                Assert.AreEqual(
                    expectedProducts[i].Id, productsOfType[i].Id
                    );
                Assert.AreEqual(
                    expectedProducts[i].Name, productsOfType[i].Name
                    );
                Assert.AreEqual(
                    expectedProducts[i].Price, productsOfType[i].Price
                    );
            }
        }
    }
}
