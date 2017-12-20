using System;
using System.Security.Cryptography;

namespace WeChat.Infrastructure
{
    public class HashHanlder
    {
        public static string GetHash(string input)
        {
            //哈希算法
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
