using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Recording Episodes
/// https://www.hackerrank.com/contests/world-codesprint-7/challenges/episode-recording
/// </summary>
class Solution6
{
    // -------------------------------------------------------------------------
    public class Graph
    {
        public int V { get; private set; }
        // [][ v1, v2 ... ]
        int[][] adj = null;
        // [][ w1, w2 ... ]
        int[][] wt = null;
        // edges: [][u, v, w]
        public Graph(int v, IEnumerable<int[]> edges, bool directed = false)
        {
            V = v;
            adj = new int[V][];
            wt = new int[V][];

            int[][] e2a = edges.ToArray();
            int[] deg = new int[V];
            foreach (int[] e in e2a)
            {
                deg[e[0]]++;
                if (!directed) deg[e[1]]++;
            }
            for (int i = 0; i < V; i++)
            {
                adj[i] = new int[deg[i]];
                wt[i] = new int[deg[i]];
            }
            Array.Clear(deg, 0, V);

            foreach (int[] e in e2a)
            {
                adj[e[0]][deg[e[0]]] = e[1];
                wt[e[0]][deg[e[0]]] = e[2];
                deg[e[0]]++;
                if (!directed)
                {
                    adj[e[1]][deg[e[1]]] = e[0];
                    wt[e[1]][deg[e[1]]] = e[2];
                    deg[e[1]]++;
                }
            }
        }
        public int AdjV(int v, int ix)
        {
            return adj[v][ix];
        }
        public int AdjW(int v, int ix)
        {
            return wt[v][ix];
        }
        public int Deg(int v)
        {
            return adj[v].Length;
        }
        public int[][] IncidenceMatrix()
        {
            int[][] mx = new int[V][];
            for (int v = 0; v < V; v++) mx[v] = new int[V];
            for (int v = 0; v < V; v++)
            {
                for (int i = 0; i < Deg(v); i++)
                {
                    mx[v][AdjV(v, i)] = AdjW(v, i);
                    mx[AdjV(v, i)][v] = -AdjW(v, i);
                }
            }
            return mx;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int v = 0; v < V; v++)
                sb.AppendFormat("{0}:{1}", v, string.Join(",", Enumerable.Range(0, adj[v].Length).Select(p => string.Format("{0}[{1}]", adj[v][p], wt[v][p])).ToArray())).AppendLine();
            return sb.ToString();
        }
    }
    // -------------------------------------------------------------------------
    public class TarjanSCC
    {
        public int Count { get; private set; }

        bool[] marked;
        int[] id;
        int[] low;
        int pre;
        Stack<int> stack;

        public TarjanSCC(Graph g)
        {
            marked = new bool[g.V];
            stack = new Stack<int>();
            id = new int[g.V];
            low = new int[g.V];
            for (int v = 0; v < g.V; v++)
            {
                if (!marked[v]) dfs(g, v);
            }
        }

        void dfs(Graph g, int v)
        {
            int w;
            marked[v] = true;
            low[v] = pre++;
            int min = low[v];
            stack.Push(v);
            for (int i = 0; i < g.Deg(v); i++)
            {
                w = g.AdjV(v, i);
                if (!marked[w]) dfs(g, w);
                if (low[w] < min) min = low[w];
            }
            if (min < low[v])
            {
                low[v] = min;
                return;
            }
            do
            {
                w = stack.Pop();
                id[w] = Count;
                low[w] = g.V;
            } while (w != v);
            Count++;
        }

        public int ID(int v) { return id[v]; }
        public bool StronglyConnected(int u, int v)
        {
            return id[u] == id[v];
        }
    }
    // -------------------------------------------------------------------------
    public class SAT2
    {
        int N = 0;
        List<int[]> E = null;
        public SAT2(int n)
        {
            N = n;
            E = new List<int[]>();
        }
        public int c_not(int a)
        {
            return -a - 1;
        }
        int c_convert(int a)
        {
            return a < 0 ? (c_not(a) << 1) ^ 1 : a << 1;
        }
        void c_must(int a)
        {
            E.Add(new int[] { a ^ 1, a, 1 });
        }
        void c_or(int a, int b)
        {
            E.Add(new int[] { a ^ 1, b, 1 });
            E.Add(new int[] { b ^ 1, a, 1 });
        }
        public void c_xor(int a, int b)
        {
            c_or(a, b);
            c_or(a ^ 1, b ^ 1);
        }
        void c_and(int a, int b)
        {
            E.Add(new int[] { a, b, 1 });
            E.Add(new int[] { b, a, 1 });
        }
        void c_not_and(int a, int b)
        {
            E.Add(new int[] { a, b ^ 1, 1 });
            E.Add(new int[] { b, a ^ 1, 1 });
        }

        public int NOT(int a) { return c_not(a); }
        public void MUST(int a) { c_must(c_convert(a)); }
        public void OR(int a, int b) { c_or(c_convert(a), c_convert(b)); }
        public void XOR(int a, int b) { c_xor(c_convert(a), c_convert(b)); }
        public void AND(int a, int b) { c_and(c_convert(a), c_convert(b)); }
        public void NOT_AND(int a, int b) { c_not_and(c_convert(a), c_convert(b)); }

        public bool Possible()
        {
            Graph g = new Graph(N * 2, E, true);
            TarjanSCC scc = new TarjanSCC(g);
            for (int v = 0; v < N; v++)
                if (scc.StronglyConnected(v << 1, (v << 1) ^ 1))
                    return false;
            return true;
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
        int Q = int.Parse(sr.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int N = int.Parse(sr.ReadLine());
            int[][] SE = new int[N * 2][];
            for (int i = 0; i < N; i++)
            {
                int[] se = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
                SE[i * 2] = new int[] { se[0], se[1] };
                SE[i * 2 + 1] = new int[] { se[2], se[3] };
            }
            bool[,] OL = new bool[N * 2, N * 2];
            for (int i = 0; i < N * 2 - 1; i++)
                for (int j = i + 1; j < N * 2; j++)
                {
                    OL[i, j] = (SE[i][0] <= SE[j][1] && SE[j][0] <= SE[i][1]);
                    OL[j, i] = OL[i, j];
                }

            int LL = 0;
            int RR = 0;

            int L = LL;
            int R = RR;
            while (true)
            {
                SAT2 sat2 = new SAT2(N);
                for (int x = L; x < R; x++)
                    for (int y = x + 1; y <= R; y++)
                    {
                        if (OL[x * 2, y * 2]) sat2.OR(x, y);
                        if (OL[x * 2 + 1, y * 2]) sat2.OR(sat2.NOT(x), y);
                        if (OL[x * 2, y * 2 + 1]) sat2.OR(x, sat2.NOT(y));
                        if (OL[x * 2 + 1, y * 2 + 1]) sat2.OR(sat2.NOT(x), sat2.NOT(y));
                    }
                if (sat2.Possible())
                {
                    if (R - L > RR - LL)
                    {
                        LL = L;
                        RR = R;
                    }
                    if (R == N - 1) break;
                    R++;
                } else
                {
                    L++;
                    if (R < L) R = L;
                }
            }

            sw.WriteLine("{0} {1}", LL + 1, RR + 1);
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
        string s = File.ReadAllText(@"c:\temp\test_resording_episodes.txt");
        //----------------------------------------------------------------------
        return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(s)));
        //----------------------------------------------------------------------
    }
}
