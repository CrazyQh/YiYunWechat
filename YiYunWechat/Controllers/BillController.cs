using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYunWechat.Controllers
{
    public class BillController : Controller
    {
        [OAuth(Redirect = "Bill/ReadZDMXByHouseID", Type = "snsapi_base")]
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

        public ActionResult ReadZDMXByTaskHouseID(string HouseID,string TaskID)
        {
            List<ZDMX> _lszdmx = CopZDMXSvr.GetZDMXByHousID(HouseID);
            House _card = CopHouseSvr.GetHouseByID(HouseID);
            ViewBag.HouseID = HouseID;
            ViewBag.TaskID = TaskID;
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

        [OAuth(Redirect = "Bill/ReadZDMXByOpenID", Type = "snsapi_base")]
        public ActionResult ReadZDMXByOpenID()
        {
            string OpenID = ConfigManager.OpenId;
            List<House> _housemx = CopHouseSvr.GetHouseByOpenID(OpenID);
            if (_housemx != null && _housemx.Count == 1)
            {
                return Redirect("/Bill/ReadZDMXByHouseID?HouseID=" + _housemx[0].ID.ToString());
            }

            if (_housemx != null && _housemx.Count > 1)
            {
                return Redirect("/Owner");
            }
            return Redirect("/Owner/OwnerRegister");
        }

        /// <summary>
        /// 账单页面金额计算
        /// </summary>
        /// <param name="HouseID"></param>
        /// <param name="SoID"></param>
        /// <returns></returns>
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