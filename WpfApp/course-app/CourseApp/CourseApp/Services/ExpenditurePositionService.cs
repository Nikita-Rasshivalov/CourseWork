using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CourseApp.Services
{
    public class ExpenditurePositionService : IService<ExpenditurePosition>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление позиции отходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(ExpenditurePosition entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM expenditure_positions " +
                    "WHERE expenditure_position_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.ExpenditurePositionId);

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
        /// Получение всех позиций отходных накладных.
        /// </summary>
        /// <returns></returns>
        public List<ExpenditurePosition> GetAll()
        {
            try
            {
                List<ExpenditurePosition> entities = new List<ExpenditurePosition>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM expenditure_positions " +
                    "ORDER BY expenditure_position_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExpenditurePosition entity = new ExpenditurePosition()
                    {
                        ExpenditurePositionId = reader.GetInt32(0),
                        ProductId = reader.GetInt32(1),
                        CountProduct = reader.GetDouble(2),
                        ExpenditureInvoiceId = reader.GetInt32(3),
                        ProductPrice = Math.Round(reader.GetDouble(4),3)

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
        /// Получение  позиции отходной накладной по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExpenditurePosition GetById(int id)
        {
            ExpenditurePosition entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM expenditure_positions " +
                    "WHERE expenditure_position_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new ExpenditurePosition()
                    {
                        ExpenditurePositionId = reader.GetInt32(0),
                        ProductId = reader.GetInt32(1),
                        CountProduct = reader.GetDouble(2),
                        ExpenditureInvoiceId = reader.GetInt32(3),
                        ProductPrice = reader.GetDouble(4)
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
        /// Добавление позиции  отходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(ExpenditurePosition entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO expenditure_positions " +
                                                          "( product_id, count_product,expenditure_invoice_id,product_price) " +
                                                          "VALUES (@product_id, @count_product,@expenditure_invoice_id,@product_price);"
                                                          , connection.GetConnection());

                command.Parameters.AddWithValue("@product_id", entity.ProductId);
                command.Parameters.AddWithValue("@count_product", entity.CountProduct);
                command.Parameters.AddWithValue("@expenditure_invoice_id", entity.ExpenditureInvoiceId);
                command.Parameters.AddWithValue("@product_price", entity.ProductPrice);
                command.ExecuteNonQuery();
                connection.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обновление позиции отходной накладной.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(ExpenditurePosition entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE expenditure_positions " +
                                                          "SET count_product=@count_product " +
                                                          "WHERE product_id=@product_id", connection.GetConnection());
                command.Parameters.AddWithValue("@product_id", entity.ProductId);
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

