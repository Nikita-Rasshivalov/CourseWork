using System;
using System.Linq;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseAppTests
{
    [TestClass]
    public class StockServiceTest
    {
        IService<Stock> service = new StockService();
        static Stock testCustomer = new Stock() { StockName = "TestCustomerName", Description = "TestCustomerDescription", Markup = 1, UserId = 1 };

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
                                    .FirstOrDefault(c => c.StockName == testCustomer.StockName && c.Description == testCustomer.Description);

            updateItem.StockName = "NewTestName";

            Assert.AreEqual(true, service.Update(updateItem));

            testCustomer.StockId = updateItem.StockId;
            updateItem.StockName = testCustomer.StockName;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testCustomer.StockId);

            Assert.AreEqual(findItem.StockName, testCustomer.StockName);
            Assert.AreEqual(findItem.StockId, testCustomer.StockId);
            Assert.AreEqual(findItem.Description, testCustomer.Description);
            Assert.AreEqual(findItem.Markup, testCustomer.Markup);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testCustomer));
        }
    }
}
