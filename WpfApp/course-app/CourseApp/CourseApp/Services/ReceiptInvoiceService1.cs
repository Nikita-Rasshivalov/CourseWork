using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseApp.Services
{
    class ReceiptInvoiceService1 : IService<ProductInStockDecorator>
    {
        DbConnection connection = new DbConnection();
        public bool Delete(ProductInStockDecorator entity)
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

        public List<ProductInStockDecorator> GetAll()
        {
            try
            {
                List<ProductInStockDecorator> entities = new List<ProductInStockDecorator>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM receipt_invoices ORDER BY receipt_invoice_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductInStockDecorator entity;

                    string className = reader.GetString(8);

                    if (className.Equals(nameof(ProductInStockWithBigMarkup)))
                        entity = new ProductInStockWithBigMarkup();

                    if (className.Equals(nameof(ProductInStockWithSmallMarkup)))
                        entity = new ProductInStockWithSmallMarkup();

                    if (className.Equals(nameof(ProductInStockWithAverageMarkup)))
                        entity = new ProductInStockWithAverageMarkup();
                    else
                        entity = new ProductInStockWithCustomMarkup(reader.GetFloat(9));

                    entity.ReceiptInvoiceId = reader.GetInt32(0);
                    entity.ReceiptInvoiceDate = reader.GetDate(1);
                    entity.CustomerId = reader.GetInt32(2);
                    entity.ProductId = reader.GetInt32(4);
                    entity.CountProduct = reader.GetFloat(5);
                    entity.StockName = reader.GetString(7);
                    entity.ClassName = className;

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

        public ProductInStockDecorator GetById(int id)
        {
            ProductInStockDecorator entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM receipt_invoices WHERE receipt_invoice_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string className = reader.GetString(6);

                    if (className.Equals(nameof(ProductInStockWithBigMarkup)))
                        entity = new ProductInStockWithBigMarkup();

                    if (className.Equals(nameof(ProductInStockWithSmallMarkup)))
                        entity = new ProductInStockWithSmallMarkup();

                    if (className.Equals(nameof(ProductInStockWithAverageMarkup)))
                        entity = new ProductInStockWithAverageMarkup();
                    else
                        entity = new ProductInStockWithCustomMarkup(reader.GetFloat(7));

                    entity.ReceiptInvoiceId = reader.GetInt32(0);
                    entity.ReceiptInvoiceDate = reader.GetDate(1);
                    entity.CustomerId = reader.GetInt32(2);
                    entity.ProductId = reader.GetInt32(3);
                    entity.CountProduct = reader.GetFloat(4);
                    entity.StockName = reader.GetString(5);
                    entity.ClassName = className;
                }

                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {

            }

            return entity;
        }

        public bool Insert(ProductInStockDecorator entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO receipt_invoices " +
                                                          "(receipt_invoice_date, customer_id, product_id, count_product, stock_name, class_name, markup) " +
                                                          "VALUES (@receipt_invoice_date, @customer_id, @product_id, @count_product, @stock_name, @class_name, @markup);"
                                                          , connection.GetConnection());

                command.Parameters.AddWithValue("@receipt_invoice_date", entity.ReceiptInvoiceDate);
                command.Parameters.AddWithValue("@customer_id", entity.CustomerId);
                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
                command.Parameters.AddWithValue("@stock_name", entity.StockName);
                command.Parameters.AddWithValue("@class_name", entity.GetType().ToString().Split('.').Last());
                command.Parameters.AddWithValue("@markup", entity.Markup);

                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(ProductInStockDecorator entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE receipt_invoices " +
                                                          "SET receipt_invoice_date=@receipt_invoice_date, " +
                                                          "customer_id=@customer_id, " +
                                                          "product_id=@product_id, " +
                                                          "count_product=@count_product, " +
                                                          "stock_name=@stock_name " +
                                                          "class_name=@class_name " +
                                                          "markup=@markup " +
                                                          "WHERE receipt_invoice_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@receipt_invoice_date", entity.ReceiptInvoiceDate);
                command.Parameters.AddWithValue("@customer_id", entity.CustomerId);
                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
                command.Parameters.AddWithValue("@stock_name", entity.StockName);
                command.Parameters.AddWithValue("@class_name", entity.GetType().ToString().Split('.').Last());
                command.Parameters.AddWithValue("@markup", entity.Markup);
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
    }
}
