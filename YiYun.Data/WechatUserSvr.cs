using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class WechatUserSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public WechatUserSvr()
        { }
        public WechatUserSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        /// <summary>
        /// 用户关注后插入用户表
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        public int SaveUser(WechatUser _user)
        {
            string sql = @"
                INSERT dbo.PMC_WechatUser
                (
                    AccountID,
                    OpenID,
                    Subscribe,
                    SubscribeTime,
                    UnSubscribeTime
                )
                VALUES
                (   @AccountID,
                    @OpenID,
                    1,
                    GETDATE(),
                    NULL
                )
            ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("AccountID",_user.AccountID),
                new SqlParameter("OpenID",_user.OpenID)
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

        /// <summary>
        /// 根据OPENID获取用户
        /// </summary>
        /// <param name="_OpenID"></param>
        /// <returns></returns>
        public WechatUser GetUserByOpenId(string _OpenID)
        {
            string sql = " SELECT * FROM dbo.PMC_WechatUser WHERE OpenID = @OpenID ";
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
                List<WechatUser> lsuser = ObHelper.GetList<WechatUser>(ds);
                if (lsuser != null && lsuser.Count > 0)
                {
                    return lsuser[0];
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
        /// 用户重新关注后更新用户表
        /// </summary>
        /// <param name="_OpenID"></param>
        /// <returns></returns>
        public int UpdateUser(string _OpenID)
        {
            string sql = " UPDATE dbo.PMC_WechatUser SET Subscribe = 1,SubscribeTime = GETDATE(),UnSubscribeTime = NULL WHERE OpenID = @OpenID ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("OpenID",_OpenID),
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

        /// <summary>
        /// 用户取消关注
        /// </summary>
        /// <param name="_OpenID"></param>
        /// <returns></returns>
        public int ModifyUserAway(string _OpenID)
        {
            string sql = " UPDATE dbo.PMC_WechatUser SET Subscribe = 0,UnSubscribeTime = GETDATE() WHERE OpenID = @OpenID ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("OpenID",_OpenID),
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
