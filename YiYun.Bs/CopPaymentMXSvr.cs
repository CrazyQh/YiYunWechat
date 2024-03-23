using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public static class CopPaymentMXSvr
    {
        public static List<PaymentMX> GetPaymentMXByTaskID(string _PayType, string _TaskID)
        {
            PaymentMXSvr _mxSvr = new PaymentMXSvr();
            return _mxSvr.GetPaymentMXByTaskID(_PayType,_TaskID);
        }

        public static List<RefundMX> GetRefundMXByTaskID(string _TaskID)
        {
            PaymentMXSvr _mxSvr = new PaymentMXSvr();
            return _mxSvr.GetRefundMXByTaskID(_TaskID);
        }
    }
}
