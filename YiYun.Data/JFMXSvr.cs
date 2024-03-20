using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Entity;

namespace YiYun.Data
{
    public class JFMXSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public JFMXSvr()
        { }
        public JFMXSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public List<JFMX> GetJFMXByHousID(string _HouseID)
        {
            string sql = @" 
                SELECT DISTINCT
                       SN,
                       WxPayType,
                       DJSSAmount,
                       CreateAt
                FROM dbo.V_PMC_YJFMX
                WHERE HouseID = @HouseID
            ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("HouseID",_HouseID)
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
                List<JFMX> ls = ObHelper.GetList<JFMX>(ds);
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
    }
}
