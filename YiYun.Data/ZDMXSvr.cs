﻿using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class ZDMXSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public ZDMXSvr()
        { }
        public ZDMXSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        /// <summary>
        /// 根据房屋ID获取账单
        /// </summary>
        /// <param name="_HouseID"></param>
        /// <returns></returns>
        public List<ZDMX> GetZDMXByHousID(string _HouseID)
        {
            string sql = @" 
                SELECT SoID,
                       SoTaskID,
                       YearMonth,
                       SFProjectName,
                       JSTypeT,
                       Qty,
                       Amount,
                       WYJ,
                       IsMustPay
                FROM dbo.V_PMC_ZDMiddle
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
                List<ZDMX> ls = ObHelper.GetList<ZDMX>(ds);
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

        /// <summary>
        /// 根据房屋ID及账单ID获取账单
        /// </summary>
        /// <param name="_HouseID"></param>
        /// <param name="_SoID"></param>
        /// <returns></returns>
        public List<ZDMX> GetZDMXByHouseSoID(string _HouseID,string _SoID)
        {
            string sql = @" 
                SELECT SoID,
                       SoTaskID,
                       YearMonth,
                       SFProjectName,
                       JSTypeT,
                       Qty,
                       Amount,
                       ISNULL(WYJ, 0) WYJ,
                       IsMustPay
                FROM dbo.V_PMC_ZDMiddle
                WHERE HouseID = @HouseID
                      AND SoID IN
                          (
                              SELECT * FROM dbo.f_split2(@SoID, ',')
                          )
            ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("HouseID",_HouseID),
                new SqlParameter("SoID",_SoID)
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
                List<ZDMX> ls = ObHelper.GetList<ZDMX>(ds);
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
