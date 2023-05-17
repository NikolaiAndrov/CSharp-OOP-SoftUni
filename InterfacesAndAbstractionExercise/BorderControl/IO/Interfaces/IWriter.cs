﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl.IO.Interfaces
{
    public interface IWriter
    {
        void WriteLine(string text);
        void Write(string text);
    }
}
