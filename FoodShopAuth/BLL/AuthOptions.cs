using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BLL
{
    public class AuthOptions
    {
        public const string Issuer = "Client";
        public const string Audience = "Client";
        private const string Key = "InternetShop project";
        public const int Lifetime = 5;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.ASCII.GetBytes(Key));
        }
    }
}
