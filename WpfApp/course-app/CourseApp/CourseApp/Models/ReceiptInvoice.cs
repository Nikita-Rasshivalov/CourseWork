using NpgsqlTypes;

namespace CourseApp.Models
{
    public class ReceiptInvoice
    {
		public int ReceiptInvoiceId { get; set; }
		public NpgsqlDate? ReceiptInvoiceDate { get; set; }
		public int? CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int? StockId { get; set; }
		public Stock Stock { get; set; }
		public int? ProductId { get; set; }
		public Product Product { get; set; }
		public float? CountProduct { get; set; }
		public float? PriceProduct { get; set; }
	}
}
