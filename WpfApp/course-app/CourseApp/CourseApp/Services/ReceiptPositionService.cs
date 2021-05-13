using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Services
{
    public class ReceiptPositionService: IService<ReceiptPosition>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление позиции приходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(ReceiptPosition entity)
        {
            try
            {
                Npgsql.NpgsqlCommand command = new NpgsqlCommand("DELETE FROM receipt_positions WHERE position_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.PositionId);
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
        /// Получение всех позиций приходных накладных.
        /// </summary>
        /// <returns></returns>
        public List<ReceiptPosition> GetAll()
        {
            try
            {
                List<ReceiptPosition> entities = new List<ReceiptPosition>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM receipt_positions ORDER BY position_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ReceiptPosition entity = new ReceiptPosition()
                    {
                        PositionId = reader.GetInt32(0),
                        CountProduct = reader.GetDouble(1),
                        ProductId = reader.GetInt32(2),
                        ReceiptInvoiceId = reader.GetInt32(3)
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
        public ReceiptPosition GetById(int id)
        {
            ReceiptPosition entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM receipt_positions WHERE position_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new ReceiptPosition()
                    {
                        PositionId = reader.GetInt32(0),
                        CountProduct = reader.GetDouble(1),
                        ProductId = reader.GetInt32(2),
                        ReceiptInvoiceId = reader.GetInt32(3)
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
        public bool Insert(ReceiptPosition entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO receipt_positions " +
                                                          "(product_id, count_product,receipt_invoice_id) " +
                                                          "VALUES (@product_id, @count_product,@receipt_invoice_id);"
                                                          , connection.GetConnection());
                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
                command.Parameters.AddWithValue("@receipt_invoice_id", entity.ReceiptInvoiceId);


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
        public bool Update(ReceiptPosition entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE receipt_positions " +
                                                          "SET count_product=@count_product " +
                                                          "WHERE position_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", entity.PositionId);
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
