using FEApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FEApp.Service.Controllers
{
    public class FoldersController : ApiController
    {

        FoldersControllerModel model = 
            new FoldersControllerModel(EntryPathOptions.EntryPath);

        // GET: api/Folders
        public Common.Folder Get()
        {
            return model.GetDirs();
        }

        // GET: api/Folders/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Folders
        public bool Post([FromBody]Common.NewFolder value)
        {
            return model.AddDir(value);
        }

        // PUT: api/Folders/5
        public void Put(int id, [FromBody]string value)
        {

        }

        [HttpDelete]
        [Route("api/Folders/delete")]
        public bool Delete([FromBody]Common.Folder folder)
        {
            return model.DeleteDir(folder);
        }
    }
}
