using LBDC.WeChat.MessageHandlers;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Web.Mvc;
using YiYunWeChat.Auth;

namespace YiYunWeChat.Controllers
{
    public class WeChatController : Controller
    {
        //private readonly log4net.ILog _log = log4net.LogManager.GetLogger("RollingLogFileAppender_Info");
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigManager.TokenCheck))
            {
                return Content(echostr);
            }
            else
            {
                return Content("failed:" + postModel.Signature + ",token:" + postModel.Token + " "
                    + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token) + "。" +
                                "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            var maxRecordCount = 10;
            var messageHandler = new CustomMessageHandler(Request.InputStream, ref postModel, maxRecordCount);
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, postModel.Token))
            {
                return Content("参数错误！");
            }
            #region 设置消息去重
            /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
             * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
            messageHandler.OmitRepeatedMessage = true;//默认已经开启,也可以设置为false在本次请求中停用此功能
            #endregion
            try
            {
                //执行微信处理过程
                messageHandler.Execute();
                var weixinResult = new WeixinResult(messageHandler);
                return weixinResult;//v0.8+ 
            }
            catch (Exception)
            {
                #region 异常处理
                return Content("");
                #endregion
            }
        }
    }
}