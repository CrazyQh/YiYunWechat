using YiYun.Data;
using YiYun.Entity;

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
