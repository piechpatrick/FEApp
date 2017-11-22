using FEApp.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Models
{
    public class UnsupportedFIleContent : FileContent
    {
        public UnsupportedFIleContent(IDownloadedFile file)
            :base(file)
        {

        }
    }
}
