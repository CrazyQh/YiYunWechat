using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopWechatTemplateSvr
    {
        public static int InsertTemplate(WechatTemplate _template, out string _resms)
        {
            _resms = "";
            int ir = 0;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WechatTemplateSvr templateSvr = new WechatTemplateSvr(null, trans);
                            ///注册
                            ir = templateSvr.InsertTemplate(_template);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return -1;
                            }
                            trans.Commit();
                            return 1;
                        }
                        catch (Exception ex0)
                        {
                            _resms = ex0.Message;
                            trans.Rollback();
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
                return -1;
            }
        }

        public static int DeleteTemplate(out string _resms)
        {
            _resms = "";
            int ir = 0;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WechatTemplateSvr templateSvr = new WechatTemplateSvr(null, trans);
                            ///注册
                            ir = templateSvr.DeleteTemplate();
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return -1;
                            }
                            trans.Commit();
                            return 1;
                        }
                        catch (Exception ex0)
                        {
                            _resms = ex0.Message;
                            trans.Rollback();
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
                return -1;
            }
        }
    }
}
