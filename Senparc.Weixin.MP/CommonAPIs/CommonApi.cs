﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：CommonApi.cs
    文件功能描述：通用接口(用于和微信服务器通讯，一般不涉及自有网站服务器的通讯)
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150330
    修改描述：获取调用微信JS接口的临时票据中的AccessToken添加缓存
    
    修改标识：Senparc - 20150401
    修改描述：添加公众号第三方平台获取授权码接口
    
    修改标识：Senparc - 20150430
    修改描述：公众号第三方平台分离
 
    修改标识：Senparc - 20160721
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20161110
    修改描述：完善GetTicket系列方法备注

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3&oldid=103
    
 */

using System.Threading.Tasks;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using System.Text;
using System.Security.Cryptography;
using System;
using Senparc.Weixin.Helpers;
using System.Net;
using System.IO;
using System.Net.Http;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// </summary>
    public partial class CommonApi
    {
        #region 同步方法

        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }


        /// <summary>
        /// 获取授权url临时凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static TemTokenResult GetToken(string appid, string secret, string code, string grant_type = "authorization_code")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}",
                                    appid.AsUrlData(), secret.AsUrlData(), code.AsUrlData(), grant_type.AsUrlData());
            TemTokenResult result = Get.GetJson<TemTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 获取二维码url,titio
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <param name="_resms"></param>
        /// <returns></returns>
        public static QrcodeReturn PostQrReturn(string token, string content, out string _resms)
        {
            _resms = "";
            QrcodeReturn json = new QrcodeReturn();
            try
            {
                CookieContainer cookieContainer = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(content);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token + "";
                    json = Post.PostGetJson<QrcodeReturn>(url, cookieContainer, ms);
                }
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
            }
            return json;
        }
        /// <summary>
        /// 获取Ticket
        /// </summary>
        /// <param name="accessToken">获取access_token填写client_credential</param>
        /// <returns></returns>
        public static jsapiTicketModel GetJsApiTicket(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken);
            jsapiTicketModel result = Get.GetJson<jsapiTicketModel>(url);
            return result;
        }
        /// <summary>
        /// 用户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WeixinUserInfoResult GetUserInfo(string accessTokenOrAppId, string openId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("http://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                                        accessToken.AsUrlData(), openId.AsUrlData());
                WeixinUserInfoResult result = Get.GetJson<WeixinUserInfoResult>(url);
                return result;

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 用户临时信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WeixinUserInfoResult GetUserInfoTemp(string token, string openid)
        {

            var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN",
                                    token.AsUrlData(), openid.AsUrlData());
            WeixinUserInfoResult result = Get.GetJson<WeixinUserInfoResult>(url);
            return result;

        }
        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicket(string appId, string secret, string type = "jsapi")
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            return GetTicketByAccessToken(accessToken, type);
        }
        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketByAccessToken(string accessTokenOrAppId, string type = "jsapi")
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                        accessToken.AsUrlData(), type.AsUrlData());

                JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
                return result;

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取微信服务器的ip段
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static GetCallBackIpResult GetCallBackIp(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

                return Get.GetJson<GetCallBackIpResult>(url);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 签名加密
        /// </summary>
        /// <param name="orgStr"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Sha1(string orgStr, Encoding encode)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = encode.GetBytes(orgStr);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            string result = BitConverter.ToString(bytes_out);
            result = result.Replace("-", "");
            return result;
        }
        /// <summary>
        /// 获取消息模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T GetMessagePlate<T>(string token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token={0}",token);

            T result = Get.GetJson<T>(url);
            return result;
        }
        /// <summary>
        ///  发送消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <param name="_resms"></param>
        /// <returns></returns>
        public static MessageCallback SendMessage(string token, string content, out string _resms)
        {
            _resms = "";
            MessageCallback json = new MessageCallback();
            try
            {
                CookieContainer cookieContainer = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(content);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    var url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token + "";
                    json = Post.PostGetJson<MessageCallback>(url, cookieContainer, ms);
                }
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
            }
            return json;
        }
        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static async Task<AccessTokenResult> GetTokenAsync(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = await Get.GetJsonAsync<AccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 【异步方法】用户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static async Task<WeixinUserInfoResult> GetUserInfoAsync(string accessTokenOrAppId, string openId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format("http://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                                       accessToken.AsUrlData(), openId.AsUrlData());
               var result = Get.GetJsonAsync<WeixinUserInfoResult>(url);
               return await result;

           }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取带参数的二维码
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static byte[] Getqrcodeimage(string ticket)
        {
            string url = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + ticket.UrlEncode() + "";
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(url);
            return bytes;
        }

        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketAsync(string appId, string secret, string type = "jsapi")
        {
            var accessToken = await AccessTokenContainer.TryGetAccessTokenAsync(appId, secret);
            return GetTicketByAccessToken(accessToken, type);
        }

        /// <summary>
        /// 【异步方法】获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="type">默认为jsapi，当作为卡券接口使用时，应当为wx_card</param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetTicketByAccessTokenAsync(string accessTokenOrAppId, string type = "jsapi")
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                        accessToken.AsUrlData(), type.AsUrlData());

                var result = Get.GetJsonAsync<JsApiTicketResult>(url);
                return await result;

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取微信服务器的ip段
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static async Task<GetCallBackIpResult> GetCallBackIpAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

                return await Get.GetJsonAsync<GetCallBackIpResult>(url);

            }, accessTokenOrAppId);
        }

        #endregion
    }
}
