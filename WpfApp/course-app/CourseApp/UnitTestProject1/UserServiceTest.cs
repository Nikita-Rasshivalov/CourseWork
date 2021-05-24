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
        static User testUser = new User()
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
            Assert.AreEqual(true, service.Insert(testUser));
        }

        [TestMethod]
        public void CUpdateTest()
        {
            var updateItem = service.GetAll()
                                    .FirstOrDefault(c => c.UserName == testUser.UserName && c.UserPass == testUser.UserPass);

            updateItem.UserName = "NewTestName";

            Assert.AreEqual(true, service.Update(updateItem));

            testUser.UserId = updateItem.UserId;
            updateItem.UserName = testUser.UserName;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testUser.UserId);

            Assert.AreEqual(findItem.UserName, testUser.UserName);
            Assert.AreEqual(findItem.UserId, testUser.UserId);
            Assert.AreEqual(findItem.UserPass, testUser.UserPass);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testUser));
        }
    }
}
