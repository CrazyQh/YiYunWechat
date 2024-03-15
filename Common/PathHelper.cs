using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class PathHelper
    {
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Isvirtual_path">是否绝对路径</param>
        /// <returns></returns>
        public static string Getpath(string Path, bool Isvirtual_path)
        {
            if (Isvirtual_path)
            {
                return HttpContext.Current.Server.MapPath(Path);
            }
            return Path;
        }
    }
}
