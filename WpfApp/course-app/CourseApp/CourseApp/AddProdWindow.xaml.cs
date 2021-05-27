using CourseApp.Models;
using CourseApp.Services;
using System;
using System.Linq;
using System.Windows;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для AddProdWindow.xaml
    /// </summary>
    public partial class AddProdWindow : Window
    {
        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        IService<Product> _productService = new ProductService();
        IService<ReceiptPosition> _receiptPositionService = new ReceiptPositionService();
        IService<ReceiptInvoice> _receiptInvoiceService = new ReceiptInvoiceService();
        IService<ProductInStock> _productInStockSevice = new ProductInStockeService();

        public int StckId { get; set; }
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public AddProdWindow(int stockId)
        {
            InitializeComponent();
            this.StckId = stockId;
            ProductComboBox.ItemsSource = _productService.GetAll().Select(o => o.ProductName).ToList();
        }
        /// <summary>
        /// Кнопка добавления товаров в позицию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem != null && CountBox.Text != "")
            {
                int receiptInvoiceId = _receiptInvoiceService.GetAll().Select(p => p.ReceiptInvoiceId).Last();

                int productId = _productService.GetAll()
                                                   .SingleOrDefault(p => p.ProductName.Equals(ProductComboBox.SelectedItem.ToString()))
                                                   .EntityId;
                CreateResPos(productId, receiptInvoiceId);
                CountBox.Text = "";
                ProductComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Заполните данные");
            }
        }
        /// <summary>
        /// Добавление продуктов в позиции накладной
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="receiptInvoiceId"></param>
        private void CreateResPos(int productId, int receiptInvoiceId)
        {
            if (_receiptPositionService.GetAll()
                    .Where(o => o.ProductId == productId && o.ReceiptInvoiceId.Equals(receiptInvoiceId)).Count() == 0)
            {
                _receiptPositionService.Insert(new ReceiptPosition
                {
                    CountProduct = double.Parse(CountBox.Text),
                    ProductId = productId,
                    ReceiptInvoiceId = receiptInvoiceId

                });
            }
            else
            {
                double countInPosition = _receiptPositionService.GetAll()
                    .Where(o => o.ProductId == productId && o.ReceiptInvoiceId.Equals(receiptInvoiceId))
                    .Select(o => o.CountProduct).FirstOrDefault();
                _receiptPositionService.Update(new ReceiptPosition
                {
                    CountProduct = double.Parse(CountBox.Text) + countInPosition,
                    ProductId = productId
                });
            }
            if (_productInStockSevice.GetAll().Where(o => o.StockId == StckId && o.ProductId == productId).Count() == 0)
            {
                _productInStockSevice.Insert(new ProductInStock
                {
                    ProductId = productId,
                    CountProduct = double.Parse(CountBox.Text),
                    StockId = StckId
                });
            }
            else
            {
                var id = _productInStockSevice.GetAll()
                    .SingleOrDefault(o => o.StockId.Equals(StckId) && o.ProductId.Equals(productId)).Id;
                double countProductsInStock = _productInStockSevice.GetAll()
                    .Where(o => o.StockId == StckId && o.ProductId == productId).Select(o => o.CountProduct).FirstOrDefault();
                _productInStockSevice.Update(new ProductInStock
                {
                    Id = id,
                    CountProduct = double.Parse(CountBox.Text) + countProductsInStock
                });
            }
        }
        /// <summary>
        /// Ввод только целых чисел в CountBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Удаление пустой накладной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int receiptInvoiceId = _receiptInvoiceService.GetAll()
                .Select(p => p.ReceiptInvoiceId)
                .Last();
            if (_receiptPositionService.GetAll()
                .Where(o => o.ReceiptInvoiceId.Equals(receiptInvoiceId)).
                Select(o => o.ReceiptInvoiceId)
                .Count() == 0)
            {

                _receiptInvoiceService.Delete(new ReceiptInvoice
                {
                    ReceiptInvoiceId = receiptInvoiceId
                });
            }
        }
    }
}
