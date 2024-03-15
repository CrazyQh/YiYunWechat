using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class SesstionHelper
    {
        /// <summary>
        /// 写入Sessions
        /// </summary>
        /// <param name="SessionName"></param>
        /// <param name="SessionValue"></param>
        public static void WriteSession(string SessionName, string SessionValue)
        {
            HttpContext.Current.Session[SessionName] = SessionValue;
        }
        /// <summary>
        /// 读取Session
        /// </summary>
        /// <param name="SessionName"></param>
        /// <returns></returns>
        public static string ReadSession(string SessionName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionName] != null)
            {
                return HttpContext.Current.Session[SessionName].ToString();
            }
            return null;
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="SessionName"></param>
        public static void RemoveSession(string SessionName)
        {
            if (HttpContext.Current.Session[SessionName] != null)
            {
                HttpContext.Current.Session[SessionName] = null;
            }
        }
    }
}
