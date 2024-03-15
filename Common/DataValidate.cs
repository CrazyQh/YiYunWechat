using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    /// <summary>
    /// 数据验证类
    /// </summary>
    public class DataValidate
    {
        /// <summary>
        /// 验证是否合法Email
        /// </summary>
        /// <param name="_email"></param>
        /// <returns></returns>
        public static bool IsEmail(string _email)
        {
            if (string.IsNullOrEmpty(_email))
            {
                return false;
            }
            return Regex.IsMatch(_email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        /// <summary>
        /// 验证是否是数值型
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(string _Number)
        {
            return Regex.IsMatch(_Number, "^[0-9]+$");
        }
        /// <summary>
        /// 检测是否符合邮编格式
        /// </summary>
        /// <param name="postCode"></param>
        /// <returns></returns>
        public static bool IsPostCode(string _postCode)
        {
            return Regex.IsMatch(_postCode, @"^\d{6}$");
        }
        /// <summary>
        /// 检测是否符合身份证号码格式
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsIdentityNumber(string _num)
        {
            return Regex.IsMatch(_num, @"^\d{17}[\d|X]|\d{15}$");
        }
        /// <summary>
        /// 检测是否符合url格式,前面必需含有http://
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsURL(string _url)
        {
            if (string.IsNullOrEmpty(_url))
            {
                return false;
            }
            return Regex.IsMatch(_url, @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }
        /// <summary>
        /// 检测是否符合电话格式
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string _phoneNumber)
        {
            return Regex.IsMatch(_phoneNumber, @"^(\(\d{3}\)|\d{3}-)?\d{7,8}$");
        }
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 手机型号
        /// </summary>
        public static string Mobile = "nokia|iphone|iPad|android|motorola|^mot-|softbank|foma|docomo|kddi|up.browser|"
            + "up.link|htc|dopod|blazer|netfront|helio|hosin|huawei|novarra|CoolPad|webos|techfaith|palmsource|blackberry|"
            + "alcatel|amoi|ktouch|nexian|samsung|^sam-|s[cg]h|^lge|ericsson|philips|sagem|wellcom|bunjalloo|maui|symbian|"
            + " smartphone|midp|wap|phone|windows ce|iemobile|^spice|^bird|^zte-|longcos|pantech|gionee|^sie-|portalmmm|jigs"
            + "browser|hiptop|^benq|haier|^lct|operas*mobi|opera*mini|320x320|240x320|176x220";
        /// <summary>  
        /// Boolean_检测是否为手机用户  
        /// </summary>  
        /// <returns></returns>  
        public static bool IsMobile()
        {
            if (HttpContext.Current.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            string MobileUserAgent = Mobile;
            Regex MOBILE_REGEX = new Regex(MobileUserAgent);
            if (MOBILE_REGEX.IsMatch(HttpContext.Current.Request.UserAgent.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
