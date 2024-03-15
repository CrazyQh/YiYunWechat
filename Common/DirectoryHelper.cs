using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 文件夹操作类
    /// </summary>
    public class DirectoryHelper
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="_path"></param>
        public static void CreateFolder(string _path)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="bol"></param>
        public static void DeleteFolder(string _path, bool bol)
        {
            if (Directory.Exists(_path))
            {
                Directory.Delete(_path, bol);
            }
        }
        /// <summary>
        /// 拷贝文件到指定文件夹
        /// </summary>
        /// <param name="_pathfrom"></param>
        /// <param name="_pathto"></param>
        public static void CopyFolder(string _pathfrom, string _pathto)
        {
            if (!Directory.Exists(_pathto))
            {
                Directory.CreateDirectory(_pathto);
            }
            string[] files = Directory.GetFiles(_pathfrom, "*.jpg");
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                File.Copy(file, Path.Combine(_pathto, filename), true);
            }
        }
    }
}
