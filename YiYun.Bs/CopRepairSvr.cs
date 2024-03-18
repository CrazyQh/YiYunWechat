using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopRepairSvr
    {
        public static List<Repair> GetRepairRecord(string _OpenID)
        {
            RepairSvr _mxSvr = new RepairSvr();
            return _mxSvr.GetRepairRecord(_OpenID);
        }

        public static int InsertRepair(string HouseID, string Remarks, string PhoneNo, string Files, out string _resms)
        {
            int ir = 0;
            _resms = string.Empty;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            RepairSvr _repairsvr = new RepairSvr(null, trans);
                            #region
                            ir = _repairsvr.InsertRepair(HouseID, Remarks, PhoneNo, Files);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return ir;
                            }
                            #endregion
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

        public static int InsertRepairMX(string Files,string FilePath, out string _resms)
        {
            int ir = 0;
            _resms = string.Empty;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            RepairSvr _repairsvr = new RepairSvr(null, trans);
                            #region
                            ir = _repairsvr.InsertRepairMX(Files, FilePath);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return ir;
                            }
                            #endregion
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
