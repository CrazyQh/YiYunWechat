using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class PaymentMXSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public PaymentMXSvr()
        { }
        public PaymentMXSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        /// <summary>
        /// 根据TaskID获取缴费单明细
        /// </summary>
        /// <param name="_PayType"></param>
        /// <param name="_TaskID"></param>
        /// <returns></returns>
        public List<PaymentMX> GetPaymentMXByTaskID(string _PayType, string _TaskID)
        {
            string sql = string.Empty;
            if (_PayType == "WYJF")
            {
                sql = " SELECT YearMonth,ProjectName,Qty,YSAmount,SSAmount FROM dbo.PMC_PaymentMX WHERE TaskID = @TaskID ";
            }
            else if (_PayType == "WXJF")
            {
                sql = " SELECT YearMonth,ProjectName,Qty,YSAmount,SSAmount FROM dbo.PMC_WXPaymentMX WHERE ID = @TaskID ";
            }
            
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("TaskID",_TaskID)
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
                List<PaymentMX> ls = ObHelper.GetList<PaymentMX>(ds);
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
        /// 根据TaskID获取退费单明细
        /// </summary>
        /// <param name="_PayType"></param>
        /// <param name="_TaskID"></param>
        /// <returns></returns>
        public List<RefundMX> GetRefundMXByTaskID(string _TaskID)
        {
            string sql = " SELECT YearMonth,ProjectName,Qty,SSAmount,RefundAmount FROM dbo.PMC_RefundMX WHERE TaskID = @TaskID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("TaskID",_TaskID)
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
                List<RefundMX> ls = ObHelper.GetList<RefundMX>(ds);
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
