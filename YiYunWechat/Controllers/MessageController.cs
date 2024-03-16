using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YiYunWechat.Controllers
{
    public class MessageController : Controller
    {
        public ActionResult Warn(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}