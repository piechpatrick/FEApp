using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Models
{
    public abstract class FileContent : BindableBase
    {
        protected Common.IDownloadedFile File { get; private set; }
        public FileContent(Common.IDownloadedFile file)
        {
            File = file;
        }
    }
}
