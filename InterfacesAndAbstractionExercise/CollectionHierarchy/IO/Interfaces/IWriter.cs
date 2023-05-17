﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.IO.Interfaces
{
    public interface IWriter
    {
        void Write(string value);
        void WriteLine(string value);
    }
}
