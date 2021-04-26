using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System.Collections.Generic;

namespace CourseApp.Services
{
    /// <summary>
    /// Сервис для работы с приходными накладными.
    /// </summary>
    public class ReceiptInvoiceService : IService<ReceiptInvoice>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление приходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(ReceiptInvoice entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM receipt_invoices WHERE receipt_invoice_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.ReceiptInvoiceId);

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
        /// Получение всех приходных накладных.
        /// </summary>
        /// <returns></returns>
        public List<ReceiptInvoice> GetAll()
        {
            try
            {
                List<ReceiptInvoice> entities = new List<ReceiptInvoice>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM receipt_invoices ORDER BY receipt_invoice_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ReceiptInvoice entity = new ReceiptInvoice()
                    {
                        ReceiptInvoiceId = reader.GetInt32(0),
                        ReceiptInvoiceDate = reader.GetDate(1),
                        CustomerId = reader.GetInt32(2),
                        StockId = reader.GetInt32(3),
                        ProductId = reader.GetInt32(4),
                        CountProduct = reader.GetFloat(5),
                        PriceProduct = reader.GetFloat(6)
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
        /// Получение приходной накладной по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReceiptInvoice GetById(int id)
        {
            ReceiptInvoice entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM receipt_invoices WHERE receipt_invoice_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new ReceiptInvoice()
                    {
                        ReceiptInvoiceId = reader.GetInt32(0),
                        ReceiptInvoiceDate = reader.GetDate(1),
                        CustomerId = reader.GetInt32(2),
                        StockId = reader.GetInt32(3),
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
        /// Добавление приходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(ReceiptInvoice entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO receipt_invoices " +
                                                          "(receipt_invoice_date, customer_id, stock_id, product_id, count_product, price_product) " +
                                                          "VALUES (@receipt_invoice_date, @customer_id, @stock_id, @product_id, @count_product, @price_product);"
                                                          , connection.GetConnection());

                command.Parameters.AddWithValue("@receipt_invoice_date", entity.ReceiptInvoiceDate);
                command.Parameters.AddWithValue("@customer_id", entity.CustomerId);
                command.Parameters.AddWithValue("@stock_id", entity.StockId);
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
        /// Обновление приходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(ReceiptInvoice entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE receipt_invoices " +
                                                          "SET receipt_invoice_date=@receipt_invoice_date, " +
                                                          "customer_id=@customer_id, " +
                                                          "stock_id=@stock_id, " +
                                                          "product_id=@product_id, " +
                                                          "count_product=@count_product, " +
                                                          "price_product=@price_product " +
                                                          "WHERE receipt_invoice_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.ReceiptInvoiceId);
                command.Parameters.AddWithValue("@receipt_invoice_date", entity.ReceiptInvoiceDate);
                command.Parameters.AddWithValue("@customer_id", entity.CustomerId);
                command.Parameters.AddWithValue("@stock_id", entity.StockId);
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
