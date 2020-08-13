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
        public string updateGroup;

        public Shop(string label, string collection, string name, string updateGroup)
        {
            this.label = label;
            this.collection = collection;
            this.name = name;
            this.updateGroup = updateGroup;
        }
    }
}
