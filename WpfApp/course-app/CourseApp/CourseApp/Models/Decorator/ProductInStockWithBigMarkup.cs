using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class ProductInStockWithBigMarkup : ProductInStockDecorator
    {
        public override float GetFullPrice()
        {
            return (Product.GetFullPrice() + Product.GetFullPrice() * 25 / 100);
        }
    }
}
