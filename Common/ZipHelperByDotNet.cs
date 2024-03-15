using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
     ///调用
     //List<string> _list = new List<string>() { @"E:\image" };
     //ZipHelperByDotNet.Zip(_list,@"E:\txt.zip",true,"1111");
     //return File(@"E:\txt.zip", "application/zip", "sample.zip");
    public class ZipHelperByDotNet
    {
        /// <summary>
        /// 压缩ZIP文件
        /// 支持多文件和多目录，或是多文件和多目录一起压缩
        /// </summary>
        /// <param name="list">待压缩的文件或目录集合</param>
        /// <param name="strZipName">压缩后的文件名</param>
        /// <param name="IsDirStruct">是否按目录结构压缩</param>
        /// <returns>成功：true/失败：false</returns>
        public static bool Zip(List<string> list, string strZipName, bool IsDirStruct, string password)
        {
            try
            {
                using (ZipFile zip = new ZipFile(Encoding.Default))//设置编码，解决压缩文件时中文乱码
                {
                    zip.Password = password;
                    foreach (string path in list)
                    {
                        string fileName = Path.GetFileName(path);//取目录名称
                        //如果是目录
                        if (Directory.Exists(path))
                        {
                            if (IsDirStruct)
                            {
                                zip.AddDirectory(path, fileName);
                            }
                            else
                            {
                                zip.AddDirectory(path);
                            }
                        }
                        //如果是文件
                        if (File.Exists(path))
                        {
                            zip.AddFile(path);
                        }
                    }
                    //压缩
                    zip.Save(strZipName);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
