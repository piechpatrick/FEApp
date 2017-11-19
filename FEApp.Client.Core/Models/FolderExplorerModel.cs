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

        public Common.Folder GetFoldersAndDirs()
        {
            var req = (HttpWebRequest)WebRequest.Create(_endpoint);
            // default is JSON, but you can request XML
            req.Accept = "application/xml";
            req.ContentType = "application/xml";

            var resp = req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            // read the response stream as Text.
            var text = sr.ReadToEnd();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(text));

            return Serialization.DataContractDeserializeObject<Common.Folder>(ms);
        }

        public async Task<HttpResponseMessage> AddDir(string path)
        {
            var newFolder = new Common.NewFolder() { Path = path, Name = "New Folder" };
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(Serialization.DataContractSerializeObject<Common.NewFolder>(newFolder), 
                    Encoding.UTF8, "application/xml");

                var response = await client.PostAsync(_endpoint, httpContent);
                return response;
            }
        }

        public async Task<HttpResponseMessage> DeleteDir(Common.Folder folder)
        {
            var deletePath = _endpoint + "delete";
            var request = new HttpRequestMessage(HttpMethod.Delete, deletePath);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(deletePath);
                request.Content = new StringContent(Serialization.DataContractSerializeObject<Common.Folder>(folder),
                    Encoding.UTF8, "application/xml");

                var response = await client.SendAsync(request);
                return response;
            }

        }

    }
}
