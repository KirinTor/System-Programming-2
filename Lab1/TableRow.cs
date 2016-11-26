using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class TableRow
    {
        private Key key;
        private Function value;
        public TableRow(Key key, Function value)
        {
            this.key = key;
            this.value = value;
        }
        public Key Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
        public Function Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public void Update(TableRow updatedTableRow)
        {
            this.directAddressKey = updatedTableRow.getDirectAddressKey();
            this.foreignAddressKey = updatedTableRow.getForeignAddressKey();
            this.functionalField = updatedTableRow.getFunctionalField();
        }
        public override string ToString()
        {
            return "[Key:" +
                    "charKey=" + key.KeyChar +
                    ", stringKey='" + key.KeyString + '\'' +
                    ", functionalField=" + functionalField.toString() +
                    '}';
        }
    }
}
