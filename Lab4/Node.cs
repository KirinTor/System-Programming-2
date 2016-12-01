using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Node
    {
        private static int count = 0;

        private int id;
        private string value;
        private Node left;
        private Node rigth;

        public Node(String value)
        {
            this.value = value;
            this.id = count;
            count++;
        }
        public String getValue()
        {
            return value;
        }
        public void setLeft(Node left)
        {
            this.left = left;
        }
        public void setRigth(Node rigth)
        {
            this.rigth = rigth;
        }
        public void print()
        {
            Console.WriteLine(("Node {0}; Value: {1}; Left: {2}; Right: {3}"), id, value,
                    left == null ? "<none>" : left.getValue(),
                    rigth == null ? "<none>" : rigth.getValue());
        }
    }
}
