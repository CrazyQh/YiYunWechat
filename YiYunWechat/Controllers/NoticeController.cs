using System.Collections.Generic;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYunWechat.Controllers
{
    public class NoticeController : Controller
    {
        [OAuth(Redirect = "Notice/Index", Type = "snsapi_base")]
        public ActionResult Index(string VillageID = null)
        {
            if (!string.IsNullOrEmpty(VillageID))
            {
                Village _villageinfo = new Village();
                _villageinfo = CopVillageSvr.GetVillageByID(VillageID);
                if (_villageinfo.OnLineGg == "0")
                {
                    return Redirect("/Message/Warn?message=该小区暂未开通在线公告！");
                }
                List<Notice> _adnt = CopNoticeSvr.GetNotice(VillageID);
                return View(_adnt);
            }
            else
            {
                string openid = ConfigManager.OpenId;
                //string openid = "oHOZG6gvm0LGwUuziRyahinm4s8s";
                List<Village> _xqmx = CopVillageSvr.GetVillageByOpenID(openid);
                if (_xqmx != null && _xqmx.Count == 1)
                {
                    if (_xqmx[0].OnLineGg == "0")
                    {
                        return Redirect("/Message/Warn?message=该小区暂未开通在线公告！");
                    }
                    List<Notice> _adnt = CopNoticeSvr.GetNotice(_xqmx[0].ID);
                    return View(_adnt);
                }
                if (_xqmx != null && _xqmx.Count > 1)
                {
                    return Redirect("/Notice/VillageSelect");
                }
                else
                {
                    return Redirect("/Message/Warn?message=该小区暂未开通在线公告！");
                }
            }
        }

        public ActionResult NoticeMX(string TaskID)
        {
            Notice _adnt = CopNoticeSvr.GetNoticeMX(TaskID);
            return View(_adnt);
        }
    }
}