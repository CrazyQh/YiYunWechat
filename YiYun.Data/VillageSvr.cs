using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class VillageSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public VillageSvr()
        { }
        public VillageSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public List<Village> GetVillageByOpenID(string _OpenID)
        {
            string sql = @" 
                SELECT ID,
                       VillageCode,
                       VillageName,
                       OnLinePay,
                       OnLineGg,
                       OnLineBx,
                       MerchantID,
                       TerminalNumber,
                       PayToken
                FROM dbo.PMC_BaseVillage
                WHERE ID IN
                      (
                          SELECT DISTINCT VillageID
                          FROM dbo.PMC_BaseHouse
                          WHERE OPenID1 = @OpenID
                                OR OPenID2 = @OpenID
                      )
            ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("OpenID",_OpenID)
            };
            DataSet ds = new DataSet();
            if (_trans != null)
            {
                ds = SqlHelper.Adapter(_trans, CommandType.Text, sql, paras);
            }
            else if (_conn != null)
            {
                ds = SqlHelper.Adapter(_conn, CommandType.Text, sql, paras);
            }
            else
            {
                ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Village> ls = ObHelper.GetList<Village>(ds);
                if (ls != null && ls.Count > 0)
                {
                    return ls;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<Village> GetVillage()
        {
            string sql = @" 
                SELECT ID,
                       VillageCode,
                       VillageName,
                       OnLinePay,
                       OnLineGg,
                       OnLineBx,
                       MerchantID,
                       TerminalNumber,
                       PayToken
                FROM dbo.PMC_BaseVillage
                WHERE StopFlag = 1
            ";
            DataSet ds = new DataSet();
            if (_trans != null)
            {
                ds = SqlHelper.Adapter(_trans, CommandType.Text, sql);
            }
            else if (_conn != null)
            {
                ds = SqlHelper.Adapter(_conn, CommandType.Text, sql);
            }
            else
            {
                ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, sql);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Village> ls = ObHelper.GetList<Village>(ds);
                if (ls != null && ls.Count > 0)
                {
                    return ls;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Village GetVillageByID(string _VillageID)
        {
            string sql = @" 
                SELECT ID,
                       VillageCode,
                       VillageName,
                       OnLinePay,
                       OnLineGg,
                       OnLineBx,
                       MerchantID,
                       TerminalNumber,
                       PayToken
                FROM dbo.PMC_BaseVillage
                WHERE ID = @VillageID
            ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("VillageID",_VillageID)
            };
            DataSet ds = new DataSet();
            if (_trans != null)
            {
                ds = SqlHelper.Adapter(_trans, CommandType.Text, sql, paras);
            }
            else if (_conn != null)
            {
                ds = SqlHelper.Adapter(_conn, CommandType.Text, sql, paras);
            }
            else
            {
                ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Village> ls = ObHelper.GetList<Village>(ds);
                if (ls != null && ls.Count > 0)
                {
                    return ls[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
