using FEApp.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace FEApp.Service.Controllers
{
    public class FilesController : ApiController
    {
        FilesControllerModel model = new FilesControllerModel();

        // GET: api/Files
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("api/Files/get")]
        public HttpResponseMessage GetFile([FromBody] Common.FileInfo file)
        {
            return FileAsAttachment(file.Path, file.Name);
        }

        // GET: api/Files/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Files
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Files/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        [Route("api/Files/delete")]
        public void Delete([FromBody]Common.FileInfo file)
        {
            model.Delete(file);
        }

        public static HttpResponseMessage FileAsAttachment(string path, string filename)
        {
            if (File.Exists(path))
            {

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = filename;
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
