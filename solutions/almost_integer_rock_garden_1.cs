using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Almost Integer Rock Garden 
/// https://www.hackerrank.com/contests/w29/challenges/almost-integer-rock-garden
/// </summary>
class Solution6
{
    const long R = 100000000000000L;

    static int xy2ix(int x, int y) { return (x + 12) * 26 + y + 12; }
    static int ix2x(int ix) { return ix / 26 - 12; }
    static int ix2y(int ix) { return ix % 26 - 12; }

    static long[] ixDs = null;
    static int [][] ixSv = null;
    static List<int> ixVs = null;

    static Dictionary<long, List<int>> garden = null;
    static HashSet<int> ixQ = null;

    static void AddSolution(long[] solution)
    {
        for (int i = 0; i < solution.Length; i++)
        {
            foreach (int v in garden[solution[i]])
            {
                if (!ixQ.Contains(v)) continue;
                HashSet<int> s11 = new HashSet<int>();
                for (int j = 0; j < solution.Length; j++)
                {
                    if (i == j) continue;
                    int[] uu = garden[solution[j]].Where(p => p != v && !s11.Contains(p)).ToArray();
                    if (uu.Length == 0) break;
                    s11.Add(uu[0]);
                }
                if (s11.Count == 11) ixSv[v] = s11.ToArray();
                ixQ.Remove(v);
            }
        }
        File.AppendAllLines(@"c:\temp\solutions.txt", new string[] { string.Join(",", solution.Select(p => p.ToString()).ToArray()) });
    }

    class Combi
    {
        public long[] D { get; set; }
        public long DD { get; set; }
        public Combi(long[] d)
        {
            D = d;
            DD = d.Sum() % R;
        }
    }
    static List<Combi> combi3 = null;
    static List<Combi> combi4 = null;

    static void Main(String[] args)
    {
        //----------------------------------------------------------------------
        StreamReader sr = new StreamReader(Console.OpenStandardInput());
        StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
        //----------------------------------------------------------------------
        long E = 95;
        int[] xy= sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        ixDs = new long[26 * 26 + 1];
        for (int i = 0; i < 26 * 26 + 1; i++) ixDs[i] = -1;
        ixSv = new int[26 * 26 + 1][];
        ixVs = new List<int>();

        garden = new Dictionary<long, List<int>>();
        ixQ = new HashSet<int>();

        for (int x = -12; x <= 12; x++)
            for (int y = -12; y <= 12; y++)
            {
                int z2 = x * x + y * y;
                int z = (int)Math.Sqrt(z2);
                if (z * z == z2) continue;
                decimal ds = (decimal)Math.Sqrt(z2);
                ds = ds - (int)ds;
                ds = ds * R;
                long lds = (long)ds;
                int ix = xy2ix(x, y);

                if (!garden.ContainsKey(lds)) garden[lds] = new List<int>();
                garden[lds].Add(ix);
                ixDs[ix] = lds;
                ixVs.Add(ix);
                ixQ.Add(ix);
            }

        HashSet<long> hs = new HashSet<long>();
        combi3 = new List<Combi>();
        foreach (long d1 in garden.Keys)
            foreach (long d2 in garden.Keys)
                foreach (long d3 in garden.Keys)
                {
                    Combi c = new Combi(new long[] { d1, d2, d3 });
                    if (hs.Contains(c.DD)) continue;
                    hs.Add(c.DD);
                    combi3.Add(c);
                }
        combi3.Sort((p1, p2) => p1.DD.CompareTo(p2.DD));

        hs = new HashSet<long>();
        combi4 = new List<Combi>();
        foreach (long d1 in garden.Keys)
            foreach (long d2 in garden.Keys)
                foreach (long d3 in garden.Keys)
                    foreach (long d4 in garden.Keys)
                    {
                        Combi c = new Combi(new long[] { d1, d2, d3, d4 });
                        if (hs.Contains(c.DD)) continue;
                        hs.Add(c.DD);
                        combi4.Add(c);
                    }
        combi4.Sort((p1, p2) => p1.DD.CompareTo(p2.DD));

        long minx = long.MaxValue;
        while (ixQ.Count > 0)
        {
            long dx = ixDs[ixQ.ElementAt(0)];
            bool found = false;

            for (int ic1 = 0; ic1 < combi3.Count; ic1++)
            {
                minx = long.MaxValue;
                long dx1 = combi3[ic1].DD;

                long dxx = (dx + dx1) % R;
                long xdx = (R - dxx) % R;

                for (int i = 0; i < 2; i++)
                {
                    if (i == 1) xdx = xdx + R;

                    long xdx2 = xdx / 2;
                    int l = 0;
                    int r = combi4.Count - 1;
                    while (l < r)
                    {
                        int m = (l + r) / 2;
                        if (xdx2 < combi4[m].DD) r = m - 1; else l = m + 1;
                    }
                    int ic3 = r;
                    int ic4 = r;

                    while (ic4 > 0 && ic3 < combi4.Count - 1)
                    {
                        while (ic4 > 0 && combi4[ic3].DD + combi4[ic4].DD >= xdx + E) ic4--;
                        while (ic3 < combi4.Count - 1 && combi4[ic3].DD + combi4[ic4].DD <= xdx - E) ic3++;
                        if (Math.Abs(combi4[ic3].DD + combi4[ic4].DD - xdx) < E)
                        {
                            AddSolution(new long[] {
                                dx,
                                combi3[ic1].D[0], combi3[ic1].D[1], combi3[ic1].D[2],
                                combi4[ic3].D[0], combi4[ic3].D[1], combi4[ic3].D[2], combi4[ic3].D[3],
                                combi4[ic4].D[0], combi4[ic4].D[1], combi4[ic4].D[2], combi4[ic4].D[3],
                            });
                            found = true;
                            break;
                        }
                    }

                    if (Math.Abs(combi4[ic3].DD + combi4[ic4].DD - xdx) < minx) minx = Math.Abs(combi4[ic3].DD + combi4[ic4].DD - xdx);
                }
                if (found) break;
            }
        }
        //----------------------------------------------------------------------
        sr.Dispose();
        sw.Dispose();
        //----------------------------------------------------------------------
    }
}
