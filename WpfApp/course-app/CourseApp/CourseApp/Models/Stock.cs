namespace CourseApp.Models
{
    /// <summary>
    /// Класс склад
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Id склада
        /// </summary>
        public int StockId { get; set; } 
        /// <summary>
        /// Имя склада
        /// </summary>
        public string StockName { get; set; } 
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } 
        /// <summary>
        /// Наценка
        /// </summary>
        public double Markup { get; set; } 
        /// <summary>
        /// UserId
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }
    }
}
