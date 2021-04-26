using NpgsqlTypes;

namespace CourseApp.Models
{
	public class ExpenditureInvoice
    {
		public int ExpenditureInvoiceId { get; set; }
		public NpgsqlDate? ExpenditureInvoiceDate { get; set; }
		public int? CustomerId { get; set; }
		public Customer Customer { get; set; }
		public string StockName { get; set; }
		public int? ProductId { get; set; }
		public Product Product { get; set; }
		public float? CountProduct { get; set; }
		public float? PriceProduct { get; set; }
	}
}
