using Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYunWeChat.Auth
{
    public class ConfigManager
    {
        public static NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.GetSection("appSettings") as NameValueCollection;
            }
        }

        #region 扫呗
        /// <summary>
        /// 商户号
        /// </summary>
        public static string SaoBeiShopid
        {
            get { return AppSettings.Get("SaoBeiShopid"); }
        }
        /// <summary>
        /// 终端号
        /// </summary>
        public static string SaoBeiZhongduan
        {
            get { return AppSettings.Get("SaoBeiZhongduan"); }
        }
        /// <summary>
        /// token (唯一)
        /// </summary>
        public static string SaoBeiToken
        {
            get { return AppSettings.Get("SaoBeiToken"); }
        }
        #endregion

        public static string TokenCheck
        {
            get { return AppSettings.Get("TokenCheck"); }
        }

        public static string WebSiteUrl
        {
            get { return AppSettings.Get("WebSiteUrl"); }
        }

        public static string WebSiteUrl_Image
        {
            get { return AppSettings.Get("WebSiteUrl_Image"); }
        }

        public static string DefaultLogo
        {
            get { return AppSettings.Get("DefaultLogo"); }
        }

        public static string DefaultTitle
        {
            get { return AppSettings.Get("DefaultTitle"); }
        }

        public static string DefaultDescription
        {
            get { return AppSettings.Get("DefaultDescription"); }
        }

        public static string DefaultContent
        {
            get { return AppSettings.Get("DefaultContent"); }
        }

        public static string DefaultContent2
        {
            get { return AppSettings.Get("DefaultContent2"); }
        }

        public static string DefaultContent3
        {
            get { return AppSettings.Get("DefaultContent3"); }
        }

        public static string APPIdS
        {
            get { return AppSettings.Get("AppId"); }
        }
        public static string AppsecretS
        {
            get { return AppSettings.Get("Appsecret"); }
        }
        public static string AccountId
        {
            get { return AppSettings.Get("AccountId"); }
        }
        public static string AppId
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.SESSION_APPID); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.SESSION_APPID, value); }
        }

        public static string Token
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.SESSION_TOKEN); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.SESSION_TOKEN, value); }
        }

        public static string EncodingAESKey
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.SESSION_ENCODINGAESKEY); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.SESSION_ENCODINGAESKEY, value); }
        }

        public static string AppSecret
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.SESSION_APPSECRET); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.SESSION_APPSECRET, value); }
        }

        public static string OpenId
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.SESSION_OPENID); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.SESSION_OPENID, value); }
        }

        public static string UserInfo
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.SESSION_USERINFO); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.SESSION_USERINFO, value); }
        }

        public static string AliAppID
        {
            get { return AppSettings.Get("AliAppID"); }
        }

        public static string AliprivateKey
        {
            get { return AppSettings.Get("AliprivateKey"); }
        }

        public static string alipayPublicKey
        {
            get { return AppSettings.Get("alipayPublicKey"); }
        }

        public static string AliOpenID
        {
            get { return CookieHelper.ReadCookieDecrpyt(ConstKey.ALI_OPENID); }
            set { CookieHelper.WriteCookieEncrypt(ConstKey.ALI_OPENID, value); }
        }
    }
}
