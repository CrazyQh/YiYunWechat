using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class FilterHelper
    {
        /// <summary>
        /// 删除xss攻击
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveXss(string input)
        {
            string str;
            string str2;
            do
            {
                str = input;
                input = Regex.Replace(input, @"(&#*\w+)[\x00-\x20]+;", "$1;");
                input = Regex.Replace(input, "(&#x*[0-9A-F]+);*", "$1;", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, "&(amp|lt|gt|nbsp|quot);", "&amp;$1;");
                input = HttpUtility.HtmlDecode(input);
            }
            while (str != input);
            do
            {
                str = input;
                input = Regex.Replace(input, @"(<[^>]+style[\x00-\x20]*=[\x00-\x20]*[^>]*?)\\([^>]*>)", "$1/$2", RegexOptions.IgnoreCase);
            }
            while (str != input);
            input = Regex.Replace(input, @"[\x00-\x08\x0b-\x0c\x0e-\x19]", "");
            input = Regex.Replace(input, "(<[^>]+[\\x00-\\x20\"'/])(on|xmlns)[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "([a-z]*)[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*)[\\x00-\\x20]*j[\\x00-\\x20]*a[\\x00-\\x20]*v[\\x00-\\x20]*a[\\x00-\\x20]*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:", "$1=$2nojavascript...", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "([a-z]*)[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*)[\\x00-\\x20]*v[\\x00-\\x20]*b[\\x00-\\x20]*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:", "$1=$2novbscript...", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"(<[^>]+style[\x00-\x20]*=[\x00-\x20]*[^>]*?)/\*[^>]*\*/([^>]*>)", "$1$2", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*expression[\\x00-\\x20]*\\([^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*behaviour[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*behavior[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:*[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"</*\w+:\w[^>]*>", "noxss");
            do
            {
                str2 = input;
                input = Regex.Replace(input, "</*(applet|meta|xml|blink|link|style|script|embed|object|iframe|frame|frameset|ilayer|layer|bgsound|title|base)[^>]*>?", "no$1", RegexOptions.IgnoreCase);
            }
            while (str2 != input);
            return input;
        }
        /// <summary>
        /// 删除网页中的html
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
    }
}
