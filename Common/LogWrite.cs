using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{

    public class LogWrite
    {
        /// <summary>
        /// 写入日志 
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_path"></param>
        public static void WriteLog(string _content, string _path = null)
        {
            //日志目录默认C:盘
            if (string.IsNullOrEmpty(_path))
            {
                _path = @"C:/log";
            }
            //如果日志目录不存在就创建
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            //文件完整名称
            string filename = _path + "/" + TimerHelper.GetDate() + ".txt";
            using (StreamWriter reader = File.AppendText(filename))
            {
                reader.WriteLine(TimerHelper.GetDateTime() +" : "+ _content);
            }
        }
    }
}
