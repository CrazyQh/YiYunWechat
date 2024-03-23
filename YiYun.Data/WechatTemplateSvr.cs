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
    public class WechatTemplateSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public WechatTemplateSvr()
        { }
        public WechatTemplateSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public int InsertTemplate(WechatTemplate _template)
        {
            string sql = @" 
                INSERT dbo.PMC_WechatTemplate
                (
                    TemplateID,
                    TemplateTitle,
                    TemplateContent,
                    TemplateExample
                )
                VALUES
                (@TemplateID, @TemplateTitle, @TemplateContent, @TemplateExample)
            ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("TemplateID",_template.TemplateID),
                new SqlParameter("TemplateTitle",_template.TemplateTitle),
                new SqlParameter("TemplateContent",_template.TemplateContent),
                new SqlParameter("TemplateExample",_template.TemplateExample)
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

        public int DeleteTemplate()
        {
            string sql = @" DELETE FROM dbo.PMC_WechatTemplate ";
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
