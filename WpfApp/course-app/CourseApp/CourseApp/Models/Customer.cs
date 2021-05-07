namespace CourseApp.Models
{
    /// <summary>
    /// Класс поставщик\покупатель
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Id покупателя\поставщика
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Имя покупателя\поставщика
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Описание покупателя\поставщика
        /// </summary>
        public string Description { get; set; }
    }
}
