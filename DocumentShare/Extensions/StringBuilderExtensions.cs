using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace DocumentShare.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendRandomString(this StringBuilder builder, int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[length];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            foreach (byte b in data)
            {
                builder.Append(chars[b % (chars.Length - 1)]);
            }

            return builder;
        }
    }
}
