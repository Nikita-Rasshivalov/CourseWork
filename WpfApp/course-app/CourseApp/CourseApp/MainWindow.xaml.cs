using System.Linq;
using System.Windows;
using CourseApp.Models;
using CourseApp.Services;

namespace CourseApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IService<User> _userService = new UserService();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLoginPress_Click(object sender, RoutedEventArgs e)
        {
            // Получение пользователя с заданным логином и паролем.
            var user = _userService.GetAll()
                                   .SingleOrDefault(u => u.UserName.Equals(textBoxLogin.Text) && u.UserPass.Equals(passwordBoxPass.Password));

            if (user == null)
            {
                // Если не найден, то выводим соответсвующее сообщение.
                MessageBox.Show("Логин или пароль введены не верно!");
                passwordBoxPass.Password = "";

            }
            else
            {
                // Если пользователь найден, то открываем рабочее окно.
                WorkWindow workWindow = new WorkWindow(user.RoleKey, user.UserId);
                workWindow.Show();
                //this.Close();
                this.Hide();
                
            }
        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
