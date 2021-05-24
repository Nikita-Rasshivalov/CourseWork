using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System.Collections.Generic;

namespace CourseApp.Services
{
    public class ProductInStockeService : IService<ProductInStock>
    {
        DbConnection connection = new DbConnection();
        /// <summary>
        /// Удаление товаров со склада
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(ProductInStock entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM products_in_stock  WHERE id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.Id);

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
        /// Получение всех товаров со склада
        /// </summary>
        /// <returns></returns>
        public List<ProductInStock> GetAll()
        {
            try
            {
                List<ProductInStock> entities = new List<ProductInStock>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM products_in_stock ORDER BY product_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductInStock entity = new ProductInStock()
                    {
                        Id = reader.GetInt32(0),
                        ProductId = reader.GetInt32(1),
                        StockId = reader.GetInt32(2),
                        CountProduct = reader.GetDouble(3)
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
        /// Получение товаров со склада по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductInStock GetById(int id)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Вставка данных в таблицу
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(ProductInStock entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO products_in_stock(product_id, stock_id,count_product) VALUES (@product_id, @stock_id, @count_product);", connection.GetConnection());

                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@stock_id", entity.StockId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
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
        /// Обновление количества товаров на складе
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public bool Update(ProductInStock entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE products_in_stock SET count_product=@count_product WHERE id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
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
