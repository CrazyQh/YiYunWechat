using Common;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYunWechat.Controllers
{
    public class TemplateController : Controller
    {
        public string Index()
        {
            string message = string.Empty;
            int ir = 0;
            string accesstoken = CopWechatTokenSvr.GetToken(ConfigManager.APPIdS, ConfigManager.AppsecretS);
            GetPrivateTemplateJsonResult jsonResult = TemplateApi.GetPrivateTemplate(accesstoken);
            if (jsonResult != null)
            {
                if (jsonResult.template_list != null && jsonResult.template_list.Count > 0)
                {
                    ir = CopWechatTemplateSvr.DeleteTemplate(out message);
                    if (!string.IsNullOrEmpty(message))
                    {
                        return message;
                    }
                    else
                    {
                        WechatTemplate template = new WechatTemplate();
                        foreach (GetPrivateTemplate_TemplateItem item in jsonResult.template_list)
                        {
                            template.TemplateID = item.template_id;
                            template.TemplateTitle = item.title;
                            template.TemplateContent = item.content;
                            template.TemplateExample = item.example;
                            ir = CopWechatTemplateSvr.InsertTemplate(template, out message);
                            if (ir <= 0 || !string.IsNullOrEmpty(message))
                            {
                                return message;
                            }
                        }
                    }
                }
            }
            else
            {
                message = "未获取到有效的模板信息！";
            }

            if (string.IsNullOrEmpty(message))
            {
                message = "成功";
            }
            return message;
        }
    }
}