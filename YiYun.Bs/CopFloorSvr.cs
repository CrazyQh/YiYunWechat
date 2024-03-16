using System.Collections.Generic;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopFloorSvr
    {
        public static List<Floor> GetFloorByAreaID(string _AreaID)
        {
            FloorSvr _mxSvr = new FloorSvr();
            return _mxSvr.GetFloorByAreaID(_AreaID);
        }
    }
}
