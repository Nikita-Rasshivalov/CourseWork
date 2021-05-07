namespace CourseApp.Models
{
    /// <summary>
    /// Класс пользователей
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// RoleKey
        /// </summary>
        public string RoleKey { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string UserPass { get; set; } 
        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName { get; set; } 
    }
}
