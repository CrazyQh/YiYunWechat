using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiYunWeChat.Auth
{
    public class SaoBeiPayUrl
    {
        /// <summary>
        /// 扫呗支付地址
        /// </summary>
        public static string PayUrl = "http://pay.lcsw.cn/lcsw/pay/100/jspay";
        /// <summary>
        /// 扫呗回调地址
        /// </summary>

        public static string CallBack = "http://wx.yiyuninfo.com/SaoBeiPay/CallBack";
    }
}
