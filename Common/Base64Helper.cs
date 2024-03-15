using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Base64Helper
    {
        /// <summary>
        /// Base64编码字符串
        /// </summary>
        /// <param name="_string"></param>
        /// <param name="_coding"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string _string, Encoding _encoding = null)
        {
            if (_encoding == null)
            {
                _encoding = Encoding.UTF8;
            }
            byte[] bytes = _encoding.GetBytes(_string);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Base64解码字符串
        /// </summary>
        /// <param name="_string"></param>
        /// <param name="_coding"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string _string, Encoding _encoding = null)
        {
            if (_encoding == null)
            {
                _encoding = Encoding.UTF8;
            }
            byte[] bytes = Convert.FromBase64String(_string);
            return Encoding.Default.GetString(bytes);
        }
    }
}
