using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class CentralBank : Bank
    {
        private const int DefaultCapacity = 50;

        public CentralBank(string name) 
            : base(name, DefaultCapacity)
        {
        }
    }
}
