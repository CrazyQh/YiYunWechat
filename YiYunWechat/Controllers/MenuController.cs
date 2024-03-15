﻿using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiYun.Bs;
using YiYunWeChat.Auth;

namespace YiYunWechat.Controllers
{
    public class MenuController : Controller
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ButtonGroup bg = new ButtonGroup();

            #region 二级菜单
            var button11 = new SingleViewButton();
            button11.url = "http://www.baidu.com";
            button11.name = "业主必读";

            var button12 = new SingleViewButton();
            button12.url = "http://www.baidu.com";
            button12.name = "小区公告";

            var button13 = new SingleViewButton();
            button13.url = "http://www.baidu.com";
            button13.name = "账单查询";

            var button14 = new SingleViewButton();
            button14.url = "http://www.baidu.com";
            button14.name = "历史缴费";

            var button21 = new SingleViewButton();
            button21.url = "http://www.baidu.com";
            button21.name = "社区团购";

            var button22 = new SingleViewButton();
            button22.url = "http://www.baidu.com";
            button22.name = "报修处理";

            var button31 = new SingleViewButton();
            button31.url = "http://www.baidu.com";
            button31.name = "我的信息";

            var button32 = new SingleViewButton();
            button32.url = "http://www.baidu.com";
            button32.name = "房屋绑定";
            #endregion

            #region 一级菜单
            var subbutton1 = new SubButton();
            subbutton1.name = "信息查询";

            var subbutton2 = new SubButton();
            subbutton2.name = "便民服务";

            var subbutton3 = new SubButton();
            subbutton3.name = "业主中心";
            #endregion

            subbutton1.sub_button.Add(button11);
            subbutton1.sub_button.Add(button12);
            subbutton1.sub_button.Add(button13);
            subbutton1.sub_button.Add(button14);

            subbutton2.sub_button.Add(button21);
            subbutton2.sub_button.Add(button22);

            subbutton3.sub_button.Add(button31);
            subbutton3.sub_button.Add(button32);

            bg.button.Add(subbutton1);
            bg.button.Add(subbutton2);
            bg.button.Add(subbutton3);

            string accesstoken = CopWechatTokenSvr.GetToken(ConfigManager.APPIdS,ConfigManager.AppsecretS);
            var results = CommonApi.CreateMenu(accesstoken,bg);

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}