namespace CourseApp.Models
{
    /// <summary>
    /// Класс продукта на складе
    /// </summary>
   public class ProductInStock
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Id склада
        /// </summary>
        public int StockId { get; set; }
        /// <summary>
        /// Количество продукта
        /// </summary>
        public double CountProduct { get; set; }
        /// <summary>
        /// Продукт
        /// </summary>

        public Product Product { get; set; }
        /// <summary>
        /// Склад
        /// </summary>
        public Stock Stock { get; set; }

    }
}
