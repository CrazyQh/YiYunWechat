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

    文件名：Post.cs
    文件功能描述：Post


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：zhanghao-kooboo - 20150316
    修改描述：增加

    修改标识：Senparc - 20150407
    修改描述：发起Post请求方法修改，为了上传永久视频素材
 
    修改标识：Senparc - 20160720
    修改描述：增加了PostFileGetJsonAsync的异步方法（与之前的方法多一个参数）

    修改标识：Senparc - 20170409
    修改描述：v4.11.9 修改Download方法
----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// Post 请求处理
    /// </summary>
    public static class Post
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            
            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("微信Post请求发生错误！错误代码：{0}，说明：{1}",
                                      (int)errorResult.errcode,
                                      errorResult.errmsg),
                        null, errorResult);
                }
            }

            T result = js.Deserialize<T>(returnText);

            //TODO:加入特殊情况下的回调处理

            return result;
        }


        #region 同步方法

        /// <summary>
        /// 发起Post请求，可上传文件
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="fileDictionary"></param>
        /// <param name="postDataDictionary"></param>
        /// <returns></returns>
        public static T PostFileGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Dictionary<string, string> postDataDictionary = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                postDataDictionary.FillFormDataStream(ms); //填充formData
                string returnText = RequestUtility.HttpPost(url, cookieContainer, ms, fileDictionary, null, encoding, cer, timeOut);
                var result = GetResult<T>(returnText);
                return result;
            }
        }

        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileStream">文件流</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT, bool checkValidationResult = false)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, fileStream, null, null, encoding, cer, timeOut, checkValidationResult);

            WeixinTrace.SendApiLog(url, returnText);

            var result = GetResult<T>(returnText);
            return result;
        }

        /// <summary>
        /// PostGetJson
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="formData"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, formData, encoding, cer, timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        public static void Download(string url, string data, Stream stream)
        {
            WebClient wc = new WebClient();
            var file = wc.UploadData(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            stream.Write(file, 0, file.Length);

            //foreach (var b in file)
            //{
            //    stream.WriteByte(b);
            //}
        }

        #endregion

        #region 异步方法


        /// <summary>
        /// 【异步方法】发起Post请求，可上传文件
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="fileDictionary"></param>
        /// <param name="postDataDictionary"></param>
        /// <returns></returns>
        public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Dictionary<string, string> postDataDictionary = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                postDataDictionary.FillFormDataStream(ms); //填充formData
                string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, ms, fileDictionary, null, encoding, cer, timeOut);
                var result = GetResult<T>(returnText);
                return result;
            }
        }


        /// <summary>
        /// 【异步方法】PostGetJson的异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="fileStream"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult"></param>
        /// <returns></returns>
        public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT, bool checkValidationResult = false)
        {
            string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, fileStream, null, null, encoding, cer, timeOut, checkValidationResult);

            WeixinTrace.SendApiLog(url, returnText);

            var result = GetResult<T>(returnText);
            return result;
        }


        /// <summary>
        /// PostGetJson的异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="formData"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT)
        {
            string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, formData, encoding, cer, timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        ///// <summary>
        ///// PostFileGetJson的异步版本
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="cookieContainer"></param>
        ///// <param name="fileDictionary"></param>
        ///// <param name="encoding"></param>
        ///// <param name="cer">证书，如果不需要则保留null</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = Config.TIME_OUT)
        //{
        //    string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, null, fileDictionary, null, encoding, cer, timeOut);
        //    var result = GetResult<T>(returnText);
        //    return result;
        //}



        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        public static async Task DownloadAsync(string url, string data, Stream stream)
        {
            WebClient wc = new WebClient();

            var fileBytes = await wc.UploadDataTaskAsync(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            await stream.WriteAsync(fileBytes, 0, fileBytes.Length);//也可以分段写入
        }

        #endregion
    }
}
