using FEApp.StaticTools.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Core.Models
{
    public class FileExplorerModel
    {
        string _endPoint;
        public string DownloadPath { get; set; }
        public FileExplorerModel(string endpoint)
        {
            _endPoint = endpoint;
        }

        public async Task<HttpResponseMessage> DeleteFile(Common.FileInfo file)
        {
            return await HttpMessageSender.DeleteFile(file, _endPoint + "delete");
        }

        public async Task<IDownloadedFile> GetFile(Common.FileInfo file)
        {
            return await HttpMessageSender.GetFile(file, _endPoint + "get");
        }

    }
}
