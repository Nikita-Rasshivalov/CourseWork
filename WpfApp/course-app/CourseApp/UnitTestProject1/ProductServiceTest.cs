using System;
using System.Linq;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseAppTests
{
    [TestClass]
    public class ProductServiceTest
    {
        IService<Product> service = new ProductService();
        static Product testCustomer = new Product() { ProductName = "TestName" };

        [TestMethod]
        public void AGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }

        [TestMethod]
        public void BInsertTest()
        {
            Assert.AreEqual(true, service.Insert(testCustomer));
        }

        [TestMethod]
        public void CUpdateTest()
        {
            var updateItem = service.GetAll()
                                    .FirstOrDefault(c => c.ProductName == testCustomer.ProductName);

            updateItem.ProductName = "NewTestName";

            Assert.AreEqual(true, service.Update(updateItem));

            testCustomer.EntityId = updateItem.EntityId;
            updateItem.ProductName = testCustomer.ProductName;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testCustomer.EntityId);

            Assert.AreEqual(findItem.ProductName, testCustomer.ProductName);
            Assert.AreEqual(findItem.EntityId, testCustomer.EntityId);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testCustomer));
        }
    }
}
