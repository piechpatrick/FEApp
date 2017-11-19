using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.StaticTools.Utilities
{
    public static class FolderBrowserHelper
    {
        public static Common.Folder GetDirsAndFiles(Common.Folder folder)
        {
            try
            {
                foreach (var f in Directory.GetFiles(folder.Path))
                {
                    folder.Files.Add(new Common.File(f));
                }

                foreach (var d in Directory.GetDirectories(folder.Path))
                {
                    var subFolder = new Common.Folder(d);
                    folder.SubFolders.Add(subFolder);
                    GetDirsAndFiles(subFolder);
                }
                return folder;

            }
            catch(System.Exception e)
            {
                return null;
            }
        }

    }
}
