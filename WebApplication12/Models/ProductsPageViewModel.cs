using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication12.Models
{
    public class ProductsPageViewModel
    {
        public List<Product> Products { get; set; }
        public string CategoryName { get; set; }
    }
}