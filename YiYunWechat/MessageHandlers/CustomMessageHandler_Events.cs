using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Containers;
using System.IO;
using YiYunWeChat.Auth;
using YiYun.Entity;
using YiYun.Bs;
using Common;
using PMC.Bs;

namespace LBDC.WeChat.MessageHandlers
{
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            #region WelCome Set
            string _accountid = ConfigManager.AccountId;
            WechatInfo account = CopWechatInfoSvr.GetAccount(_accountid);
            if (account != null)
            {
                responseMessage.Articles.Add(new Article()
                {
                    Title = "使用说明",
                    PicUrl = ConfigManager.DefaultLogo,
                    Url = ConfigManager.DefaultContent
                });
            }
            SetAccount(account);
            SaveWxUser(requestMessage.FromUserName);
            #endregion
            return responseMessage;
        }
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "有空再来";
            SaveWxUserAway(requestMessage.FromUserName);
            return responseMessage;
        }
        private void SaveWxUser(string _openid)
        {
            try
            {
                #region 获取微信信息
                WechatUser user = new WechatUser();
                user.AccountID = ConfigManager.AccountId;
                user.OpenID = _openid;
                #endregion
                string _resms = "";
                WechatUser NewUser = CopWechatUserSvr.SaveUser(user, out _resms);
                if (NewUser == null || string.IsNullOrEmpty(NewUser.ID))
                {
                    LogWrite.WriteLog("保存关注用户失败:" + _resms + ",OPENID:" + _openid);
                }
                SetUser(_openid);
            }
            catch (Exception ex)
            {
                LogWrite.WriteLog(ex.Message);
            }
        }
        private void SaveWxUserAway(string _openid)
        {
            WechatUser user = CopWechatUserSvr.GetUserByOpenId(_openid);
            if (user != null && !string.IsNullOrEmpty(user.ID))
            {
                string _resms = "";
                CopWechatUserSvr.ModifyUserAway(_openid, out _resms);
            }
        }
        private void SetAccount(WechatInfo _account)
        {
            ConfigManager.AppId = _account.AppID;
            ConfigManager.AppSecret = _account.AppSecret;
            ConfigManager.Token = _account.TokenCheck;
            ConfigManager.EncodingAESKey = _account.EncodingAESKey;
        }
        private void SetUser(string _openid)
        {
            ConfigManager.OpenId = _openid;
        }
    }
}