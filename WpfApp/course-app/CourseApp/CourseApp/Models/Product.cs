namespace CourseApp.Models
{
    /// <summary>
    /// Класс продукта
    /// </summary>
    public class Product : AbstractProduct
    {
       /// <summary>
       /// Получение полной цены
       /// </summary>
       /// <returns></returns>
        public override string GetProduct() => ProductName;
    }
}
