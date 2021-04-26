using System;
using System.Linq;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseAppTests
{
    [TestClass]
    public class UserServiceTest
    {
        IService<User> service = new UserService();
        static User testCustomer = new User()
        {
            RoleKey = "admin",
            UserName = "TestCustomerDescription",
            UserPass = "TestCustomerDescription",
            FullName = "TestCustomerDescription",
        };

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
                                    .FirstOrDefault(c => c.UserName == testCustomer.UserName && c.UserPass == testCustomer.UserPass);

            updateItem.UserName = "NewTestName";

            Assert.AreEqual(true, service.Update(updateItem));

            testCustomer.UserId = updateItem.UserId;
            updateItem.UserName = testCustomer.UserName;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testCustomer.UserId);

            Assert.AreEqual(findItem.UserName, testCustomer.UserName);
            Assert.AreEqual(findItem.UserId, testCustomer.UserId);
            Assert.AreEqual(findItem.UserPass, testCustomer.UserPass);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testCustomer));
        }
    }
}
