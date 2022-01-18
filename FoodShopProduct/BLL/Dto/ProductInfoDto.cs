using System;

namespace BLL.Dto
{
    public class ProductInfoDto
    {
        public Guid ProductId { get; set; }
        public uint Amount { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
    }
}