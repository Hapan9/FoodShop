using System;

namespace DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Photo { get; set; }

        public ProductInfo ProductInfo { get; set; }
    }
}