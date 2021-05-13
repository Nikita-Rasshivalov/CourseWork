namespace CourseApp.Models
{
   /// <summary>
   /// Класс абстрактного продукта
   /// </summary>
    public abstract class AbstractProduct
    {
        /// <summary>
        /// Id продукта
        /// </summary>
        public int EntityId { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Цена продукта
        /// </summary>
        public double ProductPrice { get; set; }
        /// <summary>
        /// Получение полной цены
        /// </summary>
        /// <returns></returns>
        public abstract double GetFullPrice();
    }
}
