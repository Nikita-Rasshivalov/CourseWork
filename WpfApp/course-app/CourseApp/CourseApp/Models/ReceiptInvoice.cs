using NpgsqlTypes;

namespace CourseApp.Models
{
	/// <summary>
	/// Приходная накладная
	/// </summary>
	public class ReceiptInvoice
	{
		/// <summary>
		/// ID накладной
		/// </summary>
		public int ReceiptInvoiceId { get; set; }
		/// <summary>
		/// Дата накладной
		/// </summary>
		public NpgsqlDate? ReceiptInvoiceDate { get; set; }
		/// <summary>
		/// Id поставщика
		/// </summary>
		public int? CustomerId { get; set; }
		/// <summary>
		/// ID склада
		/// </summary>
		public int StockId { get; set; }
		/// <summary>
		/// Поставщик
		/// </summary>
		public Customer Customer { get; set; }

		/// <summary>
		/// Склад
		/// </summary>
		public Stock Stock { get; set; }
	}
}
