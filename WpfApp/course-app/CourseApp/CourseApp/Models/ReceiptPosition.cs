namespace CourseApp.Models
{
	/// <summary>
	/// Класс позиция накладной
	/// </summary>
	public class ReceiptPosition
	{
		/// <summary>
		/// Id позиции
		/// </summary>
		public int PositionId { get; set; }
		/// <summary>
		/// Id накладной
		/// </summary>
		public int? ReceiptInvoiceId { get; set; }
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
		/// Полная цена
		/// </summary>
		public double FullPrice { get; set; }
	}
}

