using FEApp.Common;
using FEApp.StaticTools.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FEApp.Service.Models
{
    internal class FoldersControllerModel
    {
        Folder _folder;

        public FoldersControllerModel(string rootPath)
        {
            _folder = new Folder(rootPath);            
        }

        internal Common.Folder GetDirs()
        {
            return FolderBrowserHelper.GetDirsAndFiles(_folder);
        }

        internal bool AddDir(Common.NewFolder folder)
        {
            Directory.CreateDirectory(folder.Path + "\\" + folder.Name);
            return true;
        }

        internal bool DeleteDir(Common.Folder folder)
        {
            Directory.Delete(folder.Path,true);
            return true;
        }



    }
}