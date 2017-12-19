using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WeChat.Infrastructure
{
    /// <summary>
    /// 缩率图
    /// </summary>
    public class ThumbnailHelper
    {
        /// <summary>
        /// 生成缩率图
        /// </summary>
        /// <param name="originalImage">原始图片Image</param>
        /// <param name="width">缩率图宽</param>
        /// <param name="height">缩率图高</param>
        /// <param name="mode">生成缩率图的方式</param>
        /// <param name="thumbnailPath">缩率图存放的地址</param>
        public static Image MakeThumbnail(Image originalImage, int width, int height, ThumbnailMode mode, string thumbnailPath, bool isSave = true)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case ThumbnailMode.HW://指定高宽缩放（可能变形）                  
                    break;
                case ThumbnailMode.W://指定宽，高按比例                      
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailMode.H://指定高，宽按比例  
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailMode.Cut://指定高宽裁减（不变形）                  
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;

                default:
                    break;
            }

            //新建一个bmp图片  
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板  
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充  
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分  
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
             new Rectangle(x, y, ow, oh),
             GraphicsUnit.Pixel);
            if (!isSave)
            {
                return bitmap;
            }
            try
            {
                //以jpg格式保存缩略图  
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
                return bitmap;

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }

    public enum ThumbnailMode
    {
        /// <summary>
        /// 指定高宽缩放（可能变形）
        /// </summary>
        HW,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,
        /// <summary>
        /// 指定宽，高按比例
        /// </summary>
        W,
        /// <summary>
        /// 指定高宽裁减（不变形）   
        /// </summary>
        Cut,

    }
}
