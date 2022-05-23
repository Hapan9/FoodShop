using System;
using DAL.Models;

namespace BLL.Dto
{
    public class ProductScoreDto
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public Score Score { get; set; }
    }
}