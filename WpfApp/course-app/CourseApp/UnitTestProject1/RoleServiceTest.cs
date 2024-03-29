﻿using System;
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
        static Role testRole = new Role() { RoleName = "TestName", RoleKey = "test" };

        [TestMethod]
        public void AGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }
        [TestMethod]
        public void BGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }
        [TestMethod]
        public void CGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }
        [TestMethod]
        public void DGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testRole));
        }
    }
}
