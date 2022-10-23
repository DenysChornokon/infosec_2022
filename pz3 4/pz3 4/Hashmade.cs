using System;
using System.Security.Cryptography;
using System.Text;

namespace pz_3_4
{
    public static class hashmade
    {


        public static string GMD5(string data)
        {
            using (var crypto = MD5.Create())
            {
                byte[] byt = Encoding.Unicode.GetBytes(data);
                return Convert.ToBase64String(crypto.ComputeHash(byt));
            }
        }

        public static string GSHA1(string data)
        {
            using (var crypto = SHA1.Create())
            {
                byte[] byt = Encoding.Unicode.GetBytes(data);
                return Convert.ToBase64String(crypto.ComputeHash(byt));
            }
        }

        public static string GSHA256(string data)
        {
            using (var crypto = SHA256.Create())
            {
                byte[] byt = Encoding.Unicode.GetBytes(data);
                return Convert.ToBase64String(crypto.ComputeHash(byt));
            }
        }

        public static string GSHA512(string data)
        {
            using (var crypto = SHA512.Create())
            {
                byte[] byt = Encoding.Unicode.GetBytes(data);
                return Convert.ToBase64String(crypto.ComputeHash(byt));
            }
        }
    }
}
