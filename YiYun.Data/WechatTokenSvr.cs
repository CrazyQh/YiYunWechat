using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class WechatTokenSvr
    {
        #region private members conn && trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public WechatTokenSvr()
        {

        }
        public WechatTokenSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        /// <summary>
        /// 插入Token
        /// </summary>
        /// <param name="_token"></param>
        /// <returns></returns>
        public int InsertToken(WechatToken _token)
        {
            string sql = @"
                INSERT dbo.PMC_WechatToken
                (
                    AppID,
                    AppSecret,
                    Token,
                    YxDateTime
                )
                VALUES
                (   
	                @AppID,
	                @AppSecret,
	                @Token,
	                @YxDateTime
                )
            ";
            
            SqlParameter[] paras = new SqlParameter[]
            {
                 new SqlParameter("AppID",_token.AppID),
                 new SqlParameter("AppSecret",_token.AppSecret),
                 new SqlParameter("Token",_token.Token),
                 new SqlParameter("YxDateTime",_token.YxDateTime)
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
            return ir;
        }

        /// <summary>
        /// 删除Token
        /// </summary>
        /// <param name="_token"></param>
        /// <returns></returns>
        public int DeleteToken(WechatToken _token)
        {
            string sql = " DELETE FROM dbo.PMC_WechatToken WHERE AppID = @AppID AND AppSecret = @AppSecret ";
            SqlParameter[] paras = new SqlParameter[]
            {
                 new SqlParameter("AppID",_token.AppID),
                 new SqlParameter("AppSecret",_token.AppSecret)
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
            return ir;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="_token"></param>
        /// <returns></returns>
        public WechatToken GetToken(WechatToken _token)
        {
            string sql = " SELECT * FROM dbo.PMC_WechatToken WHERE AppID = @AppID AND AppSecret = @AppSecret ";
            SqlParameter[] paras = new SqlParameter[]
            {
                 new SqlParameter("appid",_token.AppID),
                 new SqlParameter("appsecret",_token.AppSecret)
            };
            DataSet ds = new DataSet();
            if (_conn != null)
            {
                ds = SqlHelper.Adapter(_conn, CommandType.Text, sql, paras);
            }
            if (_trans != null)
            {
                ds = SqlHelper.Adapter(_trans, CommandType.Text, sql, paras);
            }
            else
            {
                ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<WechatToken> ls = new List<WechatToken>();
                ls = ObHelper.GetList<WechatToken>(ds);
                if (ls != null && ls.Count > 0)
                {
                    return ls[0];
                }
                return null;
            }
            return null;
        }
    }
}
