#region Apache License Version 2.0
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
    
    文件名：AccessTokenResult.cs
    文件功能描述：access_token请求后的JSON返回格式
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20170702
    修改描述：加入 IAccessTokenResult 接口
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// access_token请求后的JSON返回格式
    /// </summary>
    [Serializable]
    public class AccessTokenResult : WxJsonResult, IAccessTokenResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

    }
    /// <summary>
    /// 临时access_token请求后的JSON返回格式
    /// </summary>
    public class TemTokenResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }



    [Serializable]
    public class jsapiTicketModel
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }
    }
    [Serializable]
    public class WXShareModel
    {
        public string appId { get; set; }
        public string nonceStr { get; set; }
        public string timestamp { get; set; }
        public string signature { get; set; }
        public string ticket { get; set; }
        public string url { get; set; }
        public string openid { get; set; }
    }
    /// <summary>
    /// 二维码
    /// </summary>
    [Serializable]
    public class Qrcode
    {
        public int expire_seconds { get; set; }	//该二维码有效时间，以秒为单位。 最大不超过2592000（即30天），此字段如果不填，则默认有效期为30秒。
        public string action_name { get; set; }//二维码类型，QR_SCENE为临时的整型参数值，QR_STR_SCENE为临时的字符串参数值，QR_LIMIT_SCENE为永久的整型参数值，QR_LIMIT_STR_SCENE为永久的字符串参数值
        public action_info action_info { get; set; }//二维码详细信息
    }
    [Serializable]
    public class action_info
    {
        public scene scene { get; set; }
    }
    [Serializable]
    public class scene
    {
        public string scene_str { get; set; }
        public string scene_id { get; set; }
    }

    /// <summary>
    /// 二维码返回
    /// </summary>
    [Serializable]
    public class QrcodeReturn
    {
        public string ticket { get; set; }
        public string expire_seconds { get; set; }
        public string url { get; set; }
    }
    /// <summary>
    /// 消息模板
    /// </summary>
    public class Messageplate
    {
        public string template_id { get; set; }
        public string title { get; set; }
        public string primary_industry { get; set; }
        public string deputy_industry { get; set; }
        public string content { get; set; }
        public string example { get; set; }
    }
    /// <summary>
    /// 消息模板列表
    /// </summary>
    public class ListMessagePlate
    {
        public List<Messageplate> template_list { get; set; }
    }
    /// <summary>
    ///  模板发送消息
    /// </summary>
    public class SendMessage
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public MinProgram miniprogram { get; set; }
        public SendMessageData data { get; set; }
        public string customerid { get; set; }
        public string title { get; set; }
        public string accountid { get; set; }
    }
    /// <summary>
    /// 跳小程序所需数据
    /// </summary>
    public class MinProgram
    {
        public string appid { get; set; }
        public string pagepath { get; set; }
    }
    /// <summary>
    /// 发送消息模板数据
    /// </summary>
    public class SendMessageData
    {
        public MessageParameters first { get; set; }
        public MessageParameters keyword1 { get; set; }
        public MessageParameters keyword2 { get; set; }
        public MessageParameters keyword3 { get; set; }
        public MessageParameters keyword4 { get; set; }
        public MessageParameters keyword5 { get; set; }
        public MessageParameters remark { get; set; }
        public MessageParameters thing12 { get; set; }
        public MessageParameters thing14 { get; set; }
        public MessageParameters time27 { get; set; }
        public MessageParameters amount26 { get; set; }
        public MessageParameters thing8 { get; set; }
        public MessageParameters character_string13 { get; set; }
        public MessageParameters amount6 { get; set; }
        public MessageParameters character_string1 { get; set; }
        public MessageParameters time4 { get; set; }
        public MessageParameters amount3 { get; set; }
        public MessageParameters thing11 { get; set; }
        public MessageParameters amount16 { get; set; }
        public MessageParameters short_thing5 { get; set; }
    }
    public class MessageParameters
    {
        public string value { get; set; }
        public string color { get; set; }
    }
    /// <summary>
    /// 发送消息回调类
    /// </summary>
    public class MessageCallback
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string msgid { get; set; }

    }
}
