using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class Security
    {
        /// <summary>
        /// 返回散列码
        /// </summary>
        /// <param name="_instring"></param>
        /// <returns></returns>
        public static string SHA1_Hash(string _instring, string key)
        {
            SHA1 sha1 = SHA1.Create();
            _instring = _instring + key;
            byte[] bytes_in = System.Text.UTF8Encoding.Default.GetBytes(_instring);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            string sha1_out = BitConverter.ToString(bytes_out);
            return sha1_out.Replace("-", "").ToLower();
        }
        /// <summary>
        /// 返回散列码重载
        /// </summary>
        /// <param name="_instring"></param>
        /// <returns></returns>
        public static string SHA1_Hash(string _instring)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] bytes_in = System.Text.UTF8Encoding.Default.GetBytes(_instring);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            string sha1_out = BitConverter.ToString(bytes_out);
            return sha1_out.Replace("-", "").ToLower();
        }
        /// <summary>
        /// 参数签名并把Sign添加到字典集合
        /// </summary>
        /// <param name="_sortdictionary"></param>
        /// <param name="_key"></param>
        /// <returns>返回排序字典</returns>
        public static SortedDictionary<string, string> SignSortDictionary(SortedDictionary<string, string> _sortdictionary, string _key)
        {
            StringBuilder sbl = new StringBuilder();
            foreach (KeyValuePair<string, string> _keyvalue in _sortdictionary)
            {
                sbl.Append(_keyvalue.Key + "=" + _keyvalue.Value + "&");
            }
            sbl.Remove(sbl.Length - 1, 1);
            string sign = SHA1_Hash(sbl.ToString(), _key);
            _sortdictionary.Add("Sign", sign);
            return _sortdictionary;
        }
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="strtmp">元字符串</param>
        /// <param name="strKey">秘钥</param>
        /// <returns></returns>
        public static string EncryptNew(string strtmp, string strKey = "wfb")
        {
            int icnt = strtmp.Length;
            byte[] byCmp = Encoding.UTF8.GetBytes(strKey);
            ArrayList al = new ArrayList();
            for (int i = 0; i < icnt; i++)
            {
                string tmp = strtmp.Substring(i, 1);
                byte[] strByte = Encoding.UTF8.GetBytes(tmp);
                byte[] strByteEn = Get_YH_OP(strByte, byCmp);
                al.Add(strByteEn);
            }
            string strresult = "";
            for (int i = 0; i < icnt; i++)
            {
                byte[] bytemps = (byte[])al[i];
                strresult += Convert.ToBase64String(bytemps);
            }
            return strresult;
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="strtmp">元字符串</param>
        /// <param name="strKey">秘钥</param>
        /// <returns></returns>
        public static string DecryptNew(string strtmp, string strKey = "wfb")
        {
            byte[] byCmp = Encoding.UTF8.GetBytes(strKey);
            ArrayList al = new ArrayList();
            int ilen = strtmp.Length;
            if (ilen % 4 != 0)
            {
                return "";
            }
            string reVal = "";
            for (int i = 0; i < ilen; i = i + 4)
            {
                byte[] tmps = Convert.FromBase64String(strtmp.Substring(i, 4));
                byte[] strByteDe = Get_YH_OP(tmps, byCmp);
                reVal += Encoding.UTF8.GetString(strByteDe);
            }
            return reVal;
        }
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="_str"></param>
        /// <param name="_encoding"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string _str)
        {
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(_str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;
        }
        //public static string MD5Encrypt(string _str)
        //{
        //    string pwd = "";
        //    MD5 md5 = MD5.Create(); //实例化一个md5对像
        //    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        //    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(_str));
        //    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
        //        pwd = pwd + s[i].ToString("x2");
        //    }
        //    return pwd;
        //}
        private static byte YH_OP(byte btmp, byte[] bykey)
        {
            byte byresult = btmp;
            int ilen = bykey.Length;
            for (int i = 0; i < ilen; i++)
            {
                byresult = Convert.ToByte(byresult ^ bykey[i]);
            }
            return byresult;
        }
        private static byte[] Get_YH_OP(byte[] btmps, byte[] bykey)
        {
            int ilen = btmps.Length;
            byte[] bys = new byte[ilen];
            for (int i = 0; i < ilen; i++)
            {
                bys[i] = YH_OP(btmps[i], bykey);
            }
            return bys;
        }
    }
}
