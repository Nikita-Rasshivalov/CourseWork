using Npgsql;
using System.Data;

namespace CourseApp.Utility
{
    /// <summary>
    /// Подключение к БД.
    /// </summary>
    public class DbConnection
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=Stonks; User Id=postgres; Password = 1;");

        /// <summary>
        /// Полученик соединения.
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection GetConnection()
        {
            // Проверяем, закрыто ли соединение.
            if (connection.State == ConnectionState.Closed)
                // Если закрыто, то открываем.
                connection.Open();
            return connection;
        }

        /// <summary>
        /// Закрытие соединения.
        /// </summary>
        public void CloseConnection()
        {
            // Проверяем, открыто ли соединение.
            if (connection.State == ConnectionState.Open)
                // Если да, то закрываем.
                connection.Close();
        }
    }
}
