using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class CookieHelper
    {

        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public static void WriteCookieEncrypt(string CookieName, string CookieValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookieName);
            }
            cookie.Value = Base64Helper.Base64Encrypt(CookieValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        ///  写入Cookie设置有效期
        /// </summary>
        /// <param name="CookieName">名称</param>
        /// <param name="CookieValue">值</param>
        /// <param name="expires">有效天数</param>
        public static void WriteCookieEncrypt(string CookieName, string CookieValue, int Day)
        {
            HttpCookie Cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (Cookie == null)
            {
                Cookie = new HttpCookie(CookieName);
            }
            Cookie.Value = Base64Helper.Base64Encrypt(CookieValue);
            Cookie.Expires = DateTime.Now.AddDays(Day);
            Cookie.HttpOnly = true;
            HttpContext.Current.Response.AppendCookie(Cookie);
        }
        /// <summary>
        /// 读取cookie解码
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public static string ReadCookieDecrpyt(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                return Base64Helper.Base64Decrypt(HttpContext.Current.Request.Cookies[CookieName].Value.ToString());
            }
            return "";
        }
        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="CookieName"></param>
        public static void RemoveCookie(string CookieName)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1000);
                cookie.HttpOnly = true;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
    }
}
