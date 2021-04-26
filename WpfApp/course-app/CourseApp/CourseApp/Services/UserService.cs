using CourseApp.Models;
using CourseApp.Utility;
using Npgsql;
using System.Collections.Generic;

namespace CourseApp.Services
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class UserService : IService<User>
    {
        DbConnection connection = new DbConnection();

        /// <summary>
        /// Удаление юзера.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(User entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM users WHERE user_id=@id;", connection.GetConnection());

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
        /// Получение всех юзеров.
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            try
            {
                List<User> entities = new List<User>();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users ORDER BY user_id", connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User entity = new User()
                    {
                        UserId = reader.GetInt32(0),
                        RoleKey = reader.GetString(1),
                        UserName = reader.GetString(2),
                        UserPass = reader.GetString(3),
                        FullName = reader.GetString(4)
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
        /// Получение юзера по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            User entity = null;

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE user_id=@id;", connection.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    entity = new User()
                    {
                        UserId = reader.GetInt32(0),
                        RoleKey = reader.GetString(1),
                        UserName = reader.GetString(2),
                        UserPass = reader.GetString(3),
                        FullName = reader.GetString(4)
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
        /// Добавление юзера.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(User entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users (role_key, user_name, user_pass, full_name) VALUES(@role, @name, @pswd, @fullName);", connection.GetConnection());

                command.Parameters.AddWithValue("@role", entity.RoleKey);
                command.Parameters.AddWithValue("@name", entity.UserName);
                command.Parameters.AddWithValue("@pswd", entity.UserPass);
                command.Parameters.AddWithValue("@fullName", entity.FullName);

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
        /// Обновление юзера.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(User entity)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE users SET role_key=@role, user_name=@name, user_pass=@pswd, full_name=@fullName WHERE user_id=@id;", connection.GetConnection());

                command.Parameters.AddWithValue("@id", entity.UserId);
                command.Parameters.AddWithValue("@role", entity.RoleKey);
                command.Parameters.AddWithValue("@name", entity.UserName);
                command.Parameters.AddWithValue("@pswd", entity.UserPass);
                command.Parameters.AddWithValue("@fullName", entity.FullName);

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
