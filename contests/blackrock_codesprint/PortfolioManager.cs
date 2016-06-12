using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/portfolio-manager
/// </summary>
class Solution4
{
    class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public long Amount { get; set; }
        public long SelectedMaxAmount { get; set; }
        public long NotSelectedMaxAmount { get; set; }

        public static Node Deserialize(string line)
        {
            Node root = new Node();
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);

            string[] data = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int ix = 0;
            while (q.Count > 0 && ix < data.Length)
            {
                Node node = q.Dequeue();
                if (data[ix] != "#")
                {
                    node.Amount = long.Parse(data[ix]);
                    node.Left = new Node();
                    node.Right = new Node();
                    q.Enqueue(node.Left);
                    q.Enqueue(node.Right);
                }
                ix++;
            }
            return root;
        }

        public static long MaxAmount(Node node, bool selected)
        {
            if (node == null) return 0;
            if (selected && node.SelectedMaxAmount > 0) return node.SelectedMaxAmount;
            if (!selected && node.NotSelectedMaxAmount > 0) return node.NotSelectedMaxAmount;

            if (selected)
            {
                node.SelectedMaxAmount = node.Amount + MaxAmount(node.Left, false) + MaxAmount(node.Right, false);
                return node.SelectedMaxAmount;
            }
            else
            {
                node.NotSelectedMaxAmount =
                    Math.Max(MaxAmount(node.Left, true), MaxAmount(node.Left, false)) +
                    Math.Max(MaxAmount(node.Right, true), MaxAmount(node.Right, false));
                return node.NotSelectedMaxAmount;
            }
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"6
//3 4 5 1 3 # 1");

        tIn.ReadLine();
        Node root = Node.Deserialize(tIn.ReadLine());
        tOut.WriteLine(Math.Max(Node.MaxAmount(root, true), Node.MaxAmount(root, false)));

        //Console.ReadLine();
    }
}
