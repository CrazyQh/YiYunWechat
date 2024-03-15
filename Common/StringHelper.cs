using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// byte数组转换字符串
        /// </summary>
        /// <param name="_bytes"></param>
        /// <param name="_encoding"></param>
        /// <returns></returns>
        public static string ByteToString(byte[] _bytes)
        {
            return Encoding.Default.GetString(_bytes);
        }
        /// <summary>
        /// 字符串转换byte数组
        /// </summary>
        /// <param name="_bytes"></param>
        /// <param name="_encoding"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string _stringjson)
        {
            return Encoding.Default.GetBytes(_stringjson);
        }
        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string ToSChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }
        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string ToTChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }
        /// <summary>
        /// 返回随机数字
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int GetRandom(int first, int second)
        {
            Random r = new Random();
            return r.Next(first, second);
        }
        /// <summary>
        /// 字符串随机排序
        /// </summary>
        /// <param name="_string"></param>
        /// <returns></returns>
        public static string RandomSort(string _string)
        {
            StringBuilder sbl = new StringBuilder();
            if (!string.IsNullOrEmpty(_string))
            {
                List<int> _old = new List<int>();
                List<string> _random = new List<string>();
                for (int i = 0; i < _string.Length; i++)
                {
                    string _s = _string.Substring(i, 1);
                    _random.Add(_s);
                    _old.Add(i);
                }
                int[] _new = new int[_old.Count];
                int k = 0;
                while (k < _old.Count)
                {
                    ///New Random 种子
                    int guid = Guid.NewGuid().GetHashCode();
                    int _temp = new Random(guid).Next(0, _old.Count);
                    if (_old[_temp] != -1)
                    {
                        _new[k] = _old[_temp];
                        k++;
                        _old[_temp] = -1;
                    }
                }
                for (int i = 0; i < _random.Count; i++)
                {
                    int _index = _new[i];
                    string _or = _random[i];
                    _random[i] = _random[_index];
                    _random[_index] = _or;
                }
                for (int i = 0; i < _random.Count; i++)
                {
                    sbl.Append(_random[i]);
                }
            }
            return sbl.ToString();
        }
        /// <summary>
        /// 8位数转化进制
        /// </summary>
        /// <param name="_byte"></param>
        /// <param name="_tobase">2,8,16进制</param>
        /// <returns></returns>
        public static string ToBase(byte _byte, int _tobase)
        {
            return Convert.ToString(_byte, _tobase);
        }
        /// <summary>
        /// 16位数转化进制
        /// </summary>
        /// <param name="_byte"></param>
        /// <param name="_tobase">2,8,16进制</param>
        /// <returns></returns>
        public static string ToBase(short _short, int _tobase)
        {
            return Convert.ToString(_short, _tobase);
        }
        /// <summary>
        /// 32位数转化进制
        /// </summary>
        /// <param name="_byte"></param>
        /// <param name="_tobase">2,8,16进制</param>
        /// <returns></returns>
        public static string ToBase(int _int, int _tobase)
        {
            return Convert.ToString(_int, _tobase);
        }
        /// <summary>
        /// 64位数转化进制
        /// </summary>
        /// <param name="_byte"></param>
        /// <param name="_tobase">2,8,16进制</param>
        /// <returns></returns>
        public static string ToBase(long _long, int _tobase)
        {
            return Convert.ToString(_long, _tobase);
        }
        /// <summary>
        /// 字符串左补位
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_length"></param>
        /// <param name="_char"></param>
        /// <returns></returns>
        public static string PadLeft(string _value, int _length, char _char)
        {
            return _value.PadLeft(_length, _char);
        }
        /// <summary>
        /// 字符串右补位
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_length"></param>
        /// <param name="_char"></param>
        /// <returns></returns>
        public static string PadRight(string _value, int _length, char _char)
        {
            return _value.PadRight(_length, _char);
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        public static char[] constant =   
       {   
        '0','1','2','3','4','5','6','7','8','9',  
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
       };
    }
}
