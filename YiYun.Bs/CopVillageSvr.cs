using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopVillageSvr
    {
        public static List<Village> GetVillageByOpenID(string _OpenID)
        {
            VillageSvr _mxSvr = new VillageSvr();
            return _mxSvr.GetVillageByOpenID(_OpenID);
        }

        public static List<Village> GetVillage()
        {
            VillageSvr _mxSvr = new VillageSvr();
            return _mxSvr.GetVillage();
        }

        public static Village GetVillageByID(string _VillageID)
        {
            VillageSvr _mxSvr = new VillageSvr();
            return _mxSvr.GetVillageByID(_VillageID);
        }
    }
}
