using CourseApp.Models;
using CourseApp.Services;
using System;
using System.Linq;
using System.Windows;


namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для EAddProdWindow.xaml
    /// </summary>
    public partial class EAddProdWindow : Window
    {
        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        IService<Product> _productService = new ProductService();
        IService<ExpenditurePosition> _expenditurePositionService = new ExpenditurePositionService();
        IService<ExpenditureInvoice> _expenditureInvoiceService = new ExpenditureInvoiceService();
        IService<ProductInStock> _productInStockSevice = new ProductInStockeService();
        double Markup { get; set; }
        /// <summary>
        /// не нужно
        /// </summary>
        int StockId { get; set; }
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public EAddProdWindow(double markup, int stockId)
        {
            InitializeComponent();
            this.Markup = markup;
            this.StockId = stockId;
            ProductComboBox.ItemsSource = _productService.GetAll().Select(o => o.ProductName).ToList();
        }
        /// <summary>
        /// Кнопка добавления товаров в позицию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {
            double productPrice = _productService.GetAll()
                                               .SingleOrDefault(p => p.ProductName.Equals(ProductComboBox.SelectedItem.ToString()))
                                               .ProductPrice;
            int expenditureInvoiceId = _expenditureInvoiceService.GetAll().Select(p => p.ExpenditureInvoiceId).Last();


            int productId = _productService.GetAll()
                                               .SingleOrDefault(p => p.ProductName.Equals(ProductComboBox.SelectedItem.ToString()))
                                               .EntityId;

            double countProductsInStock = _productInStockSevice.GetAll().Where(o => o.StockId == StockId && o.ProductId == productId).Select(o => o.CountProduct).FirstOrDefault();
            

            if (countProductsInStock < double.Parse(CountBox.Text))
            {
                MessageBox.Show($"Недостаточно товара в количестве {double.Parse(CountBox.Text) - countProductsInStock}");
            }
            else
            {
                _expenditurePositionService.Insert(new ExpenditurePosition
                {
                    CountProduct = double.Parse(CountBox.Text),
                    ProductId = productId,
                    ExpenditureInvoiceId = expenditureInvoiceId,
                    ProductPrice = (Math.Round(productPrice + productPrice * (Markup / 100.0), 3))

                });

                var id = _productInStockSevice.GetAll().SingleOrDefault(o => o.StockId.Equals(StockId) && o.ProductId.Equals(productId)).Id;
 
                _productInStockSevice.Update(new ProductInStock
                {
                    Id = id,
                    CountProduct = countProductsInStock - double.Parse(CountBox.Text)
                });
            }
            CountBox.Text = "";
            ProductComboBox.SelectedIndex = 0;
        }
    }
}
