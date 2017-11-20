using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Common
{
    public class DownloadedFileInfo : IDownloadedFile
    {
        public string Path { get; set;}
        public string Name { get; set; }
        public byte[] Buffor { get; set; }
    }
}
