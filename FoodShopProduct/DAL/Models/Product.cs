using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int? Score { get; set; }

        public ProductInfo ProductInfo { get; set; }

        public IEnumerable<ProductScore> ProductScores { get; set; }
    }
}