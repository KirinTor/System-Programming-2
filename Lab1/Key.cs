using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    struct Key
    {
        private char charKey;
        private string stringKey;
        private ushort ushortKey;

        public char KeyChar { get { return this.charKey; } }
        public string KeyString { get { return this.stringKey; } }
        public ushort KeyUshort { get { return this.ushortKey; } }

        public Key(char charKey, string stringKey, ushort ushortKey)
        {
            this.charKey = charKey;
            this.stringKey = stringKey;
            this.ushortKey = ushortKey;
        }
    }
}
