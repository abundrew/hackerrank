using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Black White Tree
/// https://www.hackerrank.com/contests/university-codesprint-3/challenges/black-white-tree
/// </summary>
class BlackWhiteTree
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
        public int[] SortByPreorder()
        {
            int[] stack = new int[N];
            int nstack = 0;
            int[] order = new int[N];
            int norder = 0;
            stack[nstack++] = 0;
            while (nstack > 0)
            {
                int v = stack[--nstack];
                order[norder++] = v;
                for (int i = 0; i < Deg(v); i++)
                {
                    int u = adj[v][i];
                    if (u == _parent[v]) continue;
                    stack[nstack++] = u;
                }
            }
            return order;
        }
        public Stack<int> ToStack()
        {
            Stack<int> stack = new Stack<int>();
            Queue<int> que = new Queue<int>();
            que.Enqueue(0);
            while (que.Count > 0)
            {
                int v = que.Dequeue();
                stack.Push(v);
                for (int i = 0; i < Deg(v); i++)
                {
                    int u = adj[v][i];
                    if (u == _parent[v]) continue;
                    que.Enqueue(u);
                }
            }
            return stack;
        }
    }
    // -------------------------------------------------------------------------
    static void Main(String[] args)
    {
        //----------------------------------------------------------------------
        StreamReader sr = new StreamReader(Console.OpenStandardInput());
        StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
        //----------------------------------------------------------------------
        sr = Input();
        DateTime started = DateTime.Now;
        //----------------------------------------------------------------------
        int N = int.Parse(sr.ReadLine());
        int[] C = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
        for (int i = 0; i < N; i++)
            if (C[i] == 0) C[i] = -1;
        int[][] E = new int[N - 1][];
        for (int i = 0; i < N - 1; i++)
        {
            E[i] = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            E[i][0]--;
            E[i][1]--;
        }
        TreePath tree = new TreePath(N, E);
        int[] postorder = tree.SortByPreorder().Reverse().ToArray();
        int[] accumulation = new int[N];
        bool[] excluded = new bool[N];

        foreach (int v in postorder)
        {
            accumulation[v] = C[v];
            for (int i = 0; i < tree.Deg(v); i++)
            {
                int u = tree.Adj(v, i);
                if (u == tree.Parent(v) || excluded[u]) continue;
                accumulation[v] += accumulation[u];
            }
            if (accumulation[v] <= 0) excluded[v] = true;
        }

        int Vx = -1;
        int Ax = -1;
        for (int v = 0; v < N; v++)
            if (!excluded[v] && Ax < accumulation[v])
            {
                Vx = v;
                Ax = accumulation[v];
            }

        List<int> Nx = new List<int>();
        if (Vx != - 1)
        {
            Stack<int> st = new Stack<int>();
            st.Push(Vx);
            while (st.Count > 0)
            {
                int v = st.Pop();
                Nx.Add(v);
                for (int i = 0; i < tree.Deg(v); i++)
                {
                    int u = tree.Adj(v, i);
                    if (u == tree.Parent(v) || excluded[u]) continue;
                    st.Push(u);
                }
            }
        }

        for (int i = 0; i < N; i++) C[i] *= -1;

        accumulation = new int[N];
        excluded = new bool[N];

        foreach (int v in postorder)
        {
            accumulation[v] = C[v];
            for (int i = 0; i < tree.Deg(v); i++)
            {
                int u = tree.Adj(v, i);
                if (u == tree.Parent(v) || excluded[u]) continue;
                accumulation[v] += accumulation[u];
            }
            if (accumulation[v] <= 0) excluded[v] = true;
        }

        int Vy = -1;
        int Ay = -1;
        for (int v = 0; v < N; v++)
            if (!excluded[v] && Ay < accumulation[v])
            {
                Vy = v;
                Ay = accumulation[v];
            }

        List<int> Ny = new List<int>();
        if (Vy != -1)
        {
            Stack<int> st = new Stack<int>();
            st.Push(Vy);
            while (st.Count > 0)
            {
                int v = st.Pop();
                Ny.Add(v);
                for (int i = 0; i < tree.Deg(v); i++)
                {
                    int u = tree.Adj(v, i);
                    if (u == tree.Parent(v) || excluded[u]) continue;
                    st.Push(u);
                }
            }
        }

        if (Ax > Ay)
        {
            sw.WriteLine(Ax);
            sw.WriteLine(Nx.Count);
            sw.WriteLine(string.Join(" ", Nx.OrderBy(p => p).Select(p => (p + 1).ToString()).ToArray()));
        }
        else
        {
            sw.WriteLine(Ay);
            sw.WriteLine(Ny.Count);
            sw.WriteLine(string.Join(" ", Ny.OrderBy(p => p).Select(p => (p + 1).ToString()).ToArray()));
        }
        //----------------------------------------------------------------------
        sr.Dispose();
        sw.Dispose();
        //----------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Elapsed: {0} ms", (int)DateTime.Now.Subtract(started).TotalMilliseconds);
        Console.ReadLine();
        //----------------------------------------------------------------------
    }
    static StreamReader Input()
    {
        //----------------------------------------------------------------------
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("").AppendLine();
        string s = sb.ToString();
        //----------------------------------------------------------------------
        // s = File.ReadAllText(@"");
        //----------------------------------------------------------------------
        s = @"8
1 0 0 1 1 0 0 0
7 1
3 5
1 6
4 3
6 3
2 3
7 8";
        //----------------------------------------------------------------------
        return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(s)));
        //----------------------------------------------------------------------
    }
}
