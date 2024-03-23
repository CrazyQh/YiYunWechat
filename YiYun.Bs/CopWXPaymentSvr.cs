using Common;
using System;
using System.Data;
using System.Data.SqlClient;
using YiYun.Data;

namespace YiYun.Bs
{
    public class CopWXPaymentSvr
    {
        /// <summary>
        /// 支付完成 更新支付标识 插入支付信息
        /// </summary>
        /// <param name="_taskid"></param>
        /// <param name="_householdid"></param>
        /// <param name="_zdtype"></param>
        /// <param name="_resms"></param>
        /// <returns></returns>
        public static int UpdateWXPaymentJFFlag(string _sbsn, out string _resms)
        {
            int ir = 0;
            _resms = "";
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WXPaymentSvr _PaySvr = new WXPaymentSvr(null, trans);
                            int _pay = _PaySvr.GetWXPaymentBySN(_sbsn);
                            if (_pay == 0)
                            {
                                _resms = "已经支付";
                                trans.Rollback();
                                return -1;
                            }

                            #region 更新支付标识
                            //处理支付
                            ir = _PaySvr.UpdateWXPaymentJFFlag(_sbsn);
                            if (ir <= 0)
                            {
                                trans.Rollback();
                                return ir;
                            }
                            #endregion

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
