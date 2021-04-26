using NpgsqlTypes;

namespace CourseApp.Models
{
    public abstract class ProductInStockDecorator : AbstractProduct
    {
        public int ReceiptInvoiceId { get; set; }
        public NpgsqlDate? ReceiptInvoiceDate { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public float CountProduct { get; set; }
        public string StockName { get; set; }
        public string ClassName { get; set; }
        public float Markup { get; set; }
    }
}
