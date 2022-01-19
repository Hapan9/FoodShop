using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Hashing
    {
        public string GetHashString(string text)
        {
            var bytes = Encoding.Unicode.GetBytes(text);

            var csp = new MD5CryptoServiceProvider();

            var byteHash = csp.ComputeHash(bytes);

            var hash = string.Empty;

            foreach (var b in byteHash)
                hash += $"{b:x2}";

            return hash;
        }
    }
}
