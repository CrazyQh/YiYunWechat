using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YiYun.Bs;
using YiYun.Entity;

namespace YiYunWechat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //��ʱ��///
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);
            //��������ʱ���ʱ���� �˴�����Ϊ����
            aTimer.Interval = 1000 * 60;//3600��Ҳ����1Сʱ��ÿ1Сʱˢ��һ�Σ�����ÿ1Сʱִ�з���һ��
            aTimer.Enabled = true;
            LogWrite.WriteLog("����ʼ��");
        }

        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)

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

                    LogWrite.WriteLog(DateTime.Now.ToString() + " ���ͳɹ���");
                }
                catch (Exception ex)
                {
                    LogWrite.WriteLog(DateTime.Now.ToString() + " ����ʧ�ܡ�ԭ��" + ex.Message);
                }
            }
            else
            {
                LogWrite.WriteLog(DateTime.Now.ToString() + " ������Ҫ���͵���Ϣ��");
            }

        }
    }
}
