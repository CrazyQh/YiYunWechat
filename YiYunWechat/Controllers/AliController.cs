using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YiYunWeChat.Auth;

namespace YiYunWeChat.Controllers
{
    public class AliController : Controller
    {
        public void GetCodeByOAtuo(string Auth_Code, string state)
        {

            // 初始化SDK
            IAopClient alipayClient = new DefaultAopClient(GetAlipayConfig());
            // 构造请求参数以调用接口
            AlipaySystemOauthTokenRequest request = new AlipaySystemOauthTokenRequest();

            // 设置授权码
            request.Code = Auth_Code;

            // 设置授权方式
            request.GrantType = "authorization_code";

            AlipaySystemOauthTokenResponse response = alipayClient.Execute(request);

            if (!response.IsError)
            {
                ConfigManager.AliOpenID = response.UserId;
                System.Web.HttpContext.Current.Response.Redirect(ConfigManager.WebSiteUrl + state);
            }
            else
            {
                Console.WriteLine("调用失败");
            }

        }

        private static AlipayConfig GetAlipayConfig()
        {
            AlipayConfig alipayConfig = new AlipayConfig();
            alipayConfig.ServerUrl = "https://openapi.alipay.com/gateway.do";
            alipayConfig.AppId = ConfigManager.AliAppID;
            alipayConfig.PrivateKey = ConfigManager.AliprivateKey;
            alipayConfig.Format = "json";
            alipayConfig.AlipayPublicKey = ConfigManager.alipayPublicKey;
            alipayConfig.Charset = "UTF-8";
            alipayConfig.SignType = "RSA2";
            return alipayConfig;
        }
    }
}
