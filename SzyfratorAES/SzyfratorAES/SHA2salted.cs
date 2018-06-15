using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SzyfratorAES
{
    public class SHA2salted
    {

        public static string GenerateSHA512String(string inputString, byte[] salt)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] rv = new byte[bytes.Length + salt.Length ];
            System.Buffer.BlockCopy(bytes, 0, rv, 0, bytes.Length);
            System.Buffer.BlockCopy(salt, 0, rv, bytes.Length, salt.Length);

            byte[] hash = sha512.ComputeHash(rv);
            return GetStringFromHash(hash);
        }

        public static string GetStringFromHash(byte[] hash)
        {
            string savedPasswordHash = Convert.ToBase64String(hash);
            return savedPasswordHash;
        }
    }
}
