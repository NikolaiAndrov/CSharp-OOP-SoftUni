using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public string RandomString()
        {
            Random random = new Random();
            int n = random.Next(0, Count);
            string element = this[n];
            this.Remove(element);
            return element;
        }
    }
}
