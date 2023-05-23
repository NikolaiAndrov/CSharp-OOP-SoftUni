using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03.Detail_Printer.IO.Interfaces
{
    public interface IWriter
    {
        void Write(object value);

        void WriteLine(object value);
    }
}
