using System;

namespace DAL.Models
{
    public class ProductInfo
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Guid? ProductId { get; set; }
        public uint Amount { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public DateTime LastTimeModify { get; set; }
    }
}