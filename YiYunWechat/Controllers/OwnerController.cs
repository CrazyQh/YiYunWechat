using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYunWechat.Controllers
{
    public class OwnerController : Controller
    {
        [OAuth(Redirect = "Owner/Index", Type = "snsapi_base")]
        public ActionResult Index()
        {
            string OpenID = ConfigManager.OpenId;
            //string OpenID = "oHOZG6gvm0LGwUuziRyahinm4s8s";
            if (string.IsNullOrEmpty(OpenID))
            {
                throw new Exception("页面失效");
            }

            List<House> house = CopHouseSvr.GetHouseByOpenID(OpenID);

            if (house == null || house.Count <= 0)
            {
                return RedirectToAction("OwnerRegister");
            }

            return View(house);
        }

        [OAuth(Redirect = "Owner/OwnerRegister", Type = "snsapi_base")]
        public ActionResult OwnerRegister()
        {
            return View();
        }

        [HttpPost]
        public JsonResult OwnerRegister(string HouseID, string HouseTel)
        {
            string statusCode = "200";
            string message = string.Empty;
            string OpenID = ConfigManager.OpenId;
            //string OpenID = "oHOZG6gvm0LGwUuziRyahinm4s8s";
            try

            {
                House house = CopHouseSvr.GetHouseByID(HouseID);
                if (house == null)
                {
                    return Json(new { statusCode = "300", message = "住址状态异常！" }, JsonRequestBehavior.AllowGet);
                }
                if (house.Tel != HouseTel)
                {
                    return Json(new { statusCode = "300", message = "请输入正确的业主电话！" }, JsonRequestBehavior.AllowGet);

                }
                int ir = 0;
                string opentype = null;
                if (string.IsNullOrEmpty(house.OpenID1))
                {
                    opentype = "OpenID1";
                    ir = CopHouseSvr.UpdateOpenID(HouseID, OpenID, opentype, out message);
                    if (ir <= 0 || !string.IsNullOrEmpty(message))
                    {
                        statusCode = "300";
                    }
                }
                else if (string.IsNullOrEmpty(house.OpenID2))
                {
                    opentype = "OpenID2";
                    ir = CopHouseSvr.UpdateOpenID(HouseID, OpenID, opentype, out message);
                    if (ir <= 0 || !string.IsNullOrEmpty(message))
                    {
                        statusCode = "300";
                    }
                }
                else
                {
                    return Json(new { statusCode = "300", message = "此住宅已被绑定，如有疑问，请致电物业询问！ " }, JsonRequestBehavior.AllowGet);
                }               
            }
            catch (Exception ex0)
            {
                statusCode = "300";
                message = ex0.Message;
            }
            return Json(new { statusCode = statusCode, message = message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取小区
        /// </summary>
        /// <returns></returns>
        public string GetVillage()
        {

            List<Village> _villagemx = new List<Village>();
            _villagemx = CopVillageSvr.GetVillage();
            StringBuilder sbl = new StringBuilder("[");
            for (int i = 0; i < _villagemx.Count; i++)
            {
                sbl.Append("{\"n\":\"" + _villagemx[i].VillageName + "\",\"id\":\"" + _villagemx[i].ID + "\"}");
                if (i < _villagemx.Count - 1)
                {
                    sbl.Append(",");
                }
            }
            sbl.Append("]");
            return sbl.ToString();
        }
        /// <summary>
        /// 区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetArea(string VillageID)
        {
            List<Area> _areamx = new List<Area>();
            _areamx = CopAreaSvr.GetAreaByVillageID(VillageID);
            StringBuilder sbl = new StringBuilder("[");
            for (int i = 0; i < _areamx.Count; i++)
            {
                sbl.Append("{\"n\":\"" + _areamx[i].AreaName + "\",\"id\":\"" + _areamx[i].ID + "\"}");
                if (i < _areamx.Count - 1)
                {
                    sbl.Append(",");
                }

            }
            sbl.Append("]");
            return sbl.ToString();
        }
        /// <summary>
        /// 楼宇
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFloor(string AreaID)
        {
            List<Floor> _floormx = new List<Floor>();
            _floormx = CopFloorSvr.GetFloorByAreaID(AreaID);
            StringBuilder sbl = new StringBuilder("[");
            for (int i = 0; i < _floormx.Count; i++)
            {
                sbl.Append("{\"n\":\"" + _floormx[i].FloorName + "\",\"id\":\"" + _floormx[i].ID + "\"}");
                if (i < _floormx.Count - 1)
                {
                    sbl.Append(",");
                }
            }
            sbl.Append("]");
            return sbl.ToString();
        }
        /// <summary>
        /// 单元
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUnit(string FloorID)
        {
            List<Unit> _unitmx = new List<Unit>();
            _unitmx = CopUnitSvr.GetUnitByFloorID(FloorID);
            StringBuilder sbl = new StringBuilder("[");
            for (int i = 0; i < _unitmx.Count; i++)
            {
                sbl.Append("{\"n\":\"" + _unitmx[i].UnitName + "\",\"id\":\"" + _unitmx[i].ID + "\"}");
                if (i < _unitmx.Count - 1)
                {
                    sbl.Append(",");
                }
            }
            sbl.Append("]");
            return sbl.ToString();
        }
        /// <summary>
        /// 房号
        /// </summary>
        /// <param name="floorid"></param>
        /// <param name="unitcode"></param>
        /// <returns></returns>
        public string GetHouse(string UnitID)
        {
            List<House> _housemx = new List<House>();
            _housemx = CopHouseSvr.GetHouseByUnitID(UnitID);
            StringBuilder sbl = new StringBuilder("[");
            for (int i = 0; i < _housemx.Count; i++)
            {
                sbl.Append("{\"n\":\"" + _housemx[i].DoorNo + "\",\"id\":\"" + _housemx[i].ID + "\"}");
                if (i < _housemx.Count - 1)
                {
                    sbl.Append(",");
                }
            }
            sbl.Append("]");
            return sbl.ToString();
        }

    }
}