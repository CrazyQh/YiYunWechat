using System.Text;
using System.Web.Mvc;

namespace YiYunWeChat.Auth
{
    public class OAuth : ActionFilterAttribute
    {
        /// <summary>
        /// 重定向
        /// </summary>
        public string Redirect { get; set; }
        /// <summary>
        /// 授权类型 snsapi_base  snsapi_userinfo
        /// </summary> 
        public string Type { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //授权
            if (string.IsNullOrEmpty(ConfigManager.OpenId))
            {
                string Redirect_Uri = System.Web.HttpUtility.UrlEncode(ConfigManager.WebSiteUrl
                    + "WechatOAuth/GetCodeByOAtuo", Encoding.UTF8);
                string Appid = ConfigManager.APPIdS;
                string OAutoUrl = WechatHelper.OAutoUrl
                    .Replace("APPID", Appid)
                    .Replace("REDIRECT_URI", Redirect_Uri)
                    .Replace("SCOPE", Type)
                    .Replace("STATE", Redirect);
                System.Web.HttpContext.Current.Response.Redirect(OAutoUrl);
                //返回页面
                var json = new JsonResult();
                json.Data = new
                {
                    statusCode = "300"
                };
                json.ContentEncoding = Encoding.UTF8;
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
