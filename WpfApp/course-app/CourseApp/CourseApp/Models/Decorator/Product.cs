﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Models
{
    public class Product : AbstractProduct
    {
        public override float GetFullPrice() => ProductPrice;
    }
}