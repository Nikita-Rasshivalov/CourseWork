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
        Stock testStock = new Stock() { StockName = "TestName", Markup = 10,Description = "dasdas", UserId = 9 };
        [TestMethod]
        public void BInsertTest()
        {
            Assert.AreEqual(true, service.Insert(testStock));
        }

        [TestMethod]
        public void StockDel()
        {
            Assert.AreEqual(true, service.Delete(testStock));
        }


        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }
        [TestMethod]
        public void CGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }
        [TestMethod]
        public void BGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }

    }
}
