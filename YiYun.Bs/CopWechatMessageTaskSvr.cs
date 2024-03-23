using Common;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;
using YiYunWeChat.Auth;

namespace YiYun.Bs
{
    public class CopWechatMessageTaskSvr
    {
        public static List<WechatMessageTask> GetTaskInfo(string _SoTaskID, string _MessageType)
        { 
            WechatMessageTaskSvr taskSvr = new WechatMessageTaskSvr();
            return taskSvr.GetTaskInfo(_SoTaskID, _MessageType);
        }

        public static int UpdateMessageTask(string _ID)
        {
            WechatMessageTaskSvr taskSvr = new WechatMessageTaskSvr();
            return taskSvr.UpdateMessageTask(_ID);
        }

        /// <summary>
        /// 账单生成消息推送
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public static string ZD(List<WechatMessageTask> _tasks)
        {

            //获取token
            string access_token = CopWechatTokenSvr.GetToken(ConfigManager.APPIdS, ConfigManager.AppsecretS);

            //根据TaskID找到当前拥有人对应的openid
            for (int i = 0; i < _tasks.Count; i++)
            {
                SendMessage sm = new SendMessage();
                sm.touser = _tasks[i].OpenID;

                sm.url = ConfigManager.WebSiteUrl + "Bill/ReadZDMXByTaskHouseID?HouseID=" + _tasks[i].HouseID + "&TaskID=" + _tasks[i].SoTaskID;
                sm.template_id = _tasks[i].TemplateID;
                MessageParameters[] paras = new MessageParameters[]
                {
                    new MessageParameters(){value= _tasks[i].Params1 },
                    new MessageParameters(){value= _tasks[i].Params2 },
                    new MessageParameters(){value= _tasks[i].Params3 },
                    new MessageParameters(){value= _tasks[i].Params4 }
                };
                SendMessageData data = new SendMessageData()
                {
                    thing12 = paras[0],
                    thing14 = paras[1],
                    time27 = paras[2],
                    amount26 = paras[3]
                };
                sm.data = data;
                string message = string.Empty;
                string content = JsonHelper.GetJson<SendMessage>(sm);
                MessageCallback mcb = CommonApi.SendMessage(access_token, content, out message);
                if (mcb != null && mcb.errcode == "0")
                {
                    UpdateMessageTask(_tasks[i].ID);
                }
            }
            return "1";
        }

        /// <summary>
        /// 缴费成功消息推送
        /// </summary>
        /// <param name="_tasks"></param>
        /// <returns></returns>
        public static string JF(List<WechatMessageTask> _tasks)
        {

            //获取token
            string access_token = CopWechatTokenSvr.GetToken(ConfigManager.APPIdS, ConfigManager.AppsecretS);

            //根据TaskID找到当前拥有人对应的openid
            for (int i = 0; i < _tasks.Count; i++)
            {
                SendMessage sm = new SendMessage();
                sm.touser = _tasks[i].OpenID;

                sm.url = ConfigManager.WebSiteUrl + "Pay/ReadPaymentMXByTaskID?PayType=" + _tasks[i].MessageType + "&TaskID=" + _tasks[i].SoTaskID;
                sm.template_id = _tasks[i].TemplateID;
                MessageParameters[] paras = new MessageParameters[]
                {
                    new MessageParameters(){value= _tasks[i].Params1 },
                    new MessageParameters(){value= _tasks[i].Params2 },
                    new MessageParameters(){value= _tasks[i].Params3 },
                    new MessageParameters(){value= _tasks[i].Params4 }
                };
                SendMessageData data = new SendMessageData()
                {
                    thing8 = paras[0],
                    character_string13 = paras[1],
                    thing14 = paras[2],
                    amount6 = paras[3]
                };
                sm.data = data;
                string message = string.Empty;
                string content = JsonHelper.GetJson<SendMessage>(sm);
                MessageCallback mcb = CommonApi.SendMessage(access_token, content, out message);
                if (mcb != null && mcb.errcode == "0")
                {
                    UpdateMessageTask(_tasks[i].ID);
                }
            }
            return "1";
        }

        /// <summary>
        /// 退费成功消息推送
        /// </summary>
        /// <param name="_tasks"></param>
        /// <returns></returns>
        public static string TF(List<WechatMessageTask> _tasks)
        {

            //获取token
            string access_token = CopWechatTokenSvr.GetToken(ConfigManager.APPIdS, ConfigManager.AppsecretS);

            //根据TaskID找到当前拥有人对应的openid
            for (int i = 0; i < _tasks.Count; i++)
            {
                SendMessage sm = new SendMessage();
                sm.touser = _tasks[i].OpenID;

                sm.url = ConfigManager.WebSiteUrl + "Pay/ReadRefundMXByTaskID?TaskID=" + _tasks[i].SoTaskID;
                sm.template_id = _tasks[i].TemplateID;
                MessageParameters[] paras = new MessageParameters[]
                {
                    new MessageParameters(){value= _tasks[i].HouseID },
                    new MessageParameters(){value= _tasks[i].Params3 },
                    new MessageParameters(){value= _tasks[i].Params4 }
                };
                SendMessageData data = new SendMessageData()
                {
                    character_string1 = paras[0],
                    time4 = paras[1],
                    amount3 = paras[2]
                };
                sm.data = data;
                string message = string.Empty;
                string content = JsonHelper.GetJson<SendMessage>(sm);
                MessageCallback mcb = CommonApi.SendMessage(access_token, content, out message);
                if (mcb != null && mcb.errcode == "0")
                {
                    UpdateMessageTask(_tasks[i].ID);
                }
            }
            return "1";
        }
    }
}
