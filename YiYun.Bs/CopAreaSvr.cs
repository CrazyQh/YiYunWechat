using System.Collections.Generic;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopAreaSvr
    {
        public static List<Area> GetAreaByVillageID(string _VillageID)
        {
            AreaSvr _mxSvr = new AreaSvr();
            return _mxSvr.GetAreaByVillageID(_VillageID);
        }
    }
}
