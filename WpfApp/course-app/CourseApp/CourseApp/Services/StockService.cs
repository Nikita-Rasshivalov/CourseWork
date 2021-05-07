using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System.Collections.Generic;

namespace CourseApp.Services
{
    /// <summary>
    /// Сервис для работы со складами.
    /// </summary>
    public class StockService : IService<Stock>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление склада.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(Stock entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM stocks WHERE stock_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.UserId);

                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ПОлучение всех складов.
        /// </summary>
        /// <returns></returns>
        public List<Stock> GetAll()
        {
            try
            {
                List<Stock> entities = new List<Stock>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM stocks ORDER BY stock_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Stock entity = new Stock()
                    {
                        StockId = reader.GetInt32(0),
                        StockName = reader.GetString(1),
                        Markup = reader.GetDouble(3)
                    };

                    entities.Add(entity);
                }

                connection.CloseConnection();
                return entities;
            }
            catch (NpgsqlException ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Получение склада по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Stock GetById(int id)
        {
            Stock entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM stocks WHERE stock_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new Stock()
                    {
                        StockId = reader.GetInt32(0),
                        StockName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Markup = reader.GetDouble(3)
                    };
                }

                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {

            }

            return entity;
        }

        /// <summary>
        /// Добавление склада.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Stock entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO stocks(stock_name, markup) VALUES(@name, @markup);", connection.GetConnection());

                command.Parameters.AddWithValue("@name", entity.StockName);
                //command.Parameters.AddWithValue("@description", entity.Description);
                command.Parameters.AddWithValue("@markup", entity.Markup);
                //command.Parameters.AddWithValue("@user_id", entity.UserId);

                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обновление склада.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Stock entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE stocks SET stock_name=@name, description=@description, markup=@markup, user_id=@user_id WHERE stock_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.StockId);
                command.Parameters.AddWithValue("@name", entity.StockName);
                command.Parameters.AddWithValue("@description", entity.Description);
                command.Parameters.AddWithValue("@markup", entity.Markup);
                command.Parameters.AddWithValue("@user_id", entity.UserId);

                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

            return true;
        }
    }
}
