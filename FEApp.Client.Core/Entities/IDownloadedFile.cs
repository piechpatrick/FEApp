﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEApp.Client.Core
{
    public interface IDownloadedFile
    {
        string Path { get; set; }
        string Name { get; set; }
        byte[] Buffor { get; set; }
    }
}
