using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Kingdom Division
/// https://www.hackerrank.com/contests/world-codesprint-9/challenges/kingdom-division
/// </summary>
class Solution4
{
    // -------------------------------------------------------------------------
    //
    // Tree structure:
    //
    // int Adj(int v, int ix) - a ix-th node adjacent to v
    // int Deg(int v) - number of nodes adjacent to v (including the parent)
    // int Parent(int v) - the parent node of v
    //
    // -------------------------------------------------------------------------
    public class TreePath
    {
        public int N { get; private set; }
        int[][] adj = null;
        int[] _parent = null;
        int[] _depth = null;
        public TreePath(int n, IEnumerable<int[]> edges)
        {
            N = n;
            adj = new int[N][];
            int[][] e2a = edges.ToArray();
            int[] deg = new int[N];
            foreach (int[] e in e2a)
            {
                deg[e[0]]++;
                deg[e[1]]++;
            }
            for (int i = 0; i < N; i++) adj[i] = new int[deg[i]];
            Array.Clear(deg, 0, N);
            foreach (int[] e in e2a)
            {
                adj[e[0]][deg[e[0]]] = e[1];
                deg[e[0]]++;
                adj[e[1]][deg[e[1]]] = e[0];
                deg[e[1]]++;
            }
            _parent = new int[N];
            _depth = new int[N];
            DFS(0);
        }
        void DFS(int root)
        {
            _parent[root] = root;
            _depth[root] = 0;
            for (int i = 0; i < adj[root].Length; i++)
            {
                int xt = adj[root][i];
                DFS(xt, root, 1);
            }
        }
        void DFS(int root, int parent, int depth)
        {
            _parent[root] = parent;
            _depth[root] = depth;
            while (adj[root].Length == 2)
            {
                int xt = adj[root][0];
                if (xt == parent) xt = adj[root][1];
                parent = root;
                depth++;
                root = xt;
                _parent[root] = parent;
                _depth[root] = depth;
            }
            for (int i = 0; i < adj[root].Length; i++)
            {
                int xt = adj[root][i];
                if (xt == parent) continue;
                DFS(xt, root, depth + 1);
            }
        }
        public int PathNodes(int u, int v, ref int[] nodes)
        {
            int k = 0;
            while (_depth[u] > _depth[v])
            {
                nodes[k++] = u;
                u = _parent[u];
            }
            while (_depth[u] < _depth[v])
            {
                nodes[k++] = v;
                v = _parent[v];
            }
            while (u != v)
            {
                nodes[k++] = u;
                nodes[k++] = v;
                u = _parent[u];
                v = _parent[v];
            }
            nodes[k++] = u;
            return k;
        }
        public int Adj(int v, int ix)
        {
            return adj[v][ix];
        }
        public int Deg(int v)
        {
            return adj[v].Length;
        }
        public int Parent(int v)
        {
            return _parent[v];
        }
        public int Depth(int v)
        {
            return _depth[v];
        }
    }
    // -------------------------------------------------------------------------

    const int R = 1000000007;

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"5
//1 2
//1 3
//3 4
//3 5
//");

        int N = int.Parse(tIn.ReadLine());
        int[][] E = new int[N - 1][];
        for (int i = 0; i < N - 1; i++)
            E[i] = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p) - 1).ToArray();

        TreePath tree = new TreePath(N, E);

        //
        // Number of ways to divide a subtree with root V is
        // a SUM (number of ways to divide a subtree with root in Ui)
        // where Ui - children of V.
        //
        // We consider 2 different cases for each node(v):
        // 1) the node belongs to the same kingdom as its parent
        // 2) the node doesn't belong to the same kingdom as its parent
        //
        // In case (1) all children (ui) can belong to any kingdom.
        // In case (2) one of the children (ui) MUST belong to the same kingdom as the node.
        //
        // Let W0[v] = number of ways in case (1) and W1[v] = in case (2).
        //
        // W0[v] = (W0[u1] + W1[u1]) + (W0[u2] + W1[u2]) + (W0[u3] + W1[u3]) + ...
        //
        // W1[v] = W0[u1] + ((W0[u2] + W1[u2]) + (W0[u3] + W1[u3]) + ...) +
        //         W1[u1] + W0[u2] + ((W0[u3] + W1[u3]) + (W0[u4] + W1[u4]) + ...) +
        //         ... +
        //         (W1[u1] + W1[u2] + ...) + W0[uk] + ((W0[uk1] + W1[uk1]) + (W0[uk2] + W1[uk2]) + ...) +
        //         ...
        //
        // As the root doesn't have a parent then the solution is W1[1] * 2 (2 as the root can belong to any kingdom).
        //
        // First we put all nodes into the stack so the parent is always below all its children.
        // Then we take nodes one by one from the stack and calculate ways0[v] and ways1[v]
        // on the base of already calculated ways0[u] and ways1[u] (u - children of v).
        //

        Stack<int> stack = new Stack<int>();
        Queue<int> que = new Queue<int>();
        que.Enqueue(0);
        while (que.Count > 0)
        {
            int v = que.Dequeue();
            stack.Push(v);
            for (int i = 0; i < tree.Deg(v); i++)
            {
                int u = tree.Adj(v, i);
                if (u != tree.Parent(v)) que.Enqueue(u);
            }
        }

        int[] ways0 = new int[N];
        int[] ways1 = new int[N];

        while (stack.Count > 0)
        {
            int v = stack.Pop();
            ways0[v] = 1;
            ways1[v] = 0;
            for (int i = 0; i < tree.Deg(v); i++)
            {
                int u = tree.Adj(v, i);
                if (u != tree.Parent(v))
                {
                    ways0[v] = (int)(((long)ways0[v] * ((ways0[u] + ways1[u]) % R)) % R);

                    long K1 = ways0[u];
                    for (int j = 0; j < i; j++)
                    {
                        int u1 = tree.Adj(v, j);
                        if (u1 != tree.Parent(v)) K1 = (K1 * ways1[u1]) % R;
                    }
                    for (int j = i + 1; j < tree.Deg(v); j++)
                    {
                        int u1 = tree.Adj(v, j);
                        if (u1 != tree.Parent(v)) K1 = (K1 * ((ways0[u1] + ways1[u1]) % R)) % R;
                    }
                    ways1[v] = (int)((ways1[v] + K1) % R);
                }
            }
        }

        tOut.WriteLine((ways1[0] * 2) % R);

        //tIn.ReadLine();
    }
}
