using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Node-Point Mappings
/// https://www.hackerrank.com/contests/world-codesprint-10/challenges/node-point-mappings
/// </summary>
class wcs_10_6_NodePointMappings
{
    // -------------------------------------------------------------------------
    public class Graph
    {
        public int V { get; private set; }
        int[][] adj = null;
        int[] _parent = null;
        int[] _size = null;
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
            _parent = new int[V];
            _size = new int[V];
            DFS(0);
        }
        void DFS(int root)
        {
            _parent[root] = root;
            _size[root] = 1;
            for (int i = 0; i < adj[root].Length; i++)
            {
                int xt = adj[root][i];
                _size[root] += DFS(xt, root);
            }
        }
        int DFS(int root, int parent)
        {
            _parent[root] = parent;
            _size[root] = 1;
            for (int i = 0; i < adj[root].Length; i++)
            {
                int xt = adj[root][i];
                if (xt == parent) continue;
                _size[root] += DFS(xt, root);
            }
            return _size[root];
        }
        public int AdjV(int v, int ix)
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
        public int Size(int v)
        {
            return _size[v];
        }
    }
    // -------------------------------------------------------------------------
    public struct P2D
    {
        const double eps = 1e-9;
        static bool eq(double a, double b) { return Math.Abs(a - b) < eps; }
        static bool ge(double a, double b) { return a - b > -eps; }
        static bool le(double a, double b) { return b - a > -eps; }
        static bool gt(double a, double b) { return a - b > eps; }
        static bool lt(double a, double b) { return b - a < eps; }
        public double X { get; set; }
        public double Y { get; set; }
        public P2D(double x, double y) { X = x; Y = y; }
        public double Abs() { return Math.Sqrt(X * X + Y * Y); }
        public double Abs2() { return X * X + Y * Y; }
        public P2D Copy() { return new P2D(X, Y); }
        public override bool Equals(object o) { if (o is P2D) { P2D c = (P2D)o; return (this == c); } return false; }
        public override int GetHashCode() { return (X.GetHashCode() ^ Y.GetHashCode()); }
        public double Angle() { return Math.Atan2(Y, X); }
        public P2D Rot() { return new P2D(-Y, X); }
        public P2D Rot(P2D O, double theta)
        {
            return new P2D(Math.Cos(theta) * (X - O.X) - Math.Sin(theta) * (Y - O.Y) + O.X, Math.Sin(theta) * (X - O.X) + Math.Cos(theta) * (Y - O.Y) + O.Y);
        }
        public P2D Unit() { return this / Abs(); }
        public static bool operator ==(P2D a, P2D b) { return eq(a.X, b.X) && eq(a.Y, b.Y); }
        public static bool operator !=(P2D a, P2D b) { return !(a == b); }
        public static bool operator <(P2D a, P2D b) { if (eq(a.X, b.X)) return lt(a.Y, b.Y); return a.X < b.X; }
        public static bool operator >(P2D a, P2D b) { if (eq(a.X, b.X)) return gt(a.Y, b.Y); return a.X > b.X; }
        public static P2D operator +(P2D a) { return new P2D(a.X, a.Y); }
        public static P2D operator -(P2D a) { return new P2D(-a.X, -a.Y); }
        public static P2D operator +(P2D a, P2D b) { return new P2D(a.X + b.X, a.Y + b.Y); }
        public static P2D operator -(P2D a, P2D b) { return new P2D(a.X - b.X, a.Y - b.Y); }
        public static double operator *(P2D a, P2D b) { return a.X * b.X + a.Y * b.Y; }
        public static double operator %(P2D a, P2D b) { return a.X * b.Y - a.Y * b.X; }
        public static P2D operator *(P2D a, double f) { return new P2D(a.X * f, a.Y * f); }
        public static P2D operator *(double f, P2D a) { return new P2D(a.X * f, a.Y * f); }
        public static P2D operator /(P2D a, double f) { return new P2D(a.X / f, a.Y / f); }
    }
    // -------------------------------------------------------------------------
    static int N = 0;
    static Graph G = null;
    static P2D[] P = null;
    static int[] mapping = null;
    static double[] angle = null;
    // -------------------------------------------------------------------------
    static void Map(int vroot, List<int> points)
    {
        int ixp0 = 0;
        for (int i = 1; i < points.Count; i++)
            if (P[points[ixp0]].Y > P[points[i]].Y || P[points[ixp0]].Y == P[points[i]].Y && P[points[ixp0]].X > P[points[i]].X)
                ixp0 = i;
        mapping[vroot] = points[ixp0];
        points.RemoveAt(ixp0);

        if (G.Size(vroot) == 1) return;
        P2D proot = P[mapping[vroot]];

        for( int i = 0; i < points.Count; i++)
            angle[points[i]] = (P[points[i]] - proot).Angle();

        points.Sort((p1, p2) => angle[p1].CompareTo(angle[p2]));

        for (int i = 0; i < G.Deg(vroot); i++)
        {
            int u = G.AdjV(vroot, i);
            if (u == G.Parent(vroot)) continue;
            List<int> upoints = new List<int>();
            for (int j = 0; j < G.Size(u); j++)
                upoints.Add(points[j]);
            points.RemoveRange(0, G.Size(u));
            Map(u, upoints);
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
        N = int.Parse(sr.ReadLine());
        int[][] E = new int[N - 1][];
        for (int i = 0; i < N - 1; i++)
        {
            E[i] = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            E[i][0]--;
            E[i][1]--;
        }
        G = new Graph(N, E);
        P = new P2D[N];
        for (int i = 0; i < N; i++)
        {
            int[] xy = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            P[i] = new P2D(xy[0], 1000 - xy[1]);
        }

        mapping = new int[N];
        angle = new double[N];

        Map(0, Enumerable.Range(0, N).ToList());

        sw.WriteLine(string.Join(" ", mapping.Select(p => (p + 1).ToString()).ToArray()));
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
        //s = File.ReadAllText(@"");
        //----------------------------------------------------------------------
        s = @"15
2 4
3 4
12 1
9 4
9 5
5 12
10 5
8 9
14 15
7 8
8 14
10 6
13 10
5 11
4 5
5 10
8 1
0 4
7 6
3 8
0 7
2 0
1 1
9 4
1 5
5 0
3 9
9 10
6 2";
        //----------------------------------------------------------------------
        return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(s)));
        //----------------------------------------------------------------------
    }
}
