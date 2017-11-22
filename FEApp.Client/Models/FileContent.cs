using FEApp.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Models
{
    public abstract class FileContent : BindableBase
    {
        protected IDownloadedFile File { get; private set; }
        public FileContent(IDownloadedFile file)
        {
            File = file;
        }
    }
}
