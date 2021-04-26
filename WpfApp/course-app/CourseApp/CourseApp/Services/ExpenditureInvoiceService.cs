using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System.Collections.Generic;

namespace CourseApp.Services
{
    /// <summary>
    /// Сервис для работы с отходными накладными.
    /// </summary>
    public class ExpenditureInvoiceService : IService<ExpenditureInvoice>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление отходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(ExpenditureInvoice entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM expenditure_invoices WHERE expenditure_invoice_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.ExpenditureInvoiceId);

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
        /// Получение всех отходных накладных.
        /// </summary>
        /// <returns></returns>
        public List<ExpenditureInvoice> GetAll()
        {
            try
            {
                List<ExpenditureInvoice> entities = new List<ExpenditureInvoice>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM expenditure_invoices ORDER BY expenditure_invoice_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExpenditureInvoice entity = new ExpenditureInvoice()
                    {
                        ExpenditureInvoiceId = reader.GetInt32(0),
                        ExpenditureInvoiceDate = reader.GetDate(1),
                        CustomerId = reader.GetInt32(2),
                        StockName = reader.GetString(6),
                        ProductId = reader.GetInt32(3),
                        CountProduct = reader.GetFloat(4),
                        PriceProduct = reader.GetFloat(5)
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
        /// Получение отходной накладной по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExpenditureInvoice GetById(int id)
        {
            ExpenditureInvoice entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM expenditure_invoices WHERE expenditure_invoice_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new ExpenditureInvoice()
                    {
                        ExpenditureInvoiceId = reader.GetInt32(0),
                        ExpenditureInvoiceDate = reader.GetDate(1),
                        CustomerId = reader.GetInt32(2),
                        StockName = reader.GetString(3),
                        ProductId = reader.GetInt32(4),
                        CountProduct = reader.GetFloat(5),
                        PriceProduct = reader.GetFloat(6)
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
        /// Добавление отходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(ExpenditureInvoice entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO expenditure_invoices " +
                                                          "(expenditure_invoice_date, customer_id, stock_name, product_id, count_product, price_product) " +
                                                          "VALUES (@expenditure_invoice_date, @customer_id, @stock_name, @product_id, @count_product, @price_product);"
                                                          , connection.GetConnection());

                command.Parameters.AddWithValue("@expenditure_invoice_date", entity.ExpenditureInvoiceDate);
                command.Parameters.AddWithValue("@customer_id", entity.CustomerId);
                command.Parameters.AddWithValue("@stock_name", entity.StockName);
                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
                command.Parameters.AddWithValue("@price_product", entity.PriceProduct);

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
        /// Обновление отходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(ExpenditureInvoice entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE expenditure_invoices " +
                                                          "SET expenditure_invoice_date=@expenditure_invoice_date, " +
                                                          "customer_id=@customer_id, " +
                                                          "stock_name=@stock_name, " +
                                                          "product_id=@product_id, " +
                                                          "count_product=@count_product, " +
                                                          "price_product=@price_product " +
                                                          "WHERE expenditure_invoice_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.ExpenditureInvoiceId);
                command.Parameters.AddWithValue("@expenditure_invoice_date", entity.ExpenditureInvoiceDate);
                command.Parameters.AddWithValue("@customer_id", entity.CustomerId);
                command.Parameters.AddWithValue("@stock_name", entity.StockName);
                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
                command.Parameters.AddWithValue("@price_product", entity.PriceProduct);

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
