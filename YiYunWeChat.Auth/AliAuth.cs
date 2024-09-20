using System.Text;
using System.Web.Mvc;
using YiYunWeChat.Auth;

namespace YiYunWeChat.Auth
{
    public class AliAuth : ActionFilterAttribute
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
            if (string.IsNullOrEmpty(ConfigManager.AliOpenID))
            {
                string Redirect_Uri = System.Web.HttpUtility.UrlEncode(ConfigManager.WebSiteUrl
                    + "Ali/GetCodeByOAtuo", Encoding.UTF8);
                string Appid = ConfigManager.AliAppID;
                string OAutoUrl = AliHelper.AliAutoUrl
                    .Replace("APPID", Appid)
                    .Replace("ENCODED_URL", Redirect_Uri)
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
