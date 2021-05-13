using CourseApp.Models;
using CourseApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для EInvoiceDetalisationWindow.xaml
    /// </summary>
    public partial class EInvoiceDetalisationWindow : Window
    {
        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        IService<ExpenditurePosition> _expenditurePositionService = new ExpenditurePositionService();
        IService<Product> _productService = new ProductService();
        /// <summary>
        /// Инициализация позиций расходной
        /// </summary>
        /// <param name="stockItem"></param>
        public EInvoiceDetalisationWindow(ExpenditureInvoice stockItem)
        {
            InitializeComponent();
            List<ExpenditurePosition> positions = _expenditurePositionService.GetAll()?.Where(o => o.ExpenditureInvoiceId == stockItem.ExpenditureInvoiceId).ToList() ?? new List<ExpenditurePosition>();
            foreach (var item in positions)
            {
                item.Product = _productService.GetById((int)item.ProductId);
                item.FullPrice = item.ProductPrice* item.CountProduct;
            }
            EInvoicePositionsGrid.ItemsSource = positions;
            EInvoicePositionsGrid.IsReadOnly = true;
        }
    }
}
