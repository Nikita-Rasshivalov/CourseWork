using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourseApp.Services;
using CourseApp.Models;
using System.Globalization;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        /// <summary>
        /// Инициализация полей
        /// </summary>
        private int userId;
        private string roleKey = "";
        private User selectedUser = null;
        private Product selectedProduct = null;
        private Customer selectedCustomer = null;
        /// <summary>
        /// Для вызова отчетов
        /// </summary>
        private Report.Reports reports = new Report.Reports();


        // Инициализация сервисов для работы с таблицами в БД.
        IService<User> _userService = new UserService();
        IService<Role> _roleService = new RoleService();
        IService<Stock> _stockService = new StockService();
        IService<Product> _productService = new ProductService();
        IService<Customer> _customerService = new CustomerService();
        IService<ReceiptInvoice> _receiptInvoiceService = new ReceiptInvoiceService();
        IService<ExpenditureInvoice> _expenditureInvoiceService = new ExpenditureInvoiceService();
        IService<ProductInStock> _productInStockSevice = new ProductInStockeService();

        /// <summary>
        /// Инициализация главного окна
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="userId"></param>
        public WorkWindow(string roleKey, int userId)
        {
            InitializeComponent();
            this.roleKey = roleKey;
            this.userId = userId;
            confidantility();
        }

        /// <summary>
        /// Видимость элементов на интерфейса по ролям (конфиденциальность)
        /// </summary>
        private void confidantility()
        {
            idCustomer.Visibility = roleKey.Equals("admin") ? Visibility.Visible : Visibility.Hidden;
            idUser.Visibility = roleKey.Equals("admin") ? Visibility.Visible : Visibility.Hidden;
            idProduct.Visibility = roleKey.Equals("admin") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptDate.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            datePickerExpenditureReceiptDate.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptStock.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            comboBoxExpenditureReceiptCompany.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            stockCb.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptOperation.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            comboBoxExpenditureReceiptOperation.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            idExpenditureReceipt.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            AddProductsBtn.Visibility = Visibility.Hidden;
            ExpenditureBtn.Visibility = Visibility.Hidden;
            if (roleKey.Equals("manager"))
            {
                ProductInStock.Margin = new Thickness(-250.2, 0, 250, 0);
                idExpenditureInvoices.Margin = new Thickness(-250, 0, 249.6, 0);
                idReceiptInvoices.Margin = new Thickness(-250, 0, 250.4, 0);
                Report.Margin = new Thickness(-356.8, 0, 352.6, 0);
            }
            if (roleKey.Equals("stoker"))
            {
                ProductInStock.Margin = new Thickness(-250.2, 0, 250, 0);
                idExpenditureInvoices.Margin = new Thickness(-250, 0, 249.6, 0);
                idReceiptInvoices.Margin = new Thickness(-250, 0, 250.4, 0);
                idExpenditureReceipt.Margin = new Thickness(-249.6, 0, 249.8, 0);
                Report.Margin = new Thickness(-251.8, 0, 247.6, 0);
            }
        }

        /// <summary>
        /// Привязка организаций в таблицу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            dataGridCustomer.ItemsSource = _customerService.GetAll();
        }

        /// <summary>
        /// Добавление/обновление организации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                selectedCustomer.CustomerName = textBoxCustomerName.Text;
                selectedCustomer.Description = textBoxCustomerDescription.Text;

                _customerService.Update(selectedCustomer);

                selectedCustomer = null;
                textBoxCustomerName.Text = "";
                textBoxCustomerDescription.Text = "";
            }
            else
            {
                _customerService.Insert(new Customer
                {
                    CustomerName = textBoxCustomerName.Text,
                    Description = textBoxCustomerDescription.Text
                });

                textBoxCustomerName.Text = "";
                textBoxCustomerDescription.Text = "";
            }

            dataGridCustomer.ItemsSource = _customerService.GetAll();
        }

        /// <summary>
        /// Отображение организации в контролы при нажатии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCustomer.SelectedItem != null)
            {
                selectedCustomer = (Customer)dataGridCustomer.SelectedItem;
                textBoxCustomerName.Text = selectedCustomer.CustomerName;
                textBoxCustomerDescription.Text = selectedCustomer.Description;
            }
        }

        /// <summary>
        /// Удаление организации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DellOrganisationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCustomer.SelectedItem != null)
            {
                _customerService.Delete((Customer)dataGridCustomer.SelectedItem);
                dataGridCustomer.Items.Refresh();
                dataGridCustomer.ItemsSource = _customerService.GetAll();
            }
            else
            {
                MessageBox.Show("Выберете строку");
            }

        }

        /// <summary>
        /// Очистка контролов организации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCustomerClear_Click(object sender, RoutedEventArgs e)
        {
            selectedCustomer = null;
            textBoxCustomerName.Text = "";
            textBoxCustomerDescription.Text = "";
        }

        /// <summary>
        /// Привязка юзеров в таблицу и привязка ролей в листбокс.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdUser_MouseEnter(object sender, MouseEventArgs e)
        {
            // Получение всех пользователей и привязка их в таблицу.
            dataGridUser.ItemsSource = _userService.GetAll();
            // Получение ролей и привязка их в листбокс.
            comboBoxUserRole.ItemsSource = _roleService.GetAll()?.Select(o => o.RoleKey);
        }

        /// <summary>
        /// Добавление/обновление юзера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUser_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxUserRole.SelectedItem != null && textBoxUserName.Text != ""
                && textBoxUserFullName.Text != "" && passwordBoxUser.Password != "")
            {
                if (selectedUser != null)
                {
                    selectedUser.UserName = textBoxUserName.Text;
                    selectedUser.FullName = textBoxUserFullName.Text;
                    selectedUser.UserPass = passwordBoxUser.Password;
                    selectedUser.RoleKey = comboBoxUserRole.SelectedItem.ToString();
                    _userService.Update(selectedUser);
                    selectedUser = null;
                    textBoxUserName.Text = "";
                    textBoxUserFullName.Text = "";
                    passwordBoxUser.Password = "";
                }
                else
                {
                    _userService.Insert(new User
                    {
                        UserName = textBoxUserName.Text,
                        FullName = textBoxUserFullName.Text,
                        UserPass = passwordBoxUser.Password,
                        RoleKey = comboBoxUserRole.SelectedItem.ToString()
                    });
                    selectedUser = null;
                    textBoxUserName.Text = "";
                    textBoxUserFullName.Text = "";
                    passwordBoxUser.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
            dataGridUser.ItemsSource = _userService.GetAll();
            comboBoxUserRole.ItemsSource = _roleService.GetAll().Select(o => o.RoleKey);
        }

        /// <summary>
        /// Отображение выделенного юзера в контроллы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridUser.SelectedItem != null)
            {
                selectedUser = (User)dataGridUser.SelectedItem;
                textBoxUserName.Text = selectedUser.UserName;
                textBoxUserFullName.Text = selectedUser.FullName;
                passwordBoxUser.Password = selectedUser.UserPass;
                comboBoxUserRole.SelectedItem = selectedUser.RoleKey;
            }
        }

        /// <summary>
        /// Очистка контролов, связанных с юзером.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUserClear_Click(object sender, RoutedEventArgs e)
        {
            selectedUser = null;
            textBoxUserName.Text = "";
            textBoxUserFullName.Text = "";
            passwordBoxUser.Password = "";
        }

        /// <summary>
        /// Удаление юзера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelUserBtn_Click(object sender, RoutedEventArgs e)
        {

            if (dataGridUser.SelectedItem != null)
            {
                if (dataGridUser.SelectedIndex == 0)
                {

                    MessageBox.Show("Этот элемент нельзя удалить");
                }
                else
                {
                    _userService.Delete((User)dataGridUser.SelectedItem);
                    dataGridUser.Items.Refresh();
                    dataGridUser.ItemsSource = _userService.GetAll();
                }
            }
            else
            {
                MessageBox.Show("Выберете строку");
            }

        }

        /// <summary>
        /// Добавление склада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStockBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStockWindow().ShowDialog();
        }

        /// <summary>
        /// Очистка контролов продуктов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProductClear_Click(object sender, RoutedEventArgs e)
        {
            selectedProduct = null;
            textBoxProductName.Text = "";
            priceTb.Text = "";
        }
        /// <summary>
        /// Ввод только чисел в priceBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                var text = ((TextBox)sender).Text;
                if (text.Contains(".") || text.Length == 0)
                {
                    e.Handled = true;
                }
            }
            else if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Добавление/обновление продукта.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            double price;
            double.TryParse(priceTb.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
            if (selectedProduct != null)
            {
                selectedProduct.ProductName = textBoxProductName.Text;
                selectedProduct.ProductPrice = Math.Round(price, 2, MidpointRounding.AwayFromZero);
                _productService.Update(selectedProduct);
                selectedProduct = null;
                textBoxProductName.Text = priceTb.Text = "";
            }
            else
            {
                if (textBoxProductName.Text != "" && priceTb.Text != "")
                {
                    _productService.Insert(new Product
                    {
                        ProductName = textBoxProductName.Text,
                        ProductPrice = Math.Round(price, 2, MidpointRounding.AwayFromZero)
                    });
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
                selectedProduct = null;
                textBoxProductName.Text = priceTb.Text = "";
            }
            dataGridProduct.ItemsSource = _productService.GetAll();
        }

        /// <summary>
        /// Отображение продукта в контролы при выделении.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                selectedProduct = (Product)dataGridProduct.SelectedItem;
                textBoxProductName.Text = selectedProduct.ProductName;
            }
        }

        /// <summary>
        /// Привязка продуктов к таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            dataGridProduct.ItemsSource = _productService.GetAll();
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelProdBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                if (_productService.Delete((Product)dataGridProduct.SelectedItem) == false)
                {
                    MessageBox.Show($"Удаление запрещено, другие записи имеют ссылку на выбранный продукт.",
                        "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    _productService.Delete((Product)dataGridProduct.SelectedItem);
                    dataGridProduct.Items.Refresh();
                    dataGridProduct.ItemsSource = _productService.GetAll();
                }
            }
            else
            {
                MessageBox.Show("Выберете строку");
            }
        }

        /// <summary>
        /// Привязка складов к таблице.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdStock_MouseEnter(object sender, MouseEventArgs e)
        {
            var stokeList = _stockService.GetAll() ?? new List<Stock>();

            foreach (var item in stokeList)
            {
                item.User = _userService.GetById((int)item.UserId);
            }
        }



        /// <summary>
        /// Привязка приходных накладных к таблице.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdReceiptInvoices_MouseEnter(object sender, MouseEventArgs e)
        {
            List<ReceiptInvoice> reseiptInvoices = _receiptInvoiceService.GetAll() ?? new List<ReceiptInvoice>();

            foreach (var item in reseiptInvoices)
            {
                item.Customer = _customerService.GetById((int)item.CustomerId);
                item.Stock = _stockService.GetById((int)item.StockId);
            }
            dataGridReceiptInvoices.ItemsSource = reseiptInvoices;
        }

        /// <summary>
        /// Привязка отходных накладных к таблице.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdExpenditureInvoices_MouseEnter(object sender, MouseEventArgs e)
        {
            List<ExpenditureInvoice> expenditureInvoices = _expenditureInvoiceService.GetAll() ?? new List<ExpenditureInvoice>();

            foreach (var item in expenditureInvoices)
            {
                item.Customer = _customerService.GetById((int)item.CustomerId);
                item.Stock = _stockService.GetById((int)item.StockId);
            }

            dataGridExpenditureInvoices.ItemsSource = expenditureInvoices;
        }

        /// <summary>
        /// Инициализация контролов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdExpenditureReceipt_MouseEnter(object sender, MouseEventArgs e)
        {
            var stocks = _stockService.GetAll();
            switch (roleKey)
            {
                case "admin":
                    stockCb.ItemsSource = stocks?
                        .Select(p => p.StockName);
                    break;
                case "stoker":
                    stockCb.ItemsSource = stocks?
                        .Where(p => p.UserId.Equals(userId))
                        .Select(p => p.StockName);
                    break;
                default:
                    stockCb.ItemsSource = stocks?
                        .Select(p => p.StockName);
                    break;
            }

            comboBoxExpenditureReceiptCompany.ItemsSource = _customerService.GetAll()?.Select(c => c.CustomerName);
            comboBoxExpenditureReceiptOperation.ItemsSource = new List<string>() { "Приход", "Отгрузка" };
            ReportComboBox.ItemsSource = stocks.Select(p => p.StockName);
        }

        /// <summary>
        /// Добавление приходных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void AddProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (stockCb.SelectedItem == null && comboBoxExpenditureReceiptCompany.SelectedItem == null
                && datePickerExpenditureReceiptDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните все данные");
            }
            else
            {
                var stckId = _stockService.GetAll()
                       .SingleOrDefault(p => p.StockName.Equals(stockCb.SelectedItem.ToString())).StockId;

                AddProdWindow add = new AddProdWindow(stckId);
                _receiptInvoiceService.Insert(new ReceiptInvoice
                {
                    ReceiptInvoiceDate = (NpgsqlDate)datePickerExpenditureReceiptDate.SelectedDate,
                    CustomerId = _customerService.GetAll()
                          .SingleOrDefault(p => p.CustomerName.Equals(comboBoxExpenditureReceiptCompany.SelectedItem.ToString()))
                          .CustomerId,
                    StockId = _stockService.GetAll()
                          .SingleOrDefault(p => p.StockName.Equals(stockCb.SelectedItem.ToString())).StockId
                });
                add.ShowDialog();
            }
        }

        /// <summary>
        /// Детализация приходной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridReceiptInvoices_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridReceiptInvoices.SelectedItem != null)
            {
                var stockItem = (ReceiptInvoice)dataGridReceiptInvoices.SelectedItem;
                InvoiceDetalisationWindow details = new InvoiceDetalisationWindow(stockItem);
                details.ShowDialog();
            }
        }

        /// <summary>
        /// Изменение типа накладной.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExpenditureReceiptOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxExpenditureReceiptOperation.SelectedItem != null &&
                comboBoxExpenditureReceiptOperation.SelectedItem.ToString() == "Отгрузка")
            {
                stockCb.Visibility = Visibility.Visible;
                AddProductsBtn.Visibility = Visibility.Hidden;
                ExpenditureBtn.Visibility = Visibility.Visible;
            }
            else
            {
                stockCb.Visibility = Visibility.Visible;
                AddProductsBtn.Visibility = Visibility.Visible;
                ExpenditureBtn.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Получение детализации расходной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridExpenditureInvoices_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridExpenditureInvoices.SelectedItem != null)
            {
                var stockItem = (ExpenditureInvoice)dataGridExpenditureInvoices.SelectedItem;
                EInvoiceDetalisationWindow edetails = new EInvoiceDetalisationWindow(stockItem);
                edetails.ShowDialog();
            }
        }

        /// <summary>
        /// Формирование расходной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpenditureBtn_Click(object sender, RoutedEventArgs e)
        {
            if (stockCb.SelectedItem == null && comboBoxExpenditureReceiptCompany.SelectedItem == null && datePickerExpenditureReceiptDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните все данные");
            }
            else
            {
                double markup = _stockService.GetAll()
           .SingleOrDefault(p => p.StockName.Equals(stockCb.SelectedItem.ToString()))
           .Markup;

                int stockId = _stockService.GetAll()
                           .SingleOrDefault(p => p.StockName.Equals(stockCb.SelectedItem.ToString()))
                           .StockId;


                EAddProdWindow add = new EAddProdWindow(markup, stockId);
                _expenditureInvoiceService.Insert(new ExpenditureInvoice
                {
                    ExpenditureInvoiceDate = (NpgsqlDate)datePickerExpenditureReceiptDate.SelectedDate,
                    CustomerId = _customerService.GetAll()
                          .SingleOrDefault(p => p.CustomerName.Equals(comboBoxExpenditureReceiptCompany.SelectedItem.ToString()))
                          .CustomerId,
                    StockId = _stockService.GetAll()
                          .SingleOrDefault(p => p.StockName.Equals(stockCb.SelectedItem.ToString())).StockId
                });
                add.ShowDialog();
            }
        }


        /// <summary>
        /// Инициализация товаров на складе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductInStock_MouseEnter(object sender, MouseEventArgs e)
        {
            List<ProductInStock> prodInStock = _productInStockSevice.GetAll() ?? new List<ProductInStock>();
            foreach (var item in prodInStock)
            {
                item.Product = _productService.GetById((int)item.ProductId);
                item.Stock = _stockService.GetById((int)item.StockId);
            }
            prodInStockGrid.ItemsSource = prodInStock;
            StocksBox.ItemsSource = _stockService.GetAll().Select(o => o.StockName);
        }

        /// <summary>
        /// Поиск товаров по складам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindStocksBtn_Click(object sender, RoutedEventArgs e)
        {
            var stockId = _stockService.GetAll().FirstOrDefault(o => o.StockName.Equals(StocksBox.SelectedItem)).StockId;
            var prodInStock  = _productInStockSevice.GetAll().Where(o => o.StockId.Equals(stockId));
            foreach (var item in prodInStock)
            {
                item.Product = _productService.GetById((int)item.ProductId);
                item.Stock = _stockService.GetById((int)item.StockId);
            }
            prodInStockGrid.ItemsSource = prodInStock;
        }


        /// <summary>
        /// Получение отчета по складу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReportC_Click(object sender, RoutedEventArgs e)
        {
            if (ReportComboBox.SelectedItem != null)
            {
                var stockId = _stockService.GetAll()
                    .SingleOrDefault(p => p.StockName.Equals(ReportComboBox.SelectedItem)).StockId;
                reports.GetReportS(stockId, ReportComboBox);
            }
            else
            {
                MessageBox.Show("Выбирете склад");
            }

        }
        /// <summary>
        /// Получение отчета по всем складам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReportCA_Click(object sender, RoutedEventArgs e)
        {
            reports.GetReportAS();
        }
        /// <summary>
        /// Получение отчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReportCK_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerExpenditureReceiptDateFrom.SelectedDate != null && datePickerExpenditureReceiptDateTo.SelectedDate != null)
            {
                NpgsqlDate dataFrom = (NpgsqlDate)datePickerExpenditureReceiptDateFrom.SelectedDate;
                NpgsqlDate dataTo = (NpgsqlDate)datePickerExpenditureReceiptDateTo.SelectedDate;
                reports.GetReportDS(dataFrom, dataTo);
            }
            else
            {
                MessageBox.Show("Выберете период");
            }

        }
        /// <summary>
        /// Получение отчета о наиболее доходных товарах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ButtonReportCL_Click(object sender, RoutedEventArgs e)
        {
            reports.GetReportD();
        }
        /// <summary>
        /// Выход в окно регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
