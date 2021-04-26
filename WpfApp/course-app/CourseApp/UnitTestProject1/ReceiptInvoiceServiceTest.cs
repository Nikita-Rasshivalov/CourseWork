using System;
using System.Linq;
using CourseApp.Models;
using CourseApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NpgsqlTypes;

namespace CourseAppTests
{
    [TestClass]
    public class ReceiptInvoiceServiceTest
    {
        IService<ReceiptInvoice> service = new ReceiptInvoiceService();

        static ReceiptInvoice testCustomer = new ReceiptInvoice()
        {
            CustomerId = 1,
            StockId = 2,
            ProductId = 1,
            CountProduct = 1,
            PriceProduct = 1,
            ReceiptInvoiceDate = (NpgsqlDate)DateTime.Now
        };

        [TestMethod]
        public void BGetAllTest()
        {
            Assert.AreEqual(true, service.GetAll().Count > 0);
        }

        [TestMethod]
        public void AInsertTest()
        {
            Assert.AreEqual(true, service.Insert(testCustomer));
        }

        [TestMethod]
        public void CUpdateTest()
        {
            var updateItem = service.GetAll()
                                    .FirstOrDefault(c => c.CustomerId == testCustomer.CustomerId &&
                                                    c.StockId == testCustomer.StockId &&
                                                    c.ProductId == testCustomer.ProductId &&
                                                    c.CountProduct == testCustomer.CountProduct &&
                                                    c.PriceProduct == testCustomer.PriceProduct);

            updateItem.PriceProduct = 5;

            Assert.AreEqual(true, service.Update(updateItem));

            testCustomer.ReceiptInvoiceId = updateItem.ReceiptInvoiceId;
            updateItem.PriceProduct = 1;

            service.Update(updateItem);
        }

        [TestMethod]
        public void DGetByIdTest()
        {
            var findItem = service.GetById(testCustomer.ReceiptInvoiceId);

            Assert.AreEqual(findItem.ReceiptInvoiceId, testCustomer.ReceiptInvoiceId);
            Assert.AreEqual(findItem.CustomerId, testCustomer.CustomerId);
            Assert.AreEqual(findItem.StockId, testCustomer.StockId);
            Assert.AreEqual(findItem.ProductId, testCustomer.ProductId);
            Assert.AreEqual(findItem.PriceProduct, testCustomer.PriceProduct);
            Assert.AreEqual(findItem.CountProduct, testCustomer.CountProduct);
        }

        [TestMethod]
        public void EDeleteTest()
        {
            Assert.AreEqual(true, service.Delete(testCustomer));
        }
    }
}
