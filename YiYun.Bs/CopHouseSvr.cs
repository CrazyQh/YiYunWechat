using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopHouseSvr
    {
        public static List<House> GetHouseByUnitID(string _UnitID)
        {
            HouseSvr _mxSvr = new HouseSvr();
            return _mxSvr.GetHouseByUnitID(_UnitID);
        }

        public static List<House> GetHouseByOpenID(string _UnitID)
        {
            HouseSvr _mxSvr = new HouseSvr();
            return _mxSvr.GetHouseByOpenID(_UnitID);
        }

        public static House GetHouseByID(string _HouseID) 
        {
            HouseSvr _mxSvr = new HouseSvr();
            return _mxSvr.GetHouseByID(_HouseID);
        }

        public static int UpdateOpenID(string _HouseID, string _OpenID, string _OpenType, out string _resms)
        {
            _resms = "";
            int ir = 0;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            HouseSvr houseSvr = new HouseSvr(null, trans);
                            ///注册
                            ir = houseSvr.UpdateOpenID(_HouseID, _OpenID, _OpenType);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return -1;
                            }
                            trans.Commit();
                            return 1;
                        }
                        catch (Exception ex0)
                        {
                            _resms = ex0.Message;
                            trans.Rollback();
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
                return -1;
            }
        }
    }
}
