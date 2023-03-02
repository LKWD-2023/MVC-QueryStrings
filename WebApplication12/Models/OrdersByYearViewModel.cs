using System;
using System.Collections.Generic;

namespace WebApplication12.Models
{
    public class OrdersByYearViewModel
    {
        public List<Order> Orders { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
    }
}