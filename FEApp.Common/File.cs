﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FEApp.Common
{
    [DataContract(Name = "File", Namespace = "FEApp.Common.File")]
    [KnownType(typeof(FileInfo))]
    public class FileInfo
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

        public FileInfo(string path)
        {
            Path = path;
        }
        public FileInfo()
        {

        }
    }
}
