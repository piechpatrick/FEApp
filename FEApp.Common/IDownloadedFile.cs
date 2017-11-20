using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Common
{
    public interface IDownloadedFile
    {
        string Name { get; set; }
        byte[] Buffor { get; set; }
    }
}
