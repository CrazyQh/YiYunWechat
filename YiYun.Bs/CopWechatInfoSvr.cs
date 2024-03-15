using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Entity;
using YiYun.Data;

namespace YiYun.Bs
{
    public class CopWechatInfoSvr
    {
        /// <summary>
        /// 获取微信公众号信息
        /// </summary>
        /// <param name="_accountid"></param>
        /// <returns></returns>
        public static WechatInfo GetAccount(string _accountid)
        {
            WechatInfoSvr ws = new WechatInfoSvr();
            return ws.GetAccount(_accountid);
        }
    }
}
