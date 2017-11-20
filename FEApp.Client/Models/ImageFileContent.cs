using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FEApp.Client.Models
{
    public class ImageFileContent : FileContent
    {
        public ImageFileContent(Common.IDownloadedFile file)
            :base(file)
        {
            Content = StaticTools.Utilities.Specific.ByteArratToImage(file.Buffor);
        }

        private BitmapImage _bitmapImage;
        public BitmapImage Content
        {
            get { return _bitmapImage; }
            set
            {
                SetProperty(ref _bitmapImage, value);
            }
        }

    }
}
