using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.Core.Contracts
{
    internal interface IWriter
    {
        void Write(object message);
        void WriteLine(object message);
    }
}
