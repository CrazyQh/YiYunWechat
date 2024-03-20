using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYunWechat.Controllers
{
    public class PayController : Controller
    {
        public JsonResult WYPay(string HouseID, string SoID)
        {
            string statusCode = "200";
            string message = string.Empty;
            string Request = string.Empty;
            string ShopCode = "Yiyun";
            try
            {
                House _card = CopHouseSvr.GetHouseByID(HouseID);
                Village _village = CopVillageSvr.GetVillageByID(_card.VillageID);
                List<ZDMX> _zdmx = CopZDMXSvr.GetZDMXByHouseSoID(HouseID, SoID);
                if (_zdmx == null)
                {
                    throw new Exception("未找到可以支付的账单");
                }
                if (_village.OnLinePay == "0")//小区是否支持缴费
                {
                    throw new Exception("该小区尚未开通在线缴费，请前往物业收费处缴费！");
                }
                if (_card.OnlinePay == "0")//业主是否支持缴费
                {
                    throw new Exception("状态异常，暂不能在线缴费，请与物业联系或直接前往物业收费处缴费！");
                }
                decimal SFJE = _zdmx.Sum(m => decimal.Parse(m.Amount));
                decimal WYJE = _zdmx.Sum(m => decimal.Parse(m.WYJ));
                string YSJE = string.Empty;

                if (_card.DamagesFlag == "1")//业主是否计算违约金
                {
                    YSJE = (SFJE + WYJE).ToString("#0.00");
                }
                else
                {
                    YSJE = SFJE.ToString("#0.00");
                }
                int ir = CopZDMXSvr.WYOrderPay(HouseID, SoID, YSJE, out message, out Request);
                if (ir <= 0 || !string.IsNullOrEmpty(message))
                {
                    statusCode = "300";
                }
            }
            catch (Exception e)
            {
                statusCode = "300";
                message = e.Message;
            }
            return Json(new
            {
                statusCode = statusCode,
                message = message,
                Request = Request,
                ShopCode = ShopCode
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 支付完成扫呗回调(完成签名数据安全)
        /// </summary>
        /// <returns></returns>
        public JsonResult CallBackWY()
        {
            SaoBeiCallObject _callbackobj = new SaoBeiCallObject();
            WxPayData data = new WxPayData();
            data.SetValue("return_code", "01");
            data.SetValue("return_msg", "OK");
            string callback = string.Empty;
            string message = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    callback = reader.ReadToEnd();
                }
                _callbackobj = JsonHelper.GetObject<SaoBeiCallObject>(callback);
                string _householdid = _callbackobj.terminal_trace;
                House _card = CopHouseSvr.GetHouseByID(_householdid);
                Village _villageinfo = CopVillageSvr.GetVillageByID(_card.VillageID);
                Dictionary<string, string> _dic = new Dictionary<string, string>();
                _dic.Add("return_code", _callbackobj.return_code);
                _dic.Add("return_msg", _callbackobj.return_msg);
                _dic.Add("result_code", _callbackobj.result_code);
                _dic.Add("pay_type", _callbackobj.pay_type);
                _dic.Add("user_id", _callbackobj.user_id);
                _dic.Add("merchant_name", _callbackobj.merchant_name);
                _dic.Add("merchant_no", _callbackobj.merchant_no);
                _dic.Add("terminal_id", _callbackobj.terminal_id);
                _dic.Add("terminal_trace", _callbackobj.terminal_trace);
                _dic.Add("terminal_time", _callbackobj.terminal_time);
                _dic.Add("total_fee", _callbackobj.total_fee);
                _dic.Add("end_time", _callbackobj.end_time);
                _dic.Add("out_trade_no", _callbackobj.out_trade_no);
                _dic.Add("channel_trade_no", _callbackobj.channel_trade_no);
                _dic.Add("attach", _callbackobj.attach);
                _dic.Add("access_token", _villageinfo.PayToken);
                string sign = Security.MD5Encrypt(Common.UrlHelper.GetUrlForDictionary(_dic));
                ///验证签名
                if (_callbackobj.key_sign.Equals(sign))
                {
                    int ir = CopWXPaymentSvr.UpdateWXPaymentJFFlag(_callbackobj.out_trade_no, out message);
                    if (ir <= 0 || !string.IsNullOrEmpty(message))
                    {
                        data.SetValue("return_code", "FAIL");
                        data.SetValue("return_msg", "" + message + "");
                    }
                }
                else
                {
                    data.SetValue("return_code", "02");
                    data.SetValue("return_msg", "数据为空或支付失败!");
                }
            }
            catch (Exception ex0)
            {
                data.SetValue("return_code", "02");
                data.SetValue("return_msg", "" + ex0.Message + "");
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [OAuth(Redirect = "Pay/ReadJFMXByHouseID", Type = "snsapi_base")]
        public ActionResult ReadJFMXByHouseID(string HouseID)
        {
            List<JFMX> _lsjfmx = CopJFMXSvr.GetJFMXByHousID(HouseID);
            if (_lsjfmx != null && _lsjfmx.Count > 0)
            {
                for (int i = 0; i < _lsjfmx.Count; i++)
                {
                    float SSAmount = Convert.ToSingle(_lsjfmx[i].DJSSAmount);
                    _lsjfmx[i].DJSSAmount = SSAmount.ToString();
                }
                return View(_lsjfmx);
            }
            return Redirect("/Message/Warn?message=暂无更多缴费信息，请重新查询！");
        }

        [OAuth(Redirect = "Pay/ReadJFMXByOpenID", Type = "snsapi_base")]
        public ActionResult ReadJFMXByOpenID()
        {
            string OpenID = ConfigManager.OpenId;
            List<House> _housemx = CopHouseSvr.GetHouseByOpenID(OpenID);
            if (_housemx != null && _housemx.Count == 1)
            {
                return Redirect("/Pay/ReadJFMXByHouseID?HouseID=" + _housemx[0].ID.ToString());
            }

            if (_housemx != null && _housemx.Count > 1)
            {
                return Redirect("/Owner");
            }
            return Redirect("/Owner/OwnerRegister");
        }

        [OAuth(Redirect = "Pay/ReadZDMXByHouseID", Type = "snsapi_base")]
        public ActionResult ReadZDMXByHouseID(string HouseID)
        {
            List<ZDMX> _lszdmx = CopZDMXSvr.GetZDMXByHousID(HouseID);
            House _card = CopHouseSvr.GetHouseByID(HouseID);
            ViewBag.HouseID = HouseID;
            if (_card != null)
            {
                ViewBag.PayFlag = _card.OnlinePay;
            }
            if (_lszdmx != null && _lszdmx.Count > 0)
            {
                return View(_lszdmx);
            }
            return Redirect("/Message/Warn?message=暂无更多缴费信息，请重新查询！");
        }

        [OAuth(Redirect = "Pay/ReadZDMXByOpenID", Type = "snsapi_base")]
        public ActionResult ReadZDMXByOpenID()
        {
            string OpenID = ConfigManager.OpenId;
            List<House> _housemx = CopHouseSvr.GetHouseByOpenID(OpenID);
            if (_housemx != null && _housemx.Count == 1)
            {
                return Redirect("/Pay/ReadZDMXByHouseID?HouseID=" + _housemx[0].ID.ToString());
            }

            if (_housemx != null && _housemx.Count > 1)
            {
                return Redirect("/Owner");
            }
            return Redirect("/Owner/OwnerRegister");
        }

        [HttpPost]
        public JsonResult ZDJS(string HouseID, string SoID)
        {
            string statusCode = "200";
            string SFJE = string.Empty;
            string WYJ = string.Empty;
            try
            {
                House _card = CopHouseSvr.GetHouseByID(HouseID);
                List<ZDMX> _zdmx = CopZDMXSvr.GetZDMXByHouseSoID(HouseID, SoID);
                if (_zdmx == null)
                {
                    statusCode = "300";
                }
                SFJE = _zdmx.Sum(m => decimal.Parse(m.Amount)).ToString();
                WYJ = _zdmx.Sum(m => decimal.Parse(m.WYJ)).ToString();
            }
            catch (Exception)
            {
                statusCode = "300";
            }
            return Json(new
            {
                statusCode = statusCode,
                sfje = SFJE,
                wyj = WYJ
            }, JsonRequestBehavior.AllowGet);
        }
    }
}