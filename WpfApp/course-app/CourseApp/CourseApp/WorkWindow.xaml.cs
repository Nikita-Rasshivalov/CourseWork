using Microsoft.Win32;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using CourseApp.Services;
using CourseApp.Models;
using CourseApp.Utility;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        private int userId;
        private string roleKey = "";
        private Reports r = new Reports();
        private User selectedUser = null;
        private Product selectedProduct = null;
        private Customer selectedCustomer = null;

        // Инициализация сервисов для работы с таблицами в БД.
        IService<User> _userService = new UserService();
        IService<Role> _roleService = new RoleService();
        IService<Stock> _stockService = new StockService();
        IService<Product> _productService = new ProductService();
        IService<Customer> _customerService = new CustomerService();
        IService<ProductInStockDecorator> _receiptInvoiceService = new ReceiptInvoiceService1();
        IService<ExpenditureInvoice> _expenditureInvoiceService = new ExpenditureInvoiceService();

        public WorkWindow(string roleKey, int userId)
        {
            InitializeComponent();
            this.roleKey = roleKey;
            this.userId = userId;
            confidantility();
            textBoxExpenditureReceiptPrice.IsEnabled = false;
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
            labelExpenditureReceiptProduct.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            comboBoxExpenditureReceiptProduct.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptStock.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptCompany.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            comboBoxExpenditureReceiptCompany.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            stockCb.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            markupTb.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptCount.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            textBoxExpenditureReceiptCount.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptPrice.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            textBoxExpenditureReceiptPrice.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            labelExpenditureReceiptOperation.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            comboBoxExpenditureReceiptOperation.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            buttonExpenditureReceiptSave.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
            idExpenditureReceipt.Visibility = !roleKey.Equals("manager") ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Привязка организаций в таблицу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            // Получение всех организаций и привязка в таблицу.
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
                // Если есть выделенная организация, то обновляем.
                selectedCustomer.CustomerName = textBoxCustomerName.Text;
                selectedCustomer.Description = textBoxCustomerDescription.Text;

                _customerService.Update(selectedCustomer);

                selectedCustomer = null;
                textBoxCustomerName.Text = "";
                textBoxCustomerDescription.Text = "";
            }
            else
            {
                // Иначе - добавляем новую.
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
            listBoxUserRole.ItemsSource = _roleService.GetAll()?.Select(o => o.RoleKey);
        }

        /// <summary>
        /// Добавление/обновление юзера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUser_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser != null)
            {
                // Если есть выделенный пользователь, то обновляем.
                selectedUser.UserName = textBoxUserName.Text;
                selectedUser.FullName = textBoxUserFullName.Text;
                selectedUser.UserPass = passwordBoxUser.Password;
                selectedUser.RoleKey = listBoxUserRole.SelectedItem.ToString();

                _userService.Update(selectedUser);

                selectedUser = null;
                textBoxUserName.Text = "";
                textBoxUserFullName.Text = "";
                passwordBoxUser.Password = "";
            }
            else
            {
                // Иначе - добавляем нового.
                _userService.Insert(new User
                {
                    UserName = textBoxUserName.Text,
                    FullName = textBoxUserFullName.Text,
                    UserPass = passwordBoxUser.Password,
                    RoleKey = listBoxUserRole.SelectedItem.ToString()
                });

                selectedUser = null;
                textBoxUserName.Text = "";
                textBoxUserFullName.Text = "";
                passwordBoxUser.Password = "";
            }

            // Обновляем таблицу и листбокс в приложении.
            dataGridUser.ItemsSource = _userService.GetAll();
            listBoxUserRole.ItemsSource = _roleService.GetAll().Select(o => o.RoleKey);
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
                listBoxUserRole.SelectedItem = selectedUser.RoleKey;
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
        /// Очистка контролов продуктов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProductClear_Click(object sender, RoutedEventArgs e)
        {
            selectedProduct = null;
            textBoxProductName.Text = "";
            priceTb.Text = "" ;
        }

        /// <summary>
        /// Добавление/обновление продукта.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                selectedProduct.ProductName = textBoxProductName.Text;
                selectedProduct.ProductPrice = (float)Convert.ToDouble(priceTb.Text);

                _productService.Update(selectedProduct);

                selectedProduct = null;
                textBoxProductName.Text = priceTb.Text = "";
            }
            else
            {
                _productService.Insert(new Product
                {
                    ProductName = textBoxProductName.Text,
                    ProductPrice = (float)Convert.ToDouble(priceTb.Text)
                });

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
                _productService.Delete((Product)dataGridProduct.SelectedItem);
                dataGridProduct.Items.Refresh();
                dataGridProduct.ItemsSource = _productService.GetAll();
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
        /// Отображение выделенного склада в контролы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Очистка контролов склада.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStockClear_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Добавление/обновление склада.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStockSave_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Привязка приходных накладных к таблице.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdReceiptInvoices_MouseEnter(object sender, MouseEventArgs e)
        {
            List<ProductInStockDecorator> receiptInvoices = _receiptInvoiceService.GetAll() ?? new List<ProductInStockDecorator>();

            foreach (var item in receiptInvoices)
            {
                item.Customer = _customerService.GetById((int)item.CustomerId);
                item.Product = _productService.GetById((int)item.ProductId);
            }

            dataGridReceiptInvoices.ItemsSource = receiptInvoices;
        }

        /// <summary>
        /// Прривязка отходных накладных к таблице.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdExpenditureInvoices_MouseEnter(object sender, MouseEventArgs e)
        {
            List<ExpenditureInvoice> expenditureInvoices = _expenditureInvoiceService.GetAll() ?? new List<ExpenditureInvoice>();

            foreach (var item in expenditureInvoices)
            {
                item.Customer = _customerService.GetById((int)item.CustomerId);
                item.Product = _productService.GetById((int)item.ProductId);
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
            initControls();
        }

        /// <summary>
        /// Добавление/обновление приходных/отходных накладных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExpenditureReceiptSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxExpenditureReceiptProduct.SelectedItem == null ||
                comboBoxExpenditureReceiptCompany.SelectedItem == null ||
                textBoxExpenditureReceiptCount.Text == "" ||
                textBoxExpenditureReceiptPrice.Text == "")
            {
                MessageBox.Show("Заполние все поля.");
            }
            else if (comboBoxExpenditureReceiptOperation.SelectedItem == null)
            {
                MessageBox.Show("Выберите операцию!");
            }
            else if (comboBoxExpenditureReceiptOperation.SelectedItem.ToString() == "Приход")
            {
                if (checkMarkup())
                {
                    _receiptInvoiceService.Insert(GetProductByMarkUp((float)Convert.ToDouble(markupTb.Text)));
                    textBoxExpenditureReceiptCount.Text = "0";
                    textBoxExpenditureReceiptPrice.Text = "0";
                    MessageBox.Show("Приход выполнен успешно.");
                }

            }
            else if (comboBoxExpenditureReceiptOperation.SelectedItem.ToString() == "Отгрузка")
            {
                int productId = _productService.GetAll()
                                               .SingleOrDefault(p => p.ProductName.Equals(comboBoxExpenditureReceiptProduct.SelectedItem.ToString()))
                                               .EntityId;

                float? countProductR = _receiptInvoiceService.GetAll()
                                                             .Where(i => i.StockName.Equals(stockCb.SelectedItem.ToString()) && i.ProductId == productId)
                                                             .Select(o => o.CountProduct)
                                                             .Sum();

                float? countProductE = _expenditureInvoiceService.GetAll()
                                                                 .Where(i => i.StockName.Equals(stockCb.SelectedItem.ToString()) && i.ProductId == productId)
                                                                 .Select(o => o.CountProduct)
                                                                 .Sum();

                countProductE = countProductE == null ? 0 : countProductE;
                countProductR = countProductR == null ? 0 : countProductR;

                if (countProductR - countProductE - float.Parse(textBoxExpenditureReceiptCount.Text == "" ? "0" : textBoxExpenditureReceiptCount.Text) >= 0)
                {
                    _expenditureInvoiceService.Insert(new ExpenditureInvoice
                    {
                        ExpenditureInvoiceDate = (NpgsqlDate)datePickerExpenditureReceiptDate.DisplayDate,
                        ProductId = productId,
                        CustomerId = _customerService.GetAll()
                                .SingleOrDefault(p => p.CustomerName.Equals(comboBoxExpenditureReceiptCompany.SelectedItem.ToString())).CustomerId,
                        StockName = stockCb.SelectedItem.ToString(),
                        CountProduct = float.Parse(textBoxExpenditureReceiptCount.Text == "" ? "0" : textBoxExpenditureReceiptCount.Text),
                        PriceProduct = float.Parse(textBoxExpenditureReceiptPrice.Text == "" ? "0" : textBoxExpenditureReceiptPrice.Text)
                    });

                    textBoxExpenditureReceiptCount.Text = "0";
                    textBoxExpenditureReceiptPrice.Text = "0";
                    MessageBox.Show("Отгрузка выполнена успешно.");
                }
                else
                {
                    MessageBox.Show("Недостаточно материала для отгрузки.");
                }
                initControls();
            }
        }

        /// <summary>
        /// Инициализация контролов.
        /// </summary>
        private void initControls()
        {
            comboBoxExpenditureReceiptProduct.ItemsSource = _productService.GetAll()?.Select(p => p.ProductName);

            var stocks = _stockService.GetAll();
            stockCb.ItemsSource = _stockService.GetAll()?.Select(p => p.StockName);
            markupTb.Text = stocks[0].Markup.ToString();
            markupTb.IsEnabled = false;
            comboBoxExpenditureReceiptCompany.ItemsSource = _customerService.GetAll()?.Select(c => c.CustomerName);
            comboBoxExpenditureReceiptOperation.ItemsSource = new List<string>() { "Приход", "Отгрузка" };
            comboBoxExpenditureReceiptStockC.ItemsSource = stockCb.ItemsSource;
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
                textBoxExpenditureReceiptPrice.IsEnabled = false;
                textBoxExpenditureReceiptPrice.Visibility = labelExpenditureReceiptPrice.Visibility = Visibility.Visible;
                stockCb.Visibility = Visibility.Visible;
               
            }
            else
            {
                textBoxExpenditureReceiptPrice.Visibility = labelExpenditureReceiptPrice.Visibility = Visibility.Hidden;
                stockCb.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Вычисление цены.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxExpenditureReceiptCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (comboBoxExpenditureReceiptOperation != null &&
                comboBoxExpenditureReceiptOperation.SelectedItem != null &&
                comboBoxExpenditureReceiptProduct.SelectedItem != null &&
                stockCb.SelectedItem != null)
            {
                var product = LoadProductWhithFullData();
                float? avgPrice = _receiptInvoiceService.GetAll()
                                                        .Where(i => i.StockName.Equals(stockCb.SelectedItem.ToString()) && i.ProductId == product.ProductId)
                                                        .Select(o => o.ProductPrice)
                                                        .Average();

                textBoxExpenditureReceiptPrice.Text = String.Format("{0:0.0}", (float.Parse(textBoxExpenditureReceiptCount.Text == "" ? "0" : textBoxExpenditureReceiptCount.Text) * product.GetFullPrice()).ToString());
            }
        }

        private void ButtonReportC_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxExpenditureReceiptStockC == null ||
                comboBoxExpenditureReceiptStockC.SelectedItem == null)
            {
                MessageBox.Show("Выберите склад");
            }
            else
            {
                DbConnection connection = new DbConnection();
                try
                {
                    int stockId = _stockService.GetAll()
                                               .FirstOrDefault(o => o.StockName.Equals(comboBoxExpenditureReceiptStockC.SelectedItem.ToString()))
                                               .StockId;

                    string query = "SELECT stocks.stock_name, products.product_name, sum(receipt_invoices.count_product)" +
                                    " FROM receipt_invoices" +
                                    " JOIN stocks ON stocks.stock_id = receipt_invoices.stock_id" +
                                    " JOIN products ON products.product_id = receipt_invoices.product_id" +
                                    " WHERE receipt_invoices.stock_id = " + stockId +
                                    " GROUP BY stocks.stock_name, products.product_name";

                    NpgsqlCommand command = new NpgsqlCommand(query, connection.GetConnection());
                    NpgsqlDataReader reader = command.ExecuteReader();

                    XDocument xdoc = new XDocument();
                    XElement stocks = new XElement("Склады");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            XElement stock = new XElement("Склад");
                            XAttribute istockNameAttr = new XAttribute("Наименование", reader["stock_name"].ToString());
                            XElement istockProductElem = new XElement("Продукт", reader["product_name"].ToString());
                            XElement istockSumElem = new XElement("Количество", reader["sum"].ToString());
                            stock.Add(istockNameAttr);
                            stock.Add(istockProductElem);
                            stock.Add(istockSumElem);
                            stocks.Add(stock);
                        }
                        reader.Close();

                    }
                    xdoc.Add(stocks);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XML-File | *.xml";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        xdoc.Save(saveFileDialog.FileName);
                        MessageBox.Show("Файл сохранен");
                    }
                }
                catch (NpgsqlException ex)
                {

                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }

        private void ButtonReportCA_Click(object sender, RoutedEventArgs e)
        {
            r.GetReportCA();
        }

        private void ButtonReportCK_Click(object sender, RoutedEventArgs e)
        {
            string dataFrom = datePickerExpenditureReceiptDateFrom.DisplayDate.ToString("yyyy-MM-dd");
            string dataTo = datePickerExpenditureReceiptDateTo.DisplayDate.ToString("yyyy-MM-dd");
            r.GetReportCK(dataFrom, dataTo);
        }

        private void ButtonReportCL_Click(object sender, RoutedEventArgs e)
        {

            r.GetReportCL();
        }

        private void textBoxStockMarkup_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Проверка коректности надбавки.
        /// </summary>
        /// <returns></returns>
        private bool checkMarkup()
        {
            
            int number = int.Parse(markupTb.Text);
            if (number < 10 || number > 25)
            {
                MessageBox.Show("Наценка введена не верно (>= 10 и <= 25%");
                return false;
            }
            return true;
        }

        private ProductInStockDecorator GetProductByMarkUp(float markup)
        {
            ProductInStockDecorator product = null;
            if (markup == 5)
                product = new ProductInStockWithSmallMarkup();
            else if (markup == 15)
                product = new ProductInStockWithAverageMarkup();
            else if (markup == 25)
                product = new ProductInStockWithBigMarkup();
            else
                product = new ProductInStockWithCustomMarkup(markup);

            product.ReceiptInvoiceDate = (NpgsqlDate)datePickerExpenditureReceiptDate.DisplayDate;
            product.ProductId = _productService.GetAll()
                                .SingleOrDefault(p => p.ProductName.Equals(comboBoxExpenditureReceiptProduct.SelectedItem.ToString())).EntityId;
            product.CustomerId = _customerService.GetAll()
                           .SingleOrDefault(p => p.CustomerName.Equals(comboBoxExpenditureReceiptCompany.SelectedItem.ToString())).CustomerId;
            product.CountProduct = float.Parse(textBoxExpenditureReceiptCount.Text == "" ? "0" : textBoxExpenditureReceiptCount.Text);
            product.ProductPrice = float.Parse(textBoxExpenditureReceiptPrice.Text == "" ? "0" : textBoxExpenditureReceiptPrice.Text);
            product.Markup = markup;
            product.StockName = stockCb.Text;

            return product;
        }

        private ProductInStockDecorator LoadProductWhithFullData()
        {
            var products = _receiptInvoiceService.GetAll();

            foreach (var item in products)
                item.Product = _productService.GetById((int)item.ProductId);

            return products.Where(p => p.Product.ProductName.Equals(comboBoxExpenditureReceiptProduct.SelectedItem.ToString())).First();

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();

        }

     
    }
}
