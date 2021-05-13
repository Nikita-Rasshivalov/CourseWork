namespace CourseApp.Models
{
    /// <summary>
    /// Класс позиций расходной
    /// </summary>
    public class ExpenditurePosition
    {
        /// <summary>
        /// Id позиции
        /// </summary>
        public int ExpenditurePositionId { get; set; }
        /// <summary>
        /// Id продукта
        /// </summary>
        public int? ProductId { get; set; }
        /// <summary>
        /// Продукт
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Количество продукта
        /// </summary>
        public double CountProduct { get; set; }
        /// <summary>
        /// Id расходной
        /// </summary>
        public int ExpenditureInvoiceId { get; set; }
        /// <summary>
        /// Цена продукта
        /// </summary>
        public double ProductPrice { get; set; }
        /// <summary>
        /// Полная цена
        /// </summary>

        public double FullPrice { get; set; }
    }
}
