using System;
using DAL.Enum;

namespace DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float? ScoreCoefficient { get; set; }
        public Role Role { get; set; }
    }
}