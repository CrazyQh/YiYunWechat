using System.Collections.Generic;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopUnitSvr
    {
        public static List<Unit> GetUnitByFloorID(string _FloorID)
        {
            UnitSvr _mxSvr = new UnitSvr();
            return _mxSvr.GetUnitByFloorID(_FloorID);
        }
    }
}
