using CourseApp.Models;
using CourseApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для InvoiceDetalisationWindow.xaml
    /// </summary>
    public partial class InvoiceDetalisationWindow : Window
    {
        /// <summary>
        /// Объявление сервисов для работы с детализацией накладных
        /// </summary>
        IService<ReceiptPosition> _receiptPositionService = new ReceiptPositionService();
        IService<Product> _productService = new ProductService();
        /// <summary>
        /// Инициализация окна для работы с накладными
        /// </summary>
        /// <param name="stockItem"></param>
        public InvoiceDetalisationWindow(ReceiptInvoice stockItem)
        {
            InitializeComponent();
            if (stockItem != null)
            {
                List<ReceiptPosition> positions = _receiptPositionService.GetAll()?.
                    Where(o => o.ReceiptInvoiceId == stockItem.ReceiptInvoiceId).
                    ToList() ?? new List<ReceiptPosition>();

                foreach (var item in positions)
                {
                    item.Product = _productService.GetById((int)item.ProductId);
                    var id = _productService.GetAll().SingleOrDefault(o => o.EntityId.Equals(item.ProductId));
                    item.FullPrice = id.ProductPrice * item.CountProduct;
                    item.Product.ProductPrice = Math.Round( item.Product.ProductPrice,3);
                }
                InvoicePositionsGrid.ItemsSource = positions;
                InvoicePositionsGrid.IsReadOnly = true;
            }
            
            

        }
    }
}
