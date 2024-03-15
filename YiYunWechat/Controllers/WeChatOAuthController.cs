using Common;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYunWeChat.Controllers
{
    public class WeChatOAuthController : Controller
    {
        public void GetCodeByOAtuo(string code, string state)
        {
            string AccountId = ConfigManager.AccountId;
            WechatInfo account = CopWechatInfoSvr.GetAccount(AccountId);
            string url = WechatHelper.GetOAutoAccessTokenUrl
              .Replace("APPID", account.AppID)
              .Replace("SECRET", account.AppSecret)
              .Replace("CODE", code);
            string data = RequestUtility.HttpGet(url);
            WX_OAUTORETURNDATA wechatData = JsonHelper.GetObject<WX_OAUTORETURNDATA>(data);
            if (!string.IsNullOrEmpty(wechatData.openid))
            {
                ConfigManager.OpenId = wechatData.openid;
            }
            if (wechatData.scope.ToLower() == "snsapi_userinfo")
            {
                string userInfoUrl = WechatHelper.UserinfoUrl
                    .Replace("ACCESS_TOKEN", wechatData.access_token)
                    .Replace("OPENID", wechatData.openid);
                string userInfoData = RequestUtility.HttpGet(userInfoUrl);
                ConfigManager.UserInfo = userInfoData;
            }
            System.Web.HttpContext.Current.Response.Redirect(ConfigManager.WebSiteUrl + state);
        }
    }

    public class WX_OAUTORETURNDATA
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }
}