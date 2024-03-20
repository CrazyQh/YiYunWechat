using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopJFMXSvr
    {
        public static List<JFMX> GetJFMXByHousID(string _HouseID)
        {
            JFMXSvr _mxSvr = new JFMXSvr();
            return _mxSvr.GetJFMXByHousID(_HouseID);
        }
    }
}
