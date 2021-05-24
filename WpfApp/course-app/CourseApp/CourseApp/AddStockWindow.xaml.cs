using CourseApp.Models;
using CourseApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для AddStockWindow.xaml
    /// </summary>
    public partial class AddStockWindow : Window
    {   // Инициализация сервисов для работы с таблицами в БД.
        IService<User> _userService = new UserService();
        IService<Stock> _stockService = new StockService();
        private Stock selectedStock = null;


        /// <summary>
        /// Инициализация окна 
        /// </summary>
        public AddStockWindow()
        {
            InitializeComponent();
            InitializeStocks();
            StokerComboBox.ItemsSource = _userService.GetAll().Where(o => o.RoleKey.Equals("stoker")).Select(o => o.FullName);
        }
        /// <summary>
        /// Инициализация таблицы складов
        /// </summary>
        public void InitializeStocks()
        {
            var stokeList = _stockService.GetAll() ?? new List<Stock>();
            foreach (var item in stokeList)
            {
                item.User = _userService.GetById((int)item.UserId);
            }
            StocksGrid.ItemsSource = stokeList;

        }
        /// <summary>
        /// Ввод только целых чисел в нацеку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkupBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Выход в главное меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Удаление склада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StocksGrid.SelectedItem != null)
            {
                _stockService.Delete(new Stock
                {
                    StockId = ((Stock)(StocksGrid.SelectedItem)).StockId
                });
                InitializeStocks();
            }
        }
        /// <summary>
        /// Обновление складов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdStockBtn_Click(object sender, RoutedEventArgs e)
        {

            if (selectedStock != null)
            {
                selectedStock = (Stock)StocksGrid.SelectedItem;
                selectedStock.StockName = NameBox.Text;
                selectedStock.Description = DescrBox.Text;
                selectedStock.Markup = double.Parse(MarkupBox.Text);

                _stockService.Update(selectedStock);

                selectedStock = null;
                
            }
            else
            {
                var userId = _userService.GetAll().FirstOrDefault(o => o.FullName.Equals(StokerComboBox.SelectedItem)).UserId;
                _stockService.Insert(new Stock
                {
                    StockName = NameBox.Text,
                    Description = DescrBox.Text,
                    Markup = double.Parse(MarkupBox.Text),
                    UserId = userId
                });

                NameBox.Text = "";
                DescrBox.Text = "";
                MarkupBox.Text = "";
            }
            InitializeStocks();
        }
        /// <summary>
        /// Отображение информации в контролы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StocksGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StocksGrid.SelectedItem != null)
            {
                selectedStock = (Stock)StocksGrid.SelectedItem;
                NameBox.Text = selectedStock.StockName;
                DescrBox.Text = selectedStock.Description;
                MarkupBox.Text = selectedStock.Markup.ToString();
            }
        }
    }
}
