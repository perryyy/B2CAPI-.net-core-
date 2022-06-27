using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace B2CAPI.service
{
    public class AddPassword
    {
        //密碼加密
        public string HashPassword(string password)
        {
            string saltkey = "1q2w3e4r5t6y7u8ui9o0po7tyy";
            string saltAndPassword = string.Concat(password, saltkey);
            SHA256CryptoServiceProvider sha256Hasher = new SHA256CryptoServiceProvider();
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashData = sha256Hasher.ComputeHash(PasswordData);
            string Hashresult = Convert.ToBase64String(HashData);
            return Hashresult;

            throw new NotImplementedException();
        }
    }
}
