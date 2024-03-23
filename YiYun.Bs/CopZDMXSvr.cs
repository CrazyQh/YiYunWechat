using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using YiYun.Data;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYun.Bs
{
    public class CopZDMXSvr
    {
        /// <summary>
        /// 根据房屋ID获取账单
        /// </summary>
        /// <param name="_HouseID"></param>
        /// <returns></returns>
        public static List<ZDMX> GetZDMXByHousID(string _HouseID)
        {
            ZDMXSvr _mxSvr = new ZDMXSvr();
            return _mxSvr.GetZDMXByHousID(_HouseID);
        }

        /// <summary>
        /// 根据房屋ID及账单ID获取账单
        /// </summary>
        /// <param name="_HouseID"></param>
        /// <param name="_SoID"></param>
        /// <returns></returns>
        public static List<ZDMX> GetZDMXByHouseSoID(string _HouseID, string _SoID)
        {
            ZDMXSvr _mxSvr = new ZDMXSvr();
            return _mxSvr.GetZDMXByHouseSoID(_HouseID,_SoID);
        }

        /// <summary>
        /// 调用扫呗
        /// </summary>
        /// <param name="_taskid"></param>
        /// <param name="_householdid"></param>
        /// <param name="_ysje"></param>
        /// <param name="_zdtype"></param>
        /// <param name="_resms"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static int WYOrderPay(string _HouseID, string _SoID, string _ysje, out string _resms, out string Request)
        {
            _resms = string.Empty;
            Request = string.Empty;
            int ir = 0;
            try
            {
                using (SqlConnection conn = SqlHelper.PrepareConn())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            WXPaymentSvr _paymentSvr = new WXPaymentSvr(null, trans);
                            House _card = CopHouseSvr.GetHouseByID(_HouseID);
                            Village _villageinfo = CopVillageSvr.GetVillageByID(_card.VillageID);
                            string openid = ConfigManager.OpenId;
                            //openid = "o2HLA01_z9ni033uCYFJ1DFbnxtc";

                            #region 调用扫呗
                            string ddd = (decimal.Parse(_ysje) * 100).ToString();
                            ddd = ddd.Replace(".00", "");
                            Dictionary<string, string> _dic = new Dictionary<string, string>();
                            _dic.Add("pay_ver", "100");
                            _dic.Add("pay_type", "010");
                            _dic.Add("service_id", "012");
                            _dic.Add("merchant_no", _villageinfo.MerchantID);
                            _dic.Add("terminal_id", _villageinfo.TerminalNumber);
                            _dic.Add("terminal_trace", _HouseID);
                            _dic.Add("terminal_time", TimerHelper.GetTimeToString());
                            _dic.Add("total_fee", ddd);
                            _dic.Add("access_token", _villageinfo.PayToken);
                            _dic.Add("key_sign", Security.MD5Encrypt(UrlHelper.GetUrlForDictionary(_dic)));
                            _dic.Add("sub_appid", "");
                            _dic.Add("open_id", ConfigManager.OpenId);
                            _dic.Add("order_body", "物业费");
                            _dic.Add("notify_url", ConfigManager.WebSiteUrl + "Pay/CallBackWY");
                            _dic.Add("attach", _villageinfo.VillageName + "-" + _card.HouseCode);
                            _dic.Add("goods_detail", "物业费");
                            string content = JsonHelper.GetDictionaryJson(_dic);
                            Request = RequestUtility.HttpPostForJson("http://pay.lcsw.cn/lcsw/pay/100/jspay", content);
                            SaoBeiTongyi _tongyi = JsonHelper.GetObject<SaoBeiTongyi>(Request);
                            if (_tongyi != null && _tongyi.result_code.Equals("01") && _tongyi.return_code.Equals("01"))
                            {
                                string sbsn = _tongyi.out_trade_no;
                                ir = _paymentSvr.InsertWXPayment(_HouseID, _SoID, openid, sbsn);
                                if (ir <= 0)
                                {
                                    trans.Rollback();
                                    return ir;
                                }
                            }
                            else
                            {
                                _resms = _tongyi.return_msg;
                                trans.Rollback();
                                return -1;
                            }
                            trans.Commit();
                            return 1;

                            #endregion
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
