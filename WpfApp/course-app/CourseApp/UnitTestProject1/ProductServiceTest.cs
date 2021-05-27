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
        static Product testProd = new Product() { ProductName = "TestName" };

        [TestMethod]
        public void AGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }

        [TestMethod]
        public void BInsertTest()
        {
            Assert.AreEqual(true, service.Insert(testProd));
        }

        [TestMethod]
        public void CUpdateTest()
        {
            var updateItem = service.GetAll()
                                    .FirstOrDefault(c => c.ProductName == testProd.ProductName);

            updateItem.ProductName = "NewTestName";

            Assert.AreEqual(true, service.Update(updateItem));

            testProd.EntityId = updateItem.EntityId;
            updateItem.ProductName = testProd.ProductName;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testProd.EntityId);

            Assert.AreEqual(findItem.ProductName, testProd.ProductName);
            Assert.AreEqual(findItem.EntityId, testProd.EntityId);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testProd));
        }
    }
}
