using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Common
{
   [DataContract(Name = "Folder", Namespace = "FEApp.Common.NewFolder")]
    public class NewFolder
    {
        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
