using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FEApp.Service.Models
{
    public class FilesControllerModel
    {
        public FilesControllerModel()
        {


        }

        internal bool Delete(Common.FileInfo file)
        {
            try
            {
                File.Delete(file.Path);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}