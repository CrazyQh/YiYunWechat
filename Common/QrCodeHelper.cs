using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class QrCodeHelper
    {
         //<summary>
         //生成二维码
         //</summary>
         //<param name="strContent">写入二维码的内容</param>
         //<param name="_logourl">添加的图片</param>
         //<param name="ms">MemoryStream流</param>
        public static void CreateQrCode(string strContent, string _logourl, MemoryStream ms)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(strContent);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(12, QuietZoneModules.Two), Brushes.Black, Brushes.White);

            DrawingSize dSize = render.SizeCalculator.GetSize(qrCode.Matrix.Width);
            Bitmap map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
            Graphics g = Graphics.FromImage(map);
            render.Draw(g, qrCode.Matrix);
            //追加Logo图片 ,注意控制Logo图片大小和二维码大小的比例
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Image img = Image.FromFile(path + _logourl);
            Point imgPoint = new Point((map.Width - img.Width) / 2, (map.Height - img.Height) / 2);
            g.DrawImage(img, imgPoint.X, imgPoint.Y, img.Width, img.Height);
            map.Save(ms, ImageFormat.Png);
        }
    }
}
