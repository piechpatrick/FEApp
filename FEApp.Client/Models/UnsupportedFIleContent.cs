using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Models
{
    public class UnsupportedFIleContent : FileContent
    {
        public UnsupportedFIleContent(Common.IDownloadedFile file)
            :base(file)
        {

        }
    }
}
