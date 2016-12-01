using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Tree
    {
        private static Node root;
        private List<Node> nodes;

        public Tree()
        {
            this.nodes = new List<Node>();
        }
        public Node ArrayToTree(string[] array, int start, int end)
        {
            if (start > end)
            {
                return null;
            }
            int mid = (start + end) / 2;
            Node node = new Node(array[mid]);
            node.setLeft(ArrayToTree(array, start, mid - 1));
            node.setRigth(ArrayToTree(array, mid + 1, end));
            nodes.Add(node);
            return node;
        }
        public static void setRoot(Node root)
        {
            Tree.root = root;
        }
        public void print()
        {
            foreach (Node node in nodes)
            {
                node.print();
            }
        }
    }
}
