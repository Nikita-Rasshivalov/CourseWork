namespace CourseApp.Models
{
    public abstract class AbstractProduct
    {
        public int EntityId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }

        public abstract float GetFullPrice();
    }
}
