using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class UrlHelper
    {
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }
        /// <summary>
        /// 获取url传值
        /// </summary>
        /// <param name="arg">参数名</param>
        /// <returns>如果为null，则返回空字符串</returns>
        public static string RequestParams(string arg)
        {
            if (HttpContext.Current.Request.Params[arg] == null)
            {
                return "";
            }
            else
            {
                return HttpContext.Current.Request.Params[arg];
            }
        }
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !DataValidate.IsIP(result))
            {
                return "0.0.0.0";
            }

            return result;

        }
        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 根据url参数拼成键值对集合
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static SortedDictionary<string, string> GetSortDictionaryForUrl(string _url)
        {
            SortedDictionary<String, String> dicInfo = new SortedDictionary<string, string>();
            int intPos = _url.IndexOf("?");
            if (intPos < 1)
            {
                return dicInfo;
            }
            string strRight = _url.Substring(intPos + 1);
            string[] arrParams = strRight.Split('&');
            for (int i = 0; i < arrParams.Length; i++)
            {
                string[] arrSingle = arrParams[i].Split('=');
                if (arrSingle.Length > 1)
                {
                    string keyInfo = arrSingle[0].Trim();
                    string valueInfo = UrlDecode(arrSingle[1].Trim());
                    if (!dicInfo.ContainsKey(keyInfo) && !keyInfo.Equals("Sign", StringComparison.CurrentCultureIgnoreCase))
                    {
                        dicInfo.Add(keyInfo, valueInfo);
                    }
                }
            }
            return dicInfo;
        }
        /// <summary>
        /// 根据url参数拼成键值对集合(不包含问号)
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static SortedDictionary<string, string> GetSortDictionaryForUrlNoQuestion(string _url)
        {
            SortedDictionary<String, String> dicInfo = new SortedDictionary<string, string>();
            string[] arrParams = _url.Split('&');

            if (arrParams != null && arrParams.Length > 0)
            {
                for (int i = 0; i < arrParams.Length; i++)
                {
                    string[] arrSingle = arrParams[i].Split('=');
                    if (arrSingle.Length > 1)
                    {
                        string keyInfo = arrSingle[0].Trim();
                        string valueInfo = UrlDecode(arrSingle[1].Trim());
                        if (!dicInfo.ContainsKey(keyInfo))
                        {
                            dicInfo.Add(keyInfo, valueInfo);
                        }
                    }
                }
            }
            return dicInfo;
        }
        /// <summary>
        /// 排序字典类型转换成url参数
        /// </summary>
        /// <param name="_sortdictionary"></param>
        /// <returns></returns>
        public static string GetUrlForDictionary(SortedDictionary<string, string> _sortdictionary)
        {
            StringBuilder sbl = new StringBuilder();
            foreach (KeyValuePair<string, string> _keyvalue in _sortdictionary)
            {
                sbl.Append(_keyvalue.Key + "=" + UrlEncode(_keyvalue.Value) + "&");
            }
            sbl.Remove(sbl.Length - 1, 1);
            return sbl.ToString();
        }
        /// <summary>
        /// 字典类型转换成url参数
        /// </summary>
        /// <param name="_sortdictionary"></param>
        /// <returns></returns>
        public static string GetUrlForDictionary(Dictionary<string, string> _dictionary)
        {
            StringBuilder sbl = new StringBuilder();
            foreach (KeyValuePair<string, string> _keyvalue in _dictionary)
            {
                sbl.Append(_keyvalue.Key + "=" +_keyvalue.Value + "&");
            }
            sbl.Remove(sbl.Length - 1, 1);
            return sbl.ToString();
        }
    }
}
