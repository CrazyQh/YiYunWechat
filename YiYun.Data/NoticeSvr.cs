using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class NoticeSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public NoticeSvr()
        { }
        public NoticeSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public List<Notice> GetNotice(string _VillageID) 
        {
            string sql = @" 
                SELECT a.TaskID,
                       a.SN,
                       CONVERT(NVARCHAR(50), a.CreateDate, 23) CreateDate,
                       a.CreateName,
                       a.VillageID,
                       a.NoticeTitle,
                       a.NoticeContent
                FROM dbo.PMC_Notice a
                    LEFT JOIN dbo.BPMInstTasks b
                        ON b.TaskID = a.TaskID
                WHERE b.State = 'approved'
                      AND a.VillageID = @VillageID
                ORDER BY b.CreateAt
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
                List<Notice> ls = ObHelper.GetList<Notice>(ds);
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

        public Notice GetNoticeMX(string _TaskID)
        {
            string sql = @" 
                SELECT a.TaskID,
                       a.SN,
                       CONVERT(NVARCHAR(50), a.CreateDate, 23) CreateDate,
                       a.CreateName,
                       a.VillageID,
                       a.NoticeTitle,
                       a.NoticeContent
                FROM dbo.PMC_Notice a
                    LEFT JOIN dbo.BPMInstTasks b
                        ON b.TaskID = a.TaskID
                WHERE b.State = 'approved'
                      AND a.TaskID = @TaskID
                ORDER BY b.CreateAt
            ";
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
                List<Notice> ls = ObHelper.GetList<Notice>(ds);
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
