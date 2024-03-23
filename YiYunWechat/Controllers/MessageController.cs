using System.Collections.Generic;
using System.Web.Mvc;
using YiYun.Bs;
using YiYun.Entity;

namespace YiYunWechat.Controllers
{
    public class MessageController : Controller
    {
        public ActionResult Warn(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public string Send(string SoTaskID,string MessageType) 
        {
            List<WechatMessageTask> task = new List<WechatMessageTask>();
            task = CopWechatMessageTaskSvr.GetTaskInfo(SoTaskID, MessageType);

            if (task != null && task.Count > 0) 
            {
                switch (MessageType)
                {
                    case "SDZD":
                        CopWechatMessageTaskSvr.ZD(task);
                        break;
                    case "WYZD":
                        CopWechatMessageTaskSvr.ZD(task);
                        break;
                    case "LSZD":
                        CopWechatMessageTaskSvr.ZD(task);
                        break;
                    case "WYJF":
                        CopWechatMessageTaskSvr.JF(task);
                        break;
                    case "WXJF":
                        CopWechatMessageTaskSvr.JF(task);
                        break;
                    case "WYTF":
                        CopWechatMessageTaskSvr.TF(task);
                        break;
                }
            }

            return "1";
        }
    }
}