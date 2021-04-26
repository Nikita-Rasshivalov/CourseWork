using System;
using System.Linq;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseAppTests
{
    [TestClass]
    public class CustomerServiceTest
    {
        IService<Customer> service = new CustomerService();
        static Customer testCustomer = new Customer() { CustomerName = "TestCustomerName", Description = "TestCustomerDescription" };

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
                                    .FirstOrDefault(c => c.CustomerName == testCustomer.CustomerName && c.Description == testCustomer.Description);

            updateItem.CustomerName = "NewTestName";

            Assert.AreEqual(true, service.Update(updateItem));

            testCustomer.CustomerId = updateItem.CustomerId;
            updateItem.CustomerName = testCustomer.CustomerName;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testCustomer.CustomerId);

            Assert.AreEqual(findItem.CustomerName, testCustomer.CustomerName);
            Assert.AreEqual(findItem.CustomerId, testCustomer.CustomerId);
            Assert.AreEqual(findItem.Description, testCustomer.Description);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testCustomer));
        }
    }
}
