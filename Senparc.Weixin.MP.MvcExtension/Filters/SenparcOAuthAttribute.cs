﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：SenparcOAuthAttribute.cs
    文件功能描述：自动判断OAuth授权状态


    创建标识：Senparc - 20170509
    
----------------------------------------------------------------*/

using System;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Senparc.Weixin.MP.MvcExtension
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
            Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public abstract class SenparcOAuthAttribute : FilterAttribute,/* AuthorizeAttribute,*/ IAuthorizationFilter
    {
        protected string _appId { get; set; }
        protected string _oauthCallbackUrl { get; set; }
        protected OAuthScope _oauthScope { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="oauthCallbackUrl">网站内路径（如：/TenpayV3/OAuthCallback），以/开头！当前页面地址会加在Url中的returlUrl=xx参数中</param>
        public SenparcOAuthAttribute(string appId, string oauthCallbackUrl, OAuthScope oauthScope= OAuthScope.snsapi_userinfo)
        {
            _appId = appId;
            _oauthCallbackUrl = oauthCallbackUrl;
            _oauthScope = oauthScope;
        }

        /// <summary>
        /// 判断用户是否已经登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public abstract bool IsLogined(HttpContextBase httpContext);

        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return true;
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!IsLogined(httpContext))
            {
                return false;//未登录
            }

            return true;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                if (IsLogined(filterContext.HttpContext))
                {

                }
                else
                {
                    var returnUrl = filterContext.HttpContext.Request.Url.ToString();
                    var urlData = filterContext.HttpContext.Request.Url;
                    //授权回调字符串
                    var callbackUrl = string.Format("{0}://{1}{2}{3}{4}returnUrl={5}",
                        urlData.Scheme,
                        urlData.Host,
                        urlData.Port != 80 ? (":" + urlData.Port) : "",
                        _oauthCallbackUrl,
                        _oauthCallbackUrl.Contains("?") ? "&" : "?",
                        HttpUtility.RequestUtility.UrlEncode(returnUrl)
                        );

                    var state = string.Format("{0}|{1}", "FromSenparc", DateTime.Now.Ticks);
                    var url = OAuthApi.GetAuthorizeUrl(_appId, callbackUrl, state, _oauthScope);
                    filterContext.Result = new RedirectResult(url);
                }
            }
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }

    }
}
