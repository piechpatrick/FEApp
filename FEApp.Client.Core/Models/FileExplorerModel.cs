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

        public async Task<HttpResponseMessage> DeleteFile(Common.File file)
        {
            var deletePath = _endPoint + "delete";
            var request = new HttpRequestMessage(HttpMethod.Delete, deletePath);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(deletePath);
                request.Content = new StringContent(Serialization.DataContractSerializeObject<Common.File>(file),
                    Encoding.UTF8, "application/xml");

                var response = await client.SendAsync(request);
                return response;
            }
        }

        public async Task<Common.DownloadedFileInfo> GetFile(Common.File file)
        {
            var uri = _endPoint + "get";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                request.Content = new StringContent(Serialization.DataContractSerializeObject<Common.File>(file),
                    Encoding.UTF8, "application/xml");

                var response = await client.SendAsync(request);

                var stream = await response.Content.ReadAsStreamAsync();

                var buffer = new byte[stream.Length];

                var result = await stream.ReadAsync(buffer,0,(int)stream.Length);

                var downloadedFileInfo = new Common.DownloadedFileInfo()
                {
                    Path = DownloadPath + response.Content.Headers.ContentDisposition.FileName,
                    Name = response.Content.Headers.ContentDisposition.FileName,
                    Buffor = buffer
                };
                return downloadedFileInfo;
            }
        }

    }
}
