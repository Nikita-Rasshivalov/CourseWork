using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System.Collections.Generic;

namespace CourseApp.Services
{
    /// <summary>
    /// Сервис для работы с организациями.
    /// </summary>
    public class CustomerService : IService<Customer>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Получение всех кастомеров.
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAll()
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM customers ORDER BY customer_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer();

                    customer.CustomerId = reader.GetInt32(0);
                    customer.CustomerName = reader.GetString(1);
                    customer.Description = reader.GetString(2);

                    customers.Add(customer);
                }

                connection.CloseConnection();
                return customers;
            }
            catch (NpgsqlException ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Обновление кастомера.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Customer entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE customers SET customer_name=@name, description=@description WHERE customer_id=@id;", connection.GetConnection());
                
                command.Parameters.AddWithValue("@id", entity.CustomerId);
                command.Parameters.AddWithValue("@name", entity.CustomerName);
                command.Parameters.AddWithValue("@description", entity.Description);

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
        /// Удаление кастомера.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(Customer entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM customers WHERE customer_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.CustomerId);

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
        /// Получение кастомера по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetById(int id)
        {
            Customer customer = null;

            try
            {             
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM customers WHERE customer_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = reader.GetInt32(0),
                        CustomerName = reader.GetString(1),
                        Description = reader.GetString(2)
                    };
                }

                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {

            }

            return customer;
        }


        /// <summary>
        /// Добавление кастомера.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Customer entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO customers (customer_name, description) VALUES(@name, @description);", connection.GetConnection());

                command.Parameters.AddWithValue("@name", entity.CustomerName);
                command.Parameters.AddWithValue("@description", entity.Description);

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
