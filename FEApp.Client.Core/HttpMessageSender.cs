using FEApp.Common;
using FEApp.StaticTools.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Core
{
    internal static class HttpMessageSender
    {
        internal static async Task<IDownloadedFile> GetFile(Common.FileInfo fileInfo, string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                request.Content = new StringContent(Serialization.DataContractSerializeObject<Common.FileInfo>(fileInfo),
                    Encoding.UTF8, "application/xml");

                var response = await client.SendAsync(request);

                var stream = await response.Content.ReadAsStreamAsync();

                var buffer = new byte[stream.Length];

                var result = await stream.ReadAsync(buffer, 0, (int)stream.Length);

                var downloadedFileInfo = new Common.DownloadedFileInfo()
                {
                    Path = Paths.DownloadPath + response.Content.Headers.ContentDisposition.FileName,
                    Name = response.Content.Headers.ContentDisposition.FileName,
                    Buffor = buffer
                };
                return downloadedFileInfo;
            }                
        }

        internal static async Task<HttpResponseMessage> DeleteFile(Common.FileInfo fileInfo, string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                request.Content = new StringContent(Serialization.DataContractSerializeObject<Common.FileInfo>(fileInfo),
                    Encoding.UTF8, "application/xml");

                return await client.SendAsync(request);
            }
        }

        internal static async Task<HttpResponseMessage> DeleteDir(Common.Folder folder, string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                request.Content = new StringContent(Serialization.DataContractSerializeObject<Common.Folder>(folder),
                    Encoding.UTF8, "application/xml");

                return await client.SendAsync(request);
            }

        }

        internal static async Task<HttpResponseMessage> AddFolder(Common.NewFolder folder, string uri)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(Serialization.DataContractSerializeObject<Common.NewFolder>(folder),
                    Encoding.UTF8, "application/xml");

                return await client.PostAsync(uri, httpContent);
            }
        }

        //now supported 1 directory from server,
        //
        internal static Common.Folder GetMyFilesAndDirs(string uri)
        {
            var req = (HttpWebRequest)WebRequest.Create(uri);
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
    }
}
