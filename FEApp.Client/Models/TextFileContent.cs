using FEApp.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Models
{
    public class TextFileContent : FileContent
    {
        public TextFileContent(IDownloadedFile file)
            :base(file)
        {
            Content = StaticTools.Utilities.Specific.ByteArrayToString(file.Buffor);
        }

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                SetProperty(ref _content, value);
            }
        }
    }
}
