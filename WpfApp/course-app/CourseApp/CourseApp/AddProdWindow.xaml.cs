using CourseApp.Models;
using CourseApp.Services;
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
            int receiptInvoiceId = _receiptInvoiceService.GetAll().Select(p => p.ReceiptInvoiceId).Last();

            int productId = _productService.GetAll()
                                               .SingleOrDefault(p => p.ProductName.Equals(ProductComboBox.SelectedItem.ToString()))
                                               .EntityId;
            _receiptPositionService.Insert(new ReceiptPosition
            {
                CountProduct = double.Parse(CountBox.Text),
                ProductId = productId,
                ReceiptInvoiceId = receiptInvoiceId

            });

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
                var id = _productInStockSevice.GetAll().SingleOrDefault(o => o.StockId.Equals(StckId) && o.ProductId.Equals(productId)).Id;
                double countProductsInStock = _productInStockSevice.GetAll().Where(o => o.StockId == StckId && o.ProductId == productId).Select(o => o.CountProduct).FirstOrDefault();
                _productInStockSevice.Update(new ProductInStock
                {
                    Id = id,
                    CountProduct = double.Parse(CountBox.Text) + countProductsInStock
                });
            }

            CountBox.Text = "";
            ProductComboBox.SelectedIndex = 0;
        }
    }
}
