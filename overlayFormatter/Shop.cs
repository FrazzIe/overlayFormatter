using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overlayFormatter
{
    public class Shop
    {
        public string label;
        public string collection;
        public string name;

        public Shop(string label, string collection, string name)
        {
            this.label = label;
            this.collection = collection;
            this.name = name;
        }
    }
}
