using System;
using System.Linq;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseAppTests
{
    [TestClass]
    public class RoleServiceTest
    {
        IService<Role> service = new RoleService();
        static Role testCustomer = new Role() { RoleName = "TestName", RoleKey = "test" };

        [TestMethod]
        public void AGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testCustomer));
        }
    }
}
