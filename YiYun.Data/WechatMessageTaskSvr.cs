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
    public class WechatMessageTaskSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public WechatMessageTaskSvr()
        { }
        public WechatMessageTaskSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public List<WechatMessageTask> GetTaskInfo(string _SoTaskID, string _MessageType) 
        {
            string sql = " SELECT * FROM dbo.PMC_WechatMessageTask WHERE SoTaskID = @SoTaskID AND MessageType = @MessageType ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("SoTaskID",_SoTaskID),
                new SqlParameter("MessageType",_MessageType)
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
                List<WechatMessageTask> ls = ObHelper.GetList<WechatMessageTask>(ds);
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

        public int UpdateMessageTask(string _ID)
        {
            string sql = " UPDATE dbo.PMC_WechatMessageTask SET SendState = 1,SendAt = GETDATE() WHERE ID = '" + _ID + "' ";
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.Text, sql);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, sql);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, sql);
            }
            return ir;
        }

        public int UpdateMessageTaskError(string _ID,string _ErrMsg)
        {
            string sql = " UPDATE dbo.PMC_WechatMessageTask SET SendState = 0,,SendAt = GETDATE(), ErrMsg = '" + _ErrMsg + "' WHERE ID = '" + _ID + "' ";
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.Text, sql);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, sql);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, sql);
            }
            return ir;
        }
    }
}
