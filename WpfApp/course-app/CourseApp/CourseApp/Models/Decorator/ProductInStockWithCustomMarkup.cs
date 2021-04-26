using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class ProductInStockWithCustomMarkup : ProductInStockDecorator
    {
        public ProductInStockWithCustomMarkup(float markup)
        {
            Markup = markup;
        }

        public override float GetFullPrice()
        {
            return (Product.GetFullPrice() + Product.GetFullPrice() * Markup / 100);
        }
    }
}
