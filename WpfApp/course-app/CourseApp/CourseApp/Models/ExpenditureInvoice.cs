using NpgsqlTypes;

namespace CourseApp.Models
{
	/// <summary>
	/// Класс расходной накладной
	/// </summary>
	public class ExpenditureInvoice
	{
		/// <summary>
		/// Id расходной
		/// </summary>
		public int ExpenditureInvoiceId { get; set; }
		/// <summary>
		/// Дата расходной
		/// </summary>
		public NpgsqlDate? ExpenditureInvoiceDate { get; set; }
		/// <summary>
		/// Id покупателя
		/// </summary>
		public int? CustomerId { get; set; }
		/// <summary>
		/// Id склада
		/// </summary>
		public int? StockId { get; set; }
		/// <summary>
		/// Покупатель
		/// </summary>
		public Customer Customer { get; set; }
		/// <summary>
		/// Склад
		/// </summary>
		public Stock Stock { get; set; }
		
	}
}
