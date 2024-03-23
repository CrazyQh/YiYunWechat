using Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace YiYun.Data
{
    public class WXPaymentSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public WXPaymentSvr()
        { }
        public WXPaymentSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        /// <summary>
        /// 插入WXPayment
        /// </summary>
        /// <param name="_HouseID"></param>
        /// <param name="_SoID"></param>
        /// <param name="_OpenID"></param>
        /// <param name="_SBSN"></param>
        /// <returns></returns>
        public int InsertWXPayment(string _HouseID, string _SoID, string _OpenID, string _SBSN)
        {
            string sql = "P_PMC_InsertWXPayment";
            SqlParameter[] paras = new SqlParameter[4];
            paras[0] = new SqlParameter("HouseID", _HouseID);
            paras[1] = new SqlParameter("SoID", _SoID);
            paras[2] = new SqlParameter("OpenID", _OpenID);
            paras[3] = new SqlParameter("SBSN", _SBSN);
            paras[0].Direction = ParameterDirection.Input;
            paras[1].Direction = ParameterDirection.Input;
            paras[2].Direction = ParameterDirection.Input;
            paras[3].Direction = ParameterDirection.Input;
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, sql, paras);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, sql, paras);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.StoredProcedure, sql, paras);
            }
            return ir;
        }

        /// <summary>
        /// 根据SBSN获取缴费单
        /// </summary>
        /// <param name="_SBSN"></param>
        /// <returns></returns>
        public int GetWXPaymentBySN(string _SBSN)
        {
            string sql = " SELECT COUNT(*) Qty FROM PMC_WXPayment WHERE JFFlag = 0 AND SN = '" + _SBSN + "'";
            DataSet ds = new DataSet();
            if (_trans != null)
            {

                ds = SqlHelper.Adapter(_trans, CommandType.Text, sql, null);
            }
            else if (_conn != null)
            {
                ds = SqlHelper.Adapter(_conn, CommandType.Text, sql, null);
            }
            else
            {
                ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, sql, null);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return (int)ds.Tables[0].Rows[0]["Qty"];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新支付标识
        /// </summary>
        /// <param name="_taskid"></param>
        /// <param name="_householdid"></param>
        /// <param name="_zdtype"></param>
        /// <returns></returns>
        public int UpdateWXPaymentJFFlag(string _sbsn)
        {
            string sql = " UPDATE dbo.PMC_WXPayment SET JFFlag = 1 WHERE SN = '" + _sbsn + "' ";
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.StoredProcedure, sql);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.StoredProcedure, sql);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.StoredProcedure, sql);
            }
            return ir;
        }
    }
}
