using System;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpgsqlTypes;

namespace CourseAppTests
{
    [TestClass]
    public class ExpenditureInvoiceServiceTest
    {
        IService<ExpenditureInvoice> service = new ExpenditureInvoiceService();

        static ExpenditureInvoice testCustomer = new ExpenditureInvoice()
        {
            ExpenditureInvoiceDate = (NpgsqlDate)DateTime.Now,
            CustomerId = 1,
            StockId = 2
            
        };

        [TestMethod]
        public void BGetAllTest()
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
