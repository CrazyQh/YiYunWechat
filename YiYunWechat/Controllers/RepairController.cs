using Common;
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
    public class RepairController : Controller
    {
        [OAuth(Redirect = "Repair/Index", Type = "snsapi_base")]
        /// <summary>
        /// 报修处理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string villageid)
        {
            string OpenID = ConfigManager.OpenId;
            Village _villageinfo = CopVillageSvr.GetVillageByID(villageid);
            if (_villageinfo.OnLineBx == "0")
            {
                return Redirect("/Message/Warn?message=该小区暂未开通在线报修！");
            }
            List<Repair> _repairmx = CopRepairSvr.GetRepairRecord(OpenID);
            return View(_repairmx);
        }
        public JsonResult RepairMX(string houseid,string remarks, string phoneno, string files)
        {
            string statusCode = "200";
            string message = string.Empty;
            string openid = ConfigManager.OpenId;
            try

            {
                House HouseHoldCard = CopHouseSvr.GetHouseByID(houseid);
                if (HouseHoldCard == null)
                {
                    return Json(
                        new
                        {
                            statusCode = "300",
                            message = "住宅状态异常！"
                        }, JsonRequestBehavior.AllowGet
                        );
                }
                int ir = 0;
                ir = CopRepairSvr.InsertRepair(houseid, remarks, phoneno, files, out message);
                if (ir <= 0 || !string.IsNullOrEmpty(message))
                {
                    statusCode = "300";
                }
                int ir2 = 0;
                string FilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["BPMFilePath"].ToString();
                ir2 = CopRepairSvr.InsertRepairMX(files,FilePath, out message);
                if (ir2 <= 0 || !string.IsNullOrEmpty(message))
                {
                    statusCode = "300";
                }
            }
            catch (Exception ex0)
            {
                statusCode = "300";
                message = ex0.Message;
            }
            return Json(new { statusCode = statusCode, message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RepairUpload(HttpPostedFileBase file)
        {
            string statusCode = "200";
            string message = string.Empty;
            try

            {
                int ir = 0;
                ir = ImageUpload.upload(file, out message);
                if (ir <= 0 || (!string.IsNullOrEmpty(message) && !message.StartsWith("C:")))
                {
                    statusCode = "300";
                }
            }
            catch (Exception ex0)
            {
                statusCode = "300";
                message = ex0.Message;
            }
            return Json(new { statusCode = statusCode, message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRepairHouse()
        {
            string openid = ConfigManager.OpenId;
            //string openid = "o2HLA01_z9ni033uCYFJ1DFbnxtc";
            //获取房号
            List<House> _cardmx = new List<House>();
            _cardmx = CopHouseSvr.GetHouseByOpenID(openid);
            return Json(new { cardmx = _cardmx });
        }
    }
}