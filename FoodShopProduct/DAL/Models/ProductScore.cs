using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductScore
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UserId { get; set; }

        public Score Score { get; set; }
    }


    public enum Score
    {
        VeryBad = 0,
        Bad = 1,
        Normal = 2,
        Good = 3,
        VeryGood = 4
    }
}
