using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FEApp.StaticTools.Utilities
{
    public static class Specific
    {
        public static BitmapImage ArratToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn.Length > 0)
            {
                try
                {
                    MemoryStream stream = new MemoryStream();
                    stream.Write(byteArrayIn, 0, byteArrayIn.Length);
                    stream.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                    BitmapImage returnImage = new BitmapImage();
                    returnImage.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    returnImage.StreamSource = ms;
                    returnImage.EndInit();

                    return returnImage;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
