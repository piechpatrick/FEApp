using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FEApp.Common
{
    [DataContract(Name ="Folder", Namespace ="FEApp.Common.Folder")]
    [KnownType(typeof(Folder))]
    [KnownType(typeof(File))]
    public class Folder 
    {

        public string Name
        {
            get
            {
                var res = System.IO.Path.GetFileName(Path);
                return res;
            }
        }

        [DataMember]
        public string Path { get; set; }

        private List<Folder> _subFolders;
        [DataMember]
        public List<Folder> SubFolders
        {
            get
            {
                if (_subFolders == null)
                    _subFolders = new List<Folder>();
                return _subFolders;
            }
            set
            {
                _subFolders = value;
            }
        }

        private List<File> _files;
        [DataMember]
        public List<File> Files
        {
            get
            {
                if (_files == null)
                    _files = new List<File>();
                return _files;
            }
            set
            {
                _files = value;
            }
        }

        public Folder(string path)
        {
            Path = path;
        }

        public Folder()
        {

        }

    }
}
