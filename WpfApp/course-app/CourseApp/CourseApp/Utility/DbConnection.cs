using Npgsql;
using System.Configuration;
using System.Data;

namespace CourseApp.Utility
{
    /// <summary>
    /// Подключение к БД.
    /// </summary>
    public class DbConnection
    {
        private readonly NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString);
        /// <summary>
        /// Получение соединения.
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
