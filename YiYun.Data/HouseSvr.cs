using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Entity;

namespace YiYun.Data
{
    public class HouseSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public HouseSvr()
        { }
        public HouseSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public List<House> GetHouseByUnitID(string _UnitID)
        {
            string sql = " SELECT * FROM dbo.V_PMC_WechatHouse WHERE UnitID = @UnitID ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("UnitID",_UnitID)
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
                List<House> ls = ObHelper.GetList<House>(ds);
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

        public List<House> GetHouseByOpenID(string _OpenID)
        {
            string sql = " SELECT * FROM dbo.V_PMC_WechatHouse WHERE OPenID1 = @OPenID OR OPenID2 = @OPenID ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("OPenID",_OpenID)
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
                List<House> ls = ObHelper.GetList<House>(ds);
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

        public House GetHouseByID(string _HouseID)
        {
            string sql = " SELECT * FROM dbo.V_PMC_WechatHouse WHERE ID = @HouseID ";
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
                List<House> ls = ObHelper.GetList<House>(ds);
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

        public int UpdateOpenID(string _HouseID, string _OpenID, string _OpenType)
        {
            string sql = null;
            if (_OpenType == "OPENID1")
            {
                sql = " UPDATE dbo.PMC_BaseHouse SET OPenID1 =  @OpenID WHERE ID = @ID ";
            }
            if (_OpenType == "OPENID2")
            {
                sql = " UPDATE dbo.PMC_BaseHouse SET OPenID2 =  @OpenID WHERE ID = @ID ";
            }
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("OpenID",_OpenID),
                new SqlParameter("ID",_HouseID)
            };
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.Text, sql, paras);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, sql, paras);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            return ir;

        }
    }
}
