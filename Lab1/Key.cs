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

        public char CharKey { get { return this.charKey; } }
        public string StringKey { get { return this.stringKey; } }
        public ushort UshortKey { get { return this.ushortKey; } }

        public Key(char charKey, string stringKey, ushort ushortKey)
        {
            this.charKey = charKey;
            this.stringKey = stringKey;
            this.ushortKey = ushortKey;
        }
        public bool IsEquals(Key key)
        {
            if (this.charKey == key.CharKey &&
                this.stringKey == key.StringKey &&
                this.ushortKey == key.UshortKey)
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return "Key[Char: "+charKey+"; String: "+stringKey+"; Ushort: "+ushortKey+"]";
        }
    }
}
