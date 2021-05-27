using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseAppTests
{
    [TestClass]
    public class ReceiptInvoiceServiceTest
    {

        IService<ReceiptInvoice> service = new ReceiptInvoiceService();

        static ReceiptInvoice testReceipt = new ReceiptInvoice()
        {
            CustomerId = 1,
            StockId = 2

        };

        [TestMethod]
        public void BGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }
        [TestMethod]
        public void EGetAllTest()
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
            Assert.AreEqual(true, service.Delete(testReceipt));
        }
    }
}
