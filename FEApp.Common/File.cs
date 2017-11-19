using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FEApp.Common
{
    [DataContract(Name = "File", Namespace = "FEApp.Common.File")]
    [KnownType(typeof(File))]
    public class File
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

        public File(string path)
        {
            Path = path;
        }
        public File()
        {

        }
    }
}
