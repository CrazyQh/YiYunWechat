using Common;
using System;
using System.Collections.Generic;
using System.Data;
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
                    case "ZDCJ":
                        CopWechatMessageTaskSvr.CJ(task);
                        break;
                }
            }

            return "1";
        }

        public string AutoSend()
        {
            DataSet ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, " SELECT DISTINCT MessageType,SoTaskID FROM dbo.PMC_WechatMessageTask WHERE SendState = 0 ");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        List<WechatMessageTask> task = new List<WechatMessageTask>();
                        task = CopWechatMessageTaskSvr.GetTaskInfo(row["SoTaskID"].ToString(), row["MessageType"].ToString());

                        if (task != null && task.Count > 0)
                        {
                            switch (row["MessageType"])
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
                                case "ZDCJ":
                                    CopWechatMessageTaskSvr.CJ(task);
                                    break;
                            }
                        }
                    }

                    return DateTime.Now.ToString() + " 发送成功。";
                }
                catch (Exception e)
                {
                    return DateTime.Now.ToString() + " 发送失败。原因：" + e.Message;
                }
            }
            else
            {
                return DateTime.Now.ToString() + " 暂无需要发送的消息。";
            }
            
        }
    }
}