using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YiYunWeChat.Auth
{
    public class WechatHelper
    {
        /// <summary>
        /// 授权 获取code
        /// </summary>
        public static readonly string OAutoUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";
        /// <summary>
        /// 授权 获取accesstoken
        /// </summary>
        public static readonly string GetOAutoAccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code";
        /// <summary>
        /// 授权 获取用户信息
        /// </summary>
        public static readonly string UserinfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN ";
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
        /// <summary>
        /// 随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            string bStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(bStr.Length);
                sb.Append(bStr[number]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 拼装Signature
        /// </summary>
        /// <param name="jsticket"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string getSignature(string jsticket, string timestamp, string nonce, string url)
        {
            string ss = "jsapi_ticket=" + jsticket + "&noncestr=" + nonce + "&timestamp=" + timestamp + "&url=" + url;

            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(ss));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            return enText.ToString();
        }
    }
}
