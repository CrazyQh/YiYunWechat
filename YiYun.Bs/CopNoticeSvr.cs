using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiYun.Data;
using YiYun.Entity;

namespace YiYun.Bs
{
    public class CopNoticeSvr
    {
        /// <summary>
        /// 根据小区获取公告
        /// </summary>
        /// <param name="VillageID"></param>
        /// <returns></returns>
        public static List<Notice> GetNotice(string VillageID)
        {
            NoticeSvr _mxSvr = new NoticeSvr();
            return _mxSvr.GetNotice(VillageID);
        }

        /// <summary>
        /// 根据TaskID获取公告详细
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public static Notice GetNoticeMX(string TaskID)
        {
            NoticeSvr _mxSvr = new NoticeSvr();
            return _mxSvr.GetNoticeMX(TaskID);
        }
    }
}
