namespace CourseApp.Models
{
    public class Stock
    {
        public int StockId { get; set; } 
        public string StockName { get; set; } 
        public string Description { get; set; } 
        public float? Markup { get; set; } 
        public int? UserId { get; set; } 
        public User User { get; set; }
    }
}
