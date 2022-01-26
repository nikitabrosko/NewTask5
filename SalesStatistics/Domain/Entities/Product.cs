﻿using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Product()
        {
            Orders = new HashSet<Order>();
        }
    }
}