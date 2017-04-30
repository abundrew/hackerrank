using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Maximum Disjoint Subtree Product
/// https://www.hackerrank.com/contests/world-codesprint-10/challenges/maximum-disjoint-subtree-product
/// </summary>
class wcs_10_5_MaximumDisjointSubtreeProduct
{
    // -------------------------------------------------------------------------
    public class Graph
    {
        public int V { get; private set; }
        // [][ v1, v2 ... ]
        int[][] adj = null;
        // edges: [][u, v]
        public Graph(int v, IEnumerable<int[]> edges, bool directed = false)
        {
            V = v;
            adj = new int[V][];

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
            }
            Array.Clear(deg, 0, V);

            foreach (int[] e in e2a)
            {
                adj[e[0]][deg[e[0]]] = e[1];
                deg[e[0]]++;
                if (!directed)
                {
                    adj[e[1]][deg[e[1]]] = e[0];
                    deg[e[1]]++;
                }
            }
        }
        public int AdjV(int v, int ix)
        {
            return adj[v][ix];
        }
        public int Deg(int v)
        {
            return adj[v].Length;
        }
        public int[] SortByPreorder(int root)
        {
            int[] stack = new int[V];
            int nstack = 0;
            int[] order = new int[V];
            int norder = 0;
            bool[] visited = new bool[V];
            stack[nstack++] = root;
            visited[root] = true;
            while (nstack > 0)
            {
                int v = stack[--nstack];
                order[norder++] = v;
                for (int i = 0; i < Deg(v); i++)
                {
                    int u = AdjV(v, i);
                    if (!visited[u])
                    {
                        visited[u] = true;
                        stack[nstack++] = u;
                    }
                }
            }
            return order;
        }
    }
    // -------------------------------------------------------------------------
    static Dictionary<long, long> MaxSumInc = new Dictionary<long, long>();
    static Dictionary<long, long> MaxSumOut = new Dictionary<long, long>();
    static Dictionary<long, long> MinSumInc = new Dictionary<long, long>();
    static Dictionary<long, long> MinSumOut = new Dictionary<long, long>();
    static Graph G = null;
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
        int[] W = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
        int[][] UV = new int[N - 1][];
        for (int i = 0; i < N - 1; i++)
        {
            UV[i] = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            UV[i][0]--;
            UV[i][1]--;
        }

        G = new Graph(N, UV);
        long MaxProduct = long.MinValue;
        foreach (int[] uvx in UV)
        {
            long key1 = ((long)uvx[0] << 20) + uvx[1];
            long key2 = ((long)uvx[1] << 20) + uvx[0];

            Stack<long> stack = new Stack<long>();
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(key1);
            queue.Enqueue(key2);
            while (queue.Count > 0)
            {
                long key = queue.Dequeue();
                if (MaxSumInc.ContainsKey(key)) continue;
                stack.Push(key);
                int u = (int)(key >> 20);
                int v = (int)(key % (1L << 20));
                for (int i = 0; i < G.Deg(u); i++)
                {
                    int v1 = G.AdjV(u, i);
                    if (v1 == v) continue;
                    queue.Enqueue(((long)v1 << 20) + u);
                }
            }
            while (stack.Count > 0)
            {
                long key = stack.Pop();
                if (MaxSumInc.ContainsKey(key)) continue;
                int u = (int)(key >> 20);
                int v = (int)(key % (1L << 20));

                long maxsuminc = W[u];
                long maxsumout = 0;
                long minsuminc = W[u];
                long minsumout = 0;
                for (int i = 0; i < G.Deg(u); i++)
                {
                    int v1 = G.AdjV(u, i);
                    if (v1 == v) continue;
                    long keyv1 = ((long)v1 << 20) + u;

                    long maxsumincv1 = MaxSumInc[keyv1];
                    long maxsumoutv1 = MaxSumOut[keyv1];
                    long minsumincv1 = MinSumInc[keyv1];
                    long minsumoutv1 = MinSumOut[keyv1];

                    if (maxsumincv1 > 0) maxsuminc += maxsumincv1;
                    if (maxsumincv1 > maxsumout) maxsumout = maxsumincv1;
                    if (maxsumoutv1 > maxsumout) maxsumout = maxsumoutv1;
                    if (minsumincv1 < 0) minsuminc += minsumincv1;
                    if (minsumincv1 < minsumout) minsumout = minsumincv1;
                    if (minsumoutv1 < minsumout) minsumout = minsumoutv1;
                }
                MaxSumInc[key] = maxsuminc;
                MaxSumOut[key] = maxsumout;
                MinSumInc[key] = minsuminc;
                MinSumOut[key] = minsumout;
            }

            long product1 = Math.Max(MaxSumInc[key1], MaxSumOut[key1]) * Math.Max(MaxSumInc[key2], MaxSumOut[key2]);
            long product2 = Math.Min(MinSumInc[key1], MinSumOut[key1]) * Math.Min(MinSumInc[key2], MinSumOut[key2]);
            if (MaxProduct < product1) MaxProduct = product1;
            if (MaxProduct < product2) MaxProduct = product2;
        }

        sw.WriteLine(MaxProduct);
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
        int V = 300000;
        Graph g = Tree(V);
        Random random = new Random(1000);
        int[] W = new int[V];
        for (int i = 0; i < V; i++) W[i] = -1000 + random.Next(2001);

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("{0}", V).AppendLine();
        sb.AppendFormat("{0}", string.Join(" ", W.Select(p => p.ToString()).ToArray())).AppendLine();
        for (int v = 0; v < V; v++)
        {
            for (int i = 0; i < g.Deg(v); i++)
            {
                int u = g.AdjV(v, i);
                if (v < u)
                {
                    sb.AppendFormat("{0} {1}", u + 1, v + 1).AppendLine();
                }
            }
        }
        string s = sb.ToString();
        //----------------------------------------------------------------------
        //s = File.ReadAllText(@"");
        //----------------------------------------------------------------------
//        s = @"6
//-9 -6 -1 9 -2 0
//6 1
//4 5
//6 3
//5 2
//1 2";
        //----------------------------------------------------------------------
        return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(s)));
        //----------------------------------------------------------------------
    }
    // -------------------------------------------------------------------------
    public class BinaryHeapPQ<T> where T : IComparable
    {
        int n = 0;
        T[] que = null;
        public BinaryHeapPQ() : this(1) { }
        public BinaryHeapPQ(int capacity)
        {
            que = new T[capacity + 1];
            n = 0;
        }
        public BinaryHeapPQ(T[] a)
        {
            n = a.Length;
            que = new T[n + 1];
            for (int i = 0; i < n; i++) que[i + 1] = a[i];
            for (int i = n / 2; i >= 1; i--) Sink(i);
        }
        public int Count { get { return n; } }
        public T Min { get { return que[1]; } }
        public void Insert(T x)
        {
            if (n == que.Length - 1) Resize(que.Length * 2);
            que[++n] = x;
            Swim(n);
        }
        public T ExtractMin()
        {
            Swap(1, n);
            T min = que[n--];
            Sink(1);
            que[n + 1] = default(T);
            return min;
        }
        void Swim(int k)
        {
            while (k > 1 && que[k / 2].CompareTo(que[k]) > 0)
            {
                Swap(k, k / 2);
                k = k / 2;
            }
        }
        void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && que[j].CompareTo(que[j + 1]) > 0) j++;
                if (que[k].CompareTo(que[j]) <= 0) break;
                Swap(k, j);
                k = j;
            }
        }
        void Resize(int capacity)
        {
            T[] temp = new T[capacity + 1];
            for (int i = 1; i <= n; i++) temp[i] = que[i];
            que = temp;
        }
        void Swap(int i, int j)
        {
            T temp = que[i];
            que[i] = que[j];
            que[j] = temp;
        }
    }
    // -------------------------------------------------------------------------
    public static Graph Tree(int V)
    {
        Random random = new Random(1000);
        if (V == 1) return new Graph(V, new int[][] { });
        int[] prufer = new int[V - 2];
        for (int i = 0; i < V - 2; i++) prufer[i] = random.Next(V);
        int[] degree = new int[V];
        for (int v = 0; v < V; v++) degree[v] = 1;
        for (int i = 0; i < V - 2; i++) degree[prufer[i]]++;

        BinaryHeapPQ<int> pq = new BinaryHeapPQ<int>();
        for (int v = 0; v < V; v++)
            if (degree[v] == 1)
                pq.Insert(v);

        List<int[]> edges = new List<int[]>();
        for (int i = 0; i < V - 2; i++)
        {
            int v = pq.ExtractMin();
            edges.Add(new int[] { v, prufer[i] });
            degree[v]--;
            degree[prufer[i]]--;
            if (degree[prufer[i]] == 1) pq.Insert(prufer[i]);
        }
        edges.Add(new int[] { pq.ExtractMin(), pq.ExtractMin() });

        Graph g = new Graph(V, edges, false);
        return g;
    }
    // -------------------------------------------------------------------------
}
