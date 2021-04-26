using System.Collections.Generic;
using CourseApp.Utility;
using CourseApp.Models;
using Npgsql;

namespace CourseApp.Services
{
    /// <summary>
    /// Сервис для работы с продукцией.
    /// </summary>
    public class ProductService : IService<Product>
    {
        DbConnection connection = new DbConnection();

        public bool Delete(Product entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM products WHERE product_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.EntityId);

                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

            return true;
        }

        public List<Product> GetAll()
        {
            try
            {
                List<Product> entities = new List<Product>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM products ORDER BY product_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product entity = new Product()
                    {
                        EntityId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductPrice = reader.GetFloat(2)
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

        public Product GetById(int id)
        {
            Product entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM products WHERE product_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new Product()
                    {
                        EntityId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductPrice = reader.GetFloat(2)
                    };
                }

                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {

            }

            return entity;
        }

        public bool Insert(Product entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO products(product_name, product_price) VALUES (@name, @product_price);", connection.GetConnection());

                command.Parameters.AddWithValue("@name", entity.ProductName);
                command.Parameters.AddWithValue("@product_price", entity.ProductPrice);

                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(Product entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE products SET product_name=@name, product_price=@product_price WHERE product_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.EntityId);
                command.Parameters.AddWithValue("@name", entity.ProductName);
                command.Parameters.AddWithValue("@product_price", entity.ProductPrice);

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
