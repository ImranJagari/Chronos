using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace Chronos.Core.Extensions
{
    public static class SecurityExtensions
    {
        public static string GetMD5Hash(this string input)
        {
            var md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2", CultureInfo.CurrentCulture));
            }

            return sBuilder.ToString();
        }
        public static string GetSha512Hash(this string input)
        {
            var sha512Hasher = SHA512.Create();
            byte[] data = sha512Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2", CultureInfo.CurrentCulture));
            }

            return sBuilder.ToString();
        }
        
    }
}