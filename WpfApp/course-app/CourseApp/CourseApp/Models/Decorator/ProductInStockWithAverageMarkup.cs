using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class ProductInStockWithAverageMarkup : ProductInStockDecorator
    {
        public override float GetFullPrice()
        {
            return (Product.GetFullPrice() + Product.GetFullPrice() * 15 / 100);
        }
    }
}
