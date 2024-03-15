using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TimerHelper
    {
        /// <summary>
        /// 返回当前日期
        /// </summary>
        /// <returns></returns>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 返回当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetTimeToString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 返回当前月的第一天
        /// </summary>
        /// <returns></returns>
        public static string GetNowMonthFirstDay()
        {
            return DateTime.Now.ToString("yyyy-MM") + "-1";
        }
        /// <summary>
        /// 返回时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获取时间差-天数
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static string GetDateDiff(DateTime StartDate, DateTime EndDate)
        {
            DateTime start = Convert.ToDateTime(StartDate.ToShortDateString());
            DateTime end = Convert.ToDateTime(EndDate.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days.ToString();
        }
    }
}
