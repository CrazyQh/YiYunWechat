using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;
namespace LBDC.WeChat.MessageHandlers
{
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        /// <summary>
        /// 模板消息集合（Key：checkCode，Value：OpenId）
        /// </summary>
        public static Dictionary<string, string> TemplateMessageCollection = new Dictionary<string, string>();
        public CustomMessageHandler(Stream inputStream, ref PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
            string _accountId = ConfigManager.AccountId;
            if (!string.IsNullOrEmpty(_accountId))
            {
                WechatInfo account = CopWechatInfoSvr.GetAccount(_accountId);
                if (account != null)
                {
                    postModel.Token = account.TokenCheck;
                    postModel.EncodingAESKey = account.EncodingAESKey;
                    postModel.AppId = account.AppID;
                }
            }
            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
        }
        public CustomMessageHandler(RequestMessageBase requestMessage)
            : base(requestMessage)
        {

        }
        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }
        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }
        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var defaultResponseMessage = base.CreateResponseMessage<ResponseMessageText>();
            //defaultResponseMessage.Content = "你发送的消息：" + requestMessage.Content;
            if (requestMessage.Content.IndexOf("缴费") >= 0)
            {
                defaultResponseMessage.Content = "点击查看账单" + "http://mall.sanzhonginfo.cn/Report/ReadZDMXByOPENID";
                return defaultResponseMessage;
            }
            if (requestMessage.Content.IndexOf("账单") >= 0)
            {
                defaultResponseMessage.Content = "点击查看账单" + "http://mall.sanzhonginfo.cn/Report/ReadZDMXByOPENID";
                return defaultResponseMessage;
            }
            if (requestMessage.Content.IndexOf("绑定") >= 0)
            {
                defaultResponseMessage.Content = "点击绑定住宅" + "http://mall.sanzhonginfo.cn/Member/HouseAccount";
                return defaultResponseMessage;
            }
            else
            {
                defaultResponseMessage.Content = "只能识别以下消息！\n" + "缴费 \n" + "账单\n" + "绑定\n";
                return defaultResponseMessage;
            }
        }
        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            base.TextResponseMessage = "success";
            return null;
        }
        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            base.TextResponseMessage = "success";
            return null;
        }
        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            //一隔一返回News或Image格式
            if (base.WeixinContext.GetMessageContext(requestMessage).RequestMessages.Count() % 2 == 0)
            {
                var responseMessage = CreateResponseMessage<ResponseMessageNews>();

                responseMessage.Articles.Add(new Article()
                {
                    Title = "您刚才发送了图片信息",
                    Description = "您发送的图片将会显示在边上",
                    PicUrl = requestMessage.PicUrl,
                    Url = "http://sdk.weixin.senparc.com"
                });
                responseMessage.Articles.Add(new Article()
                {
                    Title = "第二条",
                    Description = "第二条带连接的内容",
                    PicUrl = requestMessage.PicUrl,
                    Url = "http://sdk.weixin.senparc.com"
                });

                return responseMessage;
            }
            else
            {
                var responseMessage = CreateResponseMessage<ResponseMessageImage>();
                responseMessage.Image.MediaId = requestMessage.MediaId;
                return responseMessage;
            }
        }
        /// <summary>
        /// 处理语音请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
            ////上传缩略图
            ////var accessToken = Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            //var uploadResult = Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(ConfigManage.AppId, UploadMediaFileType.image,
            //                                             Server.GetMapPath("~/Images/Logo.jpg"));

            ////设置音乐信息
            //responseMessage.Music.Title = "天籁之音";
            //responseMessage.Music.Description = "播放您上传的语音";
            //responseMessage.Music.MusicUrl = "http://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            //responseMessage.Music.HQMusicUrl = "http://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            //responseMessage.Music.ThumbMediaId = uploadResult.media_id;

            //推送一条客服消息
            try
            {
                CustomApi.SendText(ConfigManager.AppId, WeixinOpenId, "本次上传的音频MediaId：" + requestMessage.MediaId);

            }
            catch
            {
            }

            return responseMessage;
        }
        /// <summary>
        /// 处理视频请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了一条视频信息，ID：" + requestMessage.MediaId;

            //#region 上传素材并推送到客户端

            //Task.Factory.StartNew(async () =>
            //{
            //    //上传素材
            //    var dir = Server.GetMapPath("~/App_Data/TempVideo/");
            //    var file = await MediaApi.GetAsync(ConfigManage.AppId, requestMessage.MediaId, dir);
            //    var uploadResult = await MediaApi.UploadTemporaryMediaAsync(ConfigManage.AppId, UploadMediaFileType.video, file, 50000);
            //    await CustomApi.SendVideoAsync(ConfigManage.AppId, base.WeixinOpenId, uploadResult.media_id, "这是您刚才发送的视频", "这是一条视频消息");
            //}).ContinueWith(async task =>
            //{
            //    if (task.Exception != null)
            //    {
            //        WeixinTrace.Log("OnVideoRequest()储存Video过程发生错误：", task.Exception.Message);

            //        var msg = string.Format("上传素材出错：{0}\r\n{1}",
            //                   task.Exception.Message,
            //                   task.Exception.InnerException != null
            //                       ? task.Exception.InnerException.Message
            //                       : null);
            //        await CustomApi.SendTextAsync(appId, base.WeixinOpenId, msg);
            //    }
            //});

            //#endregion

            return responseMessage;
        }
        /// <summary>
        /// 处理链接消息请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = string.Format(@"您发送了一条连接信息：
Title：{0}
Description:{1}
Url:{2}", requestMessage.Title, requestMessage.Description, requestMessage.Url);
            return responseMessage;
        }
        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = base.OnEventRequest(requestMessage);//对于Event下属分类的重写方法，见：CustomerMessageHandler_Events.cs
            //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
            * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
            * 只需要在这里统一发出委托请求，如：
            * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            * return responseMessage;
            */

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "";
            return responseMessage;
        }
    }
}