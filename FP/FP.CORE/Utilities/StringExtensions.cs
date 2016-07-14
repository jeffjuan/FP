using System;
using System.Text;


namespace FP.CORE.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Base64 加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Base64 解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base64Decode(this string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}
