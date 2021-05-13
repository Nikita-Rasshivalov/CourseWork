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
        Stock testStock = new Stock() { StockId = 1, StockName = "TestName", Markup = 10 };
        [TestMethod]
        public void BInsertTest()
        {
            Assert.AreEqual(true, service.Insert(testStock));
        }
        [TestMethod]
        public void BInsertTest2()
        {
            Assert.AreEqual(true, service.Insert(testStock));
        }



        [TestMethod]
        public void BInsertTest3()
        {
            Assert.AreEqual(true, service.Insert(testStock));
        }
    }
}
