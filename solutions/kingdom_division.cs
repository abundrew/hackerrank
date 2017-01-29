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
    static TreePath T = null;
    static int[] DP0 = null;
    static int[] DP1 = null;
    static int Ways(int v, bool plusParent)
    {
        if (v != 0 && T.Deg(v) == 2)
        {
            long a1 = 1, b1 = 0, a2 = 0, b2 = 1;
            while (T.Deg(v) == 2)
            {
                long ta1 = a1, tb1 = b1;
                a1 = (a1 + a2) % R;
                b1 = (b1 + b2) % R;
                a2 = ta1;
                b2 = tb1;
                v = T.Adj(v, 0) == T.Parent(v) ? T.Adj(v, 1) : T.Adj(v, 0);
            }
            return plusParent ? (int)(((a1 * Ways(v, true)) % R + (b1 * Ways(v, false)) % R) % R) : (int)(((a2 * Ways(v, true)) % R + (b2 * Ways(v, false)) % R) % R);
        }

        if (plusParent)
        {
            if (DP0[v] > -1) return DP0[v];
            long K = 1;

            int parent = T.Parent(v);
            for (int i = 0; i < T.Deg(v); i++)
            {
                int u = T.Adj(v, i);
                if (u != parent) K = (K * (Ways(u, true) + Ways(u, false))) % R;
            }

            DP0[v] = (int)K;

            return DP0[v];
        }
        else
        {
            if (DP1[v] > -1) return DP1[v];

            long K = 0;
            int parent = T.Parent(v);
            for (int x = 0; x < T.Deg(v); x++)
            {
                int ux = T.Adj(v, x);
                if (ux == parent) continue;

                long K1 = Ways(ux, true);
                for (int i = 0; i < x; i++)
                {
                    int u = T.Adj(v, i);
                    if (u != parent) K1 = (K1 * Ways(u, false)) % R;
                }
                for (int i = x + 1; i < T.Deg(v); i++)
                {
                    int u = T.Adj(v, i);
                    if (u != parent) K1 = (K1 * (Ways(u, true) + Ways(u, false))) % R;
                }
                K = (K + K1) % R;
            }
            DP1[v] = (int)K;

            return DP1[v];
        }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        int N = int.Parse(tIn.ReadLine());
        int[][] E = new int[N - 1][];
        for (int i = 0; i < N - 1; i++)
            E[i] = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p) - 1).ToArray();

        T = new TreePath(N, E);
        DP0 = new int[N];
        DP1 = new int[N];
        for (int i = 0; i < N; i++)
        {
            DP0[i] = -1;
            DP1[i] = -1;
        }

        tOut.WriteLine((2L * Ways(0, false)) % R);

        tIn.ReadLine();
    }
}
