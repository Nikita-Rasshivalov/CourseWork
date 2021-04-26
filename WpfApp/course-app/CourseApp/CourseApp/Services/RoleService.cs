using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CourseApp.Services
{
    public class RoleService : IService<Role>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление роли.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(Role entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM roles WHERE role_key=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.RoleKey);

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
        /// Получение всех ролей.
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAll()
        {
            try
            {
                List<Role> entities = new List<Role>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM roles", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Role entity = new Role()
                    {
                        RoleName = reader.GetString(0),
                        RoleKey = reader.GetString(1)
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
        /// Получение роли по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetById(int id)
        {
            Role entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM roles WHERE role_key=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new Role()
                    {
                        RoleName = reader.GetString(0),
                        RoleKey = reader.GetString(1)
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
        /// Добавление роли.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Role entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обновление роли.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Role entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE roles SET role_name=@name WHERE role_key=@key;", connection.GetConnection());

                command.Parameters.AddWithValue("@key", entity.RoleKey);
                command.Parameters.AddWithValue("@name", entity.RoleName);

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
