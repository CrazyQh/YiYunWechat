using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 获取Web.config文件AppSettings节点
    /// </summary>
    public class Configuration
    {
        public static NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.GetSection("appSettings") as NameValueCollection;
            }
        }
        public static string AppSettingValue(string _key)
        {
            return AppSettings.Get(_key);
        }
    }
}
