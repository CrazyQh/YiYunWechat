using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Common
{
    /// <summary>
    /// 图片上传类
    /// </summary>
    public class ImageUpload
    {

        /// <summary>
        /// file标签转换成byte数组
        /// </summary>
        /// <param name="_file"></param>
        /// <returns></returns>
        public static byte[] GetFileImage(HttpPostedFileBase _file)
        {
            byte[] bytes = null;
            using (BinaryReader bianry = new BinaryReader(_file.InputStream))
            {
                bytes = bianry.ReadBytes(_file.ContentLength);
            }
            return bytes;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="path">路径</param>
        /// <returns>路径字符串</returns>
        public static int upload(HttpPostedFileBase file, string path, out string _resms)
        {
            _resms = "";
            int ir = 1;
            string str = true_name(file.FileName);
            //str = file.FileName;
            try
            {
                if (str == "0")
                {
                    throw new Exception("图片格式不正确");
                }
                if ((file != null) && (file.ContentLength > 0))
                {
                    //string filename = AppDomain.CurrentDomain.BaseDirectory + path + str;
                    string filename = "C:\\Program Files (x86)\\FlowPortal BPM 6.x\\WEB\\TempFiles\\" + str;
                    _resms = "C:\\Program Files (x86)\\FlowPortal BPM 6.x\\WEB\\TempFiles\\" + str;
                    file.SaveAs(filename);
                }
                return ir;
            }
            catch (Exception ex0)
            {
                _resms = ex0.Message;
                return -1;
            }
        }
        /// <summary>
        /// 生成图片文件名
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string true_name(string fname)
        {
            string str2 = "0";
            if (string.IsNullOrEmpty(fname))
            {
                return str2;
            }
            int startIndex = fname.LastIndexOf('.');
            string str = fname.Substring(startIndex).ToLower();
            //if ((!(str == ".bmp") && !(str == ".jpg")) && (!(str == ".gif") && !(str == ".png") && !(str == ".jpeg")))
            //{
            //    return str2;
            //}
            int num2 = new Random().Next(0, 0x3e8);
            return (System.Guid.NewGuid() + num2.ToString() + str);
        }
    }
}
