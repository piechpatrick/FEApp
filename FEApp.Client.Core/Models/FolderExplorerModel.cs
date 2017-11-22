using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Net.Http.Headers;
using System.Xml;
using FEApp.StaticTools.Utilities;

namespace FEApp.Client.Core.Models
{
    public class FolderExplorerModel
    {
        string _endpoint;

        public FolderExplorerModel(string endPoint)
        {
            _endpoint = endPoint;
        }
        public FolderExplorerModel()
        {

        }

        public Common.Folder GetFilesAndDirs()
        {
            return HttpMessageSender.GetMyFilesAndDirs(_endpoint);
        }

        public async Task<HttpResponseMessage> AddDir(string path)
        {
            var newFolder = new Common.NewFolder() { Path = path, Name = "New Folder" };

            return await HttpMessageSender.AddFolder(newFolder, _endpoint);
        }

        public async Task<HttpResponseMessage> DeleteDir(Common.Folder folder)
        {
            var deleteUri = _endpoint + "delete";

            return await HttpMessageSender.DeleteDir(folder, deleteUri);
        }

    }
}
